namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 报损报益总金额查询
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
