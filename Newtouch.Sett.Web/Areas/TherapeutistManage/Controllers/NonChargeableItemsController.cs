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

    public class NonChargeableItemsController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly ITherapeutistCompleteDmnService _therapeutistCompleteDmnService;
        public NonChargeableItemsController(ITherapeutistCompleteDmnService therapeutistCompleteDmnService)
        {
            _therapeutistCompleteDmnService = therapeutistCompleteDmnService;
        }


        #region trang khởi tạo
        public IActionResult UpgradeWorkingTime()
        {
            return View();
        }

        public IActionResult UpdateView()
        {
            return View();
        }

        #endregion

        #region Thời lượng Điều chỉnh

        /// <summary>
        /// tratra cứu được đến thời gian dài tổng và
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="ysgh"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public IActionResult GetStaffHourList(Pagination pagination, string ysgh, string year, string month)
        {
            var data = _therapeutistCompleteDmnService.GetStaffEachMonthHour(pagination, OrganizeId, ysgh, year, month);
            var list = new
            {
                rows = data,
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Content(list.ToJson());
        }

        /// <summary>
        /// được đến mỗi cái điều trị điều trị sư nào đó tháng chi tiết chi tiết thời gian dài
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public IActionResult GetFormJson(AdjustStaffHourVO vo)
        {
            // FA-016: AdjustStaffHourVO props are all `object`; a bare call binds
            // them null, so `vo.syear.ToString()` threw NullReferenceException.
            // Nulls pass through as empty filter strings; the service answer
            // (null/rows) is unchanged for populated calls.
            var data = _therapeutistCompleteDmnService.GetStaffWorkedHour(OrganizeId, vo?.ysgh?.ToString() ?? string.Empty, vo?.syear?.ToString() ?? string.Empty, vo?.smonth?.ToString() ?? string.Empty);
            return Content(data.ToJson());
        }

        /// <summary>
        /// thời gian dài điều chỉnh,hơn mới đến số dữ liệu kho
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public IActionResult submitUpdateTime(Dictionary<string, List<submitTimeVO>> vo)
        {
            _therapeutistCompleteDmnService.UpdateTime(vo);
            return Success("Thao tác thành công。");
        }

        #endregion
    }
}
