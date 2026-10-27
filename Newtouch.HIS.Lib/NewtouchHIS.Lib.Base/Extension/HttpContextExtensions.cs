using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System.Text;

namespace NewtouchHIS.Lib.Base.Extension
{
    public static class HttpContextExtensions
    {
        public static string GetCompleteUrl(this HttpRequest source)
        {
            return new StringBuilder()
                   .Append(source.Scheme)
                   .Append("://")
                   .Append(source.Host)
                   .Append(source.PathBase)
                   .Append(source.Path)
                   .Append(source.QueryString)
                   .ToString();
        }

        public static string GetRawUrl(this HttpRequest request)
        {
            var httpContext = request.HttpContext;
            var requestFeature = httpContext.Features.Get<IHttpRequestFeature>();
            return request.HttpContext.Request.Scheme + "://" + request.HttpContext.Request.Host + requestFeature?.RawTarget;
        }
        /// <summary>
        /// Kiểm tra tùy chỉnh đối với yêu cầu độc hại
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool RequestAppearsMalicious(this HttpRequest request)
        {
            return false;
        }
        /// <summary>
        /// Xác định yêu cầu có phải làAjax
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (!string.IsNullOrEmpty(request.Headers["X-Requested-With"]) &&
            request.Headers["X-Requested-With"].ToString().ToLower() == "xmlhttprequest")
            {
                return true;
            }
            return false;
        }
    }
}
