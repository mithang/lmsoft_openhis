using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Đơn lĩnh Chi tiết Truy vấn
    /// </summary>
    public class RequisitionSelectDetailVO
    {
        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Ngày hiệu lực
        /// </summary>
        public DateTime? Yxrq { get; set; }

        /// <summary>
        /// Số lượng lĩnh
        /// </summary>
        public int slsl { get; set; }

        /// <summary>
        /// Đơn vị Số lượng lĩnh
        /// </summary>
        public string slsldw { get; set; }

        /// <summary>
        /// Số lượng đã phát
        /// </summary>
        public int yfsl { get; set; }

        /// <summary>
        /// Đơn vị Số lượng đã phát
        /// </summary>
        public string yfsldw { get; set; }
    }
}
