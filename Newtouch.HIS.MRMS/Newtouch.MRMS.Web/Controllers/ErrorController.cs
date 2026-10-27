using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Init;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.MR.ManageSystem.Domain.IDomainServices;
using Newtouch.MR.ManageSystem.Domain.IRepository;
using Newtouch.MR.ManageSystem.Infrastructure;
using Newtouch.MRMS.Web.Models;

namespace Newtouch.MRMS.Web.Controllers
{
    /// <summary>
    /// Lỗi hệ thống Trải nghiệm thân thiện
    /// </summary>
    public class ErrorController : Controller
    {
        /// <summary>
        /// Gửi ngoại lệ Trang lỗi
        /// </summary>
        /// <returns></returns>
        public IActionResult HttpError()
        {
            return View("Error");
        }

        /// <summary>
        /// 404Trang nhắc nhở
        /// </summary>
        /// <returns></returns>
        public IActionResult NotFound()
        {
            return Content("Not Found");
            //return View();
        }

        /// <summary>
        /// 403Trang nhắc nhở
        /// </summary>
        /// <returns></returns>
        public IActionResult Forbidden()
        {
            return Content("Forbidden");
        }
    }
}