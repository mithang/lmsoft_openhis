using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// Nhập viện mô phỏng quyết toán-chi phí bảo hiểm y tế-Quý An
    /// </summary>
    public class HospSimulateSettlementGAYBFeeRepo : RepositoryBase<HospSimulateSettlementGAYBFeeEntity>, IHospSimulateSettlementGAYBFeeRepo
    {
        public HospSimulateSettlementGAYBFeeRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

    }
}