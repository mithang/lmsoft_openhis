using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Repository;
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
        /// Sửaform
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
