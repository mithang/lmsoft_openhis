using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// Nhật ký thao tác y lệnh thuốc nội trú
    /// </summary>
    public class ZyYpyzczjlRepo : RepositoryBase<ZyYpyzczjlEntity>, IZyYpyzczjlRepo
    {
        public ZyYpyzczjlRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
