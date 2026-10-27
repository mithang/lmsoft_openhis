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
        /// Số nhập viện
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// bệnh nhânTính chất
        /// </summary>
        public string brxz { get; set; }
        /// <summary>
        /// Tên khoa
        /// </summary>
        public string ksmc { get; set; }
        /// <summary>
        /// Nhập việnBác sĩ
        /// </summary>
        public string zyys { get; set; }
        /// <summary>
        /// chínhđiều trịBác sĩ
        /// </summary>
        public string zzys { get; set; }
        /// <summary>
        /// chínhnhiệmBác sĩ
        /// </summary>
        public string zrys { get; set; }
        /// <summary>
        /// Chẩn đoán nhập viện
        /// </summary>
        public string ryzd { get; set; }
        /// <summary>
        /// Xuất việnChẩn đoán
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
        /// ởviệnCờ
        /// </summary>
        public string zybz { get; set; }
        public List<WMDiagnosisHtmlVO> ryzdList { get; set; }
        public List<WMDiagnosisHtmlVO> cyzdList { get; set; }
    }
}
