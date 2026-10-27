using NewtouchHIS.Lib.Base.Model;
using Newtouch.Domain.ValueObjects;
using Newtouch.Domain.ValueObjects.Inpatient;
using System;
using System.Collections.Generic;

namespace Newtouch.Domain.IDomainServices
{
    public interface ITreatmentPrintDmnService
    {
        /// <summary>
        /// LấyKhu bệnhphát thuốcBệnh nhâncây
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
         IList<InpWardPatTreeVO> GetPatTree(string orgId);

        IList<TreatmentPrintVO> GetDetailGridJson(Pagination pagination, string orgId,string zyh, DateTime? kssj, DateTime? jssj, int? zyxz);
    }
}
