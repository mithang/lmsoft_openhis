using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// Chi tiết đơn mua hàng
    /// </summary>
    public class CgOrderDetailRepo : RepositoryBase<CgOrderDetailEntity>, ICgOrderDetailRepo
    {
        public CgOrderDetailRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}