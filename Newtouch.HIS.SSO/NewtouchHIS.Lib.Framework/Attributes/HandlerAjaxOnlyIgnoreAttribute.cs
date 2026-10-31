namespace NewtouchHIS.Lib.Framework.Attributes
{
    /// <summary>
    /// tùy chỉnhAttribute không xác minh làAjaxyêu cầu(choactionthêm)
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class HandlerAjaxOnlyIgnoreAttribute : Attribute
    {

    }
}
