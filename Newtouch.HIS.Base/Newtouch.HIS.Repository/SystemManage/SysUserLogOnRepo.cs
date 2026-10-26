using System;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysUserLogOnRepo : RepositoryBase<SysUserLogOnEntity>, ISysUserLogOnRepository
    {
        public SysUserLogOnRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// Cập nhật trạng thái có thể đăng nhập
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="locked"></param>
        public void UpdateLockedStatus(string userId, bool? locked)
        {
            var entity = this.IQueryable().Where(p => p.UserId == userId).FirstOrDefault();
            if (entity != null)
            {
                entity.Locked = locked;
                entity.Modify();
                this.Update(entity);
            }
        }
    }
}


