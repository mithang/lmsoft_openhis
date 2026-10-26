using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;

namespace Newtouch.Domain.IRepository
{
    public interface ISysNationalityRepo : IRepositoryBase<SysNationalityVEntity>
    {
        /// <summary>
        /// Lấy quốc tịch có hiệu lực
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysNationalityVEntity> GetgjList();

        IList<SysBrxzVEntity> GetBRXZList(string orgId);
        /// <summary>
        /// Phương thức thanh toán tiền mặt
        /// </summary>
        /// <returns></returns>
        IList<SysCashPaymenVEntity>  GetCashPay();
    }
}
