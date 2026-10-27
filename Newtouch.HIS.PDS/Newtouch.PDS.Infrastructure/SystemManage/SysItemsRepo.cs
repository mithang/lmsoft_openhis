using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysItemsRepo : RepositoryBase<SysItemsEntity>, ISysItemsRepo
    {
        public SysItemsRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// Lấy phân loại từ điển（Gồm cả không hiệu lực）
        /// </summary>
        /// <returns></returns>
        public IList<SysItemsEntity> GetList(string keyword = null)
        {
            var query = this.IQueryable();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(p => p.Name.Contains(keyword) || p.Code.Contains(keyword));
            }
            return query.OrderBy(t => t.px).ToList();
        }
    }
}
