namespace NewtouchHIS.Lib.Base.Filter
{
    /// <summary>
    /// vô hiệu hóa đóng gói kết quả
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class FreeResultAttribute : Attribute
    {
    }
}
