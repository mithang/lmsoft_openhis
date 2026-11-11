using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// xt_yp_crkmx
    /// </summary>
    public interface ISysMedicineStorageIOReceiptDetailRepo : IRepositoryBase<SysMedicineStorageIOReceiptDetailEntity>
    {
        /// <summary>
        /// Sửa số hóa đơn
        /// </summary>
        /// <param name="crkmxId"></param>
        /// <param name="fph"></param>
        /// <param name="organizeId">Tổ chức hiện tại (chi tiết phải thuộc chứng từ của tổ chức này)</param>
        /// <returns></returns>
        int UpdateFph(string crkmxId, string fph, string organizeId);
    }
}
