using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Bản ghi chính truy vấn chứng từ
    /// </summary>
    public class ReceiptQueryVO
    {
        /// <summary>
        /// Loại chứng từ
        /// </summary>
        public int djlx { get; set; }

        /// <summary>
        /// Tên loại chứng từ
        /// </summary>
        public string djlxmc { get; set; }

        /// <summary>
        /// Tên bộ phận xuất kho
        /// </summary>
        public string ckbmmc { get; set; }

        /// <summary>
        /// Tên bộ phận nhập kho
        /// </summary>
        public string rkbmmc { get; set; }

        /// <summary>
        /// Tên nhà cung cấp
        /// </summary>
        public string gysmc { get; set; }

        /// <summary>
        /// Bảng chính chứng từ xuất nhập khoID
        /// </summary>
        public string crkId { get; set; }

        /// <summary>
        /// Số chứng từ
        /// </summary>
        public string pdh { get; set; }

        /// <summary>
        /// Thời gian thao tác
        /// </summary>
        public DateTime? czsj { get; set; }

        /// <summary>
        /// Tên phương thức xuất nhập kho
        /// </summary>
        public string crkfsmc { get; set; }
        public string crkfscode { get; set; }
        /// <summary>
        /// Tổng giá bán buôn
        /// </summary>
        public decimal? pjze { get; set; }

        /// <summary>
        /// Tổng giá bán lẻ
        /// </summary>
        public decimal? ljze { get; set; }

        /// <summary>
        /// Tổng giá nhập
        /// </summary>
        public decimal? zje { get; set; }

        /// <summary>
        /// Chênh lệch giá nhập bán
        /// </summary>
        public decimal? jxcj { get; set; }

        /// <summary>
        /// Trạng thái duyệt
        /// </summary>
        public string shzt { get; set; }

        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// Thời gian nhập kho
        /// </summary>
        public DateTime? Rksj { get; set; }

        /// <summary>
        /// Thời gian xuất kho
        /// </summary>
        public DateTime? Cksj { get; set; }
        /// <summary>
        /// Tổng số tiền giá nhập
        /// </summary>
        public decimal? jjzje { get; set; }
    }
}
