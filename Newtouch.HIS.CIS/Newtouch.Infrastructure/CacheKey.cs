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
        /// Thông tin khoa nhà thuốc của người dùng đang đăng nhập {0}UserId
        /// </summary>
        public static string CurrentYfbmInfoEntityKey = "string:CurrentYfbmInfo_{0}";

        /// <summary>
        /// Token ủy quyền bên thứ ba khi người dùng đăng nhập {0} AppId hệ thống hiện tại {1} Mã tổ chức {2} Người dùng
        /// </summary>
        public static string AccessToken = "${0}${1}:accesstoken_{2}";
    }
}
