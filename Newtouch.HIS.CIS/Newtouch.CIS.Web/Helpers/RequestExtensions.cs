using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Newtouch.CIS.Web.Helpers
{
    public static class RequestExtensions
    {
        /// <summary>Port of MVC5 HttpRequest.Params: query first, then form.</summary>
        public static string Param(this HttpRequest request, string key)
        {
            if (request.Query.TryGetValue(key, out StringValues q)) return q.ToString();
            if (request.HasFormContentType && request.Form.TryGetValue(key, out StringValues f)) return f.ToString();
            return null;
        }
    }
}
