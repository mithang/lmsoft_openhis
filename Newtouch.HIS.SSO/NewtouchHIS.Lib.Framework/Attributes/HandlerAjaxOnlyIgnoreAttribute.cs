namespace NewtouchHIS.Lib.Framework.Attributes
{
    /// <summary>
    /// tùy chỉnh Attribute không xác minh là Ajax yêu cầu(cho action thêm)
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class HandlerAjaxOnlyIgnoreAttribute : Attribute
    {

    }
}
