using Newtouch.Sett.Domain;

namespace Newtouch.Sett.DomainServices
{
    /// <summary>Merged PayApp stub: typed overload = Pay gateway stub (Failed);
    /// object overload (Outpatient signature) converts and delegates.</summary>
    public class PayApp : IPayApp
    {
        public int TradeRefund(string outTradeNo, decimal refundAmount, string refundReason, string outRequestNo,
            out string errorMsg)
        {
            errorMsg = "Hoàn tiền qua cổng thanh toán chưa khả dụng trong bản port .NET 6 này (cổng Alipay/WeChat/NxPay chưa được chuyển đổi); không thực hiện hoàn tiền và không ghi dữ liệu.";
            return (int)EnumRefundStatus.Failed;
        }

        public int TradeRefund(object p0, object p1, object p2, object p3, out string p4)
        {
            string outTradeNo = p0?.ToString();
            decimal refundAmount = 0;
            try { refundAmount = System.Convert.ToDecimal(p1); } catch { refundAmount = 0; }
            string refundReason = p2?.ToString();
            string outRequestNo = p3?.ToString();
            return TradeRefund(outTradeNo, refundAmount, refundReason, outRequestNo, out p4);
        }
    }
}
