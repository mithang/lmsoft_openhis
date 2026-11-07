using System.Collections.Generic;
using Newtouch.CIS.Compat;
using Newtouch.Domain.DTO.OutputDto;
using Newtouch.Domain.ValueObjects.Inpatient;

namespace Newtouch.Application.Interface
{
    /// <summary>
    /// Bệnh nhân Chẩn đoán nhập viện
    /// </summary>
    public interface IPatientRyDiagnosisApp
    {
        /// <summary>
        /// Lấy Chẩn đoán nhập việnthông tin
        /// </summary>
        /// <param name="zhy"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<PatientRyDiagnosisDto> PatientRyDiagnosisQuery(string zhy, string organizeId);

        /// <summary>
        /// Lưu Chẩn đoán nhập viện
        /// </summary>
        /// <param name="patInAreaVo"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        string SavaInPatDiagnosis(PatInAreaVO patInAreaVo, string organizeId, string userCode);
    }
}