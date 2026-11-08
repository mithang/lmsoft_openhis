using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Outpatient
{
    /// <summary>
    /// phút khám Bác sĩ
    /// </summary>
    public class OutpatientConsultDoctorVO
    {
        /// <summary>
        /// Mã phòng khám
        /// </summary>
        public string zsCode { get; set; }
        /// <summary>
        /// Tên phòng khám
        /// </summary>
        public string zsmc { get; set; }
        /// <summary>
        /// Ngày
        /// </summary>
        public DateTime? rq { get; set; }
        /// <summary>
        /// Bác sĩ Mã nhân viên
        /// </summary>
        public string gh { get; set; }
        /// <summary>
        /// Bác sĩ Họ tên
        /// </summary>
        public string ysxm { get; set; }
		/// <summary>
		/// Phòng khám lầu lớp số
		/// </summary>
		public string zslc { get; set; }
		/// <summary>
		/// Phòng khám phòng số
		/// </summary>
		public string zsfh { get; set; }

	}
}
