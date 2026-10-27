using SqlSugar;
using System;

namespace Newtouch.Sett.Domain.Entity
{
    /// <summary>Order_PayInfo (Sett DB). Column names match the table exactly;
    /// CLR types match column types (verified against INFORMATION_SCHEMA).</summary>
    [SugarTable("Order_PayInfo")]
    public class OrderPayInfoEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        public string OutTradeNo { get; set; }
        public string TradeNo { get; set; }
        public decimal Amount { get; set; }
        public DateTime? OrderDate { get; set; }
        public int PayType { get; set; }
        public int PayStatus { get; set; }
        public string Memo { get; set; }
        public string CreatorCode { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public string zt { get; set; }
        public string PayAccount { get; set; }
        public string TradeDesc { get; set; }
        public string OrganizeId { get; set; }
    }

    /// <summary>Order_RefundInfo (Sett DB).</summary>
    [SugarTable("Order_RefundInfo")]
    public class OrderRefundInfoEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        public string OutTradeNo { get; set; }
        public string TradeNo { get; set; }
        public decimal Amount { get; set; }
        public int RefundStatus { get; set; }
        public string Memo { get; set; }
        public string CreatorCode { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public string zt { get; set; }
        public string RefundReason { get; set; }
        public DateTime? RefundDate { get; set; }
        public string OutRequestNo { get; set; }
    }
}
