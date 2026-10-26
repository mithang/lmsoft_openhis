using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// Bảo trì vật tư
    /// </summary>
    public class WzProductRepo : RepositoryBase<WzProductEntity>, IWzProductRepo
    {
        public WzProductRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

    }
}
