using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// Đăng nhậpNgười dùng,hiện tạiKhoa dượcthông tin
    /// </summary>
    public class LoginUserCurrentYfbmModel
    {
        /// <summary>
        /// Khoa dượcCode
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// Khoa dượccấploại(1cấp Kho thuốc,2cấpNhà thuốc)
        /// </summary>
        public string yfbmjb { get; set; }

        /// <summary>
        /// hiện tạiĐăng nhậpthânphần của Khoa dược:0Kho thuốc;1Ngoại trúNhà thuốc;2Nhập việnNhà thuốc   Kho thuốctrực tiếpthông quayjbmjbkiểm trathìcó thểlấyđã
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// Cấp trên Kho thuốc Code List(Nhà thuốccó thểtớiMục tiêuKho thuốcđăng kýlĩnh)
        /// </summary>
        public IList<string> TheUpperYfbmCodeList { get; set; }

        /// <summary>
        /// dướicấp Nhà thuốc Code List(Kho thuốccó thểtớiMục tiêuNhà thuốcphát thuốc)
        /// </summary>
        public IList<string> TheLowerYfbmCodeList { get; set; }

        /// <summary>
        /// Tên khoa dược
        /// </summary>
        public string yfbmmc { get; set; }
    }
}
