using System.Collections.Generic;

namespace Newtouch.Infrastructure.Model
{
    /// <summary>
    /// Người dùng đăng nhập，Thông tin bộ phận nhà thuốc hiện tại
    /// </summary>
    public class LoginUserCurrentYfbmModel
    {
        /// <summary>
        /// Bộ phận nhà thuốcCode
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// Cấp bộ phận nhà thuốc（1cấp Kho thuốc，2Nhà thuốc cấp）
        /// </summary>
        public string yfbmjb { get; set; }

        /// <summary>
        /// Danh tính đăng nhập hiện tại của Bộ phận nhà thuốc：0Kho thuốc；1Nhà thuốc ngoại trú；2Nhà thuốc nội trú   Kho thuốc trực tiếp thông quayjbmjbKiểm tra là được
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// Cấp trên Kho thuốc Code List（Nhà thuốc có thể lĩnh từ kho thuốc đích）
        /// </summary>
        public IList<string> TheUpperYfbmCodeList { get; set; }

        /// <summary>
        /// Cấp dưới Nhà thuốc Code List（Kho thuốc có thể phát thuốc cho nhà thuốc đích）
        /// </summary>
        public IList<string> TheLowerYfbmCodeList { get; set; }

        /// <summary>
        /// Tên bộ phận nhà thuốc
        /// </summary>
        public string yfbmmc { get; set; }
    }
}
