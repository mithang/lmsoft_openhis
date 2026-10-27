using System;

namespace Newtouch.HIS.Domain.VO
{
    /// <summary>
    /// Thông tin đơn thuốc
    /// </summary>
    public class CfxxVO
    {
        /// <summary>
        /// Số thẻ
        /// </summary>
        public string CardNo { get; set; }
        public string mzh { get; set; }

        /// <summary>
        /// Tên bệnh nhân
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// Mã nội bộ đơn thuốc
        /// </summary>
        public long? cfnm { get; set; }

        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string Fph { get; set; }
        
        /// <summary>
        /// Tên bác sĩ
        /// </summary>
        public string ysmc { get; set; }

        /// <summary>
        /// Tên khoa
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// 0：Chưa xếp；1：Đã xếp；2：Đã phát；3：Đã trả
        /// </summary>
        public string fybz { get; set; }

        /// <summary>
        /// Thời gian thu phí
        /// </summary>
        public DateTime? sfsj { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime? fysj { get; set; }
    }
}