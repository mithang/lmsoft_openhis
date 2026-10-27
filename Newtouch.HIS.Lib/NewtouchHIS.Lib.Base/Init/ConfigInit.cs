using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.Lib.Base
{
    public static class ConfigInitHelper
    {
        /// <summary>
        /// Cấu hình cơ sở dữ liệu
        /// </summary>
        public static BusinessDB DbConfig { get; set; }
        /// <summary>
        /// Cấu hình hệ thống nghiệp vụ
        /// </summary>
        public static BusinessConfig SysConfig { get; set; }
        /// <summary>
        /// Thiết lập liên miền
        /// </summary>
        public static AccessConfig? AccessSetting { get; set; }

        
    }

    public static class CacheConfig
    {
        /// <summary>
        /// Bộ nhớ đệm người dùng - định danh token
        /// </summary>
        public static string Cache_UserToken => "HisToken:User_";

    }
}
