using System;
using System.Collections.Generic;

namespace Newtouch.Sett.Domain
{
    public interface IDataAnalysisDmnService
    {
        // no calls observed
    }

    public interface IHighchartsDmnService
    {
        object GetInTherapistDischarge(object p0, object p1);
        object GetInTherapistVisit(object p0, object p1);
        object GetInTherapistavgDischarge(object p0, object p1);
        object GetInTherapistavgVisit(object p0, object p1);
        System.Collections.Generic.List<InpatientVisitNumVO> GetInpatientDischargeNum(object p0);
        object GetInpatientSalaryNum(object p0);
        System.Collections.Generic.List<OutpatientVisitNumVO> GetInpatientVisitNum(object p0);
        object GetMonthInTherapistDischarge(object p0, object p1, object p2);
        object GetMonthInTherapistVisit(object p0, object p1);
        object GetMonthInTherapistVisit(object p0, object p1, object p2);
        object GetMonthInTherapistavgVisit(object p0, object p1);
        object GetMonthInTherapistavgVisit(object p0, object p1, object p2);
        object GetMonthTherapistDischarge(object p0, object p1, object p2);
        object GetMonthTherapistVisit(object p0, object p1, object p2);
        object GetMonthTherapistavgDischarge(object p0, object p1, object p2);
        object GetMonthTherapistavgVisit(object p0, object p1, object p2);
        object GetOutpatientSalaryNum(object p0);
        object GetOutpatientVisitNum(object p0);
        object GetOutpatientVisitPerNum(object p0);
        object GetSalaryNumCompared(object p0);
        object GetTherapistDischarge(object p0, object p1);
        object GetTherapistVisit(object p0, object p1);
        object GetTherapistavgDischarge(object p0, object p1);
        object GetTherapistavgVisit(object p0, object p1);
    }
}
