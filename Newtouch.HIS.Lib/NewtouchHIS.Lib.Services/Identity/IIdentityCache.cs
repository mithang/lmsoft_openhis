using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.Lib.Services
{
    /// <summary>
    /// APIthông tin danh tính người dùng lấy được qua ủy quyền giao diện-giao diện
    /// </summary>
    public interface IIdentityCache
    {
        UserIdentity identity { get; }

        /// <summary>
        /// đặt thông tin người dùng vào bộ nhớ đệm
        /// </summary>
        /// <param name="info">thông tin đăng nhập người dùng</param>
        /// <param name="channel">mặc định trống</param>
        void SetIdentityInfo(UserIdentity info, string appId);
    }

    /// <summary>
    /// cung cấp một lớp triển khai trống，sử dụng cụ thểIIdentityCachekhi，sẽ dùng lớp triển khai khác
    /// </summary>
    public class NullIdentityCache : IIdentityCache
    {
        /// <summary>
        /// thể hiện đơn lẻ
        /// </summary>
        public static NullIdentityCache Instance { get; } = new NullIdentityCache();

        public UserIdentity identity => null;

        /// thiết lập thông tin（giữ trống）
        /// </summary>
        public void SetIdentityInfo(UserIdentity info, string appId)
        {
        }
    }
}
