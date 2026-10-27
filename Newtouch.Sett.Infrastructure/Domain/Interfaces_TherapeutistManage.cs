using System;
using System.Collections.Generic;

namespace Newtouch.Sett.Domain
{
    public interface ITherapeutistCompleteDmnService
    {
        object DelArrange(object p0);
        object EditRehabDoctorRange(object p0, object p1, object p2);
        object GetAllRehabDoctor(object p0);
        object GetFormJson(object p0, object p1);
        object GetStaffEachMonthHour(object p0, object p1, object p2, object p3, object p4);
        object GetStaffReport(object p0, object p1, object p2, object p3, object p4);
        object GetStaffWorkedHour(object p0, object p1, object p2, object p3);
        object GetTherapeutistListWorkedList(object p0, object p1, object p2, object p3, object p4, object p5, object p6);
        object GetTherapeutistWorkPlanList(object p0, object p1, object p2, object p3, object p4);
        object GetVisitDetailSC(object p0, object p1, object p2, object p3);
        // Source-true: TherapeutistCompleteDmnService.cs:95
        SCNumBO GetVisitSC(string orgId);
        object SubmitPlan(object p0, object p1);
        object UpdateTime(object p0);
    }

    // Source-true: Newtouch.Domain/IDomainServices/TherapeutistCompleteManage/
    // TherapistSuggestionDmnService.cs
    public interface ITherapistSuggestionDmnService
    {
        List<MzZyPatInfoDto> GetMzZyPatInfo(string mzzyh, string orgId, string brlx);
        List<TherapistAdviceDto> GetZLJYList(string mzzyh, string orgId, string brlx);
        void SaveData(IList<TherapistSuggestionEntity> itemList, IList<string> delIds, string orgId);
    }
}
