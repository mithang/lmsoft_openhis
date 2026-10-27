using System;

namespace Newtouch.Sett.Domain.DTO
{
    /// <summary>Trade list / trade detail row (same shape as the Framework
    /// MicroPayTradeInfoDTO; populated by raw SQL, not mapped to a table).</summary>
    public class MicroPayTradeInfoDTO
    {
        public int? PayType { get; set; }
        public string OutTradeNo { get; set; }
        public string TradeNo { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? PayAmount { get; set; }
        public int? PayStatus { get; set; }
        public string PayMemo { get; set; }
        public string PayAccount { get; set; }
        public string TradeDesc { get; set; }
        public DateTime? OutTradeTime { get; set; }
        public decimal? RefundAmount { get; set; }
        public DateTime? RefundDate { get; set; }
        public string RefundReason { get; set; }
        public int? RefundStatus { get; set; }
        public string RefundMemo { get; set; }
        public DateTime? OutRefundTime { get; set; }
        public string OutRequestNo { get; set; }
        public string fph { get; set; }
        public string xm { get; set; }
        public string kh { get; set; }
        public string patno { get; set; }
        public string TradeStatus { get; set; }
    }
}
