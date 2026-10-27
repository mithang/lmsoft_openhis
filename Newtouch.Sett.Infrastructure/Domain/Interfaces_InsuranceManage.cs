using System;
using System.Collections.Generic;

namespace Newtouch.Sett.Domain
{
    public interface ICommercialInsuranceRepo
    {
        object DeleteForm(object p0);
        object GetForm(object p0, object p1);
        object GetListJson(object p0, object p1, object p2);
    }

    public interface ISysCommercialInsuranceDmnService
    {
        List<SysCommercialInsuranceFilingVO> SelectCommercialInsuranceFilingList(string keyword, string orgId, string sbbabId = null);
        object SubmitForm(object p0, object p1, object p2);
    }

    public interface ISysCommercialInsuranceFilingRepo
    {
        object DeleteForm(object p0, object p1);
        object SubmitForm(object p0, object p1);
    }
}
