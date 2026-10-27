using Newtouch.HIS.Base.Infrastructure;
using SqlSugar;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// Transaction scope replacing EFDbTransaction (EF6).
    /// Usage mirrors the original: using (var db = new DbTrans()) { ...; db.Commit(); }
    /// Dispose without Commit rolls back. Wraps one SqlSugar transaction.
    /// </summary>
    public class DbTrans : IDisposable
    {
        private readonly SqlSugarProvider _db;
        private bool _committed;
        private int _affectedRows;

        public DbTrans()
        {
            _db = BaseDb.GetConnection();
            _db.Ado.BeginTran();
        }

        public ISugarQueryable<T> IQueryable<T>() where T : class, new()
        {
            return _db.Queryable<T>();
        }

        public ISugarQueryable<T> IQueryable<T>(Expression<Func<T, bool>> predicate) where T : class, new()
        {
            return _db.Queryable<T>().Where(predicate);
        }

        public T FindEntity<T>(Expression<Func<T, bool>> predicate) where T : class, new()
        {
            return _db.Queryable<T>().Where(predicate).FirstOrDefault();
        }

        public T FindEntity<T>(object keyValue) where T : class, new()
        {
            return _db.Queryable<T>().InSingle(keyValue);
        }

        public T GetForm<T>(object keyValue) where T : class, new()
        {
            return FindEntity<T>(keyValue);
        }

        public int Insert<T>(T entity) where T : class, new()
        {
            var n = _db.Insertable(entity).ExecuteCommand();
            _affectedRows += n;
            return n;
        }

        public int Update<T>(T entity) where T : class, new()
        {
            var n = _db.Updateable(entity).ExecuteCommand();
            _affectedRows += n;
            return n;
        }

        public int Delete<T>(Expression<Func<T, bool>> predicate) where T : class, new()
        {
            var n = _db.Deleteable<T>().Where(predicate).ExecuteCommand();
            _affectedRows += n;
            return n;
        }

        public int Delete<T>(T entity) where T : class, new()
        {
            var n = _db.Deleteable(entity).ExecuteCommand();
            _affectedRows += n;
            return n;
        }

        public int ExecuteSqlCommand(string sql, params SugarParameter[] pars)
        {
            var n = _db.Ado.ExecuteCommand(sql, pars);
            _affectedRows += n;
            return n;
        }

        /// <summary>Original EFDbTransaction.Commit returned affected rows; callers assign it.</summary>
        public int Commit()
        {
            _db.Ado.CommitTran();
            _committed = true;
            return _affectedRows;
        }

        public void Rollback()
        {
            _db.Ado.RollbackTran();
            _committed = true;
        }

        public void Dispose()
        {
            if (!_committed)
            {
                try { _db.Ado.RollbackTran(); } catch { }
            }
        }
    }
}
