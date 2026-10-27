

using Newtouch.Infrastructure;
using Newtouch.CIS.Repository;
using System.Linq;
using Newtouch.CIS.Compat;
using Newtouch.Domain.Entity.DeptStorageManage;
using Newtouch.Domain.IRepository.DeptStorageManage;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// 损益
    /// </summary>
    public class KcSyxxRepo : RepositoryBase<KcSyxxEntity>, IKcSyxxRepo
    {
        public KcSyxxRepo() : base()
        {
        }
    }
}
