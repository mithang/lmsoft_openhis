namespace Newtouch.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class CacheKey
    {
        /// <summary>
        /// Xử lý nghiệp vụ hệ thống Thông báo lỗi của code msg Ánh xạ {0}TopOrganizeId
        /// </summary>
        public static string SysFailedCodeMessageMapListListSetKey = "set:SysFailedCodeMessageMapListList_{0}";
        /// <summary>
        /// Từ điển liên kết người dùng thông báo tin nhắn {0} Mã tổ chức {1} Hệ thống hiện tạiAppId {2} Người dùng
        /// </summary>
        public static string NoticeConnectionKey = "MsgNotice:{0}:{1}_{2}";
    }
}
