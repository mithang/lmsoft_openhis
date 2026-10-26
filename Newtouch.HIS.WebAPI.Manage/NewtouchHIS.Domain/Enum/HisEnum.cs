using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Domain.Enum
{
    /// <summary>
    /// HISenum nội bộ
    /// </summary>
    public class HisEnum
    {
        /// <summary>
        /// cờ loại bệnh nhân
        /// </summary>
        public enum PatTypeEnum
        {
            [Description("在院")]
            zy = 1,
            [Description("出院")]
            cy = 2,
            [Description("门诊")]
            mz = 3,

        }
        /// <summary>
        /// cờ đang nằm viện
        /// </summary>
        public enum EnumZYBZ
        {
            /// <summary>
            /// đăng ký nhập viện
            /// </summary>
            Xry = 0,
            /// <summary>
            /// trong khu bệnh
            /// </summary>
            Bqz = 1,
            /// <summary>
            /// xuất viện khỏi khu bệnh（rời khu bệnh）（chờ thanh toán）
            /// </summary>
            Djz = 2,
            /// <summary>
            /// đã xuất viện（thanh toán xuất viện）
            /// </summary>
            Ycy = 3,
            /// <summary>
            /// chuyển khu
            /// </summary>
            Zq = 7,

            /// <summary>
            /// bản ghi hủy/hủy nhập viện
            /// </summary>
            Wry = 9,
        }

        public enum EnumYzzt
        {
            /// <summary>
            /// chờ duyệt 
            /// </summary>
            [Description("待审")]
            Ds = 0,
            /// <summary>
            /// duyệt
            /// </summary>
            [Description("审核")]
            Sh = 1,
            /// <summary>
            /// thực hiện
            /// </summary>
            [Description("执行")]
            Zx = 2,
            /// <summary>
            /// thu hồiDC
            /// </summary>
            [Description("DC")]
            DC = 3,
            /// <summary>
            /// dừng hoặc hủy
            /// </summary>
            [Description("停止")]
            TZ = 4
        }

        /// <summary>
        /// tính chất bệnh nhân
        /// </summary>
        public enum EnumBrxz
        {
            /// <summary>
            /// tự trả
            /// </summary>
            [Description("自费")]
            zf = 0,
            /// <summary>
            /// bảo hiểm y tế nhân viên
            /// </summary>
            [Description("职工医保")]
            zg = 1,
            /// <summary>
            /// bảo hiểm y tế cư dân
            /// </summary>
            [Description("居民医保")]
            jm = 2,
            /// <summary>
            /// nghỉ hưu
            /// </summary>
            [Description("离休")]
            lx = 3,
            /// <summary>
            /// bảo hiểm y tế thông thường
            /// </summary>
            [Description("普通医保")]
            pt = 11,
        }

    }
}
