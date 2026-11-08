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
        /// đăng ký khám Phòng khám bảng Khóa chính ID
        /// </summary>
        public int ghzsId { get; set; }
        /// <summary>
        /// đăng ký khám trong mã
        /// </summary>
        public int ghnm { get; set; }
        /// <summary>
        /// Khám bệnh thứ tự số
        /// </summary>
        public Int16 jzxh { get; set; }
        /// <summary>
        /// Tên bệnh nhân
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// đăng ký khám Ngày
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
        /// Bác sĩ Mã nhân viên
        /// </summary>
        public string ys { get; set; }
        /// <summary>
        /// Bác sĩ Tên
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
        /// gọi số Trạng thái
        /// </summary>
        public int? calledstu { get; set; }
		/// <summary>
		/// Phòng khám lầu lớp
		/// </summary>
		public string zslc { get; set; }
		/// <summary>
		/// Phòng khám phòng số
		/// </summary>
		public string zsfh { get; set; }
	}
}
