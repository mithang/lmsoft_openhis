using Microsoft.AspNetCore.Http;
using NewtouchHIS.Lib.Base.Extension;

namespace NewtouchHIS.Lib.Base.Middleware
{
    public class HttpHandleMiddleware
    {
        private readonly RequestDelegate _next;

        public HttpHandleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // IMessageWriter is injected into InvokeAsync
        public async Task InvokeAsync(HttpContext httpContext)
        {
            //Chặn yêu cầu độc hại
            if (httpContext.Request.RequestAppearsMalicious())
            {
                // Malicious requests don't even deserve an error response (e.g. 400).
                httpContext.Abort();
                return;
            }

            await _next(httpContext);
        }
    }


}
