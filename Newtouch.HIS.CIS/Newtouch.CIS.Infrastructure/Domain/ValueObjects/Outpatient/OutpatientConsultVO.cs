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
        /// đăng ký khámPhòng khámbảngKhóa chínhID
        /// </summary>
        public int ghzsId { get; set; }
        /// <summary>
        /// đăng ký khámtrongmã
        /// </summary>
        public int ghnm { get; set; }
        /// <summary>
        /// Khám bệnhthứ tựsố
        /// </summary>
        public Int16 jzxh { get; set; }
        /// <summary>
        /// Tên bệnh nhân
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// đăng ký khámNgày
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
        /// Bác sĩMã nhân viên
        /// </summary>
        public string ys { get; set; }
        /// <summary>
        /// Bác sĩTên
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
        /// gọisốTrạng thái
        /// </summary>
        public int? calledstu { get; set; }
		/// <summary>
		/// Phòng khámlầulớp
		/// </summary>
		public string zslc { get; set; }
		/// <summary>
		/// Phòng khámphòngsố
		/// </summary>
		public string zsfh { get; set; }
	}
}
