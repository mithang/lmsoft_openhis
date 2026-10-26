using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// Nhóm thu phí lớn hồ sơ bệnh án
    /// </summary>
    public interface ISysMedicalRecordChargeCategoryRepo : IRepositoryBase<SysMedicalRecordChargeCategoryEntity>
    {
        /// <summary>
        /// Lấy tất cả
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        IList<SysMedicalRecordChargeCategoryEntity> GetList(string orgId, string keyword = null);

        /// <summary>
        /// Lấy các mục có hiệu lực
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        IList<SysMedicalRecordChargeCategoryEntity> GetValidList(string orgId);

        /// <summary>
        /// Lấy dữ liệu thực thể
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        SysMedicalRecordChargeCategoryEntity GetForm(int keyValue);

        /// <summary>
        /// Gửi để lưu
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysMedicalRecordChargeCategoryEntity entity, int? keyValue);
    }
}
