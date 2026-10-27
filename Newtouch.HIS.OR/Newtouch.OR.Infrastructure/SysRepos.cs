using Newtouch.OR.ManageSystem.Domain.Entity;
using DbConfigHelper;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using Newtouch.OR.ManageSystem.Infrastructure;
using SqlSugar;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.OR.ManageSystem.Repository
{
    /// <summary>
    /// Local SystemManage repos against NewtouchHIS_Base (see ISysRepos.cs).
    /// Sys_GlobalConfig lives in Base, not in the OR database, so all SQL
    /// here uses fully-qualified [NewtouchHIS_Base].. names; the IQueryable()
    /// path uses SqlSugar AS() for the same cross-database mapping.
    /// </summary>
    public class SysConfigRepo : ISysConfigRepo
    {
        private SqlSugarProvider Db => OrDb.GetConnection();

        public SysConfigEntity GetByCode(string code, string orgId)
        {
            return Db.SqlQueryable<SysConfigEntity>(
                "select * from [NewtouchHIS_Base]..Sys_GlobalConfig with(nolock) where Code=@code and zt='1'")
                .AddParameters(new SugarParameter[] { new SugarParameter("@code", code) })
                .First();
        }

        public string GetValueByCode(string code, string orgId)
        {
            return GetByCode(code, orgId)?.Value;
        }

        public IList<SysConfigEntity> GetList(string keyword, string fields)
        {
            var sql = "select * from [NewtouchHIS_Base]..Sys_GlobalConfig with(nolock) where zt='1'";
            var pars = new List<SugarParameter>();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and (Code like @kwd or Name like @kwd)";
                pars.Add(new SugarParameter("@kwd", "%" + keyword.Trim() + "%"));
            }
            return Db.SqlQueryable<SysConfigEntity>(sql).AddParameters(pars.ToArray()).ToList();
        }

        public int Insert(IList<SysConfigEntity> entities)
        {
            if (entities == null || entities.Count == 0)
            {
                return 0;
            }
            // Insert into the Base database via fully-qualified table mapping.
            return Db.Insertable(entities.ToArray())
                .AS(DbNames.Name("BaseDb", "NewtouchHIS_Base") + "..Sys_GlobalConfig")
                .ExecuteCommand();
        }

        public ISugarQueryable<SysConfigEntity> IQueryable()
        {
            return Db.Queryable<SysConfigEntity>().AS(DbNames.Name("BaseDb", "NewtouchHIS_Base") + "..Sys_GlobalConfig");
        }
    }

    public class SysStaffRepo : ISysStaffRepo
    {
        private SqlSugarProvider Db => OrDb.GetConnection();

        public string GetNameByGh(string gh, string orgId)
        {
            if (string.IsNullOrWhiteSpace(gh))
            {
                return null;
            }
            return Db.Ado.GetString(
                "select top 1 Name from [NewtouchHIS_Base]..V_S_Sys_Staff with(nolock) where gh=@gh and OrganizeId=@orgId and zt='1'",
                new SugarParameter("@gh", gh),
                new SugarParameter("@orgId", orgId ?? ""));
        }
    }
}
