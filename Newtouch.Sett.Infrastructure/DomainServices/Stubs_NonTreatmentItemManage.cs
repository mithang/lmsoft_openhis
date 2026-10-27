using Newtouch.Sett.Domain;
using System;
using System.Collections.Generic;

namespace Newtouch.Sett.DomainServices
{
    public class NonTreatmentItemBillingRepo : INonTreatmentItemBillingRepo
    {
        public object SaveBilling(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }

    public class NonTreatmentItemDmnService : INonTreatmentItemDmnService
    {
        public List<INonTreatmentItemDmnService_GetChargeCategoryTreeListDto> GetChargeCategoryTreeList(object p0)
        {
            return new List<INonTreatmentItemDmnService_GetChargeCategoryTreeListDto>();
        }

        public object SelectNonTreatmentItemList(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7, object p8, object p9, object p10)
        {
            return null;
        }

        public object SelectPatientInfoByblhOrzyh(object p0, object p1, object p2, object p3)
        {
            return null;
        }

        public object SelectRefundItemList(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7, object p8)
        {
            return null;
        }

    }
}
