using Newtouch.Sett.Domain;
using System;
using System.Collections.Generic;

namespace Newtouch.Sett.DomainServices
{
    public class CommercialInsuranceRepo : ICommercialInsuranceRepo
    {
        public object DeleteForm(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

        public object GetForm(object p0, object p1)
        {
            return null;
        }

        public object GetListJson(object p0, object p1, object p2)
        {
            return null;
        }

    }

    public class SysCommercialInsuranceDmnService : ISysCommercialInsuranceDmnService
    {
        public List<SysCommercialInsuranceFilingVO> SelectCommercialInsuranceFilingList(string keyword, string orgId, string sbbabId = null)
        {
            return new List<SysCommercialInsuranceFilingVO>();
        }

        public object SubmitForm(object p0, object p1, object p2)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }

    public class SysCommercialInsuranceFilingRepo : ISysCommercialInsuranceFilingRepo
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
