using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysMedicalOrderFrequencyRepo : IRepositoryBase<SysMedicalOrderFrequencyEntity>
    {

        /// <summary>
        /// Lưu
        /// </summary>
        void SubmitForm(SysMedicalOrderFrequencyEntity entity, int? yzpcId);

        /// <summary>
        /// Lấy tất cả danh sách
        /// </summary>
        List<SysMedicalOrderFrequencyEntity> GetOrderFrequencyList(string orgId, string keyword = null);

        /// <summary>
        /// Sửa đổi form
        /// </summary>
        SysMedicalOrderFrequencyEntity GetOrderFrequencyEntity(int? yzpcId, string orgId);

        /// <summary>
        /// Xóa
        /// </summary>
        void DeleteForm(string orgId, int yzpcId);
    }
}
