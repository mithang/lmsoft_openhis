using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Domain.Enum
{
    /// <summary>
    /// HISEnum nội bộ
    /// </summary>
    public class HisEnum
    {
        /// <summary>
        /// Cờ loại bệnh nhân
        /// </summary>
        public enum PatTypeEnum
        {
            [Description("Đang nằm viện")]
            zy = 1,
            [Description("Xuất viện")]
            cy = 2,
            [Description("Ngoại trú")]
            mz = 3,

        }
        /// <summary>
        /// Cờ đang nằm viện
        /// </summary>
        public enum EnumZYBZ
        {
            /// <summary>
            /// Đăng ký nhập viện
            /// </summary>
            Xry = 0,
            /// <summary>
            /// Đang ở khu bệnh
            /// </summary>
            Bqz = 1,
            /// <summary>
            /// Xuất khỏi khu bệnh(Ra khỏi khu bệnh)(Chờ quyết toán)
            /// </summary>
            Djz = 2,
            /// <summary>
            /// Đã xuất viện(Quyết toán xuất viện)
            /// </summary>
            Ycy = 3,
            /// <summary>
            /// Chuyển khu
            /// </summary>
            Zq = 7,

            /// <summary>
            /// Hồ sơ hủy/Hủy nhập viện
            /// </summary>
            Wry = 9,
        }

        public enum EnumYzzt
        {
            /// <summary>
            /// Chờ duyệt 
            /// </summary>
            [Description("Chờ duyệt")]
            Ds = 0,
            /// <summary>
            /// Duyệt
            /// </summary>
            [Description("Duyệt")]
            Sh = 1,
            /// <summary>
            /// Thực hiện
            /// </summary>
            [Description("Thực hiện")]
            Zx = 2,
            /// <summary>
            /// RútDC
            /// </summary>
            [Description("DC")]
            DC = 3,
            /// <summary>
            /// Dừng hoặc hủy
            /// </summary>
            [Description("Dừng")]
            TZ = 4
        }

        /// <summary>
        /// Tính chất bệnh nhân
        /// </summary>
        public enum EnumBrxz
        {
            /// <summary>
            /// Tự chi trả
            /// </summary>
            [Description("Tự chi trả")]
            zf = 0,
            /// <summary>
            /// BHYT công nhân viên chức
            /// </summary>
            [Description("BHYT công nhân viên chức")]
            zg = 1,
            /// <summary>
            /// BHYT cư dân
            /// </summary>
            [Description("BHYT cư dân")]
            jm = 2,
            /// <summary>
            /// Nghỉ hưu theo chế độ
            /// </summary>
            [Description("Nghỉ hưu theo chế độ")]
            lx = 3,
            /// <summary>
            /// BHYT thông thường
            /// </summary>
            [Description("BHYT thông thường")]
            pt = 11,
        }

    }
}
