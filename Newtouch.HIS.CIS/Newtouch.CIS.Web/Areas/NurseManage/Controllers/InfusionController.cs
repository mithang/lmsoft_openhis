using NewtouchHIS.Framework.Web.Controllers;
using Newtouch.CIS.Web.Helpers;
using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Domain.Entity;
using Newtouch.CIS.Repository;
using Newtouch.Application.Interface;

using Newtouch.Domain.IDomainServices;
using Newtouch.CIS.Compat;
using System;
using System.Collections.Generic;

using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtouch.Infrastructure;
using Newtouch.Domain.ViewModels;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ViewModels.Outpatient;
using Newtouch.Domain.ValueObjects;
using NewtouchHIS.Lib.Framework.Operator;

namespace Newtouch.CIS.Web.Areas.NurseManage.Controllers
{
    /// <summary>
    /// truyền dịchQuản lý
    /// </summary>
    [Area("NurseManage")]
    public class InfusionController : OrgControllerBase
    {
        private readonly IMzsyypxxDmnService _mzsyypxxDmnService;
        private readonly IInfusionApp _infusionApp;
        private readonly ISysUserDmnService _sysUserDmnService;
        private readonly IMzsyzxxxRepo _mzsyzxxxRepo;
        private readonly IMzsyypxxRepo _mzsyypxxRepo;
        private readonly IOutpatientNurseDmnServise _OutpatientNurseDmnService;

        public InfusionController(IMzsyypxxDmnService mzsyypxxDmnService, IInfusionApp infusionApp, ISysUserDmnService sysUserDmnService, IMzsyzxxxRepo mzsyzxxxRepo, IMzsyypxxRepo mzsyypxxRepo, IOutpatientNurseDmnServise outpatientNurseDmnService)
        {
            this._mzsyypxxDmnService = mzsyypxxDmnService;
            this._infusionApp = infusionApp;
            this._sysUserDmnService = sysUserDmnService;
            this._mzsyzxxxRepo = mzsyzxxxRepo;
            this._mzsyypxxRepo = mzsyypxxRepo;
            this._OutpatientNurseDmnService = outpatientNurseDmnService;
        }

        #region Chế độ xem
        /// <summary>
        /// Lưu ýxạThực hiện
        /// </summary>
        /// <returns></returns>
        public IActionResult Injection()
        {
            return View();
        }

        /// <summary>
        /// Lưu ýxạLịch sửTra cứu
        /// </summary>
        /// <returns></returns>
        public IActionResult InfusionHistory()
        {
            return View();
        }

        /// <summary>
        /// truyền dịchThực hiện
        /// </summary>
        /// <returns></returns>
        public IActionResult InjectionForm()
        {
            return View();
        }

        /// <summary>
        /// giữxemĐăng ký
        /// </summary>
        /// <returns></returns>
        public IActionResult observationFrom()
        {
            return View();
        }
        /// <summary>
        /// Quản lý phản ứng bất lợi của thuốc
        /// </summary>
        /// <returns></returns>
        public IActionResult DrugReactManage()
        {
            return View();
        }
        #endregion

        #region LấyDữ liệu

