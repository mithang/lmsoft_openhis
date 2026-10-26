namespace Newtouch.Core.Common.Security
{
    /// <summary>
    /// thuộc tính không tham gia mã hóa、giải mã
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class EncryptDecryptIgnorePropertyAttribute : Attribute
    {

    }
}
