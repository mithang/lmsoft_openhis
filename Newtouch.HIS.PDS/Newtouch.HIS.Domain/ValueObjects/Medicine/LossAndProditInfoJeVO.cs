namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Truy vấn tổng số tiền báo hỏng/báo lãi
    /// </summary>
    public class LossAndProditInfoJeVo
    {
        /// <summary>
        /// giá bán lẻ tổng số tiền
        /// </summary>
        public decimal? Ljze { get; set; }

        /// <summary>
        /// giá bán buôn tổng số tiền
        /// </summary>
        public decimal? Pjze { get; set; }

        /// <summary>
        /// tổng tiền giá nhập
        /// </summary>
        public decimal? Jjze { get; set; }
    }
}
