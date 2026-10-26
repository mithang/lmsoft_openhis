using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// tra cứu chứng từ chi tiết
    /// </summary>
    public class ReceiptQueryDetailVO
    {
        /// <summary>
        /// chứng từ nhập/xuất kho chi tiết
        /// </summary>
        public string crkmxId { get; set; }

        /// <summary>
        /// hạn sử dụng
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        ///số hóa đơn
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// thuốc nhóm lớn mã
        /// </summary>
        public string ypdlCode { get; set; }

        /// <summary>
        /// loại thuốc tên
        /// </summary>
        public string yplbmc { get; set; }

        /// <summary>
        /// phiên âm
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        ///nhà sản xuất
        /// </summary>
        public string sccj { get; set; }

        /// <summary>
        /// đơn vị
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// số lượng đề nghị số lượng
        /// </summary>
        public int? sl { get; set; }

        /// <summary>
        /// số lượng kèm đơn vị
        /// </summary>
        public string slanddw { get; set; }

        /// <summary>
        /// tổng tiền bán buôn
        /// </summary>
        public decimal? pjze { get; set; }

        /// <summary>
        ///tổng tiền bán lẻ
        /// </summary>
        public decimal? ljze { get; set; }

        /// <summary>
        /// tổng tiền giá nhập
        /// </summary>
        public decimal? zje { get; set; }

        /// <summary>
        ///tỷ lệ chiết khấu
        /// </summary>
        public decimal? kl { get; set; }

        /// <summary>
        ///chênh lệch giá mua bán
        /// </summary>
        public decimal? jxcj { get; set; }

        ///// <summary>
        ///// số sổ sách
        ///// </summary>
        //public string zch { get; set; }

        /// <summary>
        /// lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// giá nhập
        /// </summary>
        public decimal? jj { get; set; }

        /// <summary>
        /// giá nhập đơn vị đơn giá
        /// </summary>
        public string jjdwdj { get; set; }

        /// <summary>
        /// lý do trả hàng
        /// </summary>
        public string thyy { get; set; }

        /// <summary>
        /// mã truy xuất
        /// </summary>
        public string zsm { get; set; }

        /// <summary>
        /// có tách lẻ không
        /// </summary>
        public string sfcl { get; set; }
    }
}
