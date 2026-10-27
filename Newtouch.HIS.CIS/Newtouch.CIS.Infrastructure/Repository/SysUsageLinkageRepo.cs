using Newtouch.CIS.Compat;

using Newtouch.CIS.Repository;
using Newtouch.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.Repository
{

    /// <summary>
    /// Hệ thốngCách dùngliênđộng
    /// </summary>
    public class SysUsageLinkageRepo : RepositoryBase<SysUsageLinkageEntity>, ISysUsageLinkageRepo
    {
        public SysUsageLinkageRepo() : base()
        {
        }

    }
}
