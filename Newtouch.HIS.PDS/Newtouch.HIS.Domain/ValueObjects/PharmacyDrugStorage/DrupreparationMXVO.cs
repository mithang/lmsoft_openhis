using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// tra cứu chứng từ chi tiết
    /// </summary>
    public class DrupreparationMXVO
    {
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
        /// số lượng đề nghị số lượng
        /// </summary>
        public string sl { get; set; }

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
    }
}
