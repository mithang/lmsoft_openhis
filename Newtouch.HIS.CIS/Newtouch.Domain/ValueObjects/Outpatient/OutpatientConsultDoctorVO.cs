using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Outpatient
{
    /// <summary>
    /// Bác sĩ phân khám
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
        /// Mã nhân viên bác sĩ
        /// </summary>
        public string gh { get; set; }
        /// <summary>
        /// Họ tên bác sĩ
        /// </summary>
        public string ysxm { get; set; }
		/// <summary>
		/// Số tầng phòng khám
		/// </summary>
		public string zslc { get; set; }
		/// <summary>
		/// Số phòng khám
		/// </summary>
		public string zsfh { get; set; }

	}
}
