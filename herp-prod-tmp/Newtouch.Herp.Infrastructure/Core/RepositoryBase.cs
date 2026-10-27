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
            return Db.Insertable(entity).ExecuteCommand();
        }

        public int Insert(List<T> entities)
        {
            return Db.Insertable(entities).ExecuteCommand();
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
