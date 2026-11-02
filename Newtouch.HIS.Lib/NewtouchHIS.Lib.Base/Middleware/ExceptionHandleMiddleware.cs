using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Extensions.Logging;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using System.Text.RegularExpressions;

namespace NewtouchHIS.Lib.Base.Middleware
{
    /// <summary>
    /// Middleware xử lý ngoại lệ
    /// </summary>
    public class ExceptionHandleMiddleware
    {
        private readonly ILogger<ExceptionHandleMiddleware> _logger;
        private readonly RequestDelegate next;
        private IHostingEnvironment environment;

        public ExceptionHandleMiddleware(RequestDelegate next, IHostingEnvironment environment, ILogger<ExceptionHandleMiddleware> logger)
        {
            _logger = logger;
            this.next = next;
            this.environment = environment;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
                var features = context.Features;
            }
            catch (Exception e)
            {
                await HandleException(context, e);
            }
        }

        // SECURITY (C91): khong ghi session token / gia tri nhay cam trong query string vao log.
        // Query tuan tu hoa thanh danh sach Key/Value (khong phai object phang) nen regex dang JSON
        // cua G7 khong bat duoc — redact theo key truoc khi tuan tu hoa, roi ap regex G7 them mot lop.
        private static string ScrubbedQuery(IQueryCollection query)
        {
            var items = new List<KeyValuePair<string, string[]>>(query.Count);
            foreach (var kv in query)
            {
                var values = kv.Value.ToArray();
                for (int i = 0; i < values.Length; i++)
                {
                    if (IsSensitiveQueryKey(kv.Key) || LooksLikeJwt(values[i]))
                    {
                        values[i] = "<redacted>";
                    }
                }
                items.Add(new KeyValuePair<string, string[]>(kv.Key, values));
            }
            return Scrub(items.ToJson());
        }

        private static bool IsSensitiveQueryKey(string key)
        {
            return key.IndexOf("token", StringComparison.OrdinalIgnoreCase) >= 0
                || key.IndexOf("password", StringComparison.OrdinalIgnoreCase) >= 0
                || key.IndexOf("pwd", StringComparison.OrdinalIgnoreCase) >= 0
                || key.IndexOf("secret", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static bool LooksLikeJwt(string? v)
        {
            return v != null && Regex.IsMatch(v, @"^eyJ[A-Za-z0-9_-]+\.[A-Za-z0-9_-]+\.[A-Za-z0-9_-]+$");
        }

        // Mau G7 (LogTrackingAttribute.Scrub): khong ghi JWT, gia tri Token, mat khau dang hash vao log.
        private static string Scrub(string? s)
        {
            if (string.IsNullOrEmpty(s)) return s ?? string.Empty;
            // JWT dang eyJ.xxx.yyy
            s = Regex.Replace(s, @"eyJ[A-Za-z0-9_-]+\.[A-Za-z0-9_-]+\.[A-Za-z0-9_-]+", "eyJ<jwt-redacted>");
            // Truong "Token": "..." (ke ca khi khong dung dang JWT)
            s = Regex.Replace(s, @"""[^""]*[Tt]oken[^""]*""\s*:\s*""[^""]*""", m => m.Value.Substring(0, m.Value.IndexOf(':') + 1) + @"""<redacted>""");
            // Truong mat khau dang hash trong request login
            s = Regex.Replace(s, @"""[^""]*[Pp]assword[^""]*""\s*:\s*""[^""]*""", m => m.Value.Substring(0, m.Value.IndexOf(':') + 1) + @"""<redacted>""");
            return s;
        }

        private async Task HandleException(HttpContext context, Exception e)
        {
            context.Response.StatusCode = 200;
            context.Response.ContentType = "text/json;charset=utf-8;";
            string error = "";
            string logError = $"PATH:{context.Request.Path}\nRequest:{ScrubbedQuery(context.Request.Query)}";
            _logger.LogError(logError);
            if (environment.IsDevelopment())
            {
                // QA phase 2 (M-05/O-01): khong bao gio tra stack trace ve client,
                // ke ca o Development — truoc day launcher Development lam ro
                // exStackTrace trong moi body JSON loi (vd view-not-found).
                // Chi tiet day du chi nam trong log server.
                //Thông tin lỗi
                switch (e)
                {
                    case FailedException ex:
                        error = new AjaxResult { state = ResultType.error.ToString(), message = ex.Msg }.ToJson();
                        if (ex.Log)
                        {
                            _logger.LogError(new AjaxResult { state = ResultType.error.ToString(), message = ex.Msg, exStackTrace = ex.Data.ToJson() }.ToJson());
                        }
                        break;
                    case RuntimeBinderException:
                        error = new AjaxResult { state = ResultType.error.ToString(), message = e.Message }.ToJson();
                        _logger.LogError(new AjaxResult { state = ResultType.error.ToString(), message = e.Message, exStackTrace = e.StackTrace, InnerException = e.InnerException }.ToJson());
                        break;
                    default:
                        error = new AjaxResult { state = ResultType.error.ToString(), message = e.Message }.ToJson();
                        _logger.LogError(new AjaxResult { state = ResultType.error.ToString(), message = e.Message, exStackTrace = e.StackTrace, InnerException = e.InnerException }.ToJson());
                        break;
                }
            }
            else
            {
                //Thông tin lỗi
                switch (e)
                {
                    case FailedException ex:
                        error = new AjaxResult { state = ResultType.error.ToString(), message = ex.Msg }.ToJson();
                        if (ex.Log)
                        {
                            _logger.LogError(error);
                        }
                        break;
                    default:
                        error = new AjaxResult { state = ResultType.error.ToString(), message = "Xin lỗi, hệ thống xử lý ngoại lệ! Vui lòng xem nhật ký hệ thống." }.ToJson();
                        _logger.LogError(new AjaxResult { state = ResultType.error.ToString(), message = e.Message, exStackTrace = e.StackTrace, InnerException = e.InnerException }.ToJson());
                        break;
                }
            }
            _logger.LogError(error);
            await context.Response.WriteAsync(error);
        }
    }
}
