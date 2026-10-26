using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRehabChargeClassificationRepo : IRepositoryBase<RehabChargeClassificationEntity>
    {
        /// <summary>
        /// Lấy tất cả danh sách
        /// </summary>
        List<RehabChargeClassificationEntity> GetRehabChargeClassificationList(string OrganizeId, string keyword = null);

        /// <summary>
        /// Sửa đổi form
        /// </summary>
        RehabChargeClassificationEntity GetRehabChargeClassificationEntity(string sfflId, string OrganizeId);

        /// <summary>
        /// Lưu
        /// </summary>
        void SubmitForm(RehabChargeClassificationEntity entity, string sfflId);
        
        /// <summary>
        /// Xóa
        /// </summary>
        void DeleteForm(string sfflId, string OrganizeId);
    }
}
