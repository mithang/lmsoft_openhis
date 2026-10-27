namespace NewtouchHIS.Lib.Framework.Attributes
{
    /// <summary>
    /// tùy chỉnhAttribute không ghi nhật ký theo dõi truy cập（choactionthêm）
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TrackerIgnoreAttribute : Attribute
    {

    }
}
