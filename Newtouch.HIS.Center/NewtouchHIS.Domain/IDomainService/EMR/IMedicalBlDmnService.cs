using NewtouchHIS.Domain.Entity.EMR;
using NewtouchHIS.Domain.InterfaceObjets;
using NewtouchHIS.Domain.InterfaceObjets.EMR;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.EnumExtend;
using NewtouchHIS.Lib.Base.Model.DRG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Domain.IDomainService.EMR
{
    public interface  IMedicalBlDmnService : IScopedDependency
    {
        /// <summary>
        /// Loại bệnh án
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<MedicalBllxItemVo> MedicalbllxRecord(string orgId, DBEnum db = DBEnum.EmrDb);
        /// <summary>
        /// Mẫu bệnh án
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<MedicalBlMbItemVo> MedicalblmbRecord(string orgId, DBEnum db = DBEnum.EmrDb);
        /// <summary>
        /// Cây loại bệnh án
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<MedicalBllxMbTreeVo> MedicalbllxTreeRecord(string orgId, DBEnum db = DBEnum.EmrDb);
        /// <summary>
        /// Danh sách bệnh nhân nội trú
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<List<MedicalPatientVo>> MedicalCenterPatInfo(string brbz,DateTime ksrq,DateTime jsrq, string srz, string orgId, DBEnum db = DBEnum.EmrDb);
        /// <summary>
        /// Danh sách chẩn đoán
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<List<MedDiagListVo>> MedicalCenterDiaglist(string zyh, string orgId, DBEnum db = DBEnum.EmrDb);
        /// <summary>
        /// Văn bản bệnh ántree
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<List<MedRecordTree>> MedicalCenterBlwsTree(string zyh, string orgId, DBEnum db = DBEnum.EmrDb);
    }
}
