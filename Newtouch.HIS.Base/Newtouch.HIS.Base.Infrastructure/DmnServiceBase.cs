using Newtouch.HIS.Base.Infrastructure;
using Newtouch.HIS.Repository;
using NewtouchHIS.Lib.Base.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// Domain-service base replacing FrameworkBase DmnServiceBase (EF6).
    /// The EF database factory is gone: data access delegates to RepositoryBase&lt;T&gt;
    /// over the SqlSugar BaseDb connection, mirroring the original base-class API
    /// (this.FindList / this.IQueryable / this.QueryWithPage / ...) so ported
    /// services keep compiling unchanged.
    /// </summary>
    public abstract class DmnServiceBase
    {
        protected DmnServiceBase()
        {
        }

        protected ISugarQueryable<T> IQueryable<T>() where T : class, new()
        {
            return new RepositoryBase<T>().IQueryable();
        }

        protected ISugarQueryable<T> IQueryable<T>(Expression<Func<T, bool>> predicate) where T : class, new()
        {
            return new RepositoryBase<T>().IQueryable(predicate);
        }

        protected List<T> FindList<T>() where T : class, new()
        {
            return new RepositoryBase<T>().FindList();
        }

        protected List<T> FindList<T>(Expression<Func<T, bool>> predicate) where T : class, new()
        {
            return new RepositoryBase<T>().FindList(predicate);
        }

        // NOTE: raw-SQL methods are intentionally unconstrained (callers use
        // string/int result types) and run directly on BaseDb, NOT through
        // RepositoryBase<T> (whose TEntity requires class, new()).
        protected List<T> FindList<T>(string sql, SqlParameter[] pars)
        {
            return BaseDb.GetConnection().Ado.SqlQuery<T>(sql, Newtouch.Infrastructure.SugarParamHelper.ToSugarParams(pars));
        }

        protected List<T> FindList<T>(string sql)
        {
            return BaseDb.GetConnection().Ado.SqlQuery<T>(sql);
        }

        protected List<T> FindList<T>(string sql, System.Data.Common.DbParameter[] pars)
        {
            return BaseDb.GetConnection().Ado.SqlQuery<T>(sql, Newtouch.Infrastructure.SugarParamHelper.ToSugarParams(pars));
        }

        protected T FirstOrDefault<T>(string sql)
        {
            var list = BaseDb.GetConnection().Ado.SqlQuery<T>(sql);
            return list.Count > 0 ? list[0] : default;
        }

        protected T FirstOrDefault<T>(string sql, SqlParameter[] pars)
        {
            var list = BaseDb.GetConnection().Ado.SqlQuery<T>(sql, Newtouch.Infrastructure.SugarParamHelper.ToSugarParams(pars));
            return list.Count > 0 ? list[0] : default;
        }

        protected T FirstOrDefault<T>(string sql, System.Data.Common.DbParameter[] pars)
        {
            var list = BaseDb.GetConnection().Ado.SqlQuery<T>(sql, Newtouch.Infrastructure.SugarParamHelper.ToSugarParams(pars));
            return list.Count > 0 ? list[0] : default;
        }

        protected List<T> GetList<T>() where T : class, new()
        {
            return new RepositoryBase<T>().GetList();
        }

        protected List<T> GetList<T>(Expression<Func<T, bool>> predicate) where T : class, new()
        {
            return new RepositoryBase<T>().GetList(predicate);
        }

        protected List<T> GetValidList<T>() where T : class, new()
        {
            return new RepositoryBase<T>().GetValidList();
        }

        protected T FindEntity<T>(object keyValue) where T : class, new()
        {
            return new RepositoryBase<T>().FindEntity(keyValue);
        }

        protected T FindEntity<T>(Expression<Func<T, bool>> predicate) where T : class, new()
        {
            return new RepositoryBase<T>().FindEntity(predicate);
        }

        protected T GetForm<T>(object keyValue) where T : class, new()
        {
            return new RepositoryBase<T>().GetForm(keyValue);
        }

        protected int Insert<T>(T entity) where T : class, new()
        {
            return new RepositoryBase<T>().Insert(entity);
        }

        protected int Insert<T>(List<T> entities) where T : class, new()
        {
            return new RepositoryBase<T>().Insert(entities);
        }

        protected int Update<T>(T entity) where T : class, new()
        {
            return new RepositoryBase<T>().Update(entity);
        }

        protected int Delete<T>(Expression<Func<T, bool>> predicate) where T : class, new()
        {
            return new RepositoryBase<T>().Delete(predicate);
        }

        protected int DeleteForm<T>(object keyValue) where T : class, new()
        {
            return new RepositoryBase<T>().DeleteForm(keyValue);
        }

        protected int SubmitForm<T>(T entity, object keyValue) where T : class, new()
        {
            return new RepositoryBase<T>().SubmitForm(entity, keyValue);
        }

        protected void DetacheEntity<T>(T entity) where T : class, new()
        {
            // No-op: SqlSugar has no change tracking to detach from.
        }

        protected List<TQ> QueryWithPage<TQ>(string sql, Pagination pagination, SqlParameter[] pars)
        {
            return QueryWithPageInternal<TQ>(sql, pagination, Newtouch.Infrastructure.SugarParamHelper.ToSugarParams(pars).ToArray());
        }

        protected List<TQ> QueryWithPage<TQ>(string sql, Pagination pagination, System.Data.Common.DbParameter[] pars)
        {
            return QueryWithPageInternal<TQ>(sql, pagination, Newtouch.Infrastructure.SugarParamHelper.ToSugarParams(pars).ToArray());
        }

        private List<TQ> QueryWithPageInternal<TQ>(string sql, Pagination pagination, SugarParameter[] sugarPars)
        {
            var db = BaseDb.GetConnection();
            var countSql = Regex.Replace(sql, @"\border\s+by\b[\s\S]*$", "", RegexOptions.IgnoreCase).Trim();
            var totalCount = db.Ado.GetInt($"SELECT COUNT(1) FROM ({countSql}) __t", sugarPars);
            var pageIndex = pagination.page ?? 1;
            if (pageIndex <= 0) pageIndex = 1;
            var pageSize = pagination.rows ?? 15;
            if (pageSize <= 0) pageSize = 15;
            var pageSql = sql.Trim();
            if (!Regex.IsMatch(pageSql, @"\border\s+by\b", RegexOptions.IgnoreCase))
            {
                pageSql += " ORDER BY (SELECT NULL)";
            }
            pageSql += $" OFFSET {(pageIndex - 1) * pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY";
            var rows = db.Ado.SqlQuery<TQ>(pageSql, sugarPars);
            pagination.records = totalCount;
            return rows;
        }

        protected int ExecuteSqlCommand(string sql, SqlParameter[] pars = null)
        {
            return Newtouch.Infrastructure.Sql.ExecuteCommand(sql, pars);
        }
    }
}
