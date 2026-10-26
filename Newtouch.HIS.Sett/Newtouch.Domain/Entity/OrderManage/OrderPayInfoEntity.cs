using System;
using FrameworkBase.MultiOrg.Domain.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity
{
    [Table("Order_PayInfo")]
    public class OrderPayInfoEntity: IEntity<OrderPayInfoEntity>
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
        /// Số tiền thanh toán
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Thời gian giao dịch
        /// </summary>
        public DateTime? OrderDate { get; set; }
        /// <summary>
        /// Nguồn thanh toán Alipay1 WeChat2
        /// </summary>
        public int PayType { get; set; }
        /// <summary>
        /// Thanh toán có thành công hay không 0 không 1 là
        /// </summary>
        public int PayStatus { get; set; }
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
        /// Tài khoản thanh toán
        /// </summary>
        public string PayAccount { get; set; }
        /// <summary>
        /// Mô tả giao dịch
        /// </summary>
        public string TradeDesc { get; set; }

        public string OrganizeId { get; set; }

    }
}
