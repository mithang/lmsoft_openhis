using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using SqlSugar;

namespace Newtouch.Infrastructure.EF
{
    /// <summary>
    /// Stand-in for the Framework EF transaction wrapper
    /// (Newtouch.Infrastructure.EF.EFDbTransaction) used by
    /// IPrepareMedicineStockInfoRepo.UpdateKcslWithTrans. Backed by a SqlSugar
    /// transaction on the CIS connection. The repository implementation that
    /// creates this wrapper owns the commit/rollback decision.
    /// </summary>
    public class EFDbTransaction : IDisposable
    {
        public EFDbTransaction() : this(Newtouch.CIS.Infrastructure.CisDb.GetConnection()) { }

        public EFDbTransaction(SqlSugarProvider db)
        {
            Db = db;
        }

        public SqlSugarProvider Db { get; }

        public void Begin()
        {
            Db.Ado.BeginTran();
        }
        public EFDbTransaction BeginTrans() { Begin(); return this; }

        public void Commit()
        {
            Db.Ado.CommitTran();
        }

        public void Rollback()
        {
            try
            {
                Db.Ado.RollbackTran();
            }
            catch
            {
                // already committed / connection closed: nothing to roll back
            }
        }

        public void Dispose()
        {
            Rollback();
        }

        // ---- FrameworkBase EFDbTransaction DbContext-like surface ----
        private static PropertyInfo GetKeyProperty<T>()
        {
            return typeof(T).GetProperties().FirstOrDefault(x => x.GetCustomAttribute<KeyAttribute>() != null);
        }
        public int Insert<T>(T entity) where T : class, new()
        {
            return Db.Insertable(entity).ExecuteCommand();
        }
        public int Update<T>(T entity) where T : class, new()
        {
            var key = GetKeyProperty<T>();
            var up = Db.Updateable(entity);
            if (key != null) up = up.WhereColumns(key.Name);
            return up.ExecuteCommand();
        }
        public int Delete<T>(T entity) where T : class, new()
        {
            var key = GetKeyProperty<T>();
            if (key == null) throw new InvalidOperationException("Delete(entity) needs a [Key] property on " + typeof(T).Name + ".");
            var param = Expression.Parameter(typeof(T), "p");
            var value = key.GetValue(entity);
            var lambda = Expression.Lambda<Func<T, bool>>(Expression.Equal(Expression.Property(param, key), Expression.Constant(value, key.PropertyType)), param);
            return Db.Deleteable<T>().Where(lambda).ExecuteCommand();
        }
        public int Delete<T>(Expression<Func<T, bool>> predicate) where T : class, new()
        {
            return Db.Deleteable<T>().Where(predicate).ExecuteCommand();
        }
        public System.Linq.IQueryable<T> IQueryable<T>() where T : class, new()
        {
            return Db.Queryable<T>().ToList().AsQueryable();
        }
        public System.Linq.IQueryable<T> IQueryable<T>(Expression<Func<T, bool>> predicate) where T : class, new()
        {
            return Db.Queryable<T>().ToList().AsQueryable().Where(predicate);
        }
        public T FindEntity<T>(object keyValue) where T : class, new()
        {
            var key = GetKeyProperty<T>();
            if (key == null) throw new InvalidOperationException("FindEntity(keyValue) needs a [Key] property on " + typeof(T).Name + ".");
            var param = Expression.Parameter(typeof(T), "p");
            var value = keyValue == null ? null : Convert.ChangeType(keyValue, Nullable.GetUnderlyingType(key.PropertyType) ?? key.PropertyType);
            var lambda = Expression.Lambda<Func<T, bool>>(Expression.Equal(Expression.Property(param, key), Expression.Constant(value, key.PropertyType)), param);
            return Db.Queryable<T>().Where(lambda).First();
        }
        public T FindEntity<T>(Expression<Func<T, bool>> predicate) where T : class, new()
        {
            return Db.Queryable<T>().Where(predicate).First();
        }
        public List<TVO> FindList<TVO>(string sql)
        {
            return Db.Ado.SqlQuery<TVO>(sql).ToList();
        }
        public List<TVO> FindList<TVO>(string sql, SqlParameter[] parameters)
        {
            return Db.Ado.SqlQuery<TVO>(sql, Newtouch.CIS.Compat.SqlParam.ToSugar(parameters)).ToList();
        }
        public List<TVO> FindList<TVO>(string sql, DbParameter[] parameters)
        {
            return Db.Ado.SqlQuery<TVO>(sql, Newtouch.CIS.Compat.SqlParam.ToSugar(parameters)).ToList();
        }
        public TVO FirstOrDefault<TVO>(string sql)
        {
            return Db.Ado.SqlQuery<TVO>(sql).FirstOrDefault();
        }
        public TVO FirstOrDefault<TVO>(string sql, SqlParameter[] parameters)
        {
            return Db.Ado.SqlQuery<TVO>(sql, Newtouch.CIS.Compat.SqlParam.ToSugar(parameters)).FirstOrDefault();
        }
        public TVO FirstOrDefault<TVO>(string sql, DbParameter[] parameters)
        {
            return Db.Ado.SqlQuery<TVO>(sql, Newtouch.CIS.Compat.SqlParam.ToSugar(parameters)).FirstOrDefault();
        }
        public List<TVO> QueryWithPage<TVO>(string sql, NewtouchHIS.Lib.Base.Model.Pagination pagination, SqlParameter[] parameters)
        {
            return QueryWithPage<TVO>(sql, pagination, parameters, true);
        }
        public List<TVO> QueryWithPage<TVO>(string sql, NewtouchHIS.Lib.Base.Model.Pagination pagination, SqlParameter[] parameters, bool countTotal)
        {
            var all = Db.Ado.SqlQuery<TVO>(sql, Newtouch.CIS.Compat.SqlParam.ToSugar(parameters)).ToList();
            return PageSlice(all, pagination);
        }
        public List<TVO> QueryWithPage<TVO>(string sql, NewtouchHIS.Lib.Base.Model.Pagination pagination, DbParameter[] parameters)
        {
            var all = Db.Ado.SqlQuery<TVO>(sql, Newtouch.CIS.Compat.SqlParam.ToSugar(parameters)).ToList();
            return PageSlice(all, pagination);
        }
        private static List<TVO> PageSlice<TVO>(List<TVO> all, NewtouchHIS.Lib.Base.Model.Pagination pagination)
        {
            int total = all.Count;
            int rows = pagination.rows ?? 20;
            int page = pagination.page ?? 1;
            if (rows <= 0) rows = 20;
            if (page <= 0) page = 1;
            pagination.records = total;
            return all.Skip((page - 1) * rows).Take(rows).ToList();
        }
        public int ExecuteSqlCommand(string sql)
        {
            return Db.Ado.ExecuteCommand(sql);
        }
        public int ExecuteSqlCommand(string sql, SqlParameter[] parameters)
        {
            return Db.Ado.ExecuteCommand(sql, Newtouch.CIS.Compat.SqlParam.ToSugar(parameters));
        }
        public int ExecuteSqlCommand(string sql, DbParameter[] parameters)
        {
            return Db.Ado.ExecuteCommand(sql, Newtouch.CIS.Compat.SqlParam.ToSugar(parameters));
        }
        public void DetacheEntity<T>(T entity)
        {
            // SqlSugar has no change tracking: nothing to detach.
        }

    }
}
