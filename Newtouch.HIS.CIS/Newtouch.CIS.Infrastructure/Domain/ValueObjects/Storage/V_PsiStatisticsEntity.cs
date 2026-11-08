namespace Newtouch.Domain.ValueObjects.Storage
{
    /// <summary>
    /// vào bán tồn tại Thống kê
    /// </summary>
    public class VPsiStatisticsEntity
    {
        /// <summary>
        /// vật tư ID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// vật tư Tên
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
        /// bản Khoa phòng Tên đơn vị
        /// </summary>
        public string bmdwmc { get; set; }

        /// <summary>
        /// kỳ ban đầu Số lượng,mang Đơn vị
        /// </summary>
        public string qcsl { get; set; }

        /// <summary>
        /// Nhập kho Số lượng,mang Đơn vị
        /// </summary>
        public string rksl { get; set; }

        /// <summary>
        /// Xuất kho Số lượng,mang Đơn vị
        /// </summary>
        public string cksl { get; set; }

        /// <summary>
        /// lãi lỗ Số lượng,mang Đơn vị
        /// </summary>
        public string sysl { get; set; }

        /// <summary>
        /// cuối kỳ Số lượng,mang Đơn vị
        /// </summary>
        public string qmsl { get; set; }

        /// <summary>
        /// kỳ ban đầu tổng bán lẻ
        /// </summary>
        public decimal qclsze { get; set; }

        /// <summary>
        /// Nhập kho tổng bán lẻ
        /// </summary>
        public decimal rklsze { get; set; }

        /// <summary>
        /// Xuất kho tổng bán lẻ
        /// </summary>
        public decimal cklsze { get; set; }

        /// <summary>
        /// lãi lỗ tổng bán lẻ
        /// </summary>
        public decimal sylsze { get; set; }

        /// <summary>
        /// cuối kỳ tổng bán lẻ
        /// </summary>
        public decimal qmlsze { get; set; }

        /// <summary>
        /// gọi giá lãi lỗ tổng bán lẻ
        /// </summary>
        public decimal tjsyze { get; set; }
    }
}
