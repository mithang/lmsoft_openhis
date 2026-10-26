using System.ComponentModel;

namespace NewtouchHIS.Base.Domain.EnumExtend
{
    public class OrderEnum
    {
        /// <summary>
        /// trạng thái đơn hàng0:chờ thanh toán 1:đang thanh toán  2:đã thanh toán 3：đã hoàn tiền 4:đã hủy
        /// </summary>
        public enum EnumOrderStatus
        {
            /// <summary>
            /// chờ thanh toán
            /// </summary>
            [Description("待支付")]
            dzf = 0,
            /// <summary>
            /// đang thanh toán
            /// </summary>
            [Description("支付中")]
            zfz = 1,
            /// <summary>
            /// đã thanh toán
            /// </summary>
            [Description("已支付")]
            yzf = 2,
            /// <summary>
            /// đã hoàn tiền
            /// </summary>
            [Description("已退款")]
            ytk = 3,
            /// <summary>
            /// đã hủy
            /// </summary>
            [Description("已作废")]
            zf = 4,
        }

        /// <summary>
        /// loại nghiệp vụ đơn hàng
        /// </summary>
        public enum EnumOrderType
        {
            /// <summary>
            /// khám ngoại trú
            /// </summary>
            [Description("门诊")]
            mz = 1,
            /// <summary>
            /// nội trú
            /// </summary>
            [Description("住院")]
            zy = 2
        }
    }
}
