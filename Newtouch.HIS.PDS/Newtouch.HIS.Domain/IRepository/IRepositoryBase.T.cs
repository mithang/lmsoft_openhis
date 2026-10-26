namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    /// <typeparam name="TEntity">thực thể loại</typeparam>
    public interface IRepositoryBase<TEntity> : Newtouch.Infrastructure.EF.IRepositoryBase<TEntity>
        where TEntity : class, new()
    {

    }
}