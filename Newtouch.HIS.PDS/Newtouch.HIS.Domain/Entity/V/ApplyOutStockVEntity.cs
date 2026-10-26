namespace Newtouch.HIS.Domain.Entity.V
{
    /// <summary>
    /// xuất kho theo phiếu lĩnh chi tiết
    /// </summary>
    public class ApplyOutStockVEntity
    {
        /// <summary>
        /// bộ phận lĩnh bộ phận nhập kho
        /// </summary>
        public string slbm { get; set; }

        /// <summary>
        /// chi tiết phiếu lĩnh ID
        /// </summary>
        public string sldmxId { get; set; }

        /// <summary>
        /// phiếu lĩnh ID
        /// </summary>
        public string sldId { get; set; }

        /// <summary>
        /// số phiếu lĩnh
        /// </summary>
        public string sldh { get; set; }

        /// <summary>
        /// trước tiên phát số đơn vị nhỏ nhất
        /// </summary>
        public int xfsl { get; set; }

        /// <summary>
        /// số lượng còn chờ phát tải lần đầu và xfsl giống nhau đơn vị nhỏ nhất
        /// </summary>
        public int sysl { get; set; }

        /// <summary>
        /// đơn vị nhỏ nhất
        /// </summary>
        public string zxdw { get; set; }

        /// <summary>
        /// hệ số chuyển đổi hiện là hệ số chuyển đổi đơn vị nhỏ nhất
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// đơn vị đóng gói
        /// </summary>
        public string bzdw { get; set; }

        /// <summary>
        /// hiện tại phát số đơn vị nhỏ nhất
        /// </summary>
        public int zxdwxfsl { get; set; }

        /// <summary>
        /// số lượng đóng gói
        /// </summary>
        public int bzs { get; set; }

        /// <summary>
        /// số lượng đã phát  kèm đơn vị
        /// </summary>
        public string yfslStr { get; set; }

        /// <summary>
        /// số lượng lĩnh kèm đơn vị
        /// </summary>
        public string slslStr { get; set; }

        /// <summary>
        /// tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// mã thuốc
        /// </summary>
        public string ypdm { get; set; }

        /// <summary>
        /// tồn kho khả dụng kèm đơn vị
        /// </summary>
        public string kyslStr { get; set; }

        /// <summary>
        /// tồn kho khả dụng đơn vị nhỏ nhất
        /// </summary>
        public int kykc { get; set; }

        /// <summary>
        /// nhà sản xuất
        /// </summary>
        public string sccj { get; set; }

        /// <summary>
        /// Đơn giá theo đơn vị bán buôn
        /// </summary>
        public string pfjdwdj { get; set; }

        /// <summary>
        /// Đơn giá theo đơn vị bán lẻ
        /// </summary>
        public string lsjdwdj { get; set; }

        /// <summary>
        /// tổng tiền bán buôn
        /// </summary>
        public decimal pjze { get; set; }

        /// <summary>
        /// tổng tiền bán lẻ
        /// </summary>
        public decimal lsze { get; set; }

        /// <summary>
        /// giá nhập đơn vị nhỏ nhất
        /// </summary>
        public decimal? zxdwjj { get; set; }

        /// <summary>
        /// đơn vị nhỏ nhất giá bán buôn
        /// </summary>
        public decimal zxdwpfj { get; set; }

        /// <summary>
        /// giá bán lẻ đơn vị nhỏ nhất
        /// </summary>
        public decimal zxdwlsj { get; set; }
    }
}
