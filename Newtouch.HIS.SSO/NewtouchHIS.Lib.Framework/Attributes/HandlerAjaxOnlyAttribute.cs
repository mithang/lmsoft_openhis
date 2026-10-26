using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Routing;
using NewtouchHIS.Lib.Framework.Attributes;

namespace NewtouchHIS.Lib.Framework.Filter
{
    /// <summary>
    /// bộ lọc chỉ xử lýAjaxxác minh yêu cầu
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class HandlerAjaxOnlyAttribute : ActionMethodSelectorAttribute
    {
        /// <summary>
        /// hàm khởi tạo
        /// </summary>
        public HandlerAjaxOnlyAttribute()
        {

        }

        public override bool IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
        {
            if (action.EndpointMetadata.Any(p => p is HandlerAjaxOnlyAttribute))
            {
                return true;
            }

            return false;
        }
    }
}
