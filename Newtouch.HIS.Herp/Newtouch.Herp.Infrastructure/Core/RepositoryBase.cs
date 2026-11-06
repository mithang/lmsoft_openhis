using Newtouch.Herp.Infrastructure;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// Generic repository contract replacing FrameworkBase IRepositoryBase&lt;T&gt; (binary-only).
    /// Only the members actually used by Herp code are declared here.
    /// </summary>
    public interface IRepositoryBase<T> where T : class, new()
    {
        ISugarQueryable<T> IQueryable();
        ISugarQueryable<T> IQueryable(Expression<Func<T, bool>> predicate);
        T FindEntity(Expression<Func<T, bool>> predicate);
        List<T> FindList(Expression<Func<T, bool>> predicate);
        int Insert(T entity);
        int Insert(List<T> entities);
        int Update(T entity);
        int Delete(T entity);
    }

    /// <summary>
    /// Shared identity-backfill helper (NX26 D-NX26-KF-001, extended NX27
    /// D-NX27-HERP-002): the original EF6 base backfilled DB-generated identity
    /// values onto the entity; SqlSugar ExecuteCommand does not. Used by
    /// RepositoryBase and EFDbTransaction (same assembly) so every insert path
    /// backfills the same way. No-op when the DB returns no identity (&lt;=0)
    /// or the entity has no IsIdentity property (explicit-Id tables).
    /// </summary>
    internal static class SqlSugarIdentity
    {
        internal static void Apply<T>(T entity, object identity)
        {
            if (identity == null) return;
            if (!long.TryParse(identity.ToString(), out var id) || id <= 0) return;
            var prop = typeof(T).GetProperties().FirstOrDefault(p =>
                p.GetCustomAttributes(typeof(SugarColumn), true).OfType<SugarColumn>().Any(a => a.IsIdentity));
            if (prop != null && prop.CanWrite)
            {
                prop.SetValue(entity, Convert.ChangeType(id, prop.PropertyType));
            }
        }
    }

    /// <summary>
    /// SqlSugar generic repository. Replaces FrameworkBase RepositoryBase&lt;T&gt;
    /// (EF6 + Newtouch.Infrastructure.EF, Framework-only, no source).
    /// SQL helpers mirror the old base's raw-SQL surface so repo implementations
    /// port with only parameter-type changes (DbParameter -&gt; SugarParameter).
    /// </summary>
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class, new()
    {
        protected virtual SqlSugarProvider Db => HerpDb.GetConnection();

        public ISugarQueryable<T> IQueryable()
        {
            return Db.Queryable<T>();
        }

        public ISugarQueryable<T> IQueryable(Expression<Func<T, bool>> predicate)
        {
            return Db.Queryable<T>().Where(predicate);
        }

        public T FindEntity(Expression<Func<T, bool>> predicate)
        {
            return Db.Queryable<T>().First(predicate);
        }

        public List<T> FindList(Expression<Func<T, bool>> predicate)
        {
            return Db.Queryable<T>().Where(predicate).ToList();
        }

        public int Insert(T entity)
        {
            // NX26 (D-NX26-KF-001): the original EF6 base backfilled DB-generated
            // identity values onto the entity; SqlSugar ExecuteCommand does not,
            // so callers linking details via the header Id (kf_crkdj -> kf_crkmx.crkId)
            // silently got Id=0. ExecuteReturnIdentity returns the new identity for
            // identity columns and 0 (no throw) for explicit-Id tables (harness-proven
            // on SqlSugarCore 5.1.4.100), so backfill only when a real identity comes
            // back. Return stays 1 on success (throws on SQL error, as before).
            var identity = Db.Insertable(entity).ExecuteReturnIdentity();
            SqlSugarIdentity.Apply(entity, identity);
            return 1;
        }

        public int Insert(List<T> entities)
        {
            // NX27 (D-NX27-HERP-002): the single-entity Insert above backfills,
            // but this list overload did not — callers inserting identity rows in
            // bulk (PurchasingPlanApp details, WbrkAdopt kcxx, price-adjustment
            // profit/loss) kept Id=0 on every row. SqlSugar has no bulk
            // identity-return API (harness-proven: ExecuteCommandIdentityIntoEntity
            // on a list backfills only the FIRST row's Id), so insert row-by-row
            // through the same ExecuteReturnIdentity primitive. The old
            // ExecuteCommand batch was atomic on partial failure (harness-proven:
            // a NULL in row 3 leaves 0 rows), so when no ambient transaction is
            // running, wrap the loop in one of our own (BeginTran/Commit, rollback
            // + rethrow on error); inside an EFDbTransaction the ambient
            // transaction already makes the batch atomic. Return stays the total
            // row count (throws on SQL error, as before). Null/empty lists keep
            // the old single-call behaviour verbatim (ExecuteCommandIdentityIntoEntity
            // throws TargetException on an empty list; ExecuteCommand returns 0).
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
            return Db.Updateable(entity).ExecuteCommand();
        }

        public int Delete(T entity)
        {
            return Db.Deleteable(entity).ExecuteCommand();
        }

        public int ExecuteSqlCommand(string sql, params SugarParameter[] parameters)
        {
            return Db.Ado.ExecuteCommand(sql, parameters);
        }

        public List<T2> FindList<T2>(string sql, params SugarParameter[] parameters) where T2 : class, new()
        {
            return Db.SqlQueryable<T2>(sql).AddParameters(parameters).ToList();
        }

        public T2 FirstOrDefault<T2>(string sql, params SugarParameter[] parameters) where T2 : class, new()
        {
            return Db.SqlQueryable<T2>(sql).AddParameters(parameters).First();
        }

        public T2 FirstOrDefault<T2>(string sql) where T2 : class, new()
        {
            return Db.SqlQueryable<T2>(sql).First();
        }

        /// <summary>
        /// Scalar single-value lookup (e.g. FirstOrDefault&lt;string&gt; for a Name
        /// column). No new() constraint: string and other scalars satisfy this.
        /// </summary>
        public T2 FirstOrDefaultScalar<T2>(string sql, params SugarParameter[] parameters)
        {
            return Db.Ado.SqlQuery<T2>(sql, parameters).FirstOrDefault();
        }

        public List<T2> SqlQuery<T2>(string sql, params SugarParameter[] parameters) where T2 : class, new()
        {
            return Db.SqlQueryable<T2>(sql).AddParameters(parameters).ToList();
        }
    }
}
