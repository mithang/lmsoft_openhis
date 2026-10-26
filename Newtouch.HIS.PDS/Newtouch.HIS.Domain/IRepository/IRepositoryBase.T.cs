namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// Giao diện kho lưu trữ
    /// </summary>
    /// <typeparam name="TEntity">thực thể loại</typeparam>
    public interface IRepositoryBase<TEntity> : Newtouch.Infrastructure.EF.IRepositoryBase<TEntity>
        where TEntity : class, new()
    {

    }
}