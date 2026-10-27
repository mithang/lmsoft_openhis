using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Outpatient
{
    /// <summary>
    /// phútkhámBác sĩ
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
        /// Bác sĩMã nhân viên
        /// </summary>
        public string gh { get; set; }
        /// <summary>
        /// Bác sĩHọ tên
        /// </summary>
        public string ysxm { get; set; }
		/// <summary>
		/// Phòng khámlầulớpsố
		/// </summary>
		public string zslc { get; set; }
		/// <summary>
		/// Phòng khámphòngsố
		/// </summary>
		public string zsfh { get; set; }

	}
}
