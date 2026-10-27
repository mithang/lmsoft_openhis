using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Framework.Web.Controllers;
using Newtouch.OR.ManageSystem.Infrastructure;

namespace Newtouch.OR.ManageSystem.Web.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    public class CommonController : OrgControllerBase
    {
        // GET: SystemManage/Common
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult GetNewFieldUniqueValueDB(string fieldName)
        {
            var data = EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValueDB("[NewtouchHIS_Sett].dbo.", fieldName, this.OrganizeId,"" , true);
            return Success("", data); ;
        }
    }
}