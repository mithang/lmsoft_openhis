using System;
using System.Collections.Generic;
using Newtouch.HIS.Domain.ValueObjects.InsuranceManage;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;

namespace Newtouch.HIS.Domain.IDomainServices
{
    public interface ISysCommercialInsuranceDmnService
    {
        /// <summary>
        /// Lấy danh sách đăng ký BHYT
        /// </summary>
        List<SysCommercialInsuranceFilingVO> SelectCommercialInsuranceFilingList(string keyword, string orgId, string sbbabId = null);

        /// <summary>
        /// Gửi bảo hiểm thương mại
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="KeyValue"></param>
        void SubmitForm(SysCommercialInsuranceVO entity, string KeyValue, string orgId);
    }
}
