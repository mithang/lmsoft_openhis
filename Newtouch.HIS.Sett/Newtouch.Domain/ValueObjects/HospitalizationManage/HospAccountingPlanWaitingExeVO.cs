using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Chờ thực hiện Ghi sổ nội trú Kế hoạch  VO
    /// </summary>
    public class HospAccountingPlanWaitingExeVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string jzjhId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string jzjhmxId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string patientName { get; set; }

        /// <summary>
        /// Tính chất y lệnh 1Tạm thời 2Dài hạn
        /// </summary>
        public string yzxz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxmmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? sl { get; set; }

        /// <summary>
        /// Đơn giá
        /// </summary>
        public decimal? dj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? zje { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ysmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// Thời gian thực hiện lần cuối
        /// </summary>
        public DateTime? LastEexcutionTime { get; set; }
        public string kflb { get; set; }
        public int? zcs { get; set; }
        public int? yzxcs { get; set; }
        public int? zll { get; set; }
        public string zlsgh { get; set; }
        public int? dtzxcs { get; set; }

        /// <summary>
        /// Thời gian ghi
        /// </summary>
        public DateTime entryTime { get; set; }
		/// <summary>
		/// Khoa thực hiện
		/// </summary>
	    public string zxks { get; set; }
	}
}
