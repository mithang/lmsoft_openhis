using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameworkBase.MultiOrg.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    [Table("Order_RefundInfo")]
    public class OrderRefundInfoEntity:IEntity<OrderRefundInfoEntity>
    {
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// Số đơn hàng yêu cầu thanh toán
        /// </summary>
        public string OutTradeNo { get; set; }
        /// <summary>
        /// Số giao dịch bên thứ ba
        /// </summary>
        public string TradeNo { get; set; }
        /// <summary>
        /// Số tiền hoàn tiền
        /// </summary>
        public decimal Amount { get; set; }
        public int RefundStatus { get; set; }
        public string Memo { get; set; }
        /// <summary>
        /// Người tạoID
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Thời gian sửa đổi lần cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi lần cuốiID
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }
        /// <summary>
        /// Lý do hoàn tiền
        /// </summary>
        public string RefundReason { get; set; }
        /// <summary>
        /// Thời gian giao dịch hoàn tiền trên nền tảng
        /// </summary>
        public DateTime? RefundDate { get; set; }
        /// <summary>
        /// Số yêu cầu hoàn tiền
        /// </summary>
        public string OutRequestNo { get; set; }
    }
}
