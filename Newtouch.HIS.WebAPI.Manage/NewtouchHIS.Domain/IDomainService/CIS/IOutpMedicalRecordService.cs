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
    /// bệnh án ngoại trú
    /// </summary>
    public interface IOutpMedicalRecordService:IScopedDependency
    {

        /// <summary>
        /// bệnh án ngoại trú bệnh nhân by mzh
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<OutpMedicalRecordVO> OutpMedicalRecordByMzh(string mzh, string orgId);
        /// <summary>
        /// thông tin bệnh án, đơn thuốc bệnh nhân
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<List<OutpPrescriptionDataVO>> OutpPrescriptionDataByMzh(string mzh, string orgId);
    }
}
