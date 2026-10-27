using NewtouchHIS.Base.Domain.InterfaceObjets;
using NewtouchHIS.Base.Domain.InterfaceObjets.DRG;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Enum;

namespace NewtouchHIS.Base.Domain.IDomainService.EMR
{
    public interface IMedicalHomeRDmnService: IScopedDependency
    {
        /// <summary>
        /// Truy vấnBệnh nhânTrang đầu bệnh án
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<MedicalHomeVO> MedicalHomeQuery(string zyh, string orgId, DBEnum db = DBEnum.EmrDb);
        /// <summary>
        /// Bệnh nhânTrang đầu bệnh án(DRGĐịnh dạng phân nhóm)
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<DrgMedicalRecord> MedicalHomeRecordFormtDrg(string zyh, string orgId, DBEnum db = DBEnum.EmrDb);
    }
}
