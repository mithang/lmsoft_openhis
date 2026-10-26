using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.Lib.Base
{
    public static class ConfigInitHelper
    {
        /// <summary>
        /// cấu hình cơ sở dữ liệu
        /// </summary>
        public static BusinessDB DbConfig { get; set; }
        /// <summary>
        /// cấu hình hệ thống nghiệp vụ
        /// </summary>
        public static BusinessConfig SysConfig { get; set; }
        /// <summary>
        /// thiết lập đa miền
        /// </summary>
        public static AccessConfig? AccessSetting { get; set; }

        
    }

    public static class CacheConfig
    {
        /// <summary>
        /// bộ nhớ đệm người dùng-tokenđánh dấu
        /// </summary>
        public static string Cache_UserToken => "HisToken:User_";

    }
}
