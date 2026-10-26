using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Chi tiết quyết toán
    /// </summary>
    public class OutpatientSettDetailVO
    {
        /// <summary>
        /// Mã nội bộ chi tiết quyết toán
        /// </summary>
        public int jsmxnm { get; set; }

        /// <summary>
        /// Tên
        /// </summary>
        public string mc { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// Số tiền
        /// </summary>
        public decimal je { get; set; }
    }
}
