using System.Collections.Generic;
using System.Linq;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using SqlSugar;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// Repo cấu hình hệ thống trên CSDL Herp (kết nối HerpDb mặc định của
    /// RepositoryBase — bảng Sys_Config thuộc NewtouchHIS_herp, CSDL Base
    /// không có bảng này). Được DI scan tự đăng ký dưới dạng ISysConfigRepo
    /// (quy ước tên lớp SysConfigRepo → I + tên lớp).
    /// </summary>
    public class SysConfigRepo : RepositoryBase<SysConfigEntity>, ISysConfigRepo
    {
        public SysConfigRepo()
        {
        }

        public IList<SysConfigEntity> GetList(string keyWord, string organizeId)
        {
            const string sql = @"SELECT Id, OrganizeId, TopOrganizeId, Code, Name, Value, Memo,
                    CreateTime, CreatorCode, LastModifyTime, LastModifierCode, zt, px
                FROM dbo.Sys_Config (NOLOCK)
                WHERE OrganizeId = @orgId
                    AND (@keyword = '' OR Code LIKE @keywordLike OR Name LIKE @keywordLike)
                ORDER BY px, Code";
            var pars = new SugarParameter[]
            {
                new SugarParameter("@orgId", organizeId ?? ""),
                new SugarParameter("@keyword", keyWord ?? ""),
                new SugarParameter("@keywordLike", "%" + (keyWord ?? "") + "%"),
            };
            return FindList<SysConfigEntity>(sql, pars);
        }

        public int GetIntValue(string code, string organizeId)
        {
            var row = IQueryable()
                .Where(p => p.Code == code && p.OrganizeId == organizeId)
                .First();
            if (row == null)
            {
                row = IQueryable()
                    .Where(p => p.Code == code && p.OrganizeId == "*")
                    .First();
            }
            if (row == null || !int.TryParse(row.Value, out var value))
            {
                return 0;
            }
            return value;
        }
    }
}
