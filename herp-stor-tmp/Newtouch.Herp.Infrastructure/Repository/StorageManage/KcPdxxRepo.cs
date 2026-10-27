using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.Infrastructure.EF;
using System.Linq;
using SqlSugar;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// 盘点
    /// </summary>
    public class KcPdxxRepo : RepositoryBase<KcPdxxEntity>, IKcPdxxRepo
    {
        public KcPdxxRepo()
        {
        }

        /// <summary>
        /// 根据盘点ID获取盘点信息
        /// </summary>
        /// <param name="pdId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public KcPdxxEntity SelectPdInfByPdId(long pdId, string organizeId)
        {
            return IQueryable().Where(p => p.Id == pdId && p.OrganizeId == organizeId).ToList().FirstOrDefault();
        }

        /// <summary>
        /// 判断是否有未完成的盘点 （结束）
        /// </summary>
        /// <returns></returns>
        public int SelectUnfinishedInventoryByPdId(long pdId, string organizeId)
        {
            return IQueryable().Count(a => a.Id == pdId && a.jssj == null && a.OrganizeId == organizeId);
        }
    }
}
