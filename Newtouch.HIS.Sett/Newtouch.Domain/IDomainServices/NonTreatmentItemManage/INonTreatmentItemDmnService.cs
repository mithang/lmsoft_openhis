using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects.NonTreatmentItemManage;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices.NonTreatmentItemManage
{
    public interface INonTreatmentItemDmnService
    {
        /// <summary>
        /// Tra cứu theo số thẻpatidvàxm
        /// </summary>
        IList<PatientInfoVO> SelectPatientInfoByblhOrzyh(string orgId, string blh, string zyh, string xm);

        /// <summary>
        /// Lấy phân loại hạng mục thu phíList
        /// </summary>
        IList<SysChargeCategoryVEntity> GetChargeCategoryTreeList(string orgId);

        /// <summary>
        /// Tra cứu ghi sổ hạng mục không điều trị
        /// </summary>
        IList<NonTreatmentItemBillingInfoVO> SelectNonTreatmentItemList(Pagination pagination, string sfdl, string keyword, string smry, string smks, DateTime? kssj, DateTime? jssj, string orgId, string kehu, string zyh, string blh);

        /// <summary>
        /// Tra cứu hoàn phí ghi sổ
        /// </summary>
        IList<NonTreatmentItemBillingInfoVO> SelectRefundItemList(Pagination pagination, string zyh, string blh, string keyword, string smry, string kehu, DateTime? kssj, DateTime? jssj, string orgId);
    }
}
