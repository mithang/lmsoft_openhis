using Newtouch.Herp.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// Chi tiết chứng từ nhập/xuất kho
    /// </summary>
    public interface IKfCrkmxRepo : IRepositoryBase<KfCrkmxEntity>
    {
        /// <summary>
        /// Sửa số hóa đơn
        /// </summary>
        /// <param name="crkmxId"></param>
        /// <param name="fph"></param>
        /// <returns></returns>
        int UpdateFph(long crkmxId, string fph);
    }
}