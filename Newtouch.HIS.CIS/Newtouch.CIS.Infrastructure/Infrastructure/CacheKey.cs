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
        /// hiện tạiĐăng nhậpcủaNgười dùngcủaKhoa dượcthông tin  {0}UserId
        /// </summary>
        public static string CurrentYfbmInfoEntityKey = "string:CurrentYfbmInfo_{0}";

        /// <summary>
        /// Người dùngđănglụcthứbaphươngủy quyềntoken {0} hiện tạiHệ thốngAppId {1} Cơ sởMã {2} Người dùng
        /// </summary>
        public static string AccessToken = "${0}${1}:accesstoken_{2}";
    }
}
