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
            request.djmx = new List<SysMedicineStorageIOReceiptDetailEntity>();
            request.dj = new SysMedicineStorageIOReceiptEntity();
        }

        /// <summary>
        /// Kiểm tra
        /// </summary>
        /// <returns></returns>
        protected override ActResult Validata()
        {
            if (Request == null)
            {
                throw new FailedException("Thông tin duyệt không được để trống！");
            }
            if (string.IsNullOrWhiteSpace(Request.crkId))
            {
                throw new FailedException("Chứng từ xuất nhập kho ID Không được để trống！");
            }
            if (string.IsNullOrWhiteSpace(Request.shzt))
            {
                throw new FailedException("Trạng thái duyệt không được để trống！");
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
                    throw new FailedException("Không khớp loại chứng từ hiệu lực！");
            }
            Request.dj = _crkdj.FindEntity(p => p.crkId == Request.crkId
            && p.OrganizeId == OrganizeId
            && p.djlx == Request.djlx
            && p.zt == "1"
            && (p.Ckbm == Constants.CurrentYfbm.yfbmCode || p.Rkbm == Constants.CurrentYfbm.yfbmCode));
            if (Request.dj == null)
            {
                throw new FailedException(string.Format("Theo chứng từ truyền vào ID và loại chứng từ không tìm thấy thông tin chứng từ đáp ứng yêu cầu，Chứng từ ID【{0}】,Loại chứng từ【{1}】！", Request.crkId, djmc));
            }
            if (Request.shzt == ((int)EnumDjShzt.Cancelled).ToString())
            {
                if (((int)EnumDjShzt.WaitingApprove).ToString() == Request.dj.shzt) throw new FailedException("Chứng từ chưa duyệt，Không cho phép hủy bỏ！"); //Thao tác hủy bỏ，Phải tồn tại bản ghi duyệt chứng từ
                if (((int)EnumDjShzt.Cancelled).ToString() == Request.dj.shzt) throw new FailedException("Chứng từ đã hủy bỏ，Không thể hủy bỏ trùng lặp！");
                if (((int)EnumDjShzt.Rejected).ToString() == Request.dj.shzt) throw new FailedException("Chứng từ đã duyệt không đạt，Không thể hủy bỏ！");
            }
            else if (Request.shzt == ((int)EnumDjShzt.Rejected).ToString())
            {
                if (((int)EnumDjShzt.WaitingApprove).ToString() != Request.dj.shzt) throw new FailedException("Chỉ có thể thao tác chứng từ trạng thái chờ xử lý！");
            }
            else
            {
                if (((int)EnumDjShzt.WaitingApprove).ToString() != Request.dj.shzt) throw new FailedException("Chứng từ đã duyệt，Không thể duyệt trùng lặp！");//Thao tác không hủy bỏ，Phải không tồn tại bản ghi duyệt chứng từ
            }
            Request.djmx = _crkdjmx.IQueryable(p => p.crkId == Request.crkId && p.zt == "1").ToList();
            if (Request.djmx == null || Request.djmx.Count <= 0)
            {
                throw new FailedException(string.Format("Theo chứng từ truyền vào ID và loại chứng từ không tìm thấy chi tiết chứng từ đáp ứng yêu cầu，Chứng từ ID【{0}】,Loại chứng từ【{1}】！", Request.crkId, djmc));
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
                        actResult.ResultMsg = "Duyệt thất bại！";
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
