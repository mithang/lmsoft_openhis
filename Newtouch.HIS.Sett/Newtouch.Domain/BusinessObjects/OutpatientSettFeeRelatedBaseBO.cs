namespace Newtouch.HIS.Domain.BusinessObjects
{
    /// <summary>
    /// Lớp cơ sở liên quan thanh toán số tiền quyết toán ngoại trú
    /// </summary>
    public class OutpatientSettFeeRelatedBaseBO
    {
        /// <summary>
        /// Khoản phải thu thanh toán ban đầu
        /// </summary>
        public decimal? orglxjzfys { get; set; }
        /// <summary>
        /// Tỷ lệ chiết khấu
        /// </summary>
        public decimal? zkbl { get; set; }
        /// <summary>
        /// Số tiền chiết khấu
        /// </summary>
        public decimal? zkje { get; set; }
        /// <summary>
        /// Khoản phải thu thanh toán(Khoản phải thu thanh toán ban đầu sau chiết khấu)
        /// </summary>
        public decimal? xjzfys { get; set; }
        /// <summary>
        /// Số tiền thực thu
        /// </summary>
        public decimal? ssk { get; set; }
        public string zffs1 { get; set; }
        public decimal? zfje1 { get; set; }
        public string zffs2 { get; set; }
        public decimal? zfje2 { get; set; }
        /// <summary>
        /// Tiền thối lại
        /// </summary>
        public decimal? zhaoling { get; set; }

    }
}
