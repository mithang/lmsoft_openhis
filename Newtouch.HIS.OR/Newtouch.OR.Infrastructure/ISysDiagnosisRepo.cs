using Newtouch.OR.ManageSystem.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.OR.ManageSystem.Domain.IRepository
{
    /// <summary>
    /// Local replacement for the Framework binary ISysDiagnosisRepo
    /// (no source). Backed by NewtouchHIS_Base..V_S_xt_zd (verified columns
    /// and demo rows in the restored database).
    /// </summary>
    public interface ISysDiagnosisRepo
    {
        IList<SysDiagnosisVO> GetList(string orgId, string keyword, string zdlx, string ybnhlx);
    }
}
