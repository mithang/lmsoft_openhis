using Newtouch.HIS.Base.Infrastructure;
using Newtouch.HIS.Repository;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Base.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// SqlSugar generic repository replacing FrameworkBase RepositoryBase&lt;T&gt;
    /// (EF6 + Newtouch.Infrastructure.EF, Framework-only, no source).
    /// Method names/signatures intentionally mirror the original base so ported
    /// repository classes keep compiling with minimal changes.
    /// </summary>
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class, new()
    {
        protected SqlSugarProvider Db => BaseDb.GetConnection();

        public ISugarQueryable<TEntity> IQueryable()
        {
            return Db.Queryable<TEntity>();
        }

        public ISugarQueryable<TEntity> IQueryable(Expression<Func<TEntity, bool>> predicate)
        {
            return Db.Queryable<TEntity>().Where(predicate);
        }

        public List<TEntity> FindList()
        {
            return Db.Queryable<TEntity>().ToList();
        }

        public List<TEntity> FindList(Expression<Func<TEntity, bool>> predicate)
        {
            return Db.Queryable<TEntity>().Where(predicate).ToList();
        }

        public List<TEntity> GetList()
        {
            return Db.Queryable<TEntity>().ToList();
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
        {
            return Db.Queryable<TEntity>().Where(predicate).ToList();
        }

        public List<TEntity> GetValidList()
        {
            return Db.Queryable<TEntity>().Where("zt = '1'").ToList();
        }

        public TEntity FindEntity(object keyValue)
        {
            var pk = FindPkProperty();
            if (pk == null)
            {
                return null;
            }
            return Db.Queryable<TEntity>().Where($"{pk.Name} = @pk", new SugarParameter("@pk", keyValue)).FirstOrDefault();
        }

        public TEntity FindEntity(Expression<Func<TEntity, bool>> predicate)
        {
            return Db.Queryable<TEntity>().Where(predicate).FirstOrDefault();
        }

        public TEntity GetForm(object keyValue)
        {
            return FindEntity(keyValue);
        }

        public int Insert(TEntity entity)
        {
            return Db.Insertable(entity).ExecuteCommand();
        }

        public int Insert(List<TEntity> entities)
        {
            return Db.Insertable(entities).ExecuteCommand();
        }

        public int Delete(Expression<Func<TEntity, bool>> predicate)
        {
            return Db.Deleteable<TEntity>().Where(predicate).ExecuteCommand();
        }

        public int Delete(TEntity entity)
        {
            return Db.Deleteable(entity).ExecuteCommand();
        }

        public List<TEntity> FindList(Expression<Func<TEntity, bool>> predicate, Pagination pagination)
        {
            var pageIndex = pagination.page ?? 1;
            if (pageIndex <= 0) pageIndex = 1;
            var pageSize = pagination.rows ?? 15;
            if (pageSize <= 0) pageSize = 15;
            var total = 0;
            var rows = Db.Queryable<TEntity>().Where(predicate).ToPageList(pageIndex, pageSize, ref total);
            pagination.records = total;
            return rows;
        }

        public int DeleteForm(object keyValue)
        {
            var pk = FindPkProperty();
            if (pk == null)
            {
                return 0;
            }
            return Db.Deleteable<TEntity>().Where($"{pk.Name} = @pk", new SugarParameter("@pk", keyValue)).ExecuteCommand();
        }

        public virtual int Update(TEntity entity, IEnumerable<string> dstnFieldNameList = null, IEnumerable<string> ignoreFieldNameList = null, bool attach = true)
        {
            var up = Db.Updateable(entity);
            var ignore = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "CreatorCode", "CreateTime", "CreatorName" };
            if (dstnFieldNameList != null && dstnFieldNameList.Any())
            {
                var cols = dstnFieldNameList.Where(c => !ignore.Contains(c)).ToList();
                up = up.UpdateColumns(cols.ToArray());
            }
            else if (ignoreFieldNameList != null && ignoreFieldNameList.Any())
            {
                foreach (var c in ignoreFieldNameList)
                {
                    ignore.Add(c);
                }
                up = up.IgnoreColumns(ignore.ToArray());
            }
            else
            {
                up = up.IgnoreColumns(ignore.ToArray());
            }
            return up.ExecuteCommand();
        }

        public int SubmitForm(TEntity entity, object keyValue)
        {
            if (entity is Newtouch.HIS.Domain.EntityBase eb)
            {
                if (keyValue == null || string.IsNullOrWhiteSpace(keyValue.ToString()))
                {
                    eb.Create(true);
                    return Insert(entity);
                }
                eb.Modify(keyValue);
                return Update(entity);
            }
            if (keyValue == null || string.IsNullOrWhiteSpace(keyValue.ToString()))
            {
                return Insert(entity);
            }
            return Update(entity);
        }

        public void DetacheEntity(TEntity entity)
        {
            // No-op: SqlSugar has no change tracking to detach from.
        }

        public static List<SugarParameter> ToSugarParams(SqlParameter[] pars)
        {
            var list = new List<SugarParameter>();
            if (pars == null)
            {
                return list;
            }
            foreach (var p in pars)
            {
                list.Add(new SugarParameter(p.ParameterName, p.Value ?? DBNull.Value));
            }
            return list;
        }

        /// <summary>
        /// Raw-SQL paged query (original FrameworkBase QueryWithPage).
        /// ORDER BY inside the SQL breaks SqlSugar's own COUNT, so the count is
        /// computed from the SQL with its trailing ORDER BY stripped, and paging
        /// is applied with OFFSET/FETCH (appending an ORDER BY only when missing).
        /// Fills pagination.records / pagination.total like the original.
        /// </summary>
        public List<TQ> QueryWithPage<TQ>(string sql, Pagination pagination, SqlParameter[] pars)
        {
            return QueryWithPageInternal<TQ>(sql, pagination, ToSugarParams(pars).ToArray());
        }

        public List<TQ> QueryWithPage<TQ>(string sql, Pagination pagination, System.Data.Common.DbParameter[] pars)
        {
            return QueryWithPageInternal<TQ>(sql, pagination, ToSugarParams(pars).ToArray());
        }

        private List<TQ> QueryWithPageInternal<TQ>(string sql, Pagination pagination, SugarParameter[] sugarPars)
        {
            var countSql = Regex.Replace(sql, @"\border\s+by\b[\s\S]*$", "", RegexOptions.IgnoreCase).Trim();
            var totalCount = Db.Ado.GetInt($"SELECT COUNT(1) FROM ({countSql}) __t", sugarPars);
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
            var rows = Db.Ado.SqlQuery<TQ>(pageSql, sugarPars);
            pagination.records = totalCount;
            return rows;
        }

        public int ExecuteSqlCommand(string sql, SqlParameter[] pars = null)
        {
            return Newtouch.Infrastructure.Sql.ExecuteCommand(sql, pars);
        }

        public static List<SugarParameter> ToSugarParams(System.Data.Common.DbParameter[] pars)
        {
            var list = new List<SugarParameter>();
            if (pars == null)
            {
                return list;
            }
            foreach (var p in pars)
            {
                list.Add(new SugarParameter(p.ParameterName, p.Value ?? DBNull.Value));
            }
            return list;
        }

        // --- Generic cross-entity overloads (original EF base allowed repos and
        // services to query ANY entity through the base, not just TEntity). ---

        public ISugarQueryable<T> IQueryable<T>() where T : class, new()
        {
            return Db.Queryable<T>();
        }

        public ISugarQueryable<T> IQueryable<T>(Expression<Func<T, bool>> predicate) where T : class, new()
        {
            return Db.Queryable<T>().Where(predicate);
        }

        public List<T> FindList<T>(string sql, SqlParameter[] pars)
        {
            return Db.Ado.SqlQuery<T>(sql, ToSugarParams(pars));
        }

        public List<T> FindList<T>(string sql, System.Data.Common.DbParameter[] pars)
        {
            return Db.Ado.SqlQuery<T>(sql, ToSugarParams(pars));
        }

        public List<T> FindList<T>(string sql)
        {
            return Db.Ado.SqlQuery<T>(sql);
        }

        public T FirstOrDefault<T>(string sql, SqlParameter[] pars)
        {
            var list = Db.Ado.SqlQuery<T>(sql, ToSugarParams(pars));
            return list.Count > 0 ? list[0] : default;
        }

        public T FirstOrDefault<T>(string sql, System.Data.Common.DbParameter[] pars)
        {
            var list = Db.Ado.SqlQuery<T>(sql, ToSugarParams(pars));
            return list.Count > 0 ? list[0] : default;
        }

        public T FirstOrDefault<T>(string sql)
        {
            var list = Db.Ado.SqlQuery<T>(sql);
            return list.Count > 0 ? list[0] : default;
        }

        public List<T> FindList<T>(Expression<Func<T, bool>> predicate) where T : class, new()
        {
            return Db.Queryable<T>().Where(predicate).ToList();
        }

        public List<T> GetList<T>() where T : class, new()
        {
            return Db.Queryable<T>().ToList();
        }

        public List<T> GetList<T>(Expression<Func<T, bool>> predicate) where T : class, new()
        {
            return Db.Queryable<T>().Where(predicate).ToList();
        }

        public List<T> GetValidList<T>() where T : class, new()
        {
            return Db.Queryable<T>().Where("zt = '1'").ToList();
        }

        public T FindEntity<T>(object keyValue) where T : class, new()
        {
            var pk = FindPkProperty<T>();
            if (pk == null)
            {
                return null;
            }
            return Db.Queryable<T>().Where($"{pk.Name} = @pk", new SugarParameter("@pk", keyValue)).FirstOrDefault();
        }

        public T FindEntity<T>(Expression<Func<T, bool>> predicate) where T : class, new()
        {
            return Db.Queryable<T>().Where(predicate).FirstOrDefault();
        }

        public T GetForm<T>(object keyValue) where T : class, new()
        {
            return FindEntity<T>(keyValue);
        }

        public int Insert<T>(T entity) where T : class, new()
        {
            return Db.Insertable(entity).ExecuteCommand();
        }

        public int Update<T>(T entity) where T : class, new()
        {
            return Db.Updateable(entity).ExecuteCommand();
        }

        public int Delete<T>(Expression<Func<T, bool>> predicate) where T : class, new()
        {
            return Db.Deleteable<T>().Where(predicate).ExecuteCommand();
        }

        public int DeleteForm<T>(object keyValue) where T : class, new()
        {
            var pk = FindPkProperty<T>();
            if (pk == null)
            {
                return 0;
            }
            return Db.Deleteable<T>().Where($"{pk.Name} = @pk", new SugarParameter("@pk", keyValue)).ExecuteCommand();
        }

        public int SubmitForm<T>(T entity, object keyValue) where T : class, new()
        {
            if (entity is Newtouch.HIS.Domain.EntityBase eb)
            {
                if (keyValue == null || string.IsNullOrWhiteSpace(keyValue.ToString()))
                {
                    eb.Create(true);
                    return Insert(entity);
                }
                eb.Modify(keyValue);
                return Update(entity);
            }
            if (keyValue == null || string.IsNullOrWhiteSpace(keyValue.ToString()))
            {
                return Insert(entity);
            }
            return Update(entity);
        }

        public void DetacheEntity<T>(T entity) where T : class, new()
        {
            // No-op: SqlSugar has no change tracking to detach from.
        }

        private static PropertyInfo FindPkProperty<T>() where T : class, new()
        {
            var t = typeof(T);
            foreach (var p in t.GetProperties())
            {
                foreach (var a in p.GetCustomAttributes(false))
                {
                    if (a.GetType().Name == "SugarColumnAttribute")
                    {
                        var prop = a.GetType().GetProperty("IsPrimaryKey");
                        if (prop != null && prop.GetValue(a) is bool b && b)
                        {
                            return p;
                        }
                    }
                }
            }
            return t.GetProperty("Id");
        }

        private static PropertyInfo FindPkProperty()
        {
            var t = typeof(TEntity);
            foreach (var p in t.GetProperties())
            {
                foreach (var a in p.GetCustomAttributes(false))
                {
                    if (a.GetType().Name == "SugarColumnAttribute")
                    {
                        var prop = a.GetType().GetProperty("IsPrimaryKey");
                        if (prop != null && prop.GetValue(a) is bool b && b)
                        {
                            return p;
                        }
                    }
                }
            }
            return t.GetProperty("Id");
        }
    }
}
