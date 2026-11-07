namespace NewtouchHIS.Lib.Framework.Attributes
{
    /// <summary>
    /// tùy chỉnh Attribute không xác minh danh tính đăng nhập(choactionthêm)
    /// </summary>
    [AttributeUsage(AttributeTargets.Method|AttributeTargets.Class, AllowMultiple = false)]
    public class HandlerLoginAttribute : Attribute
    {

    }
}
