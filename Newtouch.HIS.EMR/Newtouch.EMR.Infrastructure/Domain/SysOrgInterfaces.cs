using SqlSugar;
using System.Collections.Generic;

namespace Newtouch.EMR.Domain.IRepository
{
    /// <summary>
    /// Local SqlSugar replacements for the FrameworkBase-only system repositories
    /// the EMR web depends on (same pattern as ISysConfigRepo in SysInterfaces.cs).
    /// TABLE LOCATIONS (verified via sqlcmd against the live instance):
    /// - Sys_Department / Sys_Staff live in NewtouchHIS_Base (BaseDb), NOT in the
    ///   EMR database, so these repos query BaseDb explicitly instead of the
    ///   EmrDb-bound RepositoryBase.
    /// - There is NO Sys_Diagnosis table in any restored database. The diagnosis
    ///   dictionary in this deployment is Base..xt_zd (columns zdCode/zdmc/icd10/
    ///   py/zt/zdlx/ybnhlx match the GetList parameters exactly), so the
    ///   diagnosis repo reads xt_zd and projects id/text for the bindSelect
    ///   dropdowns that consume it.
    /// </summary>
    public interface ISysDepartmentRepo
    {
        IList<SysDepartmentEntity> GetList(string organizeId, string zt, string keyword);
        SysDepartmentEntity GetEntityByCode(string code, string organizeId);
    }

    public interface ISysStaffRepo
    {
        IList<SysStaffEntity> GetValidStaffListByOrganizeId(string organizeId);
        SysStaffEntity GetValidStaffByGh(string gh, string organizeId);
    }

    public interface ISysDiagnosisRepo
    {
        IList<SysDiagnosisDto> GetList(string organizeId, string keyword, string zdlx, string ybnhlx);
    }

    [SugarTable("Sys_Department")]
    public class SysDepartmentEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string py { get; set; }
        public string zt { get; set; }
        public string ParentId { get; set; }
    }

    [SugarTable("Sys_Staff")]
    public class SysStaffEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        public string gh { get; set; }
        public string Name { get; set; }
        public string py { get; set; }
        public string DepartmentCode { get; set; }
        public string zc { get; set; }
        public string zt { get; set; }
        public string MobilePhone { get; set; }
    }

    [SugarTable("xt_zd")]
    public class SysDiagnosisEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string zdId { get; set; }
        public string OrganizeId { get; set; }
        public string zdCode { get; set; }
        public string zdmc { get; set; }
        public string icd10 { get; set; }
        public string py { get; set; }
        public string zt { get; set; }
        public string zdlx { get; set; }
        public string ybnhlx { get; set; }
    }

    /// <summary>bindSelect dropdown shape: option value = id, label = text.</summary>
    public class SysDiagnosisDto
    {
        public string id { get; set; }
        public string text { get; set; }
        public string zdCode { get; set; }
        public string zdmc { get; set; }
        public string icd10 { get; set; }
        public string py { get; set; }
        public string zdlx { get; set; }
        public string ybnhlx { get; set; }
    }
}
