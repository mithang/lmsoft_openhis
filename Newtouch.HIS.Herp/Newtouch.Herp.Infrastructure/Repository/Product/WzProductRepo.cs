using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// Bảo trì vật tư
    /// </summary>
    public class WzProductRepo : RepositoryBase<WzProductEntity>, IWzProductRepo
    {
        public WzProductRepo()
        {
        }

    }
}
