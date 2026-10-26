using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.MRQC.Web.Controllers
{
    /// <summary>
    /// Home/Indexkhi tải được tải mặc định dữ liệu bộ nhớ đệm
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
        /// lấy phần đồng bộ
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
        /// lấy phần không đồng bộ
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetAsyncClientsDataJson()
        {
            var data = new
            {
                itemDetails = this.GetItemDetailsList(),
                enums = this.GetEnumList("Newtouch.MRQC.Infrastructure"),
            };
            return Content(data.ToJson());
        }

    }
}