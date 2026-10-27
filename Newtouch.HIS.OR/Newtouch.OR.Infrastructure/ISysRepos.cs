using Newtouch.OR.ManageSystem.Domain.Entity;
using SqlSugar;
using System.Collections.Generic;

namespace Newtouch.OR.ManageSystem.Domain.IRepository
{
    /// <summary>
    /// Local replacements for the FrameworkBase binary SystemManage repos
    /// (ISysConfigRepo / ISysStaffRepo, no source). Backed by NewtouchHIS_Base.
    /// Sys_GlobalConfig has NO OrganizeId column (verified in the demo DB),
    /// so the orgId parameter is accepted for signature compatibility but
    /// does not filter.
    /// </summary>
    public interface ISysConfigRepo
    {
        SysConfigEntity GetByCode(string code, string orgId);
        string GetValueByCode(string code, string orgId);
        IList<SysConfigEntity> GetList(string keyword, string fields);
        int Insert(IList<SysConfigEntity> entities);
        ISugarQueryable<SysConfigEntity> IQueryable();
    }

    public interface ISysStaffRepo
    {
        string GetNameByGh(string gh, string orgId);
    }
}
