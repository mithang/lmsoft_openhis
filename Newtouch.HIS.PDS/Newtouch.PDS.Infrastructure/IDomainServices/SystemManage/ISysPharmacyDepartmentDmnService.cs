using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Bộ phận nhà thuốc
    /// </summary>
    public interface ISysPharmacyDepartmentDmnService
    {

        /// <summary>
        /// Lấy nhà thuốc kho thuốc đã ủy quyền theo mã thuốc
        /// </summary>
        /// <param name="ypId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<SysPharmacyDepartmentVEntity> SelectEmpowermentYfbmByYp(string ypId, string organizeId);
    }
}