namespace Newtouch.EMR.Infrastructure
{
    /// <summary>
    /// Key đệm
    /// </summary>
    public class CacheKey
    {
        /// <summary>
        /// Token ủy quyền bên thứ ba khi người dùng đăng nhập {0}, AppId hệ thống hiện tại {1}, mã tổ chức {2}, người dùng
        /// </summary>
        public static string AccessToken = "${0}${1}:accesstoken_{2}";
    }
}