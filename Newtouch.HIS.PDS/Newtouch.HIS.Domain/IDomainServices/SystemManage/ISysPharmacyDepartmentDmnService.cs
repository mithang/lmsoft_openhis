using System.Collections.Generic;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// bộ phận nhà thuốc
    /// </summary>
    public interface ISysPharmacyDepartmentDmnService
    {

        /// <summary>
        /// theo mã thuốc lấy đã ủy quyền của nhà thuốc kho thuốc
        /// </summary>
        /// <param name="ypId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<SysPharmacyDepartmentVEntity> SelectEmpowermentYfbmByYp(string ypId, string organizeId);
    }
}