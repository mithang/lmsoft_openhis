using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Outpatient
{
    public class OutpatientConsultVO
    {
        /// <summary>
        /// Khóa chính bảng phòng khám đăng ký khámID
        /// </summary>
        public int ghzsId { get; set; }
        /// <summary>
        /// Mã nội bộ đăng ký khám
        /// </summary>
        public int ghnm { get; set; }
        /// <summary>
        /// Số thứ tự khám bệnh
        /// </summary>
        public Int16 jzxh { get; set; }
        /// <summary>
        /// Họ tên bệnh nhân
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// Ngày đăng ký khám
        /// </summary>
        public DateTime ghrq { get; set; }
        /// <summary>
        /// Khoa
        /// </summary>
        public string ksCode { get; set; }
        /// <summary>
        /// Tên khoa
        /// </summary>
        public string ksmc { get; set; }
        /// <summary>
        /// Mã nhân viên bác sĩ
        /// </summary>
        public string ys { get; set; }
        /// <summary>
        /// Tên bác sĩ
        /// </summary>
        public string ysmc { get; set; }
        /// <summary>
        /// Mã phòng khám
        /// </summary>
        public string zsCode { get; set; }
        /// <summary>
        /// Tên phòng khám
        /// </summary>
        public string zsmc { get; set; }
        /// <summary>
        /// Trạng thái gọi số
        /// </summary>
        public int? calledstu { get; set; }
		/// <summary>
		/// Tầng phòng khám
		/// </summary>
		public string zslc { get; set; }
		/// <summary>
		/// Số phòng khám
		/// </summary>
		public string zsfh { get; set; }
	}
}
