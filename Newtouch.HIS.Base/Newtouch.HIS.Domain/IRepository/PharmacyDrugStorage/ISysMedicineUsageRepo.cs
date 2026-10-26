using Newtouch.Common;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysMedicineUsageRepo : IRepositoryBase<SysMedicineUsageEntity>
    {
        /// <summary>
        /// Lưu
        /// </summary>
       void SubmitForm(SysMedicineUsageEntity entity, int? yfId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysMedicineUsageEntity> GetPagintionList(Pagination pagination, string keyword = null);

        /// <summary>
        /// Lấy tất cả danh sách
        /// </summary>
        List<SysMedicineUsageEntity> GetMedicineUsageList(string keyword = null);

        /// <summary>
        /// Sửa đổi form
        /// </summary>
        SysMedicineUsageEntity GetMedicineUsageEntity(int? yfId);
        
        /// <summary>
        /// Xóa
        /// </summary>
        void DeleteForm(int yfId);
    }
}
