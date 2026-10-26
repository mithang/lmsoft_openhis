using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Bản ghi quyết toán quyết toán trước khi nợ phí
    /// </summary>
    public class OutpatientSettArrearageVO
    {
        /// <summary>
        /// Mã nội bộ quyết toán
        /// </summary>
        public int jsnm { get; set; }

        /// <summary>
        /// Ngày thao tác quyết toán trước khi nợ phí
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Tổng số tiền
        /// </summary>
        public decimal zje { get; set; }
    }
}
