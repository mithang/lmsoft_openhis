using FrameworkBase.MultiOrg.Infrastructure;

using Newtouch.Infrastructure;
using Newtouch.Infrastructure.EF;
using System.Linq;
using Newtouch.Domain.Entity.DeptStorageManage;
using Newtouch.Domain.IRepository.DeptStorageManage;

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
        /// Lấy thông tin kiểm kê theo ID kiểm kê
        /// </summary>
        /// <param name="pdId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public KcPdxxEntity SelectPdInfByPdId(long pdId, string organizeId)
        {
            return IQueryable().FirstOrDefault(p => p.Id == pdId && p.OrganizeId == organizeId);
        }

        /// <summary>
        /// Xác định có kiểm kê chưa hoàn thành hay không (kết thúc)
        /// </summary>
        /// <returns></returns>
        public int SelectUnfinishedInventoryByPdId(long pdId, string organizeId)
        {
            return IQueryable().Count(a => a.Id == pdId && a.jssj == null && a.OrganizeId == organizeId);
        }
    }
}
