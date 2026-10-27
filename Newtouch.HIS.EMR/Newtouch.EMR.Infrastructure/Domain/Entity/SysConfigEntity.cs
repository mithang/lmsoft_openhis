using SqlSugar;
using System;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// System config row. Shape copied verbatim from the Sett module's
    /// SysConfigEntity (the FrameworkBase-era definition EMR code relied
    /// on). Verified against the live database: Newtouch_EMR.dbo.Sys_Config
    /// has exactly these 12 columns (Id, OrganizeId, Code, Name, Value,
    /// Memo, CreateTime, CreatorCode, LastModifyTime, LastModifierCode,
    /// zt, px).
    /// </summary>
    [SugarTable("Sys_Config")]
    public class SysConfigEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Memo { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public string zt { get; set; }
        public int? px { get; set; }
    }
}
