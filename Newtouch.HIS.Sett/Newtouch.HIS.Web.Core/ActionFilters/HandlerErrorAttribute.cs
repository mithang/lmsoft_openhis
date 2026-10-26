using Newtouch.Common;
using Newtouch.Common.Exceptions;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Core.ActionFilters
{
    /// <summary>
    /// Bắt bất thường toàn cục 
    /// </summary>
    public class HandlerErrorAttribute : HandleErrorAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            if (!(context.Exception is FailedException))
            {
                // bản ghi nhật ký lỗi
                var logger = AppLogger.Instance;
                if (context != null && context.Exception != null && logger != null)
                {
                    AppLogger.Instance.Error(context.Exception.Message, context.Exception);
                }
            }
            //Nếu không là nhắc nhở nghiệp vụ, không ghi nhật ký 

            if (context.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                context.ExceptionHandled = true;
                context.HttpContext.Response.StatusCode = 200;
                context.Result = new ContentResult
                {
                    Content = new AjaxResult
                    {
                        state = ResultType.error.ToString(),
                        code = (context.Exception is FailedException)
                            ? (((FailedException)context.Exception).Code ?? "").ToUpper() : "SYSTEM_ERROR",
                        message = (context.Exception is FailedException) ? ((FailedException)context.Exception).Msg : ""
                    }
                .ToJson()
                };
            }
            else
            {
                base.OnException(context);
            }
        }

    }
}
