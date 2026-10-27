using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Repository;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRehabChargeItemComparisonRepo : IRepositoryBase<RehabChargeItemComparisonEntity>
    {
        /// <summary>
        /// Lưu
        /// </summary>
        void SubmitForm(RehabChargeItemComparisonEntity entity, string sfxmdzId);

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="keyValue"></param>
        void DeleteForm(string sfxmdzId,string OrganizeId);
    }
}
