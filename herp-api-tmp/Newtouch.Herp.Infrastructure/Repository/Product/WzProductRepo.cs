using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// 物资维护
    /// </summary>
    public class WzProductRepo : RepositoryBase<WzProductEntity>, IWzProductRepo
    {
        public WzProductRepo()
        {
        }

    }
}
