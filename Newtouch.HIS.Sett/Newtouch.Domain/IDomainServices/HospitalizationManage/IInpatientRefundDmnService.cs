using Newtouch.HIS.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IDomainServices.HospitalizationManage
{
    public interface IInpatientRefundDmnService
    {
        /// <summary>
        /// Lưu hoàn phí
        /// </summary>
        string SaveRefund(List<HospItemBillingEntity> xmjfbEntitylist, List<HospDrugBillingEntity> ypjfbEntitylist, string zyh, string orgId);
    }
}
