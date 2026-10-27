using System.Collections.Generic;
using Newtouch.Domain.ValueObjects;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.Domain.IDomainServices
{
    /// <summary>
    /// Ported from binary FrameworkBase SystemManage ISysUserDmnService.
    /// Surface derived from CIS Web usage only (staff / duty lookups
    /// against NewtouchHIS_Base). Implementation queries BaseDb.
    /// </summary>
    public interface ISysUserDmnService
    {
        bool CheckStaffIsBelongDuty(string staffId, string duty);
        IList<SysStaffEntity> GetSatffList(string orgId, string keyword);
        IList<SysStaffEntity> GetStaffListByOrg(string orgId);
        IList<StaffDutyVO> GetStaffByDutyCode(string orgId, string dutyCode);
        IList<StaffDutyVO> GetStaffDutyListByOrganizeId(string orgId);
    }
}
