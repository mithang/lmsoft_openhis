using SqlSugar;
using System;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// User-role link row. Shape copied from the Sett module's
    /// SysUserRoleEntity, with one local correction: the Sett definition
    /// carries an OrganizeId property, but Newtouch_EMR.dbo.Sys_UserRole
    /// (verified via INFORMATION_SCHEMA) has only 9 columns and NO
    /// OrganizeId, so that property is marked IsIgnore here — otherwise
    /// every Insert/Update through this entity would fail with an
    /// invalid-column error. The Sett DB's own Sys_UserRole may carry the
    /// column; this classlib targets the EMR database.
    /// </summary>
    [SugarTable("Sys_UserRole")]
    public class SysUserRoleEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public int? px { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public string zt { get; set; }
        [SugarColumn(IsIgnore = true)]
        public string OrganizeId { get; set; }
    }
}
