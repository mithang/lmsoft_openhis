using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysUserLogOnRepository : IRepositoryBase<SysUserLogOnEntity>
    {
        /// <summary>
        /// Cập nhật trạng thái có thể đăng nhập
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="locked"></param>
        void UpdateLockedStatus(string userId, bool? locked);

    }
}
