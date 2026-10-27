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

namespace Newtouch.Sett.Web.Areas.SystemManage.Controllers
{

    /// <summary>
    /// 系统人员 扩展
    /// </summary>
    [Area("SystemManage")]

    public class StaffController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly ISysUserDmnService _sysUserDmnService;

        public StaffController(ISysOrganizeDmnService sysOrganizeDmnService, ISysDepartmentRepo sysDepartmentRepo, ISysUserDmnService sysUserDmnService)
        {
            this._sysUserDmnService = sysUserDmnService;
        }

        /// <summary>
        /// 获取岗位人员
        /// </summary>
        /// <param name="orgId">组织机构Id</param>
        /// <param name="dutyCode">岗位Code</param>
        /// <param name="ks">科室</param>
        /// <param name="keyword">关键字 匹配人员名称或工号</param>
        /// <returns></returns>

        public IActionResult Selector()
        {
            return View("~/Views/Shared/SysManageNotice.cshtml");
        }

        public IActionResult GetStaffByDutyCode(string orgId, string dutyCode, string ks, string keyword)
        {
            if (string.IsNullOrWhiteSpace(orgId))
            {
                orgId = this.OrganizeId;
            }
            if (string.IsNullOrWhiteSpace(orgId) || string.IsNullOrWhiteSpace(dutyCode))
            {
                return null;
            }
            var list = this._sysUserDmnService.GetStaffByDutyCode(orgId, dutyCode);
            if (!string.IsNullOrWhiteSpace(ks))
            {
                list = list.Where(a => a.ks == ks).ToList();
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                list = list.Where(a => a.StaffName.Contains(keyword.Trim()) || a.StaffGh.Contains(keyword.Trim())).ToList();
            }
            return Content(list.ToJson());
        }

    }
}
