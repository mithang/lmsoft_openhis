using System.ComponentModel;

namespace NewtouchHIS.Base.Domain.EnumExtend
{
    public class OrderEnum
    {
        /// <summary>
        /// Trạng thái đơn hàng0:Chờ thanh toán 1:Đang thanh toán  2:Đã thanh toán 3:Đã hoàn tiền 4:Đã hủy
        /// </summary>
        public enum EnumOrderStatus
        {
            /// <summary>
            /// Chờ thanh toán
            /// </summary>
            [Description("Chờ thanh toán")]
            dzf = 0,
            /// <summary>
            /// Đang thanh toán
            /// </summary>
            [Description("Đang thanh toán")]
            zfz = 1,
            /// <summary>
            /// Đã thanh toán
            /// </summary>
            [Description("Đã thanh toán")]
            yzf = 2,
            /// <summary>
            /// Đã hoàn tiền
            /// </summary>
            [Description("Đã hoàn tiền")]
            ytk = 3,
            /// <summary>
            /// Đã hủy
            /// </summary>
            [Description("Đã hủy")]
            zf = 4,
        }

        /// <summary>
        /// Loại nghiệp vụ đơn hàng
        /// </summary>
        public enum EnumOrderType
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
    }
}
