using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.Domain;
using Newtouch.Sett.Infrastructure;
using Newtouch.Sett.Web.Controllers;
using Newtouch.Sett.Web.Helpers;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Newtouch.Sett.Web.Areas.TherapeutistManage.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [Area("TherapeutistManage")]

    public class TherapeutistCompleteListController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly ITherapeutistCompleteDmnService _therapeutistCompleteDmnService;
        public TherapeutistCompleteListController(ITherapeutistCompleteDmnService therapeutistCompleteDmnService)
        {
            _therapeutistCompleteDmnService = therapeutistCompleteDmnService;
        }


        #region trang khởi tạo
        public IActionResult AddItem()
        {
            return View();
        }

        public IActionResult ArrangeWork()
        {
            return View();
        }

        public IActionResult TatisticWorkload()
        {
            return View();
        }

        public IActionResult AddArrange()
        {
            return View();
        }
        public IActionResult EditArrange()
        {
            return View();
        }

        public IActionResult WorkEfficiency()
        {
            return View();
        }
        #endregion

        #region Kỹ thuật viên trị liệuCông việc Danh sách
        /// <summary>
        /// tratra cứuđiều trịđiều trịsưcônglàmcộtbảng
        /// </summary>
        /// <returns></returns>
        public IActionResult GetGridJson(Pagination pagination, string BeginDate, string EndDate, string hzxm, string zls, string dl)
        {
            var rows = _therapeutistCompleteDmnService.GetTherapeutistListWorkedList(pagination, BeginDate, EndDate, hzxm, zls, dl, OrganizeId);
            var list = new
            {
                rows = rows,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }
        #endregion

        #region Kỹ thuật viên trị liệuCông việc Thời gian
        public IActionResult GetpbGridJson(Pagination pagination, int? year, int? month, string ysgh)
        {
            var rows = _therapeutistCompleteDmnService.GetTherapeutistWorkPlanList(pagination, year, month, ysgh, OrganizeId);
            var list = new
            {
                rows = rows,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        public IActionResult GetAllRehabDoctor()
        {
            var list = _therapeutistCompleteDmnService.GetAllRehabDoctor(OrganizeId);
            return Content(list.ToJson());
        }

        /// <summary>
        /// mớithêmxếpcathời gian,nêunộpđếnsốdữ liệukho
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="orgId"></param>
        public IActionResult AddRehabDoctorRange(Dictionary<string, List<AddStaffPlanVO>> vo)
        {
            _therapeutistCompleteDmnService.SubmitPlan(vo, OrganizeId);
            return Success();
        }

        /// <summary>
        ///đượclấyphiếucáiđiều trịđiều trịsưphiếucáithángphầnxếpca
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="orgId"></param>
        public IActionResult GetFormJson(string keyvalue)
        {
            var data = _therapeutistCompleteDmnService.GetFormJson(keyvalue, OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// mãbiên tậpphiếucáiđiều trịđiều trịsưxếpca
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="orgId"></param>
        public IActionResult EditRehabDoctorRange(DoctorWorkingDaysPlanEntity entity, string keyvalue)
        {
            _therapeutistCompleteDmnService.EditRehabDoctorRange(entity, keyvalue, OrganizeId);
            return Success("Thao tác thành công。");
        }

        /// <summary>
        /// xóatrừxếpca
        /// </summary>
        /// <param name="keyvalue"></param>
        /// <returns></returns>
        public IActionResult DelArrange(string keyvalue)
        {
            _therapeutistCompleteDmnService.DelArrange(keyvalue);
            return Success("Xóa thành công。");
        }
        #endregion

        #region Kỹ thuật viên trị liệuCông việc Thống kê
        public IActionResult GetStaffReport(Pagination pagination, string ysgh, string year, string month)
        {
            var rows = _therapeutistCompleteDmnService.GetStaffReport(pagination, OrganizeId, ysgh, year, month);
            return Content(rows.ToJson());
        }
        #endregion

        /// <summary>
        /// điều trịđiều trịsưcônglàmhiệu quảtỷ lệcộttrạng tháibiểu đồ
        /// </summary>
        public IActionResult GetVisitSC()
        {
            SCNumBO BO = null;
            var userCode = this.UserIdentity.UserCode;
            var orgId = OrganizeId;
            if (string.IsNullOrEmpty(orgId))
            {
                throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
            }
            BO = _therapeutistCompleteDmnService.GetVisitSC(orgId);
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = BO }.ToJson());
        }

        public IActionResult GetVisitDetailSC(Pagination pagination, string time, string type)
        {
            if (string.IsNullOrWhiteSpace(time) || string.IsNullOrWhiteSpace(type))
            {
                return Content("");
                //throw FailedException("loạiloạihoặcthời giangiữalàrỗng!");
            }
            time = (int.Parse(time) + 1).ToString();
            var data = _therapeutistCompleteDmnService.GetVisitDetailSC(pagination, OrganizeId, type, time);
            var list = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
    }
}
