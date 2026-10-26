using FrameworkBase.MultiOrg.Web;
using Newtouch.MRQC.Domain.IDomainServices;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.MRQC.Web.Areas.SystemManage.Controllers
{
    public class CommonController : OrgControllerBase
    {
       
        private readonly ICommonDmnService _CommonDmnService;

        /// <summary>
        /// danh sách nhân sự theo vị trí
        /// </summary>
        /// <param name="dutyCode"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetStaffListByDutyCode(string dutyCode, string keyword)
        {
            var list = _CommonDmnService.GetStaffByDutyCode(this.OrganizeId, dutyCode, keyword);
            return Content(list.ToJson());
        }
    }
}