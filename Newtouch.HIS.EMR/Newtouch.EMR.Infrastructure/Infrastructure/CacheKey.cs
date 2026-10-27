namespace Newtouch.EMR.Infrastructure
{
    /// <summary>
    /// Bộ nhớ đệmKey
    /// </summary>
    public class CacheKey
    {
        /// <summary>
        /// Người dùng đăng nhập ủy quyền bên thứ batoken {0} Hệ thống hiện tạiAppId {1} Mã tổ chức {2} Người dùng
        /// </summary>
        public static string AccessToken = "${0}${1}:accesstoken_{2}";
    }
}
