using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.WebAPI.Manage.Models.OrderCenter
{
    public class OrderPayRequest
    {
        /// <summary>
        /// Khám bệnhSố thẻ
        /// </summary>
        [Required(ErrorMessage = "Số thẻ khám bệnh không được để trống")]
        public string CardNo { get; set; }
        /// <summary>
        /// Số đơn hàng
        /// </summary>
        [Required(ErrorMessage = "Mã đơn hàng không được để trống")]
        public string OrderNo { get; set; }
        /// <summary>
        /// Phương thức thanh toán
        /// </summary>
        [Required(ErrorMessage = "Phương thức thanh toán không được để trống")]
        public string PayWay { get; set; }
        /// <summary>
        /// Số tiền thanh toán
        /// </summary>
        [Required(ErrorMessage = "Số tiền thanh toán không được để trống")]
        public decimal PayFee { get; set; }
        /// <summary>
        /// Số giao dịch thanh toán
        /// </summary>
        [Required(ErrorMessage = "Số giao dịch thanh toán không được để trống")]
        public string PayLsh { get; set; }
        /// <summary>
        /// Thông tin danh tính người thanh toán
        /// </summary>
        public string? PayerId { get; set; }
        /// <summary>
        /// Thời gian thanh toán đơn hàng
        /// </summary>
        [Required(ErrorMessage = "Thời gian thanh toán đơn hàng không được để trống")]
        public DateTime? PayTime { get; set; }
        /// <summary>
        /// HISSố quyết toán nội bộ
        /// </summary>
        public string? SettTradeNo { get; set; }
        /// <summary>
        /// Loại đơn hàng EnumOrderType 
        /// 1 Đơn hàng ngoại trú
        /// 2 Đơn hàng nội trú
        /// </summary>
        public int? OrderType { get; set; }
    }
}
