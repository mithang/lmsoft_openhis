using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Framework.Attributes;

namespace NewtouchHIS.Lib.Framework.Filter
{
    public class FrameworkAuthorizationFilter : IAuthorizationFilter
    {
        /// <summary>
        /// ứng dụngId（trongconfigcấu hình AppId）
        /// </summary>
        public static string _appId { get; private set; }

        /// <summary>
        /// hàm khởi tạo tĩnh
        /// </summary>
        static FrameworkAuthorizationFilter()
        {
            _appId = ConfigInitHelper.SysConfig.AppId ?? throw new FailedException("AppId chưa được định nghĩa");
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //Console.WriteLine($"{nameof(FrameworkAuthorizationFilter)}:{nameof(OnAuthorization)}");
            if (context.HttpContext.GetEndpoint()?.Metadata.GetMetadata<HandlerLoginAttribute>() != null)
            {
                return;
            }
            Authorization(context);
            return;
        }
        private void Authorization(AuthorizationFilterContext filtercontext)
        {
            //xác minh trạng thái đăng nhập
            HttpContext context = filtercontext.HttpContext;
            string? exception = null;
            OperatorProvider.WriteLoginUserCookie();
            var operatorModel = OperatorProvider.GetCurrent(ref exception, true);
            
            if (!string.IsNullOrWhiteSpace(exception))
            {
                if (exception == "SIDELINED")
                {
                    var loginErorKey = "Newtouch_login_error";
                    if (!string.IsNullOrWhiteSpace(_appId))
                    {
                        loginErorKey = _appId + "_" + loginErorKey;
                    }
                    HttpWebHelper.WriteCookie(loginErorKey, "sidelined");

                    if (context.Request.IsAjaxRequest())
                    {
                        //is ajax yêu cầu，thì trả về mã lỗi
                        filtercontext.Result = new ContentResult
                        {
                            Content = new AjaxResult
                            {
                                state = ResultType.error.ToString(),
                                code = "SESSION_SIDELINED",
                                message = "Đăng nhập hết hạn",
                            }.ToJson()
                        };
                    }

                    filtercontext.Result = new RedirectResult("/Login/Index");
                }
            }

            if (operatorModel == null)
            {
                var loginErorKey = "Newtouch_login_error";
                if (!string.IsNullOrWhiteSpace(_appId))
                {
                    loginErorKey = _appId + "_" + loginErorKey;
                }
                HttpWebHelper.WriteCookie(loginErorKey, "overdue");

                if (context.Request.IsAjaxRequest())
                {
                    //is ajax yêu cầu，thì trả về mã lỗi
                    filtercontext.Result = new ContentResult
                    {
                        Content = new AjaxResult
                        {
                            state = ResultType.error.ToString(),
                            code = "SESSION_TIMEOUT",
                            message = "Đăng nhập hết hạn",
                        }.ToJson()
                    };
                }
                else
                {
                    filtercontext.Result = new RedirectResult("/Login/Index");
                }
            }
            //đã đăng nhập
            context.Items["IDENTITY_ORGANIZEID"] = operatorModel?.OrganizeId ?? "";
        }
    }

}
