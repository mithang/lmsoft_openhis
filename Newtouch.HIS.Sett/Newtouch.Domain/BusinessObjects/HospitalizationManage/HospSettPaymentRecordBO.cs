namespace Newtouch.HIS.Domain.BusinessObjects
{
    /// <summary>
    /// (Quyết toán xuất viện)Thêm mới Bản ghi thu chi
    /// </summary>
    public class HospSettPaymentRecordBO
    {
        /// <summary>
        /// Mã phương thức thanh toán
        /// </summary>
        public int? xjzffsbh { get; set; }

        /// <summary>
        /// Phương thức thanh toán
        /// </summary>
        public string xjzffs { get; set; }

        /// <summary>
        /// Tên phương thức thanh toán
        /// </summary>
        public string zffsmc { get; set; }

        /// <summary>
        /// Số tiền thanh toán
        /// </summary>
        public decimal zfje { get; set; }

        /// <summary>
        /// Tài khoản thanh toán
        /// </summary>
        public int? zfzh { get; set; }

    }
}
