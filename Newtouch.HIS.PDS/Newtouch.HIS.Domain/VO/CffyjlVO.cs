using System;

namespace Newtouch.HIS.Domain.VO
{
    /// <summary>
    /// đơn thuốc phát thuốc bản ghi
    /// </summary>
    public class CffyjlVO
    {
        /// <summary>
        /// số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// mã nội bộ đơn thuốc
        /// </summary>
        public long cfnm { get; set; }

        /// <summary>
        /// cờ phát thuốc 0: chưa xếp; 1: đã xếp; 2: đã phát; 3: đã trả
        /// </summary>
        public string fybz { get; set; }

        /// <summary>
        /// thời gian phát thuốc
        /// </summary>
        public DateTime? fysj { get; set; }

        /// <summary>
        /// số hóa đơn
        /// </summary>
        public string Fph { get; set; }

        /// <summary>
        /// họ tên bệnh nhân
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// số thẻ
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// tuổi
        /// </summary>
        public int? nl { get; set; }

        /// <summary>
        /// tính chất bệnh nhân mô tả
        /// </summary>
        public string brxzmc { get; set; }

        /// <summary>
        /// phát thuốc nhân viên
        /// </summary>
        public string fyry { get; set; }
    }
}