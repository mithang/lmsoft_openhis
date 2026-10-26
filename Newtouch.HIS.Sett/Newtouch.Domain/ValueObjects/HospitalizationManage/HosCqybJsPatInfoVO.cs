using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.HospitalizationManage
{
	public class HosCqybJsPatInfoVO
	{
		/// <summary>
		/// Ngày giường nội trú
		/// </summary>
		public string zycr { get; set; }
		/// <summary>
		/// Khoa xuất viện
		/// </summary>
		public string cyks { get; set; }
		/// <summary>
		/// Bác sĩ xuất viện
		/// </summary>
		public string cyys { get; set; }
        /// <summary>
        /// Mã bác sĩ quốc gia khi xuất viện
        /// </summary>
        public string cyysgjbm { get; set; }
        public string cyrq { get; set; }

    }
    public class HosCqybJsPatInfoVO_cr
    {
        /// <summary>
        /// Ngày nhập viện
        /// </summary>
        public DateTime ryrq { get; set; }
        /// <summary>
        /// Ngày xuất viện
        /// </summary>
        public DateTime? cyrq { get; set; }
        /// <summary>
        /// Khoa xuất viện
        /// </summary>
        public string cyks { get; set; }
        /// <summary>
        /// Bác sĩ xuất viện
        /// </summary>
        public string cyys { get; set; }
        /// <summary>
        /// Mã bác sĩ quốc gia khi xuất viện
        /// </summary>
        public string cyysgjbm { get; set; }

    }
}
