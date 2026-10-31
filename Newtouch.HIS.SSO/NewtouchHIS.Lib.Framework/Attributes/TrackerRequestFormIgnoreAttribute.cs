namespace NewtouchHIS.Lib.Framework.Attributes
{
    /// <summary>
    /// tùy chỉnhAttribute nhật ký theo dõi truy cập nhưng không ghi lạiFormnội dung của(choactionthêm)
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TrackerRequestFormIgnoreAttribute : Attribute
    {

    }
}
