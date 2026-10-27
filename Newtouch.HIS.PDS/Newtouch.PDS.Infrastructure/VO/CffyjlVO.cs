using System;

namespace Newtouch.HIS.Domain.VO
{
    /// <summary>
    /// Bản ghi phát thuốc đơn thuốc
    /// </summary>
    public class CffyjlVO
    {
        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// Mã nội bộ đơn thuốc
        /// </summary>
        public long cfnm { get; set; }

        /// <summary>
        /// Cờ phát thuốc 0：Chưa xếp；1：Đã xếp；2：Đã phát；3：Đã trả
        /// </summary>
        public string fybz { get; set; }

        /// <summary>
        /// Thời gian phát thuốc
        /// </summary>
        public DateTime? fysj { get; set; }

        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string Fph { get; set; }

        /// <summary>
        /// Tên bệnh nhân
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// Số thẻ
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// Tuổi
        /// </summary>
        public int? nl { get; set; }

        /// <summary>
        /// Mô tả tính chất bệnh nhân
        /// </summary>
        public string brxzmc { get; set; }

        /// <summary>
        /// Nhân viên phát thuốc
        /// </summary>
        public string fyry { get; set; }
    }
}