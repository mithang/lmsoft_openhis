using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// Người tạo: chl
    /// Ngày: 2022-06-28 21:50
    /// Mô tả: Inject interface
    /// </summary>
    public class AccesscenterRepo : RepositoryBase<AccesscenterEntity>, IAccesscenterRepo
    {
        /// <summary>
        /// Hàm tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">Factory EF context</param>
        public AccesscenterRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }

    }
}