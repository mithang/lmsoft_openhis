using System;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;

namespace Newtouch.Sett.Domain
{
    public interface IMedicalInsuranceDmnService
    {
        List<SysMedicalInsuranceFilingVO> SelectMedicalInsuranceFilingList(Pagination pagination, string keyword, string orgId, string ybbabId = null);
    }

    public interface ISysMedicalInsuranceFilingRepo
    {
        object DeleteForm(object p0, object p1);
        object SubmitForm(object p0, object p1);
    }
}
