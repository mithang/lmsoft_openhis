using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.Infrastructure.EF;
using System.Linq;
using SqlSugar;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// Chênh lệch
    /// </summary>
    public class KcSyxxRepo : RepositoryBase<KcSyxxEntity>, IKcSyxxRepo
    {
        public KcSyxxRepo()
        {
        }
    }
}
