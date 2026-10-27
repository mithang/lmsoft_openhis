using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// Ported from binary FrameworkBase SystemManage ISysDiagnosisRepo.
    /// Surface derived from CIS Web usage only.
    /// </summary>
    public interface ISysDiagnosisRepo
    {
        SysDiagnosisEntity SelectDataByICD10(string organizeId, string icd10);
        IList<SysDiagnosisVEntity> GetList(string orgId, string keyword, string zdlb, string ybnhlx);
    }
}
