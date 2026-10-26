using System;

namespace Newtouch.HIS.Domain.VO
{
    /// <summary>
    /// Kết quả trả về khi truy vấn sổ cái
    /// </summary>
    public class StandingBookInventoryDetail
    {
        /// <summary>
        /// ra nhập kho mô tả
        /// </summary>
        public string crksm { get;set; }

        /// <summary>
        /// số lượng nhập kho+đơn vị
        /// </summary>
        public string rkslanddw { get; set; }

        /// <summary>
        /// số lượng xuất kho+đơn vị
        /// </summary>
        public string ckslanddw { get; set; }

        /// <summary>
        /// số lượng kết chuyển+đơn vị
        /// </summary>
        public string jzslanddw { get; set; }

        /// <summary>
        /// số phiếu
        /// </summary>
        public string pzh { get; set; }

        /// <summary>
        /// khoa
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// xảy ra ngày
        /// </summary>
        public DateTime fsrq { get; set; }
    }
}
