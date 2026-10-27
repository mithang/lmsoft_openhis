
namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Chi tiết xuất kho thuốc hệ thống
    /// </summary>
    public class XT_YP_LS_NBFYMXK
    {
        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string Ypdm { get; set; }

        /// <summary>
        /// Số tần suất
        /// </summary>
        public string Ph { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Hạn dùng
        /// </summary>
        public string Yxq { get; set; }

        /// <summary>
        /// Giá bán lẻ kho thuốc
        /// </summary>
        public decimal Lsj { get; set; }

        /// <summary>
        /// Số tiền bán buôn kho thuốc
        /// </summary>
        public decimal Pfj { get; set; }

        /// <summary>
        /// Tổng giá bán lẻ
        /// </summary>
        public decimal lsje { get; set; }

        /// <summary>
        /// Số lượng phát thuốc Số lượng đơn vị nhỏ nhất
        /// </summary>
        public int fysl { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Chi tiết lĩnh nhưngID
        /// </summary>
        public string sldmxId { get; set; }

        /// <summary>
        /// Đơn vị đóng gói
        /// </summary>
        public string bzdw { get; set; }
    }
}
