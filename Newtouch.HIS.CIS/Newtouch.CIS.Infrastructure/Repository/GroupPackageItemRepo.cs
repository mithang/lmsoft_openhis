using Newtouch.CIS.Compat;

using Newtouch.CIS.Repository;
using Newtouch.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class GroupPackageItemRepo : RepositoryBase<GroupPackageItemEntity>, IGroupPackageItemRepo
    {
        public GroupPackageItemRepo()
            : base()
        {
        }
    }
}
