using NewtouchHIS.Domain.InterfaceObjets.CIS;
using NewtouchHIS.Lib.Base;

namespace NewtouchHIS.Domain.IDomainService.CIS
{
    public interface IOutpPrescriptionService:IScopedDependency
    {
        /// <summary>
        /// lấy thông tin bệnh án, đơn thuốc của bệnh nhân by jzId
        /// </summary>
        /// <param name="jzId"></param>
        /// <returns></returns>
        Task<List<OutpPrescriptionDataVO>> GetPresDatabyJzId(string jzId);
    }
}
