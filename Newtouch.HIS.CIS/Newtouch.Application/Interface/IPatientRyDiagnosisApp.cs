using System.Collections.Generic;
using Newtouch.Domain.DTO.OutputDto;
using Newtouch.Domain.ValueObjects.Inpatient;

namespace Newtouch.Application.Interface
{
    /// <summary>
    /// Chẩn đoán nhập viện của bệnh nhân
    /// </summary>
    public interface IPatientRyDiagnosisApp
    {
        /// <summary>
        /// Lấy thông tin chẩn đoán nhập viện
        /// </summary>
        /// <param name="zhy"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<PatientRyDiagnosisDto> PatientRyDiagnosisQuery(string zhy, string organizeId);

        /// <summary>
        /// Lưu chẩn đoán nhập viện
        /// </summary>
        /// <param name="patInAreaVo"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        string SavaInPatDiagnosis(PatInAreaVO patInAreaVo, string organizeId, string userCode);
    }
}