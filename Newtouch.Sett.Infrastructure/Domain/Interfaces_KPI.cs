using System;
using System.Collections.Generic;

namespace Newtouch.Sett.Domain
{
    public interface IMonthProfitShareConfigDmnService
    {
        object DoGenerateMedicalOrgPS(object p0, object p1, object p2, object p3, object p4);
        object DoGenerateTherapeutistPS(object p0, object p1, object p2, object p3, object p4);
        object GetMedicalOrgPSConfig(object p0);
        object GetMedicalOrgPSConfigList(object p0, object p1);
        object GetTherapeutistPSConfig(object p0);
        object GetTherapeutistPSConfigList(object p0, object p1);
        object MedicalOrgPSCheckIsGenerated(object p0, object p1, object p2);
        object SubmitMedicalOrgPSConfig(object p0, object p1);
        object SubmitTherapeutistPSConfig(object p0, object p1);
        object TherapeutistPSCheckIsGenerated(object p0, object p1, object p2);
    }
}
