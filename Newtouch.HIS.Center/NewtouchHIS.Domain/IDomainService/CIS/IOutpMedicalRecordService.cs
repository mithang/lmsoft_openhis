using NewtouchHIS.Domain.InterfaceObjets.CIS;
using NewtouchHIS.Lib.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Domain.IDomainService.CIS
{
    /// <summary>
    /// Bệnh án ngoại trú
    /// </summary>
    public interface IOutpMedicalRecordService:IScopedDependency
    {

        /// <summary>
        /// Bệnh nhânBệnh án ngoại trú by mzh
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<OutpMedicalRecordVO> OutpMedicalRecordByMzh(string mzh, string orgId);
        /// <summary>
        /// Bệnh nhânThông tin đơn thuốc trong bệnh án
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<List<OutpPrescriptionDataVO>> OutpPrescriptionDataByMzh(string mzh, string orgId);
    }
}
