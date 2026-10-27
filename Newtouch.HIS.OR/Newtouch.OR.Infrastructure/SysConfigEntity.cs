using SqlSugar;
using System;

namespace Newtouch.OR.ManageSystem.Domain.Entity
{
    /// <summary>NewtouchHIS_Base..Sys_GlobalConfig (verified columns in demo DB).</summary>
    [SugarTable("Sys_GlobalConfig")]
    public class SysConfigEntity : IEntityBase
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
        public string Memo { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public int? px { get; set; }
        public string zt { get; set; }
    }
}
