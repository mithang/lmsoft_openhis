using Newtouch.Sett.Domain;
using System;
using System.Collections.Generic;

namespace Newtouch.Sett.DomainServices
{
    public class MedicalInsuranceDmnService : IMedicalInsuranceDmnService
    {
        public List<SysMedicalInsuranceFilingVO> SelectMedicalInsuranceFilingList(NewtouchHIS.Lib.Base.Model.Pagination pagination, string keyword, string orgId, string ybbabId = null)
        {
            return new List<SysMedicalInsuranceFilingVO>();
        }

    }

    public class SysMedicalInsuranceFilingRepo : ISysMedicalInsuranceFilingRepo
    {
        public object DeleteForm(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

        public object SubmitForm(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }
}
