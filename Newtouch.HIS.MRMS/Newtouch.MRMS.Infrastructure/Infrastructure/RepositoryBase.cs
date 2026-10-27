using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NewtouchHIS.Lib.Base.Model;
using SqlSugar;

namespace Newtouch.MR.ManageSystem.Infrastructure
{
    /// <summary>
    /// Generic repository contract. Replaces FrameworkBase IRepositoryBase&lt;T&gt;
    /// (Newtouch.Infrastructure.EF, Framework-only, no source).
    /// </summary>
    public interface IRepositoryBase<T> where T : class, new()
    {
        T FindEntity(object keyValue);
        T FindEntity(Expression<Func<T, bool>> predicate);
        int Update(T entity);
        int Insert(T entity);
        int Delete(Expression<Func<T, bool>> predicate);
        ISugarQueryable<T> IQueryable();
        ISugarQueryable<T> IQueryable(Expression<Func<T, bool>> predicate);
        List<TRes> FindList<TRes>(string sql, params SugarParameter[] pars) where TRes : class, new();
        List<T> FindScalarList<T>(string sql, params SugarParameter[] pars);
        List<T> FindList(Expression<Func<T, bool>> predicate, Pagination pagination);
        List<TRes> QueryWithPage<TRes>(string sql, Pagination pagination, params SugarParameter[] pars) where TRes : class, new();
        int ExecuteSqlCommand(string sql, params SugarParameter[] pars);
    }

    /// <summary>Constrained SqlQueryable helper (SqlSugar requires a class type).</summary>
    internal static class SqlListHelper
    {
        public static List<T> SqlList<T>(SqlSugarProvider db, string sql, SugarParameter[] pars) where T : class, new()
        {
            var q = db.SqlQueryable<T>(sql);
            if (pars != null && pars.Length > 0) q = q.AddParameters(pars);
            return q.ToList();
        }
    }

    /// <summary>
    /// SqlSugar generic repository. Replaces FrameworkBase RepositoryBase&lt;T&gt; (EF6).
    /// Paging follows the MRQC pilot: ORDER BY is stripped from raw SQL for the
    /// COUNT query and re-applied from pagination.sidx/sord (validated against
    /// the result type's properties) because ORDER BY inside raw SQL breaks
    /// ToPageList's COUNT.
    /// </summary>
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class, new()
    {
        protected SqlSugarProvider Db => MrmsDb.GetConnection();

        public T FindEntity(object keyValue)
        {
            return Db.Queryable<T>().Where("Id=@id", new SugarParameter("@id", keyValue)).First();
        }

        public T FindEntity(Expression<Func<T, bool>> predicate)
        {
            return Db.Queryable<T>().First(predicate);
        }

        public int Update(T entity)
        {
            return Db.Updateable(entity).ExecuteCommand();
        }

        public int Insert(T entity)
        {
            return Db.Insertable(entity).ExecuteCommand();
        }

        public int Delete(Expression<Func<T, bool>> predicate)
        {
            return Db.Deleteable<T>().Where(predicate).ExecuteCommand();
        }

        public ISugarQueryable<T> IQueryable()
        {
            return Db.Queryable<T>();
        }

        public ISugarQueryable<T> IQueryable(Expression<Func<T, bool>> predicate)
        {
            return Db.Queryable<T>().Where(predicate);
        }

        public List<TRes> FindList<TRes>(string sql, params SugarParameter[] pars) where TRes : class, new()
        {
            return SqlListHelper.SqlList<TRes>(Db, sql, pars);
        }

        /// <summary>Scalar (value-type) SQL list, via Ado — SqlQueryable requires a class type.</summary>
        public List<T> FindScalarList<T>(string sql, params SugarParameter[] pars)
        {
            return Db.Ado.SqlQuery<T>(sql, pars);
        }

        public List<T> FindList(Expression<Func<T, bool>> predicate, Pagination pagination)
        {
            int total = 0;
            var list = Db.Queryable<T>().Where(predicate)
                .ToPageList(pagination.page ?? 1, pagination.rows ?? 15, ref total);
            pagination.records = total;
            return list;
        }

        public List<TRes> QueryWithPage<TRes>(string sql, Pagination pagination, params SugarParameter[] pars) where TRes : class, new()
        {
            string cleanSql = StripOrderBy(sql);
            var q = Db.SqlQueryable<TRes>(cleanSql);
            if (pars != null && pars.Length > 0) q = q.AddParameters(pars);
            string sortCol = null;
            if (!string.IsNullOrWhiteSpace(pagination.sidx))
            {
                var first = pagination.sidx.Trim().Split(new[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(first) &&
                    typeof(TRes).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        .Any(p => string.Equals(p.Name, first, StringComparison.OrdinalIgnoreCase)))
                {
                    sortCol = first;
                }
            }
            if (sortCol != null)
            {
                string dir = string.Equals(pagination.sord, "desc", StringComparison.OrdinalIgnoreCase) ? "desc" : "asc";
                q = q.OrderBy($"{sortCol} {dir}");
            }
            int total = 0;
            var list = q.ToPageList(pagination.page ?? 1, pagination.rows ?? 15, ref total);
            pagination.records = total;
            return list;
        }

        public int ExecuteSqlCommand(string sql, params SugarParameter[] pars)
        {
            return Db.Ado.ExecuteCommand(sql, pars);
        }

        internal static string StripOrderBy(string sql)
        {
            if (string.IsNullOrWhiteSpace(sql)) return sql;
            int idx = sql.ToLowerInvariant().LastIndexOf("order by", StringComparison.Ordinal);
            if (idx < 0) return sql;
            // do not strip an ORDER BY that lives inside parentheses (subquery)
            string tail = sql.Substring(idx);
            if (tail.Contains(")")) return sql;
            return sql.Substring(0, idx);
        }
    }

    /// <summary>
    /// Domain-service base. Replaces FrameworkBase DmnServiceBase (EF6).
    /// </summary>
    public class DmnServiceBase
    {
        protected SqlSugarProvider Db => MrmsDb.GetConnection();

        public List<T> FindList<T>(string sql, params SugarParameter[] pars) where T : class, new()
        {
            return SqlListHelper.SqlList<T>(Db, sql, pars);
        }

        /// <summary>Scalar (value-type) SQL list, via Ado — SqlQueryable requires a class type.</summary>
        public List<T> FindScalarList<T>(string sql, params SugarParameter[] pars)
        {
            return Db.Ado.SqlQuery<T>(sql, pars);
        }

        public List<TRes> QueryWithPage<TRes>(string sql, Pagination pagination, params SugarParameter[] pars) where TRes : class, new()
        {
            // reuse the repository paging implementation (same SQL semantics)
            var repo = new RepositoryBase<TRes>();
            return repo.QueryWithPage<TRes>(sql, pagination, pars);
        }

        public T FirstOrDefault<T>(string sql, params SugarParameter[] pars) where T : class, new()
        {
            var q = Db.SqlQueryable<T>(sql);
            if (pars != null && pars.Length > 0) q = q.AddParameters(pars);
            return q.First();
        }

        public T FirstOrDefault<T>(Expression<Func<T, bool>> predicate) where T : class, new()
        {
            return Db.Queryable<T>().First(predicate);
        }

        public int ExecuteSqlCommand(string sql, params SugarParameter[] pars)
        {
            return Db.Ado.ExecuteCommand(sql, pars);
        }

        public DataTable SqlQueryForDataTatable(string sql, params SugarParameter[] pars)
        {
            return Db.Ado.GetDataTable(sql, pars);
        }
    }
}
