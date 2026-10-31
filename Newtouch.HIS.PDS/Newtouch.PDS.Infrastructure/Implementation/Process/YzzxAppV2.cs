using Newtouch.Infrastructure.EF;
using Newtouch.HIS.Application.Implementation;
using FrameworkBase.MultiOrg.DmnService;
using System;
using System.Collections.Generic;
using System.Text;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.DomainServices;
using Newtouch.Infrastructure;
using Newtouch.Infrastructure.ExException;
using Newtouch.PDS.Requset;
using Newtouch.PDS.Requset.Zyypyz;

namespace Newtouch.HIS.Application.Implementation.Process
{
    /// <summary>
    /// Tài nguyên đặt trước nội trú(Đóng băng tồn kho)
    /// </summary>
    public class YzzxAppV2 : ProcessorFun<ZyypyzzxRequest>
    {
        private List<ZyYpyzxxEntity> _yzEntities = new List<ZyYpyzxxEntity>();
        private string zxId = Guid.NewGuid().ToString();//Thực hiện y lệnhID

        private Dictionary<string, Newtouch.HIS.Domain.Entity.SysMedicineComplexVEntity> 
            _ypDict = new Dictionary<string, Newtouch.HIS.Domain.Entity.SysMedicineComplexVEntity>();

        private readonly IZyYpyzxxRepo _zyYpyzzxRepo;
        private readonly IDispensingDmnService _dispensingDmnService;

        public YzzxAppV2(ZyypyzzxRequest request) : base(request)
        {
        }

        /// <summary>
        /// Kiểm tra
        /// </summary>
        /// <returns></returns>
        protected override ActResult Validata()
        {
            if (Request == null) throw new FailedException("Bản tin yêu cầu không được để trống");
            if (Request.yzList == null || Request.yzList.Count <= 0) throw new FailedException("Danh sách y lệnh không được để trống");
            if (string.IsNullOrWhiteSpace(Request.OrganizeId)) throw new FailedException("Tổ chức ID Không được để trống");
            try
            {
                foreach(var detail in Request.yzList)
                {
                    if (string.IsNullOrWhiteSpace(detail.ypCode)) throw new Exception("Vui lòng truyền mã thuốc hiệu lực");
                    if (string.IsNullOrWhiteSpace(detail.yzId)) throw new Exception("Vui lòng truyền y lệnh hiệu lựcID");
                    if (string.IsNullOrWhiteSpace(detail.bqCode)) throw new Exception("Vui lòng truyền mã khu bệnh hiệu lực");
                    if (string.IsNullOrWhiteSpace(detail.fyyf)) throw new Exception("Vui lòng truyền mã nhà thuốc phát thuốc hiệu lực");
                    if (detail.sl <= 0) throw new Exception("Vui lòng nhập số lượng thực hiện y lệnh hiệu lực");

                    var ypxx = GetYpDetails(Request.OrganizeId, detail.ypCode);
                    if (ypxx == null) throw new Exception("Không tìm thấy thuốc【" + detail.ypCode + "】。");
                    detail.ypmc = ypxx.ypmc;
                    if (detail.zhyz <= 0)
                    {
                        if (ypxx.zycls == null) throw new Exception("Thuốc【" + ypxx.ypmc + "】Số chia lẻ nội trú trống。");
                        detail.zhyz = (int)ypxx.zycls;
                    }

                    detail.dj = ypxx.lsj / ypxx.bzs * detail.zhyz;
                    detail.je = detail.dj * detail.sl;
                }
            }
            catch (Exception e)
            {
                throw new FailedException("", e.Message + (e.InnerException != null ? e.InnerException.Message : ""));
            }
            return new ActResult();
        }

