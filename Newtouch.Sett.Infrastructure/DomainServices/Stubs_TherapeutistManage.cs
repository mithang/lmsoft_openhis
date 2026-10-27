using Newtouch.Sett.Domain;
using System;
using System.Collections.Generic;

namespace Newtouch.Sett.DomainServices
{
    public class TherapeutistCompleteDmnService : ITherapeutistCompleteDmnService
    {
        public object DelArrange(object p0)
        {
            return null;
        }

        public object EditRehabDoctorRange(object p0, object p1, object p2)
        {
            return null;
        }

        public object GetAllRehabDoctor(object p0)
        {
            return null;
        }

        public object GetFormJson(object p0, object p1)
        {
            return null;
        }

        public object GetStaffEachMonthHour(object p0, object p1, object p2, object p3, object p4)
        {
            return null;
        }

        public object GetStaffReport(object p0, object p1, object p2, object p3, object p4)
        {
            return null;
        }

        public object GetStaffWorkedHour(object p0, object p1, object p2, object p3)
        {
            return null;
        }

        public object GetTherapeutistListWorkedList(object p0, object p1, object p2, object p3, object p4, object p5, object p6)
        {
            return null;
        }

        public object GetTherapeutistWorkPlanList(object p0, object p1, object p2, object p3, object p4)
        {
            return null;
        }

        public object GetVisitDetailSC(object p0, object p1, object p2, object p3)
        {
            return null;
        }

        public SCNumBO GetVisitSC(string orgId)
        {
            return new SCNumBO();
        }

        public object SubmitPlan(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

        public object UpdateTime(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }

    public class TherapistSuggestionDmnService : ITherapistSuggestionDmnService
    {
        public List<MzZyPatInfoDto> GetMzZyPatInfo(string mzzyh, string orgId, string brlx)
        {
            return new List<MzZyPatInfoDto>();
        }

        public List<TherapistAdviceDto> GetZLJYList(string mzzyh, string orgId, string brlx)
        {
            return new List<TherapistAdviceDto>();
        }

        public void SaveData(IList<TherapistSuggestionEntity> itemList, IList<string> delIds, string orgId)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }
}
