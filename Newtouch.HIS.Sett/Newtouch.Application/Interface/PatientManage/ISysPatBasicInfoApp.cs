using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.PrintDto;
using Newtouch.HIS.Sett.Request;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysPatBasicInfoApp
    {
        /// <summary>
        /// (số hồ sơ bệnh án由hệ thống tự đ ộng tạo)lấy mới nhất số hồ sơ bệnh án
        /// </summary>
        /// <returns></returns>
        string Getblh();

        /// <summary>
        /// LấyTính chất bệnh nhânDanh sách
        /// </summary>
        /// <returns></returns>
        object GetBRXZList(string orgId);

        /// <summary>
        /// InNhập việnThông tin
        /// </summary>
        /// <param name="info"></param>
        void PrinZYInfo(ZYInfoVO info);

        /// <summary>
        /// In腕带
        /// </summary>
        /// <param name="info"></param>
        void PrintWDInfo(WDInfoVO info);

        /// <summary>
        /// SửaChẩn đoán nhập viện
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        void ModifyRyDiagnosis(ModifyRyDiagnosisRequestDTO request);
























        /// <summary>
        /// theo patid lấy thông tin cơ bản bệnh nhân
        /// </summary>
        /// <param name="patid"></param>
        /// <returns></returns>
        SysPatientBasicInfoEntity GetInfoByPatid(string patid);
    }
}
