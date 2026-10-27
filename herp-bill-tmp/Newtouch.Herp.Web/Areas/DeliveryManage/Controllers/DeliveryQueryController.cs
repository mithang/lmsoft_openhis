using Microsoft.AspNetCore.Mvc;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Infrastructure;
using Newtouch.Herp.Infrastructure.Enum;
using NewtouchHIS.Lib.Base.Extension;

namespace Newtouch.Herp.Web.Areas.DeliveryManage.Controllers
{
    /// <summary>
    /// 配送单查询 (Core port)
    /// NOTE (verified from the original source): this controller is JSON-only —
    /// both actions return Content(...ToJson()) and there are no DeliveryManage
    /// views in the source tree (only a web.config). No views are invented.
    /// </summary>
    [Area("DeliveryManage")]
    public class DeliveryQueryController : ControllerBase
    {
        private readonly IKfCrkdjDmnService crkdjDmnService;

        public DeliveryQueryController(IKfCrkdjDmnService crkdjDmnService)
        {
            this.crkdjDmnService = crkdjDmnService;
        }

        /// <summary>
        /// 入库
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult InStorageDeliveryQuery(string keyword)
        {
            var result = crkdjDmnService.SelectInStorageDeliveryInfo(keyword, Constants.CurrentKf.currentKfId, UserIdentity.UserCode, OrganizeId);
            return Content(result.ToJson());
        }

        /// <summary>
        /// 获取已审核配送单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult OutStorageDeliveryQuery(string keyword)
        {
            var result = crkdjDmnService.SelectInStorageDeliveryInfo(keyword, Constants.CurrentKf.currentKfId, UserIdentity.UserCode, OrganizeId, ((int)EnumAuditState.Adopt).ToString());
            return Content(result.ToJson());
        }
    }
}
