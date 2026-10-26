using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Herp.Domain.Entity.Product;
using Newtouch.Herp.Domain.IRepository;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// Bảng đối chiếu khoản thu vật tư
    /// </summary>
    public class RelProductAndsfxmRepo : RepositoryBase<RelProductAndsfxmEntity>, IRelProductAndsfxmRepo
    {
        public RelProductAndsfxmRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
