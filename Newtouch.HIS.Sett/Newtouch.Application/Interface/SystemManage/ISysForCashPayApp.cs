using System;
using System.Collections.Generic;
using System.Data;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysForCashPayApp
    {
        List<SysCashPaymentModelEntity> GetList(string keyword = null);

        SysCashPaymentModelEntity GetForm(int keyValue);

        void DeleteForm(int keyValue);

        void SubmitForm(SysCashPaymentModelEntity sysForCashPayEntity, int? keyValue);

        DataTable GetListBySearch(string keyword);

        /// <summary>
        /// Lấy hệ thống Phư ơng thức thanh toán tiền mặt bộ nhớ đ ệm dư ới kéo xuốngdanh sách
        /// </summary>
        /// <returns></returns>
        object GetCashPay();
    }
}
