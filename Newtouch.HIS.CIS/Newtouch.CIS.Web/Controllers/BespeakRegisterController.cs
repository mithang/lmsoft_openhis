using NewtouchHIS.Framework.Web.Controllers;
using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.CIS.Compat;
using Newtouch.Application.Interface;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.CIS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

namespace Newtouch.CIS.Web.Controllers
{
    /// <summary>
    /// Ngoại trúđặt hẹn
    /// </summary>
    public class BespeakRegisterController : OrgControllerBase
    {
        // GET: BespeakRegister view
        public IActionResult BespeakRegister()
        {
            return View();
        }

        /// <summary>
        /// Hủyđặt hẹn
        /// </summary>
        /// <param name="yyId"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        [HttpPost]
        public IActionResult CancelYY(string yyId)
        {
            return Success();
        }
    }
}