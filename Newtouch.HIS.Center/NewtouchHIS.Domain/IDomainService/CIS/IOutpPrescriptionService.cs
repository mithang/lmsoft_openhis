using NewtouchHIS.Domain.InterfaceObjets.CIS;
using NewtouchHIS.Lib.Base;

namespace NewtouchHIS.Domain.IDomainService.CIS
{
    public interface IOutpPrescriptionService:IScopedDependency
    {
        /// <summary>
        /// LấyBệnh nhânThông tin đơn thuốc trong bệnh án by jzId
        /// </summary>
        /// <param name="jzId"></param>
        /// <returns></returns>
        Task<List<OutpPrescriptionDataVO>> GetPresDatabyJzId(string jzId);
    }
}
