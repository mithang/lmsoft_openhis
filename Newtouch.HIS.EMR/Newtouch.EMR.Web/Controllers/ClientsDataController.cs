using Newtouch.EMR.Domain.IRepository;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.EMR.Web.Controllers
{
    /// <summary>
    /// Dữ liệu đệm tải mặc định khi tải Home/Index
    /// </summary>
    public class ClientsDataController : FrameworkBase.MultiOrg.Web.Controllers.ClientsDataController
    {
        private readonly IBlysRepo _blysRepo;

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
                ysdataList = _blysRepo.GetYsTreeV2(OrganizeId, ""),//Phần tử bệnh án
                ysmxdataList = _blysRepo.GetYsMX(OrganizeId, ""), //Chi tiết phần tử
                itemDetails = this.GetItemDetailsList(),
                enums = this.GetEnumList("Newtouch.EMR.Infrastructure"),
            };
            return Content(data.ToJson());
        }

    }
}