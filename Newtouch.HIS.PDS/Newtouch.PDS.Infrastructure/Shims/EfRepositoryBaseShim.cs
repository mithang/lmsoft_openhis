using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NewtouchHIS.Lib.Base.Model;
using SqlSugar;

namespace Newtouch.Infrastructure.EF
{
    public interface IRepositoryBase<TEntity> where TEntity : class, new()
    {
        List<TEntity> FindList(System.Linq.Expressions.Expression<System.Func<TEntity, bool>> predicate);
        List<TEntity> FindList(System.Linq.Expressions.Expression<System.Func<TEntity, bool>> predicate, Pagination pagination);
        IQueryable<TEntity> IQueryable();
        IQueryable<TEntity> IQueryable(Expression<Func<TEntity, bool>> predicate);
        TEntity FindEntity(Expression<Func<TEntity, bool>> predicate);
        TEntity FindEntity(string sql, params SugarParameter[] parameters);
        T2 FindEntity<T2>(string sql, params SugarParameter[] parameters);
        List<T2> FindList<T2>(string sql, params SugarParameter[] parameters);
        List<T2> SqlQuery<T2>(string sql, params SugarParameter[] parameters);
        int ExecuteSqlCommand(string sql, params SugarParameter[] parameters);
        int Insert(object entity);
        int Update(TEntity entity, IEnumerable<string> dstnFieldNameList = null, IEnumerable<string> ignoreFieldNameList = null, bool attach = true);
        int Update(object entity);
        int Delete(object entity);
        int Delete(Expression<Func<TEntity, bool>> predicate);
        List<T2> QueryWithPage<T2>(string sql, Pagination pagination, params SugarParameter[] parameters) where T2 : class, new();
    }
}

namespace FrameworkBase.MultiOrg.Domain.IDomainServices
{
}

namespace Newtouch.Common.Exceptions
{
    public class FailedException : NewtouchHIS.Lib.Base.Exceptions.FailedException
    {
        public FailedException(string message) : base(message) { }
        public FailedException(string code, string message) : base(code, message) { }
    }
}
