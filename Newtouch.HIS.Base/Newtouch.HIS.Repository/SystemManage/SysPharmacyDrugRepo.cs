using Newtouch.HIS.Domain;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// Thuốc của phòng dược
    /// </summary>
    public class SysPharmacyDrugRepo : RepositoryBase<SysPharmacyDepartmentOpenMedicineEntity>, ISysPharmacyDrugRepo
    {
        public SysPharmacyDrugRepo(IBaseDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}