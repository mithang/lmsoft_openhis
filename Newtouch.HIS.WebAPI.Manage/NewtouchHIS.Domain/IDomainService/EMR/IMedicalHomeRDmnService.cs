using NewtouchHIS.Domain.InterfaceObjets;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.EnumExtend;
using NewtouchHIS.Lib.Base.Model.DRG;

namespace NewtouchHIS.Domain.IDomainService.EMR
{
    public interface IMedicalHomeRDmnService: IScopedDependency
    {
        /// <summary>
        /// truy vấn trang đầu bệnh án bệnh nhân
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<MedicalHomeVO> MedicalHomeQuery(string zyh, string orgId, DBEnum db = DBEnum.EmrDb);
        /// <summary>
        /// trang đầu bệnh án bệnh nhân（DRGđịnh dạng nhóm）
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<DrgMedicalRecord> MedicalHomeRecordFormtDrg(string zyh, string orgId, DBEnum db = DBEnum.EmrDb);
    }
}
