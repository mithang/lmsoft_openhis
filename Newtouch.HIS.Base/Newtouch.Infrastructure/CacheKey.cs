namespace Newtouch.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class CacheKey
    {
        /// <summary>
        /// Ánh xạ code msg thông báo lỗi xử lý nghiệp vụ hệ thống {0}TopOrganizeId
        /// </summary>
        public static string SysFailedCodeMessageMapListListSetKey = "set:SysFailedCodeMessageMapListList_{0}";
        /// <summary>
        /// Từ điển liên kết người dùng thông báo tin nhắn {0} Mã cơ sở {1} AppId hệ thống hiện tại {2} Người dùng
        /// </summary>
        public static string NoticeConnectionKey = "MsgNotice:{0}:{1}_{2}";
    }
}
