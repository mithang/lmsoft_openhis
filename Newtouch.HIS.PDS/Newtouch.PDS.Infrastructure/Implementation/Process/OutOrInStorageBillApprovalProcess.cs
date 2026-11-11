using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Infrastructure.EF;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Domain.DTO.OutOrInStoredOperate;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.PDS.Requset;
using System.Collections.Generic;
using System.Linq;
using Newtouch.Tools;
using Newtouch.HIS.Application.Implementation.Process;
using Newtouch.HIS.Domain.IDomainServices;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// Duyệt chứng từ xuất nhập kho
    /// </summary>
    public class OutOrInStorageBillApprovalProcess : ProcessorFun<OutOrInStorageBillApprovalDTO>
    {
        private readonly ISysMedicineStorageIOReceiptRepo _crkdj;
        private readonly ISysMedicineStorageIOReceiptDetailRepo _crkdjmx;
        private readonly ISysMedicineReceiptDmnService sysMedicineReceiptDmnService;

        public OutOrInStorageBillApprovalProcess(OutOrInStorageBillApprovalDTO request) : base(request)
        {
            // G26: null-guard (ctor cũ NRE khi request null; qua HTTP form
            // model binding luôn tạo DTO rỗng nên không trigger được, nhưng
            // chặn ở đây để Validata trả lỗi TV thay vì NRE).
            if (request != null)
            {
                request.djmx = new List<SysMedicineStorageIOReceiptDetailEntity>();
                request.dj = new SysMedicineStorageIOReceiptEntity();
            }
        }

        /// <summary>
        /// Kiểm tra
        /// </summary>
        /// <returns></returns>
        protected override ActResult Validata()
        {
            if (Request == null)
            {
                throw new FailedException("Thông tin duyệt không được để trống!");
            }
            if (string.IsNullOrWhiteSpace(Request.crkId))
            {
                throw new FailedException("ID chứng từ xuất nhập kho không được để trống!");
            }
            if (string.IsNullOrWhiteSpace(Request.shzt))
            {
                throw new FailedException("Trạng thái duyệt không được để trống!");
            }
            // G26: chỉ chấp nhận 1=duyệt, 2=không đạt, 3=hủy bỏ. Giá trị khác
            // (vd "abc") trước đây rơi vào nhánh else rồi FormatException ở
            // Convert.ToInt32(Request.shzt) trong InStorageBillApprovalProcess.
            if (Request.shzt != ((int)EnumDjShzt.Approved).ToString()
                && Request.shzt != ((int)EnumDjShzt.Rejected).ToString()
                && Request.shzt != ((int)EnumDjShzt.Cancelled).ToString())
            {
                throw new FailedException("Trạng thái duyệt không hợp lệ, chỉ chấp nhận duyệt, không đạt hoặc hủy bỏ!");
            }
            var djmc = "";//Tên chứng từ
            switch (Request.djlx)
            {
                case (int)EnumDanJuLX.keshifayao:
                    djmc = EnumDanJuLX.keshifayao.GetDescription();
                    break;
                case (int)EnumDanJuLX.neibufayaotuihui:
                    djmc = EnumDanJuLX.neibufayaotuihui.GetDescription();
                    break;
                case (int)EnumDanJuLX.shenlingfayao:
                    djmc = EnumDanJuLX.shenlingfayao.GetDescription();
                    break;
                case (int)EnumDanJuLX.waibucuku:
                    djmc = EnumDanJuLX.waibucuku.GetDescription();
                    break;
                case (int)EnumDanJuLX.yaopinruku:
                    djmc = EnumDanJuLX.yaopinruku.GetDescription();
                    break;
                case (int)EnumDanJuLX.zhijiefayao:
                    djmc = EnumDanJuLX.zhijiefayao.GetDescription();
                    break;
                default:
                    throw new FailedException("Loại chứng từ không hợp lệ!");
            }
            // G36 F36-3: phiếu phát thuốc cho khoa (keshifayao) được duyệt ngay khi tạo
            // (SubmitDeliveryToDepartment sinh shzt="1"), nên luồng duyệt không hỗ trợ
            // loại này — chặn sớm bằng message rõ thay vì để service trả lỗi chung
            // "Loại chứng từ không hỗ trợ duyệt".
            if (Request.djlx == (int)EnumDanJuLX.keshifayao)
            {
                throw new FailedException("Chứng từ phát thuốc cho khoa đã duyệt ngay khi tạo, không hỗ trợ duyệt lại!");
            }
            Request.dj = _crkdj.FindEntity(p => p.crkId == Request.crkId
            && p.OrganizeId == OrganizeId
            && p.djlx == Request.djlx
            && p.zt == "1"
            && (p.Ckbm == Constants.CurrentYfbm.yfbmCode || p.Rkbm == Constants.CurrentYfbm.yfbmCode));
            if (Request.dj == null)
            {
                throw new FailedException(string.Format("Không tìm thấy chứng từ phù hợp với ID chứng từ [{0}] và loại chứng từ [{1}]!", Request.crkId, djmc));
            }
            if (Request.shzt == ((int)EnumDjShzt.Cancelled).ToString())
            {
                if (((int)EnumDjShzt.WaitingApprove).ToString() == Request.dj.shzt) throw new FailedException("Chứng từ chưa duyệt, không cho phép hủy bỏ!"); //Thao tác hủy bỏ, phải tồn tại bản ghi duyệt chứng từ
                if (((int)EnumDjShzt.Cancelled).ToString() == Request.dj.shzt) throw new FailedException("Chứng từ đã hủy bỏ, không thể hủy bỏ trùng lặp!");
                if (((int)EnumDjShzt.Rejected).ToString() == Request.dj.shzt) throw new FailedException("Chứng từ đã duyệt không đạt, không thể hủy bỏ!");
            }
            else if (Request.shzt == ((int)EnumDjShzt.Rejected).ToString())
            {
                if (((int)EnumDjShzt.WaitingApprove).ToString() != Request.dj.shzt) throw new FailedException("Chỉ có thể thao tác với chứng từ đang chờ duyệt!");
            }
            else
            {
                if (((int)EnumDjShzt.WaitingApprove).ToString() != Request.dj.shzt) throw new FailedException("Chứng từ đã duyệt, không thể duyệt trùng lặp!");//Thao tác không hủy bỏ, phải không tồn tại bản ghi duyệt chứng từ
            }
            Request.djmx = _crkdjmx.IQueryable(p => p.crkId == Request.crkId && p.zt == "1").ToList();
            if (Request.djmx == null || Request.djmx.Count <= 0)
            {
                throw new FailedException(string.Format("Không tìm thấy chi tiết chứng từ phù hợp với ID chứng từ [{0}] và loại chứng từ [{1}]!", Request.crkId, djmc));
            }
            //if (_mx.Any(p => p.Sl <= 0))
            //{
            //    throw new FailedException("Tồn tại bản ghi chi tiết có Số lượng bằng 0,Thao tác thất bại");
            //}
            return new ActResult();
        }

        /// <summary>
        /// Xử lý chính
        /// </summary>
        /// <returns></returns>
        protected override void Action(ActResult actResult)
        {
            switch (Request.djlx)
            {
                case (int)EnumDanJuLX.keshifayao:
                case (int)EnumDanJuLX.neibufayaotuihui:
                case (int)EnumDanJuLX.shenlingfayao:
                case (int)EnumDanJuLX.waibucuku:
                case (int)EnumDanJuLX.zhijiefayao:
                    if (!sysMedicineReceiptDmnService.ApprovalStorageIoReceipt(Request.crkId, Request.djlx, Request.shzt))
                    {
                        actResult.IsSucceed = false;
                        actResult.ResultMsg = "Duyệt thất bại!";
                    }
                    break;
                case (int)EnumDanJuLX.yaopinruku:
                    var processResult = new InStorageBillApprovalProcess(Request).Process();
                    actResult.IsSucceed = processResult.IsSucceed;
                    actResult.ResultMsg = processResult.ResultMsg;
                    break;
            }
        }

    }
}
