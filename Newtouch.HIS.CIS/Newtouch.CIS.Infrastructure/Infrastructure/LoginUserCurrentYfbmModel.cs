using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// Đăng nhập Người dùng,hiện tại Khoa dược thông tin
    /// </summary>
    public class LoginUserCurrentYfbmModel
    {
        /// <summary>
        /// Khoa dược Code
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// Khoa dược cấp loại(1cấp Kho thuốc,2cấp Nhà thuốc)
        /// </summary>
        public string yfbmjb { get; set; }

        /// <summary>
        /// hiện tại Đăng nhập thân phần của Khoa dược:0Kho thuốc;1Ngoại trú Nhà thuốc;2Nhập viện Nhà thuốc   Kho thuốc trực tiếp thông qua yjbmjb kiểm tra thì có thể lấy đã
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// Cấp trên Kho thuốc Code List(Nhà thuốc có thể tới Mục tiêu Kho thuốc đăng ký lĩnh)
        /// </summary>
        public IList<string> TheUpperYfbmCodeList { get; set; }

        /// <summary>
        /// dưới cấp Nhà thuốc Code List(Kho thuốc có thể tới Mục tiêu Nhà thuốc phát thuốc)
        /// </summary>
        public IList<string> TheLowerYfbmCodeList { get; set; }

        /// <summary>
        /// Tên khoa dược
        /// </summary>
        public string yfbmmc { get; set; }
    }
}
