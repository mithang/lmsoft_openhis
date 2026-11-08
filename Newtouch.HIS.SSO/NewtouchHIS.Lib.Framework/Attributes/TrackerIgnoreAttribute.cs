namespace NewtouchHIS.Lib.Framework.Attributes
{
    /// <summary>
    /// tùy chỉnh Attribute không ghi nhật ký theo dõi truy cập(cho action thêm)
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TrackerIgnoreAttribute : Attribute
    {

    }
}
