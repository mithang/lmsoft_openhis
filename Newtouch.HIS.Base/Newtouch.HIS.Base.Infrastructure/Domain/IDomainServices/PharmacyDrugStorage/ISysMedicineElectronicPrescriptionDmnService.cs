using Newtouch.HIS.Domain.ValueObjects;
using NewtouchHIS.Lib.Base.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage
{
    public interface ISysMedicineElectronicPrescriptionDmnService
    {
        IList<SysMedicineElectronicPrescriptionVO> GetPaginationList(Pagination pagination, string genname, string medListCodg, string listType, string zt);
    }
}
