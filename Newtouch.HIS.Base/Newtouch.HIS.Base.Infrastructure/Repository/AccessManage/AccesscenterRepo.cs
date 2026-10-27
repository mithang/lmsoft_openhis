using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Repository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// Ported from Newtouch.HIS.Repository/AccessManage/AccesscenterRepo.cs (same
    /// Compile-list skip as AccessAuthRepo). No custom methods: base generic repo only.
    /// </summary>
    public class AccesscenterRepo : RepositoryBase<AccesscenterEntity>, IAccesscenterRepo
    {
    }
}
