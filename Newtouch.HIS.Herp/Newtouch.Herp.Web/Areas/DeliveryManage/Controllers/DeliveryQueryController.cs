using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Infrastructure;
using Newtouch.Herp.Infrastructure.Enum;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.Herp.Web.Areas.DeliveryManage.Controllers
{
    /// <summary>
    /// Tra cứu phiếu giao hàng
    /// </summary>
    public class DeliveryQueryController : ControllerBase
    {
        private readonly IKfCrkdjDmnService crkdjDmnService;

        /// <summary>
        /// Nhập kho
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult InStorageDeliveryQuery(string keyword)
        {
            var result = crkdjDmnService.SelectInStorageDeliveryInfo(keyword, Constants.CurrentKf.currentKfId, UserIdentity.UserCode, OrganizeId);
            return Content(result.ToJson());
        }

        /// <summary>
        /// Lấy phiếu giao hàng đã duyệt
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OutStorageDeliveryQuery(string keyword)
        {
            var result = crkdjDmnService.SelectInStorageDeliveryInfo(keyword, Constants.CurrentKf.currentKfId, UserIdentity.UserCode, OrganizeId, ((int)EnumAuditState.Adopt).ToString());
            return Content(result.ToJson());
        }
    }
}