using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using NewtouchHIS.Lib.Base.Extension;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace NewtouchHIS.Lib.Base.Filter
{
    /// <summary>
    /// Theo dõi nhật ký
    /// </summary>
    public class LogTrackingAttribute : Attribute, IActionFilter, IExceptionFilter
    {
        //private readonly ILoggerHelper _logger;
        private readonly ILogger<AppResultFilter> _logger;
        public LogTrackingAttribute(ILogger<AppResultFilter> logger)
        {
            _logger = logger;

        }
        private object _request { get; set; } = new object();
        // SECURITY (G7): khong ghi JWT, gia tri Token, mat khau dang hash vao log.
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


        private readonly Stopwatch logwatch = new Stopwatch();
        private bool enableLoggingMonitor = ConfigInitHelper.SysConfig?.EnableLoggingMonitorAttr ?? false;
        private bool logWithHeader = ConfigInitHelper.SysConfig?.EnableLoggingMonitorWithHeaderAttr ?? false;
        public void OnActionExecuting(ActionExecutingContext context)
        {
            logwatch.Start();//Bắt đầu
            //Lấy tham số yêu cầu
            _request = context.ActionArguments;
            if (enableLoggingMonitor && logWithHeader)
            {
                _logger.LogInformation($"[Theo dõi nhật ký] OnActionExecuting :\n[Request]\n[Request.Url]: {context.HttpContext.Request.Path.ToString()}\n[Request.Method]:{context.HttpContext.Request.Method}\n[Request.Headers]:{Scrub(context.HttpContext.Request.Headers.ToDictionary(x => x.Key, v => string.Join(";", v.Value.ToList())).ToJson())}\n[Request.ExecuteStartTime]:{DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}\n[Request.Data]:{Scrub(_request.ToJson())}");
            }
            else if (enableLoggingMonitor)
            {
                _logger.LogInformation($"[Theo dõi nhật ký] OnActionExecuting :\n[Request]\n[Request.Url]: {context.HttpContext.Request.Path.ToString()}\n[Request.Method]:{context.HttpContext.Request.Method}\n[Request.ExecuteStartTime]:{DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}\n[Request.Data]:{Scrub(_request.ToJson())}");
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            logwatch.Stop();
            if (enableLoggingMonitor)
            {
                string result;
                var resultObj = (context.Result is ObjectResult) ? (context.Result as ObjectResult) : null;
                if (resultObj != null)
                {
                    result = resultObj.Value!.ToJson();
                }
                else
                {
                    result = context.Result!.ToJson();
                }
                _logger.LogInformation($"[Theo dõi nhật ký] OnActionExecuted :\n[Request.Url]: {context.HttpContext.Request.Path.ToString()}\n[Request.Data]:{Scrub(_request.ToJson())}\n" +
                    $"[Response]\n[Response.executeEndTime]:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}\n[Response.executeTime]:{logwatch.ElapsedMilliseconds}\n[Response.Result]:{Scrub(result)}");
            }
        }


        public void OnException(ExceptionContext context)
        {
            _logger.LogError($"[Theo dõi nhật ký] OnException :\n[Request.Url]: {context.HttpContext.Request.Path.ToString()}\n[Request.Data]:{Scrub(_request.ToJson())}\n[Response]\n[Response.executeEndTime]:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}\n[Response.executeTime]:{logwatch.ElapsedMilliseconds}\n[Response.Exception]:{Scrub(context.Exception.ToJson())}");
        }

    }
}
