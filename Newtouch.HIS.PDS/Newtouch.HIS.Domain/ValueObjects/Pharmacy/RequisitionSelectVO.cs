using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// phiếu lĩnh tra cứu
    /// </summary>
    public class RequisitionSelectVO
    {
        /// <summary>
        /// phiếu lĩnh Id
        /// </summary>
        public string sldId { get; set; }

        /// <summary>
        /// số phiếu lĩnh
        /// </summary>
        public string Sldh { get; set; }

        /// <summary>
        /// bộ phận lĩnh tên
        /// </summary>
        public string slbmmc { get; set; }

        /// <summary>
        /// bộ phận xuất kho tên
        /// </summary>
        public string ckbmmc { get; set; }

        /// <summary>
        /// thời gian lĩnh
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// trạng thái cấp phát
        /// </summary>
        public int ffzt { get; set; }

    }
}
