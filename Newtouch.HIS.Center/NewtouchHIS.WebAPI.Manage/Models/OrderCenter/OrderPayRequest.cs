using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.WebAPI.Manage.Models.OrderCenter
{
    public class OrderPayRequest
    {
        /// <summary>
        /// 就诊Số thẻ
        /// </summary>
        [Required(ErrorMessage = "Số thẻ khám bệnh không được để trống")]
        public string CardNo { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        [Required(ErrorMessage = "Mã đơn hàng không được để trống")]
        public string OrderNo { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        [Required(ErrorMessage = "Phương thức thanh toán không được để trống")]
        public string PayWay { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        [Required(ErrorMessage = "Số tiền thanh toán không được để trống")]
        public decimal PayFee { get; set; }
        /// <summary>
        /// 支付流水号
        /// </summary>
        [Required(ErrorMessage = "Số giao dịch thanh toán không được để trống")]
        public string PayLsh { get; set; }
        /// <summary>
        /// 支付者身份信息
        /// </summary>
        public string? PayerId { get; set; }
        /// <summary>
        /// 订单支付时间
        /// </summary>
        [Required(ErrorMessage = "Thời gian thanh toán đơn hàng không được để trống")]
        public DateTime? PayTime { get; set; }
        /// <summary>
        /// HIS内部结算号
        /// </summary>
        public string? SettTradeNo { get; set; }
        /// <summary>
        /// 订单类型 EnumOrderType 
        /// 1 门诊订单
        /// 2 住院订单
        /// </summary>
        public int? OrderType { get; set; }
    }
}
