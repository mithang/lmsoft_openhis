using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.EMR.Domain.IRepository
{
    /// <summary>
    /// Local SqlSugar replacements for the two FrameworkBase-only repository
    /// interfaces the EMR code depends on. In the original, both resolved
    /// via the binary FrameworkBase.MultiOrg.Domain.IRepository namespace;
    /// per the port brief they are re-declared here under
    /// Newtouch.EMR.Domain.IRepository (the namespace EMR code already
    /// imports), with the same member set as the Sett module's definitions
    /// so the EMR web's existing call sites compile unchanged.
    ///
    /// CommonDmnService calls only GetByCode; the EMR web's HomeController
    /// additionally calls GetList and bulk Insert, so the full Sett member
    /// set (including the IRepositoryBase bulk-Insert overload) is kept.
    /// </summary>
    public interface ISysConfigRepo : IRepositoryBase<SysConfigEntity>
    {
        SysConfigEntity GetByCode(string code, string orgId);
        string GetValueByCode(string code, string orgId);
        int GetIntValueByCode(string code, string orgId, int defaultValue = 0);
        bool? GetBoolValueByCode(string code, string orgId, bool? defaultValue = null);
        IList<SysConfigEntity> GetList(string keyword, string organizeId);
        SysConfigEntity GetForm(string keyValue);
        void SubmitForm(SysConfigEntity entity, string keyValue);
    }

    public interface ISysUserRoleRepo : IRepositoryBase<SysUserRoleEntity>
    {
        IList<string> GetRoleIdListByUserId(string userId);
    }
}
