using Newtouch.Domain.IRepository;
using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.CIS.Compat;
using Newtouch.Application.Interface;
using Newtouch.Domain.Entity;
using Newtouch.CIS.Repository;
using Newtouch.Domain.IRepository;
using NewtouchHIS.Framework.Web.Controllers;
using Newtouch.Domain.IDomainServices;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

namespace Newtouch.CIS.Web.Areas.DoctorManage.Controllers
{
    
    [Area("DoctorManage")]
    
    public class ApplyController : OrgControllerBase
    {
        // GET: DoctorManage/Apply
        public IActionResult inspection()
        {
            return View();
        }
        public IActionResult Examination()
        {
            return View();
        }

        public IActionResult GetSqdhMethod() {
            var thisDate = DateTime.Now.ToString("yyyyMMdd");
            var data= EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue("zy_lsyz.sqdh", OrganizeId, "{0:D6}", true);
            return Success("", thisDate + data);
        }
    }
}