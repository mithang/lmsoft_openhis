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
        /// tratra cứuđượcđếnthời giandàitổngvà
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
        /// đượcđếnmỗicáiđiều trịđiều trịsưnào đóthángchi tiếtchi tiếtthời giandài
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public IActionResult GetFormJson(AdjustStaffHourVO vo)
        {
            var data = _therapeutistCompleteDmnService.GetStaffWorkedHour(OrganizeId, vo.ysgh, vo.syear.ToString(), vo.smonth.ToString());
            return Content(data.ToJson());
        }

        /// <summary>
        /// thời giandàiđiềuchỉnh，hơnmớiđếnsốdữ liệukho
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
