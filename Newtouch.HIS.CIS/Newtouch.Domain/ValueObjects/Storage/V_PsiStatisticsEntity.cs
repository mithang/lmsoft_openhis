namespace Newtouch.Domain.ValueObjects.Storage
{
    /// <summary>
    /// Thống kê nhập xuất tồn
    /// </summary>
    public class VPsiStatisticsEntity
    {
        /// <summary>
        /// Vật tưID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// Tên vật tư
        /// </summary>
        public string wzmc { get; set; }

        /// <summary>
        /// Tên loại
        /// </summary>
        public string lbmc { get; set; }

        /// <summary>
        /// Quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// Tên đơn vị của khoa này
        /// </summary>
        public string bmdwmc { get; set; }

        /// <summary>
        /// Số lượng đầu kỳ,Kèm đơn vị
        /// </summary>
        public string qcsl { get; set; }

        /// <summary>
        /// Số lượng nhập kho,Kèm đơn vị
        /// </summary>
        public string rksl { get; set; }

        /// <summary>
        /// Số lượng xuất kho,Kèm đơn vị
        /// </summary>
        public string cksl { get; set; }

        /// <summary>
        /// Số lượng chênh lệch,Kèm đơn vị
        /// </summary>
        public string sysl { get; set; }

        /// <summary>
        /// Số lượng cuối kỳ,Kèm đơn vị
        /// </summary>
        public string qmsl { get; set; }

        /// <summary>
        /// Tổng giá bán lẻ đầu kỳ
        /// </summary>
        public decimal qclsze { get; set; }

        /// <summary>
        /// Tổng giá bán lẻ nhập kho
        /// </summary>
        public decimal rklsze { get; set; }

        /// <summary>
        /// Tổng giá bán lẻ xuất kho
        /// </summary>
        public decimal cklsze { get; set; }

        /// <summary>
        /// Tổng giá bán lẻ chênh lệch
        /// </summary>
        public decimal sylsze { get; set; }

        /// <summary>
        /// Tổng giá bán lẻ cuối kỳ
        /// </summary>
        public decimal qmlsze { get; set; }

        /// <summary>
        /// Tổng giá bán lẻ chênh lệch do điều chỉnh giá
        /// </summary>
        public decimal tjsyze { get; set; }
    }
}
