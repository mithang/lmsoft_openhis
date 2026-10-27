using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Đơn lĩnh Truy vấn
    /// </summary>
    public class RequisitionSelectVO
    {
        /// <summary>
        /// Đơn lĩnhId
        /// </summary>
        public string sldId { get; set; }

        /// <summary>
        /// Số đơn lĩnh
        /// </summary>
        public string Sldh { get; set; }

        /// <summary>
        /// Tên bộ phận lĩnh
        /// </summary>
        public string slbmmc { get; set; }

        /// <summary>
        /// Tên bộ phận xuất kho
        /// </summary>
        public string ckbmmc { get; set; }

        /// <summary>
        /// Thời gian lĩnh
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Trạng thái phát
        /// </summary>
        public int ffzt { get; set; }

    }
}
