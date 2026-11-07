namespace NewtouchHIS.Lib.Framework.Attributes
{
    /// <summary>
    /// tùy chỉnh Attribute không xác minh là Ajaxyêu cầu(choactionthêm)
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class HandlerAjaxOnlyIgnoreAttribute : Attribute
    {

    }
}
