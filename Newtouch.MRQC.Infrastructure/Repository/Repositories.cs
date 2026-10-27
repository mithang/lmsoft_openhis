using Newtouch.MRQC.Domain;
using Newtouch.MRQC.Domain.Entity.QcItemManage;
using Newtouch.MRQC.Infrastructure;
using SqlSugar;
using System;
using System.Linq.Expressions;

namespace Newtouch.MRQC.Repository
{
    /// <summary>
    /// SqlSugar generic repository. Replaces FrameworkBase RepositoryBase&lt;T&gt;
    /// (EF6 + Newtouch.Infrastructure.EF, Framework-only, no source).
    /// </summary>
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class, new()
    {
        public ISugarQueryable<T> IQueryable()
        {
            return MrqcDb.GetConnection().Queryable<T>();
        }

        public ISugarQueryable<T> IQueryable(Expression<Func<T, bool>> predicate)
        {
            return MrqcDb.GetConnection().Queryable<T>().Where(predicate);
        }

        public int Delete(Expression<Func<T, bool>> predicate)
        {
            return MrqcDb.GetConnection().Deleteable<T>().Where(predicate).ExecuteCommand();
        }
    }

    public class MrApplyRepo : RepositoryBase<MrApplyEntity>, IMrApplyRepo
    {
    }

    public class QcItemDataRepo : RepositoryBase<QcItemDataEntity>, IQcItemDataRepo
    {
        public void DeleteForm(int Id, string orgId)
        {
            this.Delete(a => a.Id == Id && a.OrganizeId == orgId);
        }
    }
}
