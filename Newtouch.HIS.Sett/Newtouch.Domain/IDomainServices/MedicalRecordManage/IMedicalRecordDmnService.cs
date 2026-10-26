using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Trang đầu bệnh ánDrgĐo lường theo nhóm
    /// </summary>
    public interface IMedicalRecordDmnService
    {
        /// <summary>
        /// Lấy dữ liệu chẩn đoán trang đầu bệnh án nội trú theo số nhập viện
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        List<MedicalRecordDiagnosisVO> GetAllMedicalRecordDiagnosisVOList(string zyh, string orgId);
        /// <summary>
        /// Lấy dữ liệu phẫu thuật trang đầu bệnh án nội trú theo số nhập viện
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        List<MedicalRecordOperationVO> GetAllMedicalRecordOperationVOList(string zyh, string orgId);
        /// <summary>
        /// Điều chỉnh thứ tự dữ liệu chẩn đoán trang đầu bệnh án
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        string UpdatePatDiagnosisOrder(string zdIds, string orgId);
        /// <summary>
        /// Điều chỉnh thứ tự dữ liệu phẫu thuật trang đầu bệnh án
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        string UpdatePatOperationOrder(string ssIds, string orgId);
        PatInformationVO GetPatInformationList(string zyh, string orgId);

        List<MedicalRecordOperationVO> GetAllOpVOList(string ssmc, string orgId);
        List<MedicalRecordPatVO> GetPatMedicalRecordList(string zyh, string kssj, string jssj, string orgId);
    }

}
