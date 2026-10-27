using System;
using System.Collections.Generic;

namespace Newtouch.Sett.Domain
{
    public interface IDaySettleDmnService
    {
        object CancleDaySettleInfo(object p0, object p1);
        object GetHistoryCheckList(object p0, object p1, object p2);
        object GetOutPatientSettListJson(object p0, object p1, object p2, object p3, object p4);
        object GetQsdzzs(object p0, object p1, object p2, object p3, object p4);
        object GetQsht(object p0, object p1, object p2);
        object GetQsmx(object p0, object p1, object p2, object p3, object p4);
        object GetQsmx_1(object p0, object p1, object p2, object p3, object p4);
        object GetQssq(object p0, object p1, object p2, object p3);
        object Getclr_type();
        object Getinsutype();
        object Newdzfysj(object p0, object p1);
        object SaveInpatientDaySettleInfo(object p0, object p1, object p2, object p3);
        object SaveOutpatientDaySettleInfo(object p0, object p1, object p2, object p3);
        string getLastSettDate(string mzzybz);
        object inserqssq(object p0, object p1);
        object lsdzList(object p0, object p1, object p2);
    }
}
