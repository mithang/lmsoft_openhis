using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// Bản ghi thao tác thuốc ngoại trú
    /// </summary>
    public class MzCfypczjlRepo : RepositoryBase<MzCfypczjlEntity>, IMzCfypczjlRepo
    {
        public MzCfypczjlRepo() { }
        public MzCfypczjlRepo(FrameworkBase.MultiOrg.Infrastructure.IDefaultDatabaseFactory databaseFactory) : base(databaseFactory) { }
            }
}
