using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.MRQC.Domain;

namespace Newtouch.MRQC.Web.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    public class CommonController : OrgControllerBase
    {
        private readonly ICommonDmnService _CommonDmnService;

        public CommonController(ICommonDmnService commonDmnService)
        {
            _CommonDmnService = commonDmnService;
        }

        /// <summary>
        /// 岗位人员列表
        /// </summary>
        public IActionResult GetStaffListByDutyCode(string dutyCode, string keyword)
        {
            var list = _CommonDmnService.GetStaffByDutyCode(this.OrganizeId, dutyCode, keyword);
            return Content(list.ToJson());
        }
    }
}
