

using Newtouch.Infrastructure;
using Newtouch.CIS.Repository;
using System.Linq;
using Newtouch.CIS.Compat;
using Newtouch.Domain.Entity.DeptStorageManage;
using Newtouch.Domain.IRepository.DeptStorageManage;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// Kiểm kê
    /// </summary>
    public class KcPdxxRepo : RepositoryBase<KcPdxxEntity>, IKcPdxxRepo
    {
        public KcPdxxRepo() : base()
        {
        }

        /// <summary>
        /// TheoKiểm kêIDLấyKiểm kêthông tin
        /// </summary>
        /// <param name="pdId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public KcPdxxEntity SelectPdInfByPdId(long pdId, string organizeId)
        {
            return IQueryable().FirstOrDefault(p => p.Id == pdId && p.OrganizeId == organizeId);
        }

        /// <summary>
        /// kiểm tralàKhôngcóchưaHoàn thànhcủaKiểm kê (Kết thúc)
        /// </summary>
        /// <returns></returns>
        public int SelectUnfinishedInventoryByPdId(long pdId, string organizeId)
        {
            return IQueryable().Count(a => a.Id == pdId && a.jssj == null && a.OrganizeId == organizeId);
        }
    }
}
