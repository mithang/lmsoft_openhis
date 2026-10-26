using System.Web.Mvc;
using System.Web.Routing;

namespace Newtouch.OR.ManageSystem.Web
{
    /// <summary>
    /// Đăng ký định tuyến
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// Đăng ký định tuyến
        /// </summary>
        /// <param name="routes"></param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );
        }
    }

}