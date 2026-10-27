using System.Collections.Generic;
using Newtouch.MR.ManageSystem.Domain.DTO;
using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.MR.ManageSystem.Infrastructure;
using SqlSugar;
using Newtouch.MR.ManageSystem.Domain.DTO;

namespace Newtouch.MR.ManageSystem.Domain.IRepository
{
    /// <summary>
    /// System-parameter repository. Replaces FrameworkBase ISysConfigRepo
    /// (Framework-only, no source). Backed by Newtouch_MRMS..Sys_Config.
    /// </summary>
    public interface ISysConfigRepo
    {
        ISugarQueryable<SysConfigEntity> IQueryable();
        SysConfigEntity GetByCode(string code, string orgId);
        IList<SysConfigEntity> GetList(string organizeId, string zt);
        int Insert(IEnumerable<SysConfigEntity> entities);
    }

    /// <summary>
    /// Diagnosis dictionary repository. Replaces FrameworkBase ISysDiagnosisRepo.
    /// Backed by NewtouchHIS_Base..xt_zd.
    /// </summary>
    public interface ISysDiagnosisRepo
    {
        IList<SysZdListDto> GetList(string orgId, string keyword, string zdlx, string ybnhlx);
    }

    /// <summary>
    /// Department repository. Replaces FrameworkBase ISysDepartmentRepo.
    /// Backed by NewtouchHIS_Base..Sys_Department.
    /// </summary>
    public interface ISysDepartmentRepo
    {
        IList<SysDepartmentDto> GetList(string orgId, string zt, string keyword);
    }
}
