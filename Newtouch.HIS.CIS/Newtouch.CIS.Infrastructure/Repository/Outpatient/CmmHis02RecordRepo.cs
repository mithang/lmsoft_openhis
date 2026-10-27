using Newtouch.CIS.Compat;

using Newtouch.CIS.Repository;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;

namespace Newtouch.Repository
{
    /// <summary>
    /// 中医馆已推草药记录
    /// </summary>
    public class CmmHis02RecordRepo : RepositoryBase<CmmHis02RecordEntity>, ICmmHis02RecordRepo
    {
        public CmmHis02RecordRepo() : base()
        {
        }
    }
}