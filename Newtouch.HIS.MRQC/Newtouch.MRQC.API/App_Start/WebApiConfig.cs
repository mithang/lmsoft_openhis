using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Newtouch.MRQC.API.App_Start
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
            //bật chia sẻ tài nguyên khác miền
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // WebAPI routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //trả về mặc định json
            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings.Add(
            //  new QueryStringMapping("datatype", "json", "application/json")
            //  );
            config.Formatters.JsonFormatter.SupportedMediaTypes.Clear();
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));

            //đăng ký toàn cụcFilters

            //tiêm phụ thuộc
            DependencyResolver.RegisteWebApiComponent();

        }

    }

}