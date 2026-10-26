using NewtouchHIS.Domain.Entity.PatientCenter;
using NewtouchHIS.Domain.InterfaceObjets;
using NewtouchHIS.Lib.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NewtouchHIS.Domain.Enum.HisEnum;

namespace NewtouchHIS.Domain.IDomainService.PatientCenter
{
    public interface IPatientInfoDmnService:IScopedDependency
    {
        /// <summary>
        /// truy vấn thông tin cơ bản bệnh nhân（cơ bản）
        /// </summary>
        /// <param name="patIndex"></param>
        /// <param name="brxz">ưu tiên lấy thông tin loại thẻ</param>
        /// <returns></returns>
        Task<SysPatInfoVO> PatientQuery(SysPatIndexVO patIndex, EnumBrxz? brxz);
        /// <summary>
        /// theoPatid truy vấn thông tin bệnh nhân
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<SysPatInfoVO> GetPatientbyPatid(int patid, string orgId);
        /// <summary>
        /// truy vấn thông tin thẻ khám bệnh của bệnh nhân List
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="orgId"></param>
        /// <param name="cardNo"></param>
        /// <param name="cardType"></param>
        /// <returns></returns>
        Task<List<SysPatCardIndexVO>> GetPatientCard(int patid, string orgId, string? cardNo = null, string? cardType = null);
    }
}
