using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System.Collections.Generic;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.ValueObjects;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// Dạng bào chế thuốc V_S_xt_ypjx 
    /// </summary>
    public class SysMedicineFormulationRepo : RepositoryBase<SysMedicineFormulationVEntity>, ISysMedicineFormulationRepo
    {
        
        /// <summary>
        /// Trả về dạng bào chế thuốclist
        /// </summary>
        /// <returns></returns>
        public List<MedicineFormulationVO> GetMedicineFormulationList()
        {
            return FindList<MedicineFormulationVO>(@"SELECT DISTINCT jxCode,jxmc from [NewtouchHIS_Base].dbo.V_S_xt_ypjx(nolock) WHERE zt='1' ORDER BY jxmc");
        }
    }
}
