namespace Newtouch.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class CacheKey
    {
        public static string ValidSystemDiagnosisListSetKey = "set:VDList_{0}";
        public static string ValidSysMedicalOrderFrequencyListSetKey = "set:VMOFList_{0}";
        public static string ValidSysMedicineUsageListSetKey = "set:VMUList";
        public static string ValidSysDaiJianFeeSetKey = "set:VDFSetKey_{0}";
        public static string ValidSysBodyPartsSetKey_ = "set:VBodyPSetKey_{0}";
        public static string ValidSysAuxiliaryDictionaryListSetKey = "set:VADList_{0}";
        public static string ValidSystemBwFlSetKey = "set:VBodyDlSetKey_{0}";

        /// <summary>
        /// hiện tại Đăng nhập của Người dùng của Khoa dược thông tin  {0}UserId
        /// </summary>
        public static string CurrentYfbmInfoEntityKey = "string:CurrentYfbmInfo_{0}";

        /// <summary>
        /// Người dùng đăng lục thứ ba phương ủy quyền token {0} hiện tại Hệ thống AppId {1} Cơ sở Mã {2} Người dùng
        /// </summary>
        public static string AccessToken = "${0}${1}:accesstoken_{2}";
    }
}
