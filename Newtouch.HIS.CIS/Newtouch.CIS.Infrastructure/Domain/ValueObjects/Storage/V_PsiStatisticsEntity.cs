namespace Newtouch.Domain.ValueObjects.Storage
{
    /// <summary>
    /// vàobántồn tạiThống kê
    /// </summary>
    public class VPsiStatisticsEntity
    {
        /// <summary>
        /// vật tưID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// vật tưTên
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
        /// bảnKhoa phòngTên đơn vị
        /// </summary>
        public string bmdwmc { get; set; }

        /// <summary>
        /// kỳban đầuSố lượng,mangĐơn vị
        /// </summary>
        public string qcsl { get; set; }

        /// <summary>
        /// Nhập khoSố lượng,mangĐơn vị
        /// </summary>
        public string rksl { get; set; }

        /// <summary>
        /// Xuất khoSố lượng,mangĐơn vị
        /// </summary>
        public string cksl { get; set; }

        /// <summary>
        /// lãi lỗSố lượng,mangĐơn vị
        /// </summary>
        public string sysl { get; set; }

        /// <summary>
        /// cuối kỳSố lượng,mangĐơn vị
        /// </summary>
        public string qmsl { get; set; }

        /// <summary>
        /// kỳban đầutổng bán lẻ
        /// </summary>
        public decimal qclsze { get; set; }

        /// <summary>
        /// Nhập khotổng bán lẻ
        /// </summary>
        public decimal rklsze { get; set; }

        /// <summary>
        /// Xuất khotổng bán lẻ
        /// </summary>
        public decimal cklsze { get; set; }

        /// <summary>
        /// lãi lỗtổng bán lẻ
        /// </summary>
        public decimal sylsze { get; set; }

        /// <summary>
        /// cuối kỳtổng bán lẻ
        /// </summary>
        public decimal qmlsze { get; set; }

        /// <summary>
        /// gọigiálãi lỗtổng bán lẻ
        /// </summary>
        public decimal tjsyze { get; set; }
    }
}
