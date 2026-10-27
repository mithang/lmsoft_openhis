using Newtouch.EMR.Domain.IRepository;
using Newtouch.EMR.Infrastructure;
using NewtouchHIS.Lib.Base.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Newtouch.EMR.Repository
{
    /// <summary>
    /// SqlSugar generic repository. Replaces FrameworkBase RepositoryBase&lt;T&gt;
    /// (EF6 + Newtouch.Infrastructure.EF, Framework-only, no source).
    /// The public surface mirrors what the EMR repositories and domain
    /// services actually call: IQueryable, FindEntity (predicate and key),
    /// Insert, Update, Delete, paged FindList, raw-SQL FindList /
    /// FirstOrDefault, ExecuteSqlCommand, SqlQueryForDataTable, QueryWithPage.
    ///
    /// IEntity.Create()/Modify() semantics: the old Create(true) generated a
    /// Guid primary key for new rows; Insert() replicates that for string
    /// keys that are still empty. Modify(key) only re-asserted the key on an
    /// already-loaded entity, so update call sites need no replacement.
    /// </summary>
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class, new()
    {
        public ISugarQueryable<T> IQueryable()
        {
            return EmrDb.GetConnection().Queryable<T>();
        }

        public ISugarQueryable<T> IQueryable(Expression<Func<T, bool>> predicate)
        {
            return EmrDb.GetConnection().Queryable<T>().Where(predicate);
        }

        public T FindEntity(Expression<Func<T, bool>> predicate)
        {
            return IQueryable(predicate).Take(1).ToList().FirstOrDefault();
        }

        public T FindEntity(object keyValue)
        {
            if (keyValue == null)
            {
                return null;
            }
            var pk = GetPrimaryKeyProperty();
            if (pk == null)
            {
                return null;
            }
            object value = keyValue;
            try
            {
                Type target = Nullable.GetUnderlyingType(pk.PropertyType) ?? pk.PropertyType;
                if (value.GetType() != target)
                {
                    value = Convert.ChangeType(value, target);
                }
            }
            catch
            {
                return null;
            }
            var param = Expression.Parameter(typeof(T), "p");
            var body = Expression.Equal(Expression.Property(param, pk), Expression.Constant(value, pk.PropertyType));
            var predicate = Expression.Lambda<Func<T, bool>>(body, param);
            return FindEntity(predicate);
        }

        public int Insert(T entity)
        {
            EnsureStringKey(entity);
            return EmrDb.GetConnection().Insertable(entity).ExecuteCommand();
        }

        public int Insert(IEnumerable<T> entities)
        {
            var list = entities != null ? entities.ToList() : new List<T>();
            foreach (var entity in list)
            {
                EnsureStringKey(entity);
            }
            if (list.Count == 0)
            {
                return 0;
            }
            return EmrDb.GetConnection().Insertable(list).ExecuteCommand();
        }

        private static void EnsureStringKey(T entity)
        {
            if (entity == null)
            {
                return;
            }
            var pk = GetPrimaryKeyProperty();
            if (pk != null && pk.PropertyType == typeof(string))
            {
                string current = pk.GetValue(entity) as string;
                if (string.IsNullOrWhiteSpace(current))
                {
                    pk.SetValue(entity, Guid.NewGuid().ToString());
                }
            }
        }

        public int Update(T entity)
        {
            return EmrDb.GetConnection().Updateable(entity).ExecuteCommand();
        }

        public int Delete(Expression<Func<T, bool>> predicate)
        {
            return EmrDb.GetConnection().Deleteable<T>().Where(predicate).ExecuteCommand();
        }

        public List<T> FindList(Expression<Func<T, bool>> predicate, Pagination pagination)
        {
            var q = IQueryable(predicate);
            if (pagination != null && !string.IsNullOrWhiteSpace(pagination.sidx)
                && System.Text.RegularExpressions.Regex.IsMatch(pagination.sidx, @"^[\w\.,\s]+$"))
            {
                string first = pagination.sidx.Trim().Split(new[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
                string sord = string.Equals(pagination.sord, "asc", StringComparison.OrdinalIgnoreCase) ? "asc" : "desc";
                if (!string.IsNullOrWhiteSpace(first))
                {
                    q = q.OrderBy(first + " " + sord);
                }
            }
            int total = 0;
            var page = q.ToPageList(pagination != null && pagination.page.HasValue ? pagination.page.Value : 1,
                pagination != null && pagination.rows.HasValue ? pagination.rows.Value : 15, ref total);
            if (pagination != null)
            {
                pagination.records = total;
            }
            return page;
        }

        public List<T2> FindList<T2>(string sql, SugarParameter[] parameters) where T2 : class, new()
        {
            return EmrQuery.FindList<T2>(sql, parameters);
        }

        public List<T2> FindList<T2>(string sql) where T2 : class, new()
        {
            return EmrQuery.FindList<T2>(sql, null);
        }

        public T2 FirstOrDefault<T2>(string sql, SugarParameter[] parameters) where T2 : class, new()
        {
            return EmrQuery.FirstOrDefault<T2>(sql, parameters);
        }

        public T2 FirstOrDefault<T2>(string sql) where T2 : class, new()
        {
            return EmrQuery.FirstOrDefault<T2>(sql, null);
        }

        public int ExecuteSqlCommand(string sql, SugarParameter[] parameters)
        {
            return EmrQuery.ExecuteSqlCommand(sql, parameters);
        }

        public int ExecuteSqlCommand(string sql)
        {
            return EmrQuery.ExecuteSqlCommand(sql, null);
        }

        public DataTable SqlQueryForDataTable(string sql, SugarParameter[] parameters)
        {
            return EmrQuery.SqlQueryForDataTable(sql, parameters);
        }

        public List<T2> QueryWithPage<T2>(string sql, Pagination pagination, SugarParameter[] parameters) where T2 : class, new()
        {
            return EmrQuery.QueryWithPage<T2>(sql, pagination, parameters);
        }

        public List<T2> QueryWithPage<T2>(string sql, Pagination pagination, SugarParameter[] parameters, bool ignored) where T2 : class, new()
        {
            return EmrQuery.QueryWithPage<T2>(sql, pagination, parameters);
        }

        private static PropertyInfo GetPrimaryKeyProperty()
        {
            foreach (var prop in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                // Attribute type referenced by name only: SqlSugar exposes
                // it as [SugarColumn] and the CLR type name differs across
                // SqlSugar builds, so resolve it reflectively instead of
                // naming the type (which failed to compile here).
                var attr = prop.GetCustomAttributes()
                    .FirstOrDefault(a => a.GetType().Name == "SugarColumnAttribute" || a.GetType().Name == "SugarColumn");
                if (attr != null)
                {
                    var isPk = attr.GetType().GetProperty("IsPrimaryKey");
                    if (isPk != null && isPk.GetValue(attr) is bool b && b)
                    {
                        return prop;
                    }
                }
            }
            return typeof(T).GetProperty("Id") ?? typeof(T).GetProperty("ID");
        }
    }
}
