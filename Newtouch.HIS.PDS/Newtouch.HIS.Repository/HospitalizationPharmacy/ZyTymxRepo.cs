using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// Chi tiết trả thuốc nội trú
    /// </summary>
    public class ZyTymxRepo: RepositoryBase<ZyTymxEntity>, IZyTymxRepo
    {
        public ZyTymxRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }


    }
}