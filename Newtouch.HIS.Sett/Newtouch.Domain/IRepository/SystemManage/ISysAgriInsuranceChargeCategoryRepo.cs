using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// Loại lớn thu phí Bảo hiểm nông nghiệp
    /// </summary>
    public interface ISysAgriInsuranceChargeCategoryRepo : IRepositoryBase<SysAgriInsuranceChargeCategoryVEntity>
    {
        /// <summary>
        /// Lấy hộp thả xuống tất cả loại lớn thu phí Bảo hiểm nông nghiệp
        /// </summary>
        /// <returns></returns>
        IList<SysAgriInsuranceChargeCategoryVEntity> GetnbdlSelect(string orgId);
    }
}
