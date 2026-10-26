using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// Quyết toán nhập viện-chi phí bảo hiểm y tế-贵安
    /// </summary>
    public class HospSettlementGAYBZYMXXRFeeRepo : RepositoryBase<HospSettlementGAYBZYMXXRFeeEntity>, IHospSettlementGAYBZYMXXRFeeRepo
    {
        public HospSettlementGAYBZYMXXRFeeRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

    }
}