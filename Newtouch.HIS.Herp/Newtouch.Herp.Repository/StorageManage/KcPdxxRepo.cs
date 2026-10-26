using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.Infrastructure.EF;
using System.Linq;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// Kiểm kê
    /// </summary>
    public class KcPdxxRepo : RepositoryBase<KcPdxxEntity>, IKcPdxxRepo
    {
        public KcPdxxRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// Theo kiểm kê ID Lấy thông tin kiểm kê
        /// </summary>
        /// <param name="pdId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public KcPdxxEntity SelectPdInfByPdId(long pdId, string organizeId)
        {
            return IQueryable().FirstOrDefault(p => p.Id == pdId && p.OrganizeId == organizeId);
        }

        /// <summary>
        /// Kiểm tra có kiểm kê chưa hoàn thành không (Kết thúc)
        /// </summary>
        /// <returns></returns>
        public int SelectUnfinishedInventoryByPdId(long pdId, string organizeId)
        {
            return IQueryable().Count(a => a.Id == pdId && a.jssj == null && a.OrganizeId == organizeId);
        }
    }
}
