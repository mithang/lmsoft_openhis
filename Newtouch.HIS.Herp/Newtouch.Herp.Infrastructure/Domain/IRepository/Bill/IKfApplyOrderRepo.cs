using Newtouch.Herp.Domain.Entity;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// Phiếu lĩnh
    /// </summary>
    public interface IKfApplyOrderRepo : IRepositoryBase<KfApplyOrderEntity>
    {

        /// <summary>
        /// Truy vấn thông tin chính phiếu lĩnh
        /// </summary>
        /// <param name="sldh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        KfApplyOrderEntity SelectData(string sldh, string organizeId);
    }
}