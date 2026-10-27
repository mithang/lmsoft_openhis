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

namespace Newtouch.Sett.Web.Areas.ExampleManage.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [Area("ExampleManage")]

    public class SendMailController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        [HttpPost]

        //[ValidateAntiForgeryToken]
        
        public IActionResult SendMail(string account, string title, string content)
        {
            //MailHelper mail = new MailHelper();
            //mail.MailServer = ConfigurationHelper.GetAppConfigValue("MailHost");
            //mail.MailUserName = ConfigurationHelper.GetAppConfigValue("MailUserName");
            //mail.MailPassword = ConfigurationHelper.GetAppConfigValue("MailPassword");
            //mail.MailName = "";
            //mail.Send(account, title, content);
            return Success("Gửi thành công。");
        }
    }
}
