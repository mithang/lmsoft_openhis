using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.Lib.Services
{
    /// <summary>
    /// APIThông tin danh tính người dùng do giao diện lấy qua ủy quyền - giao diện
    /// </summary>
    public interface IIdentityCache
    {
        UserIdentity identity { get; }

        /// <summary>
        /// Đặt thông tin người dùng vào bộ nhớ đệm
        /// </summary>
        /// <param name="info">Thông tin đăng nhập người dùng</param>
        /// <param name="channel">Mặc định trống</param>
        void SetIdentityInfo(UserIdentity info, string appId);
    }

    /// <summary>
    /// Cung cấp một lớp triển khai trống, khi sử dụng IIdentityCache cụ thể sẽ dùng lớp triển khai khác
    /// </summary>
    public class NullIdentityCache : IIdentityCache
    {
        /// <summary>
        /// Thể hiện đơn lệ
        /// </summary>
        public static NullIdentityCache Instance { get; } = new NullIdentityCache();

        public UserIdentity identity => null;

        /// Thông tin thiết lập (giữ trống)
        /// </summary>
        public void SetIdentityInfo(UserIdentity info, string appId)
        {
        }
    }
}
