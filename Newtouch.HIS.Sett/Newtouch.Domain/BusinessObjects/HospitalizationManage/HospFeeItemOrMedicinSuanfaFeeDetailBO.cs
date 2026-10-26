namespace Newtouch.HIS.Domain.BusinessObjects
{
    /// <summary>
    /// Quyết toán xuất viện Chi phí(Hạng mục/Thuốc)Chi tiết Thuật toán Chi phí Chi tiết
    /// </summary>
    public class HospFeeItemOrMedicinSuanfaFeeDetailBO
    {
        /// <summary>
        /// Số tiền giảm miễn
        /// </summary>
        public decimal jmje { get; set; }
        /// <summary>
        /// Tổng chi phí(Tiền mặt bệnh nhân tự chi trả cần phải trả)
        /// </summary>
        public decimal total { get; set; }

        /// <summary>
        /// Tự chi trả theo phân loại
        /// </summary>
        public decimal flzf { get; set; }

        /// <summary>
        /// Chi phí ghi sổ
        /// </summary>
        public decimal jzfy { get; set; }

        /// <summary>
        /// Chi phí tự lo
        /// </summary>
        public decimal zl { get; set; }

        /// <summary>
        /// Tiền mặt
        /// </summary>
        public decimal xj { get; set; }

        /// <summary>
        /// Tự chi trả theo thuật toán(Số tiền tự chi trả tính được qua thuật toán)
        /// </summary>
        public decimal sfzf { get; set; }

        /// <summary>
        /// Tự lo theo thuật toán(Số tiền tự chi trả tính được qua thuật toán)
        /// </summary>
        public decimal sfzl { get; set; }

    }
}
