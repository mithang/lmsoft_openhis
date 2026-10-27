using System;

namespace Newtouch.HIS.Domain.VO
{
    /// <summary>
    /// Kết quả trả về truy vấn sổ cái
    /// </summary>
    public class StandingBookInventoryDetail
    {
        /// <summary>
        /// Giải thích xuất nhập kho
        /// </summary>
        public string crksm { get;set; }

        /// <summary>
        /// Số lượng nhập kho+Đơn vị
        /// </summary>
        public string rkslanddw { get; set; }

        /// <summary>
        /// Số lượng xuất kho+Đơn vị
        /// </summary>
        public string ckslanddw { get; set; }

        /// <summary>
        /// Số lượng kết chuyển+Đơn vị
        /// </summary>
        public string jzslanddw { get; set; }

        /// <summary>
        /// Số phiếu đơn
        /// </summary>
        public string pzh { get; set; }

        /// <summary>
        /// Khoa
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// Ngày phát sinh
        /// </summary>
        public DateTime fsrq { get; set; }
    }
}
