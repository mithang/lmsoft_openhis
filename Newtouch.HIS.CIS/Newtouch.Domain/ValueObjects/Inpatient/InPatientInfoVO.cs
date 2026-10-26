using Newtouch.Domain.DTO.InputDto.Inpatient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Inpatient
{
    public class InPatientInfoVO
    {
        /// <summary>
        /// Số nội trú
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// Tính chất bệnh nhân
        /// </summary>
        public string brxz { get; set; }
        /// <summary>
        /// Tên khoa
        /// </summary>
        public string ksmc { get; set; }
        /// <summary>
        /// Bác sĩ nội trú
        /// </summary>
        public string zyys { get; set; }
        /// <summary>
        /// Bác sĩ điều trị chính
        /// </summary>
        public string zzys { get; set; }
        /// <summary>
        /// Bác sĩ chủ nhiệm
        /// </summary>
        public string zrys { get; set; }
        /// <summary>
        /// Chẩn đoán nhập viện
        /// </summary>
        public string ryzd { get; set; }
        /// <summary>
        /// Chẩn đoán xuất viện
        /// </summary>
        public string cyzd { get; set; }
        /// <summary>
        /// Ngày nhập viện
        /// </summary>
        public DateTime? ryrq { get; set; }
        /// <summary>
        /// Ngày xuất viện
        /// </summary>
        public DateTime? cyrq { get; set; }
        /// <summary>
        /// Cờ đang ở viện
        /// </summary>
        public string zybz { get; set; }
        public List<WMDiagnosisHtmlVO> ryzdList { get; set; }
        public List<WMDiagnosisHtmlVO> cyzdList { get; set; }
    }
}
