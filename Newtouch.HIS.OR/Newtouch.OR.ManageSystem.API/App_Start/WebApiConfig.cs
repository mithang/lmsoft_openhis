using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Newtouch.OR.ManageSystem.API.App_Start
{
    /// <summary>
    /// 
    /// </summary>
    public class WebApiConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            //Bật truy cập liên miền
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //Trả về mặc định json
            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings.Add(
            //  new QueryStringMapping("datatype", "json", "application/json")
            //  );
            config.Formatters.JsonFormatter.SupportedMediaTypes.Clear();
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));

            //Đăng ký toàn cụcFilters
            config.Filters.Add(new Newtouch.HIS.API.Common.Filter.TokenDecryptFilterAttribute());

            //Dependency Injection
            DependencyResolver.RegisteWebApiComponent();

        }

    }

}