using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Repository;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRehabChargeItemRepo : IRepositoryBase<RehabChargeItemEntity>
    {
        /// <summary>
        /// Lưu
        /// </summary>
        void SubmitForm(RehabChargeItemEntity entity, string sfflId);

        /// <summary>
        /// Xóa
        /// </summary>
        void DeleteForm(string sfflId, string OrganizeId);

    }
}
