using Newtouch.Herp.Domain.IRepository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Newtouch.Infrastructure.EF
{
    /// <summary>
    /// Port of the EF-era transaction handle used by KfKcxx storage code.
    /// The original EFDbTransaction wrapped an EF DbContextTransaction created
    /// from IDefaultDatabaseFactory; here it wraps the SqlSugar provider for the
    /// Herp database (SqlSugarScope shares the connection within one async flow,
    /// so operations on this provider participate in the same transaction).
    /// BeginTrans/Commit/Rollback map to SqlSugar Ado transaction calls.
    /// Insert/Update/Delete mirror the EF-era methods the services call on the
    /// transaction handle (insert/update/delete against the same provider).
    /// </summary>
    public class EFDbTransaction : IDisposable
    {
        public SqlSugarProvider Db { get; }

        private int _affected;

        public EFDbTransaction(SqlSugarProvider db)
        {
            Db = db;
        }

        public EFDbTransaction BeginTrans()
        {
            Db.Ado.BeginTran();
            return this;
        }

        /// <summary>Commits and returns the rows affected by operations on this handle.</summary>
        public int Commit()
        {
            Db.Ado.CommitTran();
            return _affected;
        }

        public void Rollback()
        {
            try { Db.Ado.RollbackTran(); } catch { /* already rolled back */ }
        }

        public void Dispose()
        {
            try { Db.Ado.RollbackTran(); } catch { /* committed already */ }
        }

        public int Insert<T>(T entity) where T : class, new()
        {
            // NX27 (D-NX27-HERP-002): same identity backfill as
            // RepositoryBase.Insert — the EF-era base backfilled DB-generated
            // identities; ExecuteCommand does not, so callers that read the Id
            // after an in-transaction insert (PurchasingOrderApp re-reads the
            // order by orderNo to work around Id<=0; PurchasingPlanApp details
            // link purchaseId from the header) got Id=0. ExecuteReturnIdentity
            // runs SCOPE_IDENTITY() on this transaction's connection, so the
            // value is correct inside the transaction. _affected/return stay 1
            // on success (throws on SQL error, as before); explicit-Id tables
            // return 0 from ExecuteReturnIdentity and are left untouched.
            var identity = Db.Insertable(entity).ExecuteReturnIdentity();
            SqlSugarIdentity.Apply(entity, identity);
            _affected += 1;
            return 1;
        }

        public int Insert<T>(List<T> entities) where T : class, new()
        {
            // NX27 (D-NX27-HERP-002): list overload backfills each row's
            // identity, same rationale as RepositoryBase.Insert(List<T>).
            // Row-by-row through the same ExecuteReturnIdentity primitive, on
            // this transaction's connection. Callers always run this inside
            // BeginTrans, but guard anyway: if no ambient transaction is running,
            // wrap the loop in one so a mid-list failure stays atomic, exactly
            // like the old ExecuteCommand batch. Null/empty lists keep the old
            // single-call behaviour verbatim.
            if (entities == null || entities.Count == 0)
            {
                int n = Db.Insertable(entities).ExecuteCommand();
                _affected += n;
                return n;
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

        public int Update<T>(T entity) where T : class, new()
        {
            int n = Db.Updateable(entity).ExecuteCommand();
            _affected += n;
            return n;
        }

        public int Delete<T>(Expression<Func<T, bool>> predicate) where T : class, new()
        {
            int n = Db.Deleteable<T>().Where(predicate).ExecuteCommand();
            _affected += n;
            return n;
        }

        public int Delete<T>(T entity) where T : class, new()
        {
            int n = Db.Deleteable(entity).ExecuteCommand();
            _affected += n;
            return n;
        }
    }
}