        /// <summary>
        /// LấyNgoại trúDanh sách bệnh nhân
        /// </summary>
        /// <returns></returns>
        public IActionResult PatientListQuery(Pagination pagination, string kh, string mzh,string fph, DateTime kssj, DateTime jssj)
        {
            var data = new
            {
                rows = _mzsyypxxDmnService.SelectKhAndXm(pagination, kh, mzh,fph, kssj, jssj, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// LấyNgoại trúDanh sách bệnh nhân (chờlàmTest da)
        /// </summary>
        /// <returns></returns>
        public IActionResult PatientHistoryListQuery(Pagination pagination, string kh, string mzh,string fph, DateTime kssj, DateTime jssj)
        {
            var data = new
            {
                rows = _mzsyypxxDmnService.SelectKhAndXm(pagination, kh,mzh, fph, kssj, jssj, OrganizeId, true),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// TheoSố thẻLấychưaKết thúctruyền dịchthông tin
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="kh"></param>
        /// <returns></returns>
        public IActionResult MzsyypxxQury(Pagination pagination, string kh,string mzh)
        {
            var data = new
            {
                rows = _mzsyypxxDmnService.SelectMzsyypxxByKh(pagination, kh,mzh, OrganizeId, false),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// TheoSố thẻLấychưaKết thúctruyền dịchthông tin
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="kh"></param>
        /// <returns></returns>
        public IActionResult HistoryMzsyypxxQury(Pagination pagination, string kh,string mzh)
        {
            var data = new
            {
                rows = _mzsyypxxDmnService.SelectMzsyypxxByKh(pagination, kh,mzh, OrganizeId, true),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// Đồng bộĐơn thuốcthông tin
        /// </summary>
        /// <param name="kh"></param>
        /// <param name="fph"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <returns></returns>
        public IActionResult SyncSettRpDetail(string kh,string mzh, string fph, DateTime kssj, DateTime jssj)
        {
            var result = _infusionApp.SyncSettRpDetail(kssj, jssj, fph, kh,mzh);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        /// <summary>
        /// Lấyvị trí ký tựthông tin
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetStaffList(string keyword)
        {
            return Content(_sysUserDmnService.GetSatffList(OrganizeId, keyword).ToJson());
        }

        #endregion

        #region Thực hiện

        /// <summary>
        /// phútnhóm
        /// </summary>
        /// <param name="tags"></param>
        /// <param name="kh"></param>
        /// <returns></returns>
        public IActionResult Grouping(List<long> tags, string kh,string mzh)
        {
            var groupNo = _infusionApp.CreateGroupNo(kh,mzh);
            var result = _infusionApp.Grouping(tags, groupNo);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        /// <summary>
        /// Bắt đầutruyền dịch
        /// </summary>
        /// <param name="tags"></param>
        /// <param name="seatNum">ghếsố/Số giường</param>
        /// <returns></returns>
        public IActionResult StartInfusion(List<long> tags, string seatNum)
        {
            var result = _infusionApp.StartInfusion(tags, DateTime.Now, seatNum);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        /// <summary>
        /// Kết thúctruyền dịch
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        public IActionResult EndInfusion(List<long> tags)
        {
            var result = _infusionApp.EndInfusion(tags, DateTime.Now);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        /// <summary>
        /// Cài đặtghế ký tựsố
        /// </summary>
        /// <param name="tag">Mục tiêuID</param>
        /// <param name="seatNum">ghế ký tựsố</param>
        /// <returns></returns>
        public IActionResult SetSeatNum(long tag, string seatNum)
        {
            var result = _infusionApp.SetSeatNum(tag, seatNum);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        /// <summary>
        /// In nhãn chai
        /// </summary>
        /// <returns></returns>
        public IActionResult PrintBottleLabel(string tags)
        {
            var ids = new List<long>();
            if (!string.IsNullOrWhiteSpace(tags))
            {
                foreach (var item in tags.Split(','))
                {
                    long tmp;
                    long.TryParse(item, out tmp);
                    if (tmp > 0) ids.Add(tmp);
                }
            }
            ViewBag.src = "data:image/png;base64," + GenerateInfusionQueryQr(ids);
            var drugs = _infusionApp.MzsyxxQuery(ids, OrganizeId);
            ViewBag.patInfo = new InfusionExecVO();
            ViewBag.drugs = drugs ?? new List<MzsyypxxVO>();
            if (drugs == null || drugs.Count <= 0) return View();
            ViewBag.patInfo.patientName = drugs[0].xm;
            ViewBag.patInfo.seatNum = drugs[0].seatNum;
            ViewBag.patInfo.remark = drugs[0].remark;
            ViewBag.patInfo.dispenser = drugs[0].dispenser;
            ViewBag.patInfo.dispenserName = drugs[0].dispenserName;
            ViewBag.patInfo.executor = drugs[0].executor;
            ViewBag.patInfo.executorName = drugs[0].executorName;
            return View();
        }

        /// <summary>
        /// tạotruyền dịchTra cứuMã QR
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        private string GenerateInfusionQueryQr(List<long> tags)
        {
            var authority = HttpContext.Request.Host.Value;
            if (string.IsNullOrWhiteSpace(authority)) throw new Exception("Lấy địa chỉ máy chủ thất bại, vui lòng liên hệ quản trị viên");
            const string absolutePath = "/InfusionManage/Query";
            var query = new StringBuilder("?organizeId=").Append(OrganizeId).Append("&tags=");
            if (tags != null && tags.Count > 0)
            {
                tags.ForEach(p => { query.Append(p + ","); });
            }
            var url = new StringBuilder("http://").Append(authority).Append(absolutePath).Append(query.ToString().Trim(','));
            var iconPath = WebPath.MapPath("~/Content/img/cloud.png");
            // Ported QREncoder returns PNG bytes directly (no GDI+ on Linux).
            var qrCode = QrEncoder.Code(url.ToString(), 12, 15, iconPath, 0, 0, false);
            return Convert.ToBase64String(qrCode);
        }

        /// <summary>
        /// Thực hiện
        /// </summary>
        /// <param name="syIds"></param>
        /// <param name="zt"></param>
        /// <param name="dispenser"></param>
        /// <param name="executor"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SubmitExec(string syIds, string seatNum, string zt, string dispenser, string executor,DateTime? sykssj,DateTime? syjssj, string remark)
        {
            if (string.IsNullOrWhiteSpace(syIds)) return Error("Vui lòng truyền vào thuốc cần thực hiện");
            var ids = new List<long>();
            foreach (var item in syIds.Split(','))
            {
                long tmp = 0;
                long.TryParse(item, out tmp);
                if (tmp > 0) ids.Add(tmp);
            }
            var result = _mzsyzxxxRepo.Exec(ids, seatNum, zt, dispenser, executor, sykssj,syjssj,remark, OrganizeId);
            _mzsyypxxRepo.Exec(ids,OrganizeId);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        /// <summary>
        /// truyền dịchThực hiệnThu hồi
        /// </summary>
        /// <param name="syIds"></param>
        /// <returns></returns>
        public IActionResult CancelExec(string syIds)
        {
            if (string.IsNullOrWhiteSpace(syIds)) return Error("Vui lòng truyền vào thuốc cần thực hiện");
            var idss = syIds.Split('|');
            var ids = new List<string>();
            var ypids = new List<long>();
            foreach (var item in idss[0].Split(','))
            {
                if(item!="") ids.Add(item);

            }
            foreach (var item in idss[1].Split(','))
            {
                long tmp = 0;
                long.TryParse(item, out tmp);
                if (tmp > 0) ypids.Add(tmp);
            }
            var result = _mzsyzxxxRepo.CanCelExec(ids, OrganizeId);
            _mzsyypxxRepo.CanCelExec(ypids, OrganizeId);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }
        #endregion

        public IActionResult Getlgjl(string syxxids)
        {
            var data = new
            {
                rows = _OutpatientNurseDmnService.Getlgjl(syxxids, OrganizeId),

            };
            return Content(data.ToJson());
        }

        public IActionResult SavaLgdj(IList<ObservationFromVO> lgdjlist)
        {
            OperatorModel user = this.UserIdentity;
            var patOutMsg = _OutpatientNurseDmnService.SavaLgdj(lgdjlist, this.OrganizeId, user);
            return patOutMsg == "T" ? Success("Lưu thành công.") : Error(patOutMsg);
        }
    }
}