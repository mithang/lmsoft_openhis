using Newtouch.PDS.Requset;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// Quản lý thông tin cơ bản bệnh nhân
    /// </summary>
    public interface IPatientBaseInfoApp
    {
        /// <summary>
        /// Bổ sung thông tin cơ bản bệnh nhân
        /// </summary>
        /// <param name="patientBaseInfo"></param>
        /// <returns></returns>
        string SupplementMzPatientBaseInfo(SupplementPatientBaseInfoRequest patientBaseInfo);
    }
}