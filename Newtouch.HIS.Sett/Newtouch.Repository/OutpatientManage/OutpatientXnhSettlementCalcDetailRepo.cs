using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// Ngoại trú BHYT nông thôn mới quyết toán
    /// </summary>
    public class OutpatientXnhSettlementCalcDetailRepo : RepositoryBase<OutpatientXnhSettlementCalcDetailEntity>, IOutpatientXnhSettlementCalcDetailRepo
    {
        public OutpatientXnhSettlementCalcDetailRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }


    }
}