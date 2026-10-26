using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// phiếu lĩnh chi tiết tra cứu
    /// </summary>
    public class RequisitionSelectDetailVO
    {
        /// <summary>
        /// tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// ngày hiệu lực
        /// </summary>
        public DateTime? Yxrq { get; set; }

        /// <summary>
        /// số lượng lĩnh
        /// </summary>
        public int slsl { get; set; }

        /// <summary>
        /// đơn vị số lượng lĩnh
        /// </summary>
        public string slsldw { get; set; }

        /// <summary>
        /// số lượng đã phát
        /// </summary>
        public int yfsl { get; set; }

        /// <summary>
        /// số lượng đã phát đơn vị
        /// </summary>
        public string yfsldw { get; set; }
    }
}
