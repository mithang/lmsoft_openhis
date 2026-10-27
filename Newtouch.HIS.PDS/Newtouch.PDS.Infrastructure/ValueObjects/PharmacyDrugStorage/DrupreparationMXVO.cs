using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Chi tiết truy vấn chứng từ
    /// </summary>
    public class DrupreparationMXVO
    {
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
        /// Số lượng Số lượng xin
        /// </summary>
        public string sl { get; set; }

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
    }
}
