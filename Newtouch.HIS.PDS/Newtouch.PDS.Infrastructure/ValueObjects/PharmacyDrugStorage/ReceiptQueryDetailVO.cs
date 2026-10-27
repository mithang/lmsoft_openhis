using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Chi tiết truy vấn chứng từ
    /// </summary>
    public class ReceiptQueryDetailVO
    {
        /// <summary>
        /// Chi tiết chứng từ xuất nhập kho
        /// </summary>
        public string crkmxId { get; set; }

        /// <summary>
        /// Hạn dùng
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        ///Số hóa đơn
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// Mã loại lớn thuốc
        /// </summary>
        public string ypdlCode { get; set; }

        /// <summary>
        /// Tên loại thuốc
        /// </summary>
        public string yplbmc { get; set; }

        /// <summary>
        /// Phiên âm
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// Quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        ///Nhà sản xuất
        /// </summary>
        public string sccj { get; set; }

        /// <summary>
        /// Đơn vị
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// Số lượng Số lượng xin
        /// </summary>
        public int? sl { get; set; }

        /// <summary>
        /// Số lượng kèm đơn vị
        /// </summary>
        public string slanddw { get; set; }

        /// <summary>
        /// Tổng giá bán buôn
        /// </summary>
        public decimal? pjze { get; set; }

        /// <summary>
        ///Tổng giá bán lẻ
        /// </summary>
        public decimal? ljze { get; set; }

        /// <summary>
        /// Tổng giá nhập
        /// </summary>
        public decimal? zje { get; set; }

        /// <summary>
        ///Tỷ lệ khấu trừ
        /// </summary>
        public decimal? kl { get; set; }

        /// <summary>
        ///Chênh lệch giá nhập bán
        /// </summary>
        public decimal? jxcj { get; set; }

        ///// <summary>
        ///// Số sổ sách
        ///// </summary>
        //public string zch { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// Giá nhập
        /// </summary>
        public decimal? jj { get; set; }

        /// <summary>
        /// Đơn giá đơn vị giá nhập
        /// </summary>
        public string jjdwdj { get; set; }

        /// <summary>
        /// Lý do trả hàng
        /// </summary>
        public string thyy { get; set; }

        /// <summary>
        /// Mã truy xuất
        /// </summary>
        public string zsm { get; set; }

        /// <summary>
        /// Có chia lẻ không
        /// </summary>
        public string sfcl { get; set; }
    }
}
