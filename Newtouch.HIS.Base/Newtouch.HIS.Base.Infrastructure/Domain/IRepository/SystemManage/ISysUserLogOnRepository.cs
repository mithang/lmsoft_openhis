using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Repository;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysUserLogOnRepository : IRepositoryBase<SysUserLogOnEntity>
    {
        /// <summary>
        /// Cập nhật có thể Đăng nhập Trạng thái
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="locked"></param>
        void UpdateLockedStatus(string userId, bool? locked);

    }
}
