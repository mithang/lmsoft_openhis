using System;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class CacheKey
    {
        /// <summary>
        /// Thông tin bộ phận nhà thuốc của người dùng đăng nhập hiện tại  {0}UserId
        /// </summary>
        public static string AccessToken = "AccessToken_{0}_{1}_{2}";
        public static string CurrentYfbmInfoEntityKey = "string:CurrentYfbmInfo_{0}";

        /// <summary>
        /// Cây người dùng tổ chức(được nhân sự tương ứng)Đệm   {0}OrganizeId
        /// </summary>
        public static string OrganizeUserTreeSetKey = "set:OrganizeUserTree_{0}";

        /// <summary>
        /// Đệm cây nhân sự tổ chức   {0}OrganizeId
        /// </summary>
        public static string OrganizeStaffTreeSetKey = "set:OrganizeStaffTree_{0}";

        /// <summary>
        /// Xử lý nghiệp vụ hệ thống Thông báo lỗi của code msg Ánh xạ {0}TopOrganizeId
        /// </summary>
        public static string SysFailedCodeMessageMapListListSetKey = "set:SysFailedCodeMessageMapListList_{0}";

    }
}
