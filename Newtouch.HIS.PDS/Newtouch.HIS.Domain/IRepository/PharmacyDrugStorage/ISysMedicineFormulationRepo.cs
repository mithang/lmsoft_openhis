using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;
using Newtouch.HIS.Domain.ValueObjects;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// V_S_xt_ypjx
    /// </summary>
    public interface ISysMedicineFormulationRepo : IRepositoryBase<SysMedicineFormulationVEntity>
    {
        /// <summary>
        /// quay lại dạng bào chế thuốc list
        /// </summary>
        /// <returns></returns>
        List<MedicineFormulationVO> GetMedicineFormulationList();
    }
}
