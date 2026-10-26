namespace Newtouch.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class CacheKey
    {
        /// <summary>
        /// Hiệu lực của Phư ơng thức thanh toán tiền mặtdanh sách
        /// </summary>
        public static string ValidXTXJZFFSListSetKey = "set:ValidXTXJZFFSList";

        /// <summary>
        /// Hiệu lực của hệ thốngthu phí loại lớn {0}Organizeid
        /// </summary>
        public static string ValidSystemChargeMajorClassListSetKey = "set:ValidSystemChargeMajorClassList_{0}";

        /// <summary>
        /// Hệ thống nghiệp vụ xử lý lỗi nhắc nhở của code msg ánh xạ {0}TopOrganizeid
        /// </summary>
        public static string SysFailedCodeMessageMapListListSetKey = "set:SysFailedCodeMessageMapListList_{0}";

        /// <summary>
        /// Khi trư ớc đ ăng nhập của ngư ời dùng của nhà thuốc bộ phận thông tin {0}Userid
        /// </summary>
        public static string CurrentYfbmInfoEntityKey = "string:CurrentYfbmInfo_{0}";

        /// <summary>
        /// Tổ chức của ngư ời dùng cây(lấy đượcTư ơng ứng nhân sự) bộ nhớ đ ệm {0}Organizeid
        /// </summary>
        public static string OrganizeUserTreeSetKey = "set:OrganizeUserTree_{0}";

        /// <summary>
        /// Tổ chức của nhân sự cây bộ nhớ đ ệm {0}Organizeid
        /// </summary>
        public static string OrganizeStaffTreeSetKey = "set:OrganizeStaffTree_{0}";

    }
}
