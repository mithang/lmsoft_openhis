using System.Collections.Generic;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IDomainServices
{
    public interface ISysFinancialDmnService
    {
        /// <summary>
        /// Lấy danh sách tất cả hóa đơn
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="fpId"></param>
        /// <returns></returns>
        IList<FinanceReceiptVO> GetFinancialInvoiceList(string keyValue, string OrganizeId);

        IList<FinanceInvoiceVO> GetCwfpList(string keyValue, string OrganizeId);
    }
}
