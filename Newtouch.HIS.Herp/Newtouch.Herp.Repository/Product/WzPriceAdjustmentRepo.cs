using System.Collections.Generic;
using System.Linq;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure.Enum;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// Điều chỉnh giá vật tư
    /// </summary>
    public class WzPriceAdjustmentRepo : RepositoryBase<WzPriceAdjustmentEntity>, IWzPriceAdjustmentRepo
    {
        public WzPriceAdjustmentRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// Lấy danh sách chưa thực hiện
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<WzPriceAdjustmentEntity> GetUnexecutedList(string productId, string organizeId)
        {
            return IQueryable(p => p.productId == productId
                                   && p.OrganizeId == organizeId
                                   && p.zxbz == ((int)EnumTjZxbz.Not).ToString()
                                   && (p.shzt == ((int)EnumTjShzt.Audited).ToString() || p.shzt == ((int)EnumTjShzt.Waiting).ToString())).ToList();
        }

    }
}
