using FrameworkBase.MultiOrg.Web;
using Newtouch.Application.Interface;
using Newtouch.Core.Common;
using Newtouch.Domain.IDomainServices;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Web.Mvc;
using Newtouch.Infrastructure;
using Newtouch.Domain.ViewModels;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ViewModels.Outpatient;

namespace Newtouch.CIS.Web.Areas.NurseManage.Controllers
{
    /// <summary>
    /// Quản lý truyền dịch
    /// </summary>
    public class InfusionController : OrgControllerBase
    {
        private readonly IMzsyypxxDmnService _mzsyypxxDmnService;
        private readonly IInfusionApp _infusionApp;
        private readonly ISysUserDmnService _sysUserDmnService;
        private readonly IMzsyzxxxRepo _mzsyzxxxRepo;

        #region 视图
        /// <summary>
        /// Thực hiện tiêm
        /// </summary>
        /// <returns></returns>
        public ActionResult Injection()
        {
            return View();
        }

        /// <summary>
        /// Tra cứu lịch sử tiêm
        /// </summary>
        /// <returns></returns>
        public ActionResult InfusionHistory()
        {
            return View();
        }

        /// <summary>
        /// Thực hiện truyền dịch
        /// </summary>
        /// <returns></returns>
        public ActionResult InjectionForm()
        {
            return View();
        }
        #endregion

        #region 获取数据

        /// <summary>
        /// Lấy danh sách bệnh nhân ngoại trú
        /// </summary>
        /// <returns></returns>
        public ActionResult PatientListQuery(Pagination pagination, string kh, string fph, DateTime kssj, DateTime jssj)
        {
            var data = new
            {
                rows = _mzsyypxxDmnService.SelectKhAndXm(pagination, kh, fph, kssj, jssj, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// Lấy danh sách bệnh nhân ngoại trú (Test da chờ thực hiện)
        /// </summary>
        /// <returns></returns>
        public ActionResult PatientHistoryListQuery(Pagination pagination, string kh, string fph, DateTime kssj, DateTime jssj)
        {
            var data = new
            {
                rows = _mzsyypxxDmnService.SelectKhAndXm(pagination, kh, fph, kssj, jssj, OrganizeId, true),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// Lấy thông tin truyền dịch chưa kết thúc theo số thẻ
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="kh"></param>
        /// <returns></returns>
        public ActionResult MzsyypxxQury(Pagination pagination, string kh)
        {
            var data = new
            {
                rows = _mzsyypxxDmnService.SelectMzsyypxxByKh(pagination, kh, OrganizeId, false),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// Lấy thông tin truyền dịch chưa kết thúc theo số thẻ
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="kh"></param>
        /// <returns></returns>
        public ActionResult HistoryMzsyypxxQury(Pagination pagination, string kh)
        {
            var data = new
            {
                rows = _mzsyypxxDmnService.SelectMzsyypxxByKh(pagination, kh, OrganizeId, true),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// Đồng bộ thông tin đơn thuốc
        /// </summary>
        /// <param name="kh"></param>
        /// <param name="fph"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <returns></returns>
        public ActionResult SyncSettRpDetail(string kh, string fph, DateTime kssj, DateTime jssj)
        {
            var result = _infusionApp.SyncSettRpDetail(kssj, jssj, fph, kh);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        /// <summary>
        /// Lấy thông tin vị trí
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetStaffList(string keyword)
        {
            return Content(_sysUserDmnService.GetSatffList(OrganizeId, keyword).ToJson());
        }

        #endregion

        #region 执行

        /// <summary>
        /// Nhóm
        /// </summary>
        /// <param name="tags"></param>
        /// <param name="kh"></param>
        /// <returns></returns>
        public ActionResult Grouping(List<long> tags, string kh)
        {
            var groupNo = _infusionApp.CreateGroupNo(kh);
            var result = _infusionApp.Grouping(tags, groupNo);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        /// <summary>
        /// Bắt đầu truyền dịch
        /// </summary>
        /// <param name="tags"></param>
        /// <param name="seatNum">Số ghế/Số giường</param>
        /// <returns></returns>
        public ActionResult StartInfusion(List<long> tags, string seatNum)
        {
            var result = _infusionApp.StartInfusion(tags, DateTime.Now, seatNum);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        /// <summary>
        /// Kết thúc truyền dịch
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        public ActionResult EndInfusion(List<long> tags)
        {
            var result = _infusionApp.EndInfusion(tags, DateTime.Now);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        /// <summary>
        /// Đặt số ghế
        /// </summary>
        /// <param name="tag">Mục tiêuID</param>
        /// <param name="seatNum">Số ghế</param>
        /// <returns></returns>
        public ActionResult SetSeatNum(long tag, string seatNum)
        {
            var result = _infusionApp.SetSeatNum(tag, seatNum);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        /// <summary>
        /// In nhãn chai
        /// </summary>
        /// <returns></returns>
        public ActionResult PrintBottleLabel(string tags)
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
        /// Tạo mã QR tra cứu truyền dịch
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        private string GenerateInfusionQueryQr(List<long> tags)
        {
            var authority = "";
            if (HttpContext.Request.Url != null)
            {
                authority = HttpContext.Request.Url.Authority;
            }
            if (string.IsNullOrWhiteSpace(authority)) throw new Exception("获取服务器地址失败，请联系管理员");
            const string absolutePath = "/InfusionManage/Query";
            var query = new StringBuilder("?organizeId=").Append(OrganizeId).Append("&tags=");
            if (tags != null && tags.Count > 0)
            {
                tags.ForEach(p => { query.Append(p + ","); });
            }
            var url = new StringBuilder("http://").Append(authority).Append(absolutePath).Append(query.ToString().Trim(','));
            var iconPath = HttpContext.Server.MapPath("~/Content/img/cloud.png");
            var qrCode = QrEncoder.Code(url.ToString(), 12, 15, iconPath, 0, 0, false);
            using (var ms = new MemoryStream())
            {
                qrCode.Save(ms, ImageFormat.Png);
                var b = ms.ToArray();
                return Convert.ToBase64String(b);
            }
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
        public ActionResult SubmitExec(string syIds, string zt, string dispenser, string executor, string remark)
        {
            if (string.IsNullOrWhiteSpace(syIds)) return Error("请传入要执行的药品");
            var ids = new List<long>();
            foreach (var item in syIds.Split(','))
            {
                long tmp = 0;
                long.TryParse(item, out tmp);
                if (tmp > 0) ids.Add(tmp);
            }
            var result = _mzsyzxxxRepo.Exec(ids, zt, dispenser, executor, remark, OrganizeId);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }
        #endregion
    }
}