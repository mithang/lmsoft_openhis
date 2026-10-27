using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.Domain;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using System;
using System.Collections.Generic;
using System.Linq;






namespace Newtouch.Sett.Web.Areas.OutpatientManage.Controllers
{
    [Area("OutpatientManage")]

    public class QueryForSettleController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        // GET: OutpatientManage/QueryForJs
        public IActionResult QueryForJs()
        {
            return View();
        }

        public IActionResult DenyReg()
        {
            return View();
        }
    }
}
