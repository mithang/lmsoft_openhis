using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// Chi tiết kế hoạch mua hàng
    /// </summary>
    public class CgPurchaseOrderDetailRepo : RepositoryBase<CgPurchaseOrderDetailEntity>, ICgPurchaseOrderDetailRepo
    {
        public CgPurchaseOrderDetailRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
