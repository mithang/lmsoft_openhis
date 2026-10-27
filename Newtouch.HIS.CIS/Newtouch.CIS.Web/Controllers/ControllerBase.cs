using Microsoft.AspNetCore.Mvc;
using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Framework.Attributes;

namespace Newtouch.CIS.Web
{
    /// <summary>
    /// CIS local controller base, ported to ASP.NET Core.
    /// UserIdentity / OrganizeId / Success / Error / GetAuthOrganizeId come from
    /// the vendor net6.0 Framework.Web bases (OrgControllerBase -> BaseController).
    /// The original Framework local base also provided default Index/Form/Details
    /// actions returning their views; those are preserved here.
    /// </summary>
    [HandlerLogin]
    public abstract class ControllerBase : OrgControllerBase
    {
        // NOTE: Lib BaseController already declares virtual Index()/Form().
        // These MUST be 'override' (not 'virtual' hiding): hiding creates a
        // second action descriptor for the same route and ASP.NET Core throws
        // AmbiguousMatchException on every inherited Index/Form URL
        // (proven 2026-10-26: /ClinicManage/ClinicMetting/Index,
        // /InfusionManage/Index, /InfusionManage/Form all 500-ambiguous).
        [HttpGet]
        public override IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public override IActionResult Form()
        {
            return View();
        }

        [HttpGet]
        /// <summary>Port of MVC5 Server.MapPath: ~/... -&gt; wwwroot/...</summary>
        protected string MapPath(string virtualPath)
        {
            var rel = virtualPath.TrimStart('~', '/', '\\').Replace('/', System.IO.Path.DirectorySeparatorChar);
            return System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot", rel);
        }

        public virtual IActionResult Details()
        {
            return View();
        }
    }
}
