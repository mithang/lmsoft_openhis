using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace NewtouchCIS.API.App_Start
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
            //Bật đa nguồn
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //Mặc định trả về jsons
            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings.Add(
            //  new QueryStringMapping("datatype", "json", "application/json")
            //  );
            config.Formatters.JsonFormatter.SupportedMediaTypes.Clear();
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));

            //Đăng ký Filters toàn cục
            config.Filters.Add(new Newtouch.HIS.API.Common.Filter.TokenDecryptFilterAttribute());

            //Tiêm phụ thuộc
            DependencyResolver.RegisteWebApiComponent();

            // Here: 
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
        }

    }

}