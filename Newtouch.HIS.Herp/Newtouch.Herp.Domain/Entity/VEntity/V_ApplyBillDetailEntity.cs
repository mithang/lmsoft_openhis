namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// Chi tiết phiếu lĩnh
    /// </summary>
    public class VApplyBillDetailEntity
    {
        /// <summary>
        /// Phiếu lĩnh ID
        /// </summary>
        public long sldId { get; set; }

        /// <summary>
        /// Số phiếu lĩnh
        /// </summary>
        public string sldh { get; set; }

        /// <summary>
        /// Khoa nhập kho ID
        /// </summary>
        public string rkbm { get; set; }

        /// <summary>
        /// Chi tiết phiếu lĩnh ID
        /// </summary>
        public long sldmxId { get; set; }

        /// <summary>
        /// Vật tư ID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// Tên vật tư
        /// </summary>
        public string wzmc { get; set; }

        /// <summary>
        /// Số lượng lĩnh,Đơn vị khoa phòng
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// Số lượng lĩnh
        /// </summary>
        public string slStr { get; set; }

        /// <summary>
        /// Số lượng đã phát
        /// </summary>
        public string yfslStr { get; set; }

        /// <summary>
        /// Tên loại
        /// </summary>
        public string lbmc { get; set; }

        /// <summary>
        /// Quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// Nhà sản xuất
        /// </summary>
        public string sccj { get; set; }

        /// <summary>
        /// Thương hiệu
        /// </summary>
        public string brand { get; set; }

        /// <summary>
        /// Số lượng tồn kho khả dụng
        /// </summary>
        public int kykcsl { get; set; }

        /// <summary>
        /// Số lượng còn lại đơn vị khoa phòng
        /// </summary>
        public int bmdwsysl { get; set; }

        /// <summary>
        /// Số lượng phát hiện tại,Mặc định bằng Số lượng còn lại đơn vị khoa phòng
        /// </summary>
        public int xfsl { get; set; }

        /// <summary>
        /// Đơn vị khoa phòng ID
        /// </summary>
        public string bmdwId { get; set; }

        /// <summary>
        /// Tên đơn vị khoa phòng
        /// </summary>
        public string bmdwmc { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Tên đơn vị nhỏ nhất
        /// </summary>
        public string zxdwmc { get; set; }

        /// <summary>
        /// Số lượng tồn kho khả dụng
        /// </summary>
        public string kyslStr { get; set; }

        /// <summary>
        /// Số lượng còn lại đơn vị nhỏ nhất
        /// </summary>
        public int zxdwsysl { get; set; }
    }
}