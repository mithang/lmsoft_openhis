using System;

namespace Newtouch.HIS.Domain.VO
{
    /// <summary>
    /// thông tin đơn thuốc
    /// </summary>
    public class CfxxVO
    {
        /// <summary>
        /// số thẻ
        /// </summary>
        public string CardNo { get; set; }
        public string mzh { get; set; }

        /// <summary>
        /// họ tên bệnh nhân
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// mã nội bộ đơn thuốc
        /// </summary>
        public long? cfnm { get; set; }

        /// <summary>
        /// số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// số hóa đơn
        /// </summary>
        public string Fph { get; set; }
        
        /// <summary>
        /// bác sĩ tên
        /// </summary>
        public string ysmc { get; set; }

        /// <summary>
        /// tên khoa
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// 0: chưa xếp; 1: đã xếp; 2: đã phát; 3: đã trả
        /// </summary>
        public string fybz { get; set; }

        /// <summary>
        /// thời gian thu phí
        /// </summary>
        public DateTime? sfsj { get; set; }

        /// <summary>
        /// thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// thời gian tạo
        /// </summary>
        public DateTime? fysj { get; set; }
    }
}