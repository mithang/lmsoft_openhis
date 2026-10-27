using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// V_S_xt_yp
    /// </summary>
    public interface ISysMedicineRepo : IRepositoryBase<SysMedicineVEntity>
    {
        /// <summary>
        /// Lấy một đối tượng thuốc
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        SysMedicineVEntity GetMedicineByCode(string orgId, string ypCode);

        /// <summary>
        /// Lấy một tập hợp thuốc
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<SysMedicineVEntity> GetMedicineListByOrg(string orgId);
    }
}
