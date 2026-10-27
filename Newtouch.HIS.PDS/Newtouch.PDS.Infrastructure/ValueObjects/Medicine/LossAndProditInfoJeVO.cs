namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Truy vấn tổng Số tiền báo hỏng báo thừa
    /// </summary>
    public class LossAndProditInfoJeVo
    {
        /// <summary>
        /// Tổng Số tiền giá bán lẻ
        /// </summary>
        public decimal? Ljze { get; set; }

        /// <summary>
        /// Tổng số tiền giá bán buôn
        /// </summary>
        public decimal? Pjze { get; set; }

        /// <summary>
        /// Tổng số tiền giá nhập
        /// </summary>
        public decimal? Jjze { get; set; }
    }
}
