using Newtouch.HIS.API.Common;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Sett.Request.Patient
{
    /// <summary>
    /// 
    /// </summary>
    public class OutPatientUpdateDiagnosisRequest : RequestBase
    {
        /// <summary>
        /// Số khám ngoại trú
        /// </summary>
        [Required]
        public string outpatientNo { get; set; }

        /// <summary>
        /// Chẩn đoánicd10
        /// </summary>
        [Required]
        public string zdicd10 { get; set; }

        /// <summary>
        /// Chẩn đoánTên
        /// </summary>
        [Required]
        public string zdmc { get; set; }
    }
}
