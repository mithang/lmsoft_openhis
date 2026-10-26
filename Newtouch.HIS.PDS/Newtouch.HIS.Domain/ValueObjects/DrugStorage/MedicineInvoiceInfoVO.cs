using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// thuốc hóa đơn thông tin
    /// </summary>
    public class MedicineInvoiceInfoVO
    {
        /// <summary>
        /// số hóa đơn
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// ngày lập hóa đơn
        /// </summary>
        public string kprq { get; set; }

        /// <summary>
        /// nhà cung cấp
        /// </summary>
        public string gysCode { get; set; }

        /// <summary>
        /// tên nhà cung cấp
        /// </summary>
        public string gysmc { get; set; }
    }
}
