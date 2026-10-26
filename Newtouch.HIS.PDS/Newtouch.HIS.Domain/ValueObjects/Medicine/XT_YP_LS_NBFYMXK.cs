
namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// hệ thống thuốc xuất kho chi tiết
    /// </summary>
    public class XT_YP_LS_NBFYMXK
    {
        /// <summary>
        /// mã thuốc
        /// </summary>
        public string Ypdm { get; set; }

        /// <summary>
        /// số tần suất
        /// </summary>
        public string Ph { get; set; }

        /// <summary>
        /// lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// hạn sử dụng
        /// </summary>
        public string Yxq { get; set; }

        /// <summary>
        /// giá bán lẻ kho thuốc
        /// </summary>
        public decimal Lsj { get; set; }

        /// <summary>
        /// kho thuốc tiền bán buôn
        /// </summary>
        public decimal Pfj { get; set; }

        /// <summary>
        /// tổng tiền bán lẻ
        /// </summary>
        public decimal lsje { get; set; }

        /// <summary>
        /// số lượng phát thuốc số lượng đơn vị nhỏ nhất
        /// </summary>
        public int fysl { get; set; }

        /// <summary>
        /// hệ số chuyển đổi
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// ID chi tiết đơn lĩnh
        /// </summary>
        public string sldmxId { get; set; }

        /// <summary>
        /// đơn vị đóng gói
        /// </summary>
        public string bzdw { get; set; }
    }
}
