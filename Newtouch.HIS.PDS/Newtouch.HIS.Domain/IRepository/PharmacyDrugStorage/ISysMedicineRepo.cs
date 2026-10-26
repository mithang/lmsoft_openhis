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
        /// lấy một cái thuốc đối tượng
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        SysMedicineVEntity GetMedicineByCode(string orgId, string ypCode);

        /// <summary>
        /// lấy một cái thuốc tập hợp
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<SysMedicineVEntity> GetMedicineListByOrg(string orgId);
    }
}
