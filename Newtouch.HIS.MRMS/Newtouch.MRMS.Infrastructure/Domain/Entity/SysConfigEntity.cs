using System.Collections.Generic;
using SqlSugar;
using Newtouch.MR.ManageSystem.Infrastructure;

namespace Newtouch.MR.ManageSystem.Domain.Entity
{
    /// <summary>System parameter row (Newtouch_MRMS..Sys_Config).</summary>
    [SugarTable("Sys_Config")]
    public class SysConfigEntity : Newtouch.MR.ManageSystem.Infrastructure.EntityBase
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Memo { get; set; }
        public System.DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public System.DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public string zt { get; set; }
        public int px { get; set; }
    }
}
