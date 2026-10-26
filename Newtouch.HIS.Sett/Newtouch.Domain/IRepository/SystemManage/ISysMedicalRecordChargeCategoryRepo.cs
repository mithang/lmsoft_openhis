using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// Loại lớn thu phí bệnh án
    /// </summary>
    public interface ISysMedicalRecordChargeCategoryRepo : IRepositoryBase<SysMedicalRecordChargeCategoryVEntity>
    {
        /// <summary>
        /// Lấy hộp thả xuống tất cả loại lớn thu phí bệnh án
        /// </summary>
        /// <returns></returns>
        IList<SysMedicalRecordChargeCategoryVEntity> GetdlSelect(string orgId);
    }
}
