using FrameworkBase.MultiOrg.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.CIS.Web.Controllers
{
    /// <summary>
    /// Hẹn khám ngoại trú
    /// </summary>
    public class BespeakRegisterController : OrgControllerBase
    {
        // GET: BespeakRegister view
        public ActionResult BespeakRegister()
        {
            return View();
        }

        /// <summary>
        /// Hủy lịch hẹn
        /// </summary>
        /// <param name="yyId"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        [HttpPost]
        public ActionResult CancelYY(string yyId)
        {
            return Success();
        }
    }
}