using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Outpatient
{
    /// <summary>
    /// Thông tin bệnh nhân ngoại trú
    /// </summary>
    public class OutHosPatientVO
    {
        /// <summary>
        /// Số nội trú
        /// </summary>
        public string jzId { get; set; }

        /// <summary>
        /// Số bệnh án
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// Họ tên
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public string xb { get; set; }

        /// <summary>
        /// Năm tháng sinh
        /// </summary>
        public DateTime csny { get; set; }

        /// <summary>
        /// Tên khoa
        /// </summary>
        public string ghksmc { get; set; }

        /// <summary>
        /// Khoa
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// Pinyin
        /// </summary>
        public string py { get; set; }
    }
}
