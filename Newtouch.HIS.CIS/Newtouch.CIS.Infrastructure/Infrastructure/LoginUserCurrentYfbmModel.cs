using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// Đăng nhập Người dùng,hiện tại Khoa dượcthông tin
    /// </summary>
    public class LoginUserCurrentYfbmModel
    {
        /// <summary>
        /// Khoa dược Code
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// Khoa dượccấploại(1cấp Kho thuốc,2cấp Nhà thuốc)
        /// </summary>
        public string yfbmjb { get; set; }

        /// <summary>
        /// hiện tại Đăng nhậpthânphần của Khoa dược:0Kho thuốc;1Ngoại trú Nhà thuốc;2Nhập viện Nhà thuốc   Kho thuốctrực tiếpthông quayjbmjbkiểm trathìcó thểlấyđã
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// Cấp trên Kho thuốc Code List(Nhà thuốccó thểtới Mục tiêu Kho thuốcđăng kýlĩnh)
        /// </summary>
        public IList<string> TheUpperYfbmCodeList { get; set; }

        /// <summary>
        /// dướicấp Nhà thuốc Code List(Kho thuốccó thểtới Mục tiêu Nhà thuốcphát thuốc)
        /// </summary>
        public IList<string> TheLowerYfbmCodeList { get; set; }

        /// <summary>
        /// Tên khoa dược
        /// </summary>
        public string yfbmmc { get; set; }
    }
}
