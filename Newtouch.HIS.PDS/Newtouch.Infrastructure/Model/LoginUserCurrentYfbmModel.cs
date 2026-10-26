using System.Collections.Generic;

namespace Newtouch.Infrastructure.Model
{
    /// <summary>
    /// Người dùng đăng nhập, thông tin bộ phận nhà thuốc hiện tại
    /// </summary>
    public class LoginUserCurrentYfbmModel
    {
        /// <summary>
        /// Bộ phận nhà thuốcCode
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// Cấp bậc bộ phận nhà thuốc（1Kho thuốc cấp，2Nhà thuốc cấp）
        /// </summary>
        public string yfbmjb { get; set; }

        /// <summary>
        /// Bộ phận nhà thuốc của danh tính đang đăng nhập：0Kho thuốc；1Nhà thuốc ngoại trú；2Nhà thuốc nội trú, kho thuốc chuyển trực tiếpyjbmjbChỉ cần phán đoán là được
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// Kho thuốc cấp trên Code List（Nhà thuốc có thể lĩnh thuốc từ kho thuốc đích）
        /// </summary>
        public IList<string> TheUpperYfbmCodeList { get; set; }

        /// <summary>
        /// Nhà thuốc cấp dưới Code List（Kho thuốc có thể phát thuốc cho nhà thuốc đích）
        /// </summary>
        public IList<string> TheLowerYfbmCodeList { get; set; }

        /// <summary>
        /// Tên bộ phận nhà thuốc
        /// </summary>
        public string yfbmmc { get; set; }
    }
}
