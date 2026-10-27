using System.Collections.Generic;
using Newtouch.CIS.Compat;
using Newtouch.Domain.DTO.OutputDto;
using Newtouch.Domain.ValueObjects.Inpatient;

namespace Newtouch.Application.Interface
{
    /// <summary>
    /// Bệnh nhânChẩn đoán nhập viện
    /// </summary>
    public interface IPatientRyDiagnosisApp
    {
        /// <summary>
        /// LấyChẩn đoán nhập việnthông tin
        /// </summary>
        /// <param name="zhy"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<PatientRyDiagnosisDto> PatientRyDiagnosisQuery(string zhy, string organizeId);

        /// <summary>
        /// LưuChẩn đoán nhập viện
        /// </summary>
        /// <param name="patInAreaVo"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        string SavaInPatDiagnosis(PatInAreaVO patInAreaVo, string organizeId, string userCode);
    }
}