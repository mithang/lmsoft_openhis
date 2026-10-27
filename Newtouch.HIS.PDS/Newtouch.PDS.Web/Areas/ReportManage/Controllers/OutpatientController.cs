
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using System;
using Microsoft.AspNetCore.Mvc;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;
using Newtouch.Tools;

namespace Newtouch.HIS.Web.Areas.ReportManage.Controllers
{
    /// <summary>
    /// Quản lý báo cáo ngoại trú
    /// </summary>
    [Area("ReportManage")]
    public class OutpatientController : ControllerBase
    {
        private readonly IfyDmnService _ifyDmnService;
        /// <summary>
        /// Thống kê phát thuốc
        /// </summary>
        /// <returns></returns>
        public IActionResult DrugDeliveryStatistics()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

        /// <summary>
        /// Lấy thông tin phát thuốc
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="keyWork"></param>
        /// <returns></returns>
        public IActionResult GetDrugDeliveryList(DateTime startTime, DateTime endTime, string keyWork)
        {
            var result = _ifyDmnService.GetDrugDeliveryList(startTime, endTime, keyWork, Constants.CurrentYfbm.yfbmCode, OrganizeId);
            return Content(result.ToJson());
        }

        #region Thuốc、Tình hình sử dụng vật tư
        public IActionResult GetMaterialList(Pagination pagination,string ks,string ry, string slly, DateTime kssj, DateTime jssj, string keyword)
        {
            var result = _ifyDmnService.GetMaterialList(pagination, this.OrganizeId,ks, ry, slly, kssj, jssj,keyword);
            return Content(result.ToJson());
        }

        #endregion
    }
}