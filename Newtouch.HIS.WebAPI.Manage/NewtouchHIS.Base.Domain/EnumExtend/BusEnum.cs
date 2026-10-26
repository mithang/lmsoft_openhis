using System.ComponentModel;

namespace NewtouchHIS.Base.Domain.EnumExtend
{
    public class BusEnum
    {
        /// <summary>
        /// loại nghiệp vụ đơn hàng
        /// </summary>
        public enum EnumBusType
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

        public enum EnumReportStu
        {
            /// <summary>
            /// đã đăng ký
            /// </summary>
            [Description("已申请")]
            ysq = 0,
            /// <summary>
            /// đã nhận
            /// </summary>
            [Description("已接收")]
            yjs = 1,
            /// <summary>
            /// đã hoàn thành（báo cáo đã ra）
            /// </summary>
            [Description("已完成")]
            ywc = 2
        }
    }
}
