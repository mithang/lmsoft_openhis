
using System;
using System.Web.Mvc;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;
using Newtouch.Tools;

namespace Newtouch.HIS.Web.Areas.ReportManage.Controllers
{
    /// <summary>
    /// ngoại trú báo cáo quản lý
    /// </summary>
    public class OutpatientController : ControllerBase
    {
        private readonly IfyDmnService _ifyDmnService;
        /// <summary>
        /// phát thuốc thống kê
        /// </summary>
        /// <returns></returns>
        public ActionResult DrugDeliveryStatistics()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

        /// <summary>
        /// lấy thông tin phát thuốc
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="keyWork"></param>
        /// <returns></returns>
        public ActionResult GetDrugDeliveryList(DateTime startTime, DateTime endTime, string keyWork)
        {
            var result = _ifyDmnService.GetDrugDeliveryList(startTime, endTime, keyWork, Constants.CurrentYfbm.yfbmCode, OrganizeId);
            return Content(result.ToJson());
        }

        #region 药品、耗材使用情况
        public ActionResult GetMaterialList(Pagination pagination,string ks,string ry, string slly, DateTime kssj, DateTime jssj, string keyword)
        {
            var result = _ifyDmnService.GetMaterialList(pagination, this.OrganizeId,ks, ry, slly, kssj, jssj,keyword);
            return Content(result.ToJson());
        }

        #endregion
    }
}