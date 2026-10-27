using System.Linq;
using Newtouch.MRQC.Infrastructure;
using SqlSugar;

namespace Newtouch.MRQC.API.Services
{
    /// <summary>
    /// Hàng Sys_Config của Newtouch_MRQC — cùng 12 cột với SysConfigEntity của MRMS
    /// (đã đối chiếu INFORMATION_SCHEMA.COLUMNS của Newtouch_MRQC.dbo.Sys_Config).
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
        public System.DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public System.DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public string zt { get; set; }
        public int px { get; set; }
    }

    public interface ISysConfigRepo
    {
        ISugarQueryable<SysConfigEntity> IQueryable();
    }

    /// <summary>
    /// Repo Sys_Config riêng của API. MRQC Infrastructure của Web không có SysConfigRepo
    /// (chỉ có MrApplyRepo/QcItemDataRepo) nên API tự mang repo này thay vì sửa Infra
    /// của Web đang chạy — tránh mọi rủi ro chạm vào project của Web.
    /// </summary>
    public class SysConfigRepo : ISysConfigRepo
    {
        public ISugarQueryable<SysConfigEntity> IQueryable()
        {
            return MrqcDb.GetConnection().Queryable<SysConfigEntity>();
        }
    }
}
