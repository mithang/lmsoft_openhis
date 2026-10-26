using System;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class CacheKey
    {
        /// <summary>
        /// Thông tin bộ phận nhà thuốc của người dùng đang đăng nhập  {0}UserId
        /// </summary>
        public static string CurrentYfbmInfoEntityKey = "string:CurrentYfbmInfo_{0}";

        /// <summary>
        /// Bộ nhớ đệm cây người dùng của tổ chức (phải tương ứng với nhân sự)   {0}OrganizeId
        /// </summary>
        public static string OrganizeUserTreeSetKey = "set:OrganizeUserTree_{0}";

        /// <summary>
        /// Bộ nhớ đệm cây nhân sự của tổ chức   {0}OrganizeId
        /// </summary>
        public static string OrganizeStaffTreeSetKey = "set:OrganizeStaffTree_{0}";

        /// <summary>
        /// Xử lý nghiệp vụ hệ thống, thông báo lỗi của code msg Ánh xạ {0}TopOrganizeId
        /// </summary>
        public static string SysFailedCodeMessageMapListListSetKey = "set:SysFailedCodeMessageMapListList_{0}";

    }
}
