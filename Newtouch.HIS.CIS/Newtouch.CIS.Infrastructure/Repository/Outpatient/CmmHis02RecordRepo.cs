using Newtouch.CIS.Compat;

using Newtouch.CIS.Repository;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;

namespace Newtouch.Repository
{
    /// <summary>
    /// trong y quán đã đẩy thảo dược ghi chép
    /// </summary>
    public class CmmHis02RecordRepo : RepositoryBase<CmmHis02RecordEntity>, ICmmHis02RecordRepo
    {
        public CmmHis02RecordRepo() : base()
        {
        }
    }
}