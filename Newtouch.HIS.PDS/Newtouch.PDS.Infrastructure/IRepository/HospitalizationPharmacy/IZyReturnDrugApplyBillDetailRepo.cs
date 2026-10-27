using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// Chi tiết đơn xin trả thuốc nội trú
    /// </summary>
    public interface IZyReturnDrugApplyBillDetailRepo : IRepositoryBase<ZyReturnDrugApplyBillDetailEntity>
    {
        /// <summary>
        /// Theo đơn xin trả thuốcIdLấy chi tiết đơn xin trả thuốc
        /// </summary>
        /// <param name="rabId"></param>
        /// <returns></returns>
        List<ZyReturnDrugApplyBillDetailEntity> SelectData(string rabId);
    }
}