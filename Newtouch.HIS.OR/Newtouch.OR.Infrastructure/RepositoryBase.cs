using Newtouch.OR.ManageSystem.Domain.IRepository;
using Newtouch.OR.ManageSystem.Infrastructure;
using NewtouchHIS.Lib.Base.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Newtouch.OR.ManageSystem.Repository
{
    /// <summary>
    /// SqlSugar generic repository. Replaces FrameworkBase RepositoryBase&lt;T&gt;
    /// (EF6 + Newtouch.Infrastructure.EF, Framework-only, no source).
    /// NOTE: ORDER BY must never be part of the raw SQL text passed to
    /// QueryWithPage: SqlSugar wraps the SQL in a derived table for the COUNT
    /// query, where ORDER BY is illegal. Order via OrderBy() on the queryable
    /// (callers must strip ORDER BY from their SQL before paging).
    /// </summary>
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class, new()
    {
        protected SqlSugarProvider Db => OrDb.GetConnection();

        public ISugarQueryable<T> IQueryable()
        {
            return Db.Queryable<T>();
        }

        public ISugarQueryable<T> IQueryable(Expression<Func<T, bool>> predicate)
        {
            return Db.Queryable<T>().Where(predicate);
        }

        public T FindEntity(string keyValue)
        {
            return Db.Queryable<T>().InSingle(keyValue);
        }

        public T FindEntity(Expression<Func<T, bool>> predicate)
        {
            return Db.Queryable<T>().Where(predicate).First();
        }

        public int ExecuteSqlCommand(string sql, SugarParameter[] pars = null)
        {
            return Db.Ado.ExecuteCommand(sql, pars);
        }

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

        public int Insert(T entity)
        {
            return Db.Insertable(entity).ExecuteCommand();
        }

        public int Update(T entity)
        {
            return Db.Updateable(entity).ExecuteCommand();
        }

        public int Delete(Expression<Func<T, bool>> predicate)
        {
            return Db.Deleteable<T>().Where(predicate).ExecuteCommand();
        }
    }
}
