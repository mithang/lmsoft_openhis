using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;

namespace Newtouch.Repository
{
    /// <summary>
    /// Bản ghi thảo dược đã đẩy của phòng khám Đông y
    /// </summary>
    public class CmmHis02RecordRepo : RepositoryBase<CmmHis02RecordEntity>, ICmmHis02RecordRepo
    {
        public CmmHis02RecordRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}