using Newtouch.Domain.Entity;
using Newtouch.CIS.Repository;
using System.Collections.Generic;

namespace Newtouch.Domain.IRepository
{
    public interface ISysNationalityRepo : IRepositoryBase<SysNationalityVEntity>
    {
        /// <summary>
        /// LấyHiệu lựcquốc tịch
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysNationalityVEntity> GetgjList();

        IList<SysBrxzVEntity> GetBRXZList(string orgId);
        /// <summary>
        /// hiệnkimPhương thức thanh toán
        /// </summary>
        /// <returns></returns>
        IList<SysCashPaymenVEntity>  GetCashPay();
    }
}
