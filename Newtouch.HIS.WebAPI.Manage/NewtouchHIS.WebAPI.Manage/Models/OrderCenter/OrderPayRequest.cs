using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.WebAPI.Manage.Models.OrderCenter
{
    public class OrderPayRequest
    {
        /// <summary>
        /// số thẻ khám bệnh
        /// </summary>
        [Required(ErrorMessage = "就诊卡号不可为空")]
        public string CardNo { get; set; }
        /// <summary>
        /// số đơn hàng
        /// </summary>
        [Required(ErrorMessage = "订单号不可为空")]
        public string OrderNo { get; set; }
        /// <summary>
        /// phương thức thanh toán
        /// </summary>
        [Required(ErrorMessage = "支付方式不可为空")]
        public string PayWay { get; set; }
        /// <summary>
        /// số tiền thanh toán
        /// </summary>
        [Required(ErrorMessage = "支付金额不可为空")]
        public decimal PayFee { get; set; }
        /// <summary>
        /// số thứ tự thanh toán
        /// </summary>
        [Required(ErrorMessage = "支付流水号不可为空")]
        public string PayLsh { get; set; }
        /// <summary>
        /// thông tin danh tính người thanh toán
        /// </summary>
        public string? PayerId { get; set; }
        /// <summary>
        /// thời gian thanh toán đơn hàng
        /// </summary>
        [Required(ErrorMessage = "订单支付时间不可为空")]
        public DateTime? PayTime { get; set; }
        /// <summary>
        /// HISsố thanh toán nội bộ
        /// </summary>
        public string? SettTradeNo { get; set; }
        /// <summary>
        /// loại đơn hàng EnumOrderType 
        /// 1 đơn hàng ngoại trú
        /// 2 đơn hàng nội trú
        /// </summary>
        public int? OrderType { get; set; }
    }
}
