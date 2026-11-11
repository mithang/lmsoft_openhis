
namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Chi tiết xuất kho thuốc hệ thống
    /// G30-F2: THỨ TỰ property PHẢI khớp TVP dbo.XT_YP_LS_NBFYMXK (Ypdm,Ph,pc,Yxq,Lsj,Pfj,fysl,zhyz,lsje,bzdw,sldmxId).
    /// ToDataTable() tạo cột theo thứ tự khai báo và SqlClient map TVP theo VỊ TRÍ, không theo tên — đảo thứ tự sẽ làm lệch giá trị (fysl nhận lsje, bzdw nhận sldmxId...).
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
        /// Số lượng phát thuốc Số lượng đơn vị nhỏ nhất
        /// </summary>
        public int fysl { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Tổng giá bán lẻ
        /// </summary>
        public decimal lsje { get; set; }

        /// <summary>
        /// Đơn vị đóng gói
        /// </summary>
        public string bzdw { get; set; }

        /// <summary>
        /// Chi tiết lĩnh nhưng ID
        /// </summary>
        public string sldmxId { get; set; }
    }
}