        protected override void BeforeAction(ActResult actResult)
        {
            foreach (var p in Request.yzList)
            {
                decimal p_ts = 0m;
                if (p.ts == null)
                {
                    p_ts = 0;
                }
                else
                {
                    p_ts = (decimal)p.ts;
                }

                var item = new ZyYpyzxxEntity
                {
                    bqCode = p.bqCode,
                    CreatorCode = p.yzzxsqr,
                    CreateTime = DateTime.Now,
                    cw = p.cw,
                    dj = p.dj,
                    dl = p.dl,
                    fybz = ((int)EnumFybz.Yp).ToString(),
                    fyyf = p.fyyf,
                    je = p.je,
                    jsrq = p.jsrq < Convert.ToDateTime("1971-01-01") ? Constants.MinDateTime : p.jsrq,
                    ksCode = p.ksCode,
                    ksrq = p.ksrq < Convert.ToDateTime("1971-01-01") ? Constants.MinDateTime : p.ksrq,
                    lyxh = p.lyxh,
                    OrganizeId = Request.OrganizeId,
                    pcmc = p.pcmc,
                    sjap = p.sjap,
                    sl = p.sl,
                    zhyz = p.zhyz,
                    yl = p.yl,
                    yldw = p.yldw,
                    ypCode = p.ypCode,
                    ysgh = p.ysgh,
                    yzbz = p.yzbz,
                    yzxz = p.yzxz,
                    yzzxsqr = p.yzzxsqr,
                    zfbl = p.zfbl,
                    zfxz = p.zfxz,
                    zlff = p.zlff,
                    zxsl = p.zxsl,
                    zyh = p.zyh,
                    patientName = p.patientName,
                    zxrq = p.zxrq < Convert.ToDateTime("1971-01-01") ? Constants.MinDateTime : p.zxrq,
                    zh = p.zh,
                    zxId = zxId,
                    yzId = p.yzId,
                    ts = p_ts,
                    yzlx = p.yzlx
                };

                _yzEntities.Add(item);
            }
        }

        protected override void Action(ActResult actResult)
        {
            if (_yzEntities == null || _yzEntities.Count <= 0) throw new ProcessException("Thu thập dữ liệu thực hiện y lệnh thuốc nội trú thất bại。");

            int t = 0;
            try
            {
                t = _zyYpyzzxRepo.Insert(_yzEntities);
            }
            catch(Exception ex)
            {
                actResult.IsSucceed = false;
                actResult.ResultMsg = "Ngoại lệ lưu dữ liệu thực hiện y lệnh thuốc nội trú：" + ex.Message;
                return;
            }
            if (t <= 0)
            {
                actResult.IsSucceed = false;
                actResult.ResultMsg = "Lưu dữ liệu thực hiện y lệnh thuốc nội trú thất bại：Không có Số bản ghi thành công。";
                return;
            }

            var successList = new List<ZyYpyzxxEntity>();
            var errorMsg = new StringBuilder();
            foreach (var p in _yzEntities)
            {
                string phResult = "";
                try
                {
                    phResult = _dispensingDmnService.HospitalizationBook(p.yzId, p.zxId, Request.OrganizeId, p.fyyf, p.yzzxsqr, p.patientName);
                }
                catch(Exception ex)
                {
                    phResult = "HospitalizationBookNgoại lệ：" + ex.Message;
                }
                if (!string.IsNullOrWhiteSpace(phResult))
                {
                    errorMsg.Append(phResult + "; ");
                    CancelForzen(successList);
                    break;
                }
                successList.Add(p);
            }

            if (string.IsNullOrWhiteSpace(errorMsg.ToString()))
            {
                actResult.Data = Request.yzList;//Do kiểm soát giao dịch,vì vậy hoặc tất cả thất bại,nếu không tất cả thành công,DataTrả về hàng đợi thành công
                return;
            }

            _zyYpyzzxRepo.DeleteByZxId(zxId);

            actResult.IsSucceed = false;
            actResult.ResultMsg = errorMsg.ToString();
        }

        /// <summary>
        /// Ghi đè phương thức lớp cha(Triển khai rỗng)
        /// </summary>
        /// <param name="result"></param>
        protected override void AsyncProcess(ActResult result)
        {
        }

        /// <summary>
        /// Ghi đè phương thức lớp cha(Giải phóng bộ nhớ)
        /// </summary>
        /// <param name="result"></param>
        protected override void Dispose(ActResult result)
        {
            _ypDict.Clear();
        }

        /// <summary>
        /// Hủy đóng băng,Xóa chi tiết y lệnh
        /// </summary>
        /// <param name="tg"></param>
        private void CancelForzen(List<ZyYpyzxxEntity> tg)
        {
            foreach (var p in tg)
            {
                var t = _dispensingDmnService.CancelForzenAndDeleteYz(p.yzId, p.zxId);
            }
        }

        /// <summary>
        /// Lấy thông tin thuốc(Dựa trên đệm)
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        private Newtouch.HIS.Domain.Entity.SysMedicineComplexVEntity GetYpDetails(string orgId, string ypCode)
        {
            if (_ypDict.ContainsKey(ypCode))
            {
                return _ypDict[ypCode];
            }

            var ypxx = new SysMedicineExDmnService(new DefaultDatabaseFactory(), false).GetYpDetails(orgId, ypCode);
            if (ypxx != null)
            {
                _ypDict[ypCode] = ypxx;
            }

            return ypxx;
        }
    }
}