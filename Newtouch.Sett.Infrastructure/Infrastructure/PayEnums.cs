using System.ComponentModel;
using Newtouch.Sett.Infrastructure;

namespace Newtouch.Sett.Infrastructure
{
    public enum EnumPayStatus
    {
        [Description("Thanh toán thành công")]
        Success = 1,
        [Description("Thanh toán thất bại")]
        Failed = 2
    }

    public enum EnumRefundStatus
    {
        [Description("Hoàn tiền thành công")]
        Success = 1,
        [Description("Hoàn tiền thất bại")]
        Failed = 2,
        [Description("Chưa xác định")]
        UnKnown = 3
    }

    public enum EnumTradeType
    {
        [Description("Alipay")]
        Alipay_Bar_Code = 1,
        [Description("WeChat")]
        Wechat_MICROPAY = 2,
    }
}
