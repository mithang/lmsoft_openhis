using Newtouch.OR.ManageSystem.Infrastructure;
using NewtouchHIS.Lib.Base.Model;
using SqlSugar;
using System;
using System.Collections.Generic;

namespace Newtouch.OR.ManageSystem.DomainServices
{
    /// <summary>
    /// SqlSugar domain-service base. Replaces FrameworkBase DmnServiceBase
    /// (EF6 + Newtouch.Infrastructure.EF, Framework-only, no source).
    /// Same ORDER BY warning as RepositoryBase.QueryWithPage: strip ORDER BY
    /// from raw SQL before paging.
    /// </summary>
    public class DmnServiceBase
    {
        protected SqlSugarProvider Db => OrDb.GetConnection();

        public IList<TRes> FindList<TRes>(string sql, SugarParameter[] pars) where TRes : class, new()
        {
            var q = Db.SqlQueryable<TRes>(sql);
            if (pars != null && pars.Length > 0)
            {
                q = q.AddParameters(pars);
            }
            return q.ToList();
        }

        public IList<TRes> QueryWithPage<TRes>(string sql, Pagination pagination, SugarParameter[] pars) where TRes : class, new()
        {
            int total = 0;
            var q = Db.SqlQueryable<TRes>(sql);
            if (pars != null && pars.Length > 0)
            {
                q = q.AddParameters(pars);
            }
            var page = q.ToPageList(pagination.page ?? 1, pagination.rows ?? 15, ref total);
            pagination.records = total;
            return page;
        }

        public int ExecuteSqlCommand(string sql, SugarParameter[] pars = null)
        {
            return Db.Ado.ExecuteCommand(sql, pars);
        }
    }
}
