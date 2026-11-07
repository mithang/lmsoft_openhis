using Newtouch.CIS.Infrastructure;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Newtouch.CIS.Repository
{
    /// <summary>
    /// SqlSugar generic repository. Replaces FrameworkBase RepositoryBase&lt;T&gt;
    /// (EF6 + Newtouch.Infrastructure.EF, Framework-only, no source).
    /// Ported repository classes inherit this class and keep their original
    /// interface (e.g. IQueueRepo : IRepositoryBase&lt;QueueEntity&gt;).
    /// </summary>
    public interface IRepositoryBase<T> where T : class, new()
    {
        System.Linq.IQueryable<T> IQueryable();
        System.Linq.IQueryable<T> IQueryable(Expression<Func<T, bool>> predicate);
        List<TVO> FindList<TVO>(string sql);
        TVO FirstOrDefault<TVO>(string sql);
        void DetacheEntity<TVO>(TVO entity);
        T FindEntity(object keyValue);
        int Insert(System.Collections.Generic.List<T> entities);
        T FindEntity(Expression<Func<T, bool>> predicate);
        System.Collections.Generic.IList<T> FindList(Expression<Func<T, bool>> predicate, NewtouchHIS.Lib.Base.Model.Pagination pagination);
        int Insert(T entity);
        int Update(T entity);
        int Delete(T entity);
        int Delete(Expression<Func<T, bool>> predicate);
        int ExecuteSqlCommand(string sql, params SqlParameter[] parameters);
        List<T> SqlQuery(string sql, params SugarParameter[] parameters);
    }

    public class RepositoryBase<T> : IRepositoryBase<T> where T : class, new()
    {
        protected SqlSugarProvider Db => CisDb.GetConnection();

        public System.Linq.IQueryable<T> IQueryable() { return Db.Queryable<T>().ToList().AsQueryable(); }

        public System.Linq.IQueryable<T> IQueryable(Expression<Func<T, bool>> predicate)
        {
            return Db.Queryable<T>().ToList().AsQueryable().Where(predicate);
        }

        public T FindEntity(object keyValue)
        {
            var keyProp = GetKeyProperty();
            if (keyProp == null)
            {
                throw new InvalidOperationException(
                    $"FindEntity(keyValue) needs a [Key] property on {typeof(T).Name}.");
            }
            var param = Expression.Parameter(typeof(T), "p");
            var value = keyValue == null
                ? null
                : Convert.ChangeType(keyValue, Nullable.GetUnderlyingType(keyProp.PropertyType) ?? keyProp.PropertyType);
            var body = Expression.Equal(
                Expression.Property(param, keyProp),
                Expression.Constant(value, keyProp.PropertyType));
            var lambda = Expression.Lambda<Func<T, bool>>(body, param);
            return Db.Queryable<T>().Where(lambda).First();
        }

        public T FindEntity(Expression<Func<T, bool>> predicate)
        {
            return Db.Queryable<T>().Where(predicate).First();
        }

        public int Insert(T entity)
        {
            // NX28 (D-NX28-HERP-001): same class of fix as Herp NX26/NX27 —
            // the EF-era base backfilled DB-generated identity values onto the
            // entity; SqlSugar ExecuteCommand does not, so callers that read
            // the Id after insert (InsertSyxxBatch links nothing by Id today,
            // but EndInventory/SaveInventoryDetail flows and any future caller
            // do) silently got Id=0 — and worse, without IsIdentity on the
            // entity SqlSugar sent Id=0 explicitly and SQL Server rejected
            // the insert outright ("Cannot insert explicit value for identity
            // column"). ExecuteReturnIdentity returns the new identity for
            // identity columns and 0 (no throw) for explicit-Id tables
            // (harness-proven on SqlSugarCore 5.1.4.100), so backfill only
            // when a real identity comes back. Return stays 1 on success
            // (throws on SQL error, as before).
            var identity = Db.Insertable(entity).ExecuteReturnIdentity();
            Newtouch.CIS.Compat.SqlSugarIdentity.Apply(entity, identity);
            return 1;
        }

        public int Insert(System.Collections.Generic.List<T> entities)
        {
            // NX28 (D-NX28-HERP-001): list overload backfills each row's
            // identity, same rationale as Herp NX27. SqlSugar has no bulk
            // identity-return API (ExecuteCommandIdentityIntoEntity on a list
            // backfills only the FIRST row's Id), so insert row-by-row
            // through the same ExecuteReturnIdentity primitive. The old
            // ExecuteCommand batch was atomic on partial failure, so when no
            // ambient transaction is running, wrap the loop in one of our own
            // (BeginTran/Commit, rollback + rethrow on error); inside an
            // EFDbTransaction the ambient transaction already makes the batch
            // atomic. Return stays the total row count (throws on SQL error,
            // as before). Null/empty lists keep the old single-call behaviour
            // verbatim (ExecuteCommandIdentityIntoEntity throws TargetException
            // on an empty list; ExecuteCommand returns 0).
            if (entities == null || entities.Count == 0)
            {
                return Db.Insertable(entities).ExecuteCommand();
            }
            var ownTran = Db.Ado.IsNoTran();
            if (ownTran) Db.Ado.BeginTran();
            try
            {
                var total = 0;
                foreach (var entity in entities)
                {
                    total += Insert(entity);
                }
                if (ownTran) Db.Ado.CommitTran();
                return total;
            }
            catch
            {
                if (ownTran) { try { Db.Ado.RollbackTran(); } catch { /* already rolled back */ } }
                throw;
            }
        }
        public int Update(T entity)
        {
            var keyProp = GetKeyProperty();
            if (keyProp == null)
            {
                throw new InvalidOperationException(
                    $"Update needs a [Key] property on {typeof(T).Name}.");
            }
            return Db.Updateable(entity).WhereColumns(keyProp.Name).ExecuteCommand();
        }

        public int Delete(T entity)
        {
            var keyProp = GetKeyProperty();
            if (keyProp == null)
            {
                throw new InvalidOperationException(
                    $"Delete(entity) needs a [Key] property on {typeof(T).Name}.");
            }
            var param = Expression.Parameter(typeof(T), "p");
            var value = keyProp.GetValue(entity);
            var body = Expression.Equal(
                Expression.Property(param, keyProp),
                Expression.Constant(value, keyProp.PropertyType));
            var lambda = Expression.Lambda<Func<T, bool>>(body, param);
            return Db.Deleteable<T>().Where(lambda).ExecuteCommand();
        }

        public int Delete(Expression<Func<T, bool>> predicate)
        {
            return Db.Deleteable<T>().Where(predicate).ExecuteCommand();
        }

        public int ExecuteSqlCommand(string sql, params SqlParameter[] parameters)
        {
            var sugar = Newtouch.CIS.Compat.SqlParam.ToSugar(parameters);
            var rows = Db.Ado.ExecuteCommand(sql, sugar);
            Newtouch.CIS.Compat.SqlParam.CopyBack(parameters, sugar);
            return rows;
        }

        public List<T> SqlQuery(string sql, params SugarParameter[] parameters)
        {
            return Db.Ado.SqlQuery<T>(sql, parameters);
        }

        private static PropertyInfo GetKeyProperty()
        {
            return typeof(T).GetProperties()
                .FirstOrDefault(p => p.GetCustomAttribute<System.ComponentModel.DataAnnotations.KeyAttribute>() != null);
        }

        // ---- Raw-SQL helpers (FrameworkBase RepositoryBase surface) ----
        public List<TVO> FindList<TVO>(string sql) { return Db.Ado.SqlQuery<TVO>(sql).ToList(); }
        public List<TVO> FindList<TVO>(string sql, System.Data.SqlClient.SqlParameter[] parameters)
        {
            return Db.Ado.SqlQuery<TVO>(sql, Newtouch.CIS.Compat.SqlParam.ToSugar(parameters)).ToList();
        }
        public List<TVO> FindList<TVO>(string sql, System.Data.Common.DbParameter[] parameters)
        {
            return Db.Ado.SqlQuery<TVO>(sql, Newtouch.CIS.Compat.SqlParam.ToSugar(parameters)).ToList();
        }
        public TVO FirstOrDefault<TVO>(string sql) { return Db.Ado.SqlQuery<TVO>(sql).FirstOrDefault(); }
        public TVO FirstOrDefault<TVO>(string sql, System.Data.SqlClient.SqlParameter[] parameters)
        {
            return Db.Ado.SqlQuery<TVO>(sql, Newtouch.CIS.Compat.SqlParam.ToSugar(parameters)).FirstOrDefault();
        }
        public TVO FirstOrDefault<TVO>(string sql, System.Data.Common.DbParameter[] parameters)
        {
            return Db.Ado.SqlQuery<TVO>(sql, Newtouch.CIS.Compat.SqlParam.ToSugar(parameters)).FirstOrDefault();
        }
        public List<TVO> QueryWithPage<TVO>(string sql, NewtouchHIS.Lib.Base.Model.Pagination pagination, System.Data.SqlClient.SqlParameter[] parameters)
        {
            return QueryWithPage<TVO>(sql, pagination, parameters, true);
        }
        public List<TVO> QueryWithPage<TVO>(string sql, NewtouchHIS.Lib.Base.Model.Pagination pagination, System.Data.SqlClient.SqlParameter[] parameters, bool countTotal)
        {
            var all = Db.Ado.SqlQuery<TVO>(sql, Newtouch.CIS.Compat.SqlParam.ToSugar(parameters)).ToList();
            return PageSlice(all, pagination);
        }
        public List<TVO> QueryWithPage<TVO>(string sql, NewtouchHIS.Lib.Base.Model.Pagination pagination, System.Data.Common.DbParameter[] parameters)
        {
            var all = Db.Ado.SqlQuery<TVO>(sql, Newtouch.CIS.Compat.SqlParam.ToSugar(parameters)).ToList();
            return PageSlice(all, pagination);
        }
        public static List<TVO> PageSlice<TVO>(List<TVO> all, NewtouchHIS.Lib.Base.Model.Pagination pagination)
        {
            int total = all.Count;
            int rows = pagination.rows ?? 20;
            int page = pagination.page ?? 1;
            if (rows <= 0) rows = 20;
            if (page <= 0) page = 1;
            pagination.records = total;
            return all.Skip((page - 1) * rows).Take(rows).ToList();
        }
        public void DetacheEntity<TVO>(TVO entity) { /* SqlSugar has no change tracking */ }
        public int ExecuteSqlCommand(string sql)
        {
            return Db.Ado.ExecuteCommand(sql);
        }
        public int ExecuteSqlCommand(string sql, System.Data.Common.DbParameter[] parameters)
        {
            return Db.Ado.ExecuteCommand(sql, Newtouch.CIS.Compat.SqlParam.ToSugar(parameters));
        }

        public System.Collections.Generic.IList<T> FindList(System.Linq.Expressions.Expression<System.Func<T, bool>> predicate, NewtouchHIS.Lib.Base.Model.Pagination pagination)
        {
            var all = Db.Queryable<T>().Where(predicate).ToList();
            pagination.records = all.Count;
            int page = pagination.page ?? 1;
            int rows = pagination.rows ?? 20;
            return all.Skip((page - 1) * rows).Take(rows).ToList();
        }

    }
}
