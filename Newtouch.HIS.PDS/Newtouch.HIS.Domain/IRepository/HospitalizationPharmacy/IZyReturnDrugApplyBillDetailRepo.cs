using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// trả thuốc nội trú phiếu đề nghị chi tiết
    /// </summary>
    public interface IZyReturnDrugApplyBillDetailRepo : IRepositoryBase<ZyReturnDrugApplyBillDetailEntity>
    {
        /// <summary>
        /// theo trả thuốc đề nghị Id lấy trả thuốc đề nghị chi tiết
        /// </summary>
        /// <param name="rabId"></param>
        /// <returns></returns>
        List<ZyReturnDrugApplyBillDetailEntity> SelectData(string rabId);
    }
}