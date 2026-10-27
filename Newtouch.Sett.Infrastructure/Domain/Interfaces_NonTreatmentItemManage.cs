using System;
using System.Collections.Generic;

namespace Newtouch.Sett.Domain
{
    public interface INonTreatmentItemBillingRepo
    {
        object SaveBilling(object p0);
    }

    public interface INonTreatmentItemDmnService
    {
        List<INonTreatmentItemDmnService_GetChargeCategoryTreeListDto> GetChargeCategoryTreeList(object p0);
        object SelectNonTreatmentItemList(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7, object p8, object p9, object p10);
        object SelectPatientInfoByblhOrzyh(object p0, object p1, object p2, object p3);
        object SelectRefundItemList(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7, object p8);
    }
}
