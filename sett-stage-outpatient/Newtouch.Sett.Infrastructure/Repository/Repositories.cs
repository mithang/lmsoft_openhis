using NewtouchHIS.Lib.DataBaseSvr;
using SqlSugar;
using System;
using System.Linq.Expressions;

namespace Newtouch.Sett.Repository
{
    /// <summary>
    /// Minimal generic repository surface (mirrors the MRQC pilot).
    /// Area repos extend RepositoryBase&lt;T&gt; (Sett DB) or BaseRepositoryBase&lt;T&gt;
    /// (Base DB, e.g. sys-config tables). ConnId override picks the DB.
    /// </summary>
    public interface IRepositoryBase<T> where T : class, new()
    {
        ISugarQueryable<T> IQueryable();
        ISugarQueryable<T> IQueryable(Expression<Func<T, bool>> predicate);
        int Delete(Expression<Func<T, bool>> predicate);
    }

    public class RepositoryBase<T> : IRepositoryBase<T> where T : class, new()
    {
        protected virtual string ConnId => "SettDb";
        protected SqlSugarProvider Db => SqlSugarDbContext.Db.GetConnection(ConnId);

        public ISugarQueryable<T> IQueryable()
        {
            return Db.Queryable<T>();
        }

        public ISugarQueryable<T> IQueryable(Expression<Func<T, bool>> predicate)
        {
            return Db.Queryable<T>().Where(predicate);
        }

        public int Delete(Expression<Func<T, bool>> predicate)
        {
            return Db.Deleteable<T>().Where(predicate).ExecuteCommand();
        }
    }

    public class BaseRepositoryBase<T> : RepositoryBase<T> where T : class, new()
    {
        protected override string ConnId => "BaseDb";
    }

    public class UnionRepositoryBase<T> : RepositoryBase<T> where T : class, new()
    {
        protected override string ConnId => "UnionDb";
    }
}
