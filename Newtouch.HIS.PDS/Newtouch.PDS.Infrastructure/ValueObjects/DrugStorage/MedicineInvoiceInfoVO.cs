using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Thông tin hóa đơn thuốc
    /// </summary>
    public class MedicineInvoiceInfoVO
    {
        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// Ngày lập hóa đơn
        /// </summary>
        public string kprq { get; set; }

        /// <summary>
        /// Nhà cung cấp
        /// </summary>
        public string gysCode { get; set; }

        /// <summary>
        /// Tên nhà cung cấp
        /// </summary>
        public string gysmc { get; set; }
    }
}
