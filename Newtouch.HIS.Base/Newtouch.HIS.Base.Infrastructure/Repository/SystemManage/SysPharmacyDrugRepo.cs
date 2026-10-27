using Newtouch.HIS.Domain;
using Newtouch.HIS.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// Thuốc của khoa dược
    /// </summary>
    public class SysPharmacyDrugRepo : RepositoryBase<SysPharmacyDepartmentOpenMedicineEntity>, ISysPharmacyDrugRepo
    {
        public SysPharmacyDrugRepo() 
        {
        }
    }
}