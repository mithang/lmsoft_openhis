
namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Thông tin quyết toán lần trước
    /// </summary>
    public class LastSettleInfoVO
    {
        /// <summary>
        /// Họ tên
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// Khoản phải thu
        /// </summary>
        public decimal? ysk { get; set; }
        /// <summary>
        /// Thanh toán tiền mặt
        /// </summary>
        public decimal xjzf { get; set; }
        /// <summary>
        /// Tiền thối lại
        /// </summary>
        public decimal zl { get; set; }
        /// <summary>
        /// Số hóa đơn trước
        /// </summary>
        public string fph { get; set; }
    }
}
