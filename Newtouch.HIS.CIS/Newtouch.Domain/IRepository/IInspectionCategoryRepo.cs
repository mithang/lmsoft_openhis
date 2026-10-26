using Newtouch.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IInspectionCategoryRepo : IRepositoryBase<InspectionCategoryEntity>
    {
        /// <summary>
        /// Lấy danh sách
        /// </summary>
        /// <returns></returns>
        List<InspectionCategoryEntity> GetListByOrg(string orgId);

        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(InspectionCategoryEntity entity, string keyValue);

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="orgId"></param>
        void DeleteForm(string keyValue);
    }
}
