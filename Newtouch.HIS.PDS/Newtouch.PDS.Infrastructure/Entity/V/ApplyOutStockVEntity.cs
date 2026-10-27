namespace Newtouch.HIS.Domain.Entity.V
{
    /// <summary>
    /// Chi tiết xuất kho theo đơn lĩnh
    /// </summary>
    public class ApplyOutStockVEntity
    {
        /// <summary>
        /// Bộ phận lĩnh Bộ phận nhập kho
        /// </summary>
        public string slbm { get; set; }

        /// <summary>
        /// Chi tiết đơn lĩnhID
        /// </summary>
        public string sldmxId { get; set; }

        /// <summary>
        /// Đơn lĩnhID
        /// </summary>
        public string sldId { get; set; }

        /// <summary>
        /// Số đơn lĩnh
        /// </summary>
        public string sldh { get; set; }

        /// <summary>
        /// Số phát trước Đơn vị nhỏ nhất
        /// </summary>
        public int xfsl { get; set; }

        /// <summary>
        /// Số lượng chờ phát còn lại Tải lần đầu vớixfslGiống nhau Đơn vị nhỏ nhất
        /// </summary>
        public int sysl { get; set; }

        /// <summary>
        /// Đơn vị nhỏ nhất
        /// </summary>
        public string zxdw { get; set; }

        /// <summary>
        /// Hệ số quy đổi Hiện là hệ số quy đổi đơn vị nhỏ nhất
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Đơn vị đóng gói
        /// </summary>
        public string bzdw { get; set; }

        /// <summary>
        /// Số phát hiện tại Đơn vị nhỏ nhất
        /// </summary>
        public int zxdwxfsl { get; set; }

        /// <summary>
        /// Số lượng đóng gói
        /// </summary>
        public int bzs { get; set; }

        /// <summary>
        /// Số lượng đã phát  kèm đơn vị
        /// </summary>
        public string yfslStr { get; set; }

        /// <summary>
        /// Số lượng lĩnh kèm đơn vị
        /// </summary>
        public string slslStr { get; set; }

        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypdm { get; set; }

        /// <summary>
        /// Tồn kho khả dụng kèm đơn vị
        /// </summary>
        public string kyslStr { get; set; }

        /// <summary>
        /// Tồn kho khả dụng Đơn vị nhỏ nhất
        /// </summary>
        public int kykc { get; set; }

        /// <summary>
        /// Nhà sản xuất
        /// </summary>
        public string sccj { get; set; }

        /// <summary>
        /// Đơn giá đơn vị bán buôn
        /// </summary>
        public string pfjdwdj { get; set; }

        /// <summary>
        /// Đơn giá đơn vị bán lẻ
        /// </summary>
        public string lsjdwdj { get; set; }

        /// <summary>
        /// Tổng giá bán buôn
        /// </summary>
        public decimal pjze { get; set; }

        /// <summary>
        /// Tổng giá bán lẻ
        /// </summary>
        public decimal lsze { get; set; }

        /// <summary>
        /// Giá nhập đơn vị nhỏ nhất
        /// </summary>
        public decimal? zxdwjj { get; set; }

        /// <summary>
        /// Giá bán buôn đơn vị nhỏ nhất
        /// </summary>
        public decimal zxdwpfj { get; set; }

        /// <summary>
        /// Giá bán lẻ đơn vị nhỏ nhất
        /// </summary>
        public decimal zxdwlsj { get; set; }
    }
}
