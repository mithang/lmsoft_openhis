using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// tra cứu chứng từ chính bản ghi
    /// </summary>
    public class ReceiptQueryVO
    {
        /// <summary>
        /// loại chứng từ
        /// </summary>
        public int djlx { get; set; }

        /// <summary>
        /// loại chứng từ tên
        /// </summary>
        public string djlxmc { get; set; }

        /// <summary>
        /// bộ phận xuất kho tên
        /// </summary>
        public string ckbmmc { get; set; }

        /// <summary>
        /// bộ phận nhập kho tên
        /// </summary>
        public string rkbmmc { get; set; }

        /// <summary>
        /// tên nhà cung cấp
        /// </summary>
        public string gysmc { get; set; }

        /// <summary>
        /// chứng từ nhập/xuất kho chính bảng ID
        /// </summary>
        public string crkId { get; set; }

        /// <summary>
        /// số chứng từ
        /// </summary>
        public string pdh { get; set; }

        /// <summary>
        /// thời gian thao tác
        /// </summary>
        public DateTime? czsj { get; set; }

        /// <summary>
        /// phương thức nhập/xuất kho tên
        /// </summary>
        public string crkfsmc { get; set; }
        public string crkfscode { get; set; }
        /// <summary>
        /// tổng tiền bán buôn
        /// </summary>
        public decimal? pjze { get; set; }

        /// <summary>
        /// tổng tiền bán lẻ
        /// </summary>
        public decimal? ljze { get; set; }

        /// <summary>
        /// tổng tiền giá nhập
        /// </summary>
        public decimal? zje { get; set; }

        /// <summary>
        /// chênh lệch giá mua bán
        /// </summary>
        public decimal? jxcj { get; set; }

        /// <summary>
        /// trạng thái duyệt
        /// </summary>
        public string shzt { get; set; }

        /// <summary>
        /// số hóa đơn
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// thời gian nhập kho
        /// </summary>
        public DateTime? Rksj { get; set; }

        /// <summary>
        /// thời gian xuất kho
        /// </summary>
        public DateTime? Cksj { get; set; }
        /// <summary>
        /// tổng tiền giá nhập
        /// </summary>
        public decimal? jjzje { get; set; }
    }
}
