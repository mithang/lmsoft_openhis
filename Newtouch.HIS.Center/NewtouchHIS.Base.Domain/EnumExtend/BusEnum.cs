using System.ComponentModel;

namespace NewtouchHIS.Base.Domain.EnumExtend
{
    public class BusEnum
    {
        /// <summary>
        /// Loại nghiệp vụ đơn hàng
        /// </summary>
        public enum EnumBusType
        {
            /// <summary>
            /// Ngoại trú
            /// </summary>
            [Description("Ngoại trú")]
            mz = 1,
            /// <summary>
            /// Nội trú
            /// </summary>
            [Description("Nội trú")]
            zy = 2
        }

        public enum EnumReportStu
        {
            /// <summary>
            /// Đã đăng ký
            /// </summary>
            [Description("Đã đăng ký")]
            ysq = 0,
            /// <summary>
            /// Đã tiếp nhận
            /// </summary>
            [Description("Đã tiếp nhận")]
            yjs = 1,
            /// <summary>
            /// Đã hoàn thành(Báo cáo đã có)
            /// </summary>
            [Description("Đã hoàn thành")]
            ywc = 2
        }
    }
}
