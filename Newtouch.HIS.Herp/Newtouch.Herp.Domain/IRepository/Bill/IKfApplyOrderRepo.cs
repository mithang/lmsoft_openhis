using Newtouch.Herp.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// Phiếu lĩnh
    /// </summary>
    public interface IKfApplyOrderRepo : IRepositoryBase<KfApplyOrderEntity>
    {

        /// <summary>
        /// Tra cứu thông tin chính phiếu lĩnh
        /// </summary>
        /// <param name="sldh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        KfApplyOrderEntity SelectData(string sldh, string organizeId);
    }
}