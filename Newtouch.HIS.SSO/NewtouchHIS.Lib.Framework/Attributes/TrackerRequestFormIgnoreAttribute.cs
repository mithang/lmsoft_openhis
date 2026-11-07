namespace NewtouchHIS.Lib.Framework.Attributes
{
    /// <summary>
    /// tùy chỉnh Attribute nhật ký theo dõi truy cập nhưng không ghi lại Formnội dung của(choactionthêm)
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TrackerRequestFormIgnoreAttribute : Attribute
    {

    }
}
