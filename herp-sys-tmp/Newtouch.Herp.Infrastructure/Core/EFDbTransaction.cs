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
            int n = Db.Insertable(entity).ExecuteCommand();
            _affected += n;
            return n;
        }

        public int Insert<T>(List<T> entities) where T : class, new()
        {
            int n = Db.Insertable(entities).ExecuteCommand();
            _affected += n;
            return n;
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
