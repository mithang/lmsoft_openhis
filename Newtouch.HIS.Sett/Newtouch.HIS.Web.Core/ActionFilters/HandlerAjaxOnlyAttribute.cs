namespace System.Web.Mvc
{
    /// <summary>
    /// chỉ xử lýajax yêu cầu xác minh
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class HandlerAjaxOnlyAttribute : ActionMethodSelectorAttribute
    {
        public bool Ignore { get; set; }
        public HandlerAjaxOnlyAttribute(bool ignore = false)
        {
            Ignore = ignore;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public override bool IsValidForRequest(ControllerContext controllerContext, System.Reflection.MethodInfo methodInfo)
        {
            if (Ignore)
                return true;
            return controllerContext.RequestContext.HttpContext.Request.IsAjaxRequest();
        }
    }
}
