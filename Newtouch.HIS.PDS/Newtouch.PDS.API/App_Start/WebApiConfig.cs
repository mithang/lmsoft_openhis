using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtouch.HIS.API.Common.Filter;

namespace Newtouch.PDS.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Bật chia sẻ tài nguyên chéo miền
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
            config.Filters.Add(new TokenDecryptFilterAttribute());
            
            //Tiêm phụ thuộc
            App_Start.DependencyResolver.RegisteWebApiComponent();
        }
    }
}
