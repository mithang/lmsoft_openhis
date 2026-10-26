using Newtouch.HIS.Base.HOSP.Request;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Từ điển, mục từ điển DmnService
    /// </summary>
    public interface ICISApiDmnService
    {
        
        IList<MedicineInfoVO2> GETYNMidecine(MedicineQueryRequest request);

        IList<MedicineInfoVO2> GETYNzlxm(MedicineQueryRequest request);

    }
}
