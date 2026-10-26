using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// Người dùng đăng nhập, thông tin khoa nhà thuốc hiện tại
    /// </summary>
    public class LoginUserCurrentYfbmModel
    {
        /// <summary>
        /// Code khoa nhà thuốc
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// Cấp khoa nhà thuốc (cấp 1 kho thuốc, cấp 2 nhà thuốc)
        /// </summary>
        public string yfbmjb { get; set; }

        /// <summary>
        /// Khoa nhà thuốc của danh tính đăng nhập hiện tại: 0 kho thuốc; 1 nhà thuốc ngoại trú; 2 nhà thuốc nội trú. Kho thuốc có thể phán đoán trực tiếp qua yjbmjb
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// Code kho thuốc cấp trên (nhà thuốc có thể xin lĩnh từ kho thuốc mục tiêu)
        /// </summary>
        public IList<string> TheUpperYfbmCodeList { get; set; }

        /// <summary>
        /// Code nhà thuốc cấp dưới (kho thuốc có thể phát thuốc cho nhà thuốc mục tiêu)
        /// </summary>
        public IList<string> TheLowerYfbmCodeList { get; set; }

        /// <summary>
        /// Tên khoa nhà thuốc
        /// </summary>
        public string yfbmmc { get; set; }
    }
}
