namespace Newtouch.MRQC.Infrastructure
{
    /// <summary>
    /// bộ nhớ đệmKey
    /// </summary>
    public class CacheKey
    {
        /// <summary>
        /// người dùng đăng nhập ủy quyền bên thứ ba token {0} hệ thống hiện tạiAppId {1} mã cơ sở {2} người dùng
        /// </summary>
        public static string AccessToken = "${0}${1}:access token_{2}";
    }
}