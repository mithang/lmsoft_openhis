using Newtouch.Infrastructure;

namespace Newtouch.HIS.Domain.DBContext.Infrastructure
{
    /// <summary>
    /// Factory DB context của thư viện base
    /// </summary>
    public class BaseDatabaseFactory : DatabaseFactory<BaseDbContext>, IBaseDatabaseFactory
    {

    }
}
