using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.MR.ManageSystem.Web.Controllers
{
    /// <summary>
    /// Home/IndexTảigiờ tải mặc định Bộ nhớ đệmDữ liệu
    /// </summary>
    public class ClientsDataController : FrameworkBase.MultiOrg.Web.Controllers.ClientsDataController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetModuleDataJson()
        {
            var data = new
            {
                authorizeMenu = this.GetMenuList(),
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// Lấy phần đồng bộ
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetClientsDataJson()
        {
            var data = new
            {
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// Lấy phần không đồng bộ
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetAsyncClientsDataJson()
        {
            var data = new
            {
                itemDetails = this.GetItemDetailsList(),
                enums = this.GetEnumList("Newtouch.MR.ManageSystem.Infrastructure"),
            };
            return Content(data.ToJson());
        }

    }
}