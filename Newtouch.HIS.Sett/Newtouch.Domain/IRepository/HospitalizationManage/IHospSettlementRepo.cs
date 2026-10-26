using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHospSettlementRepo : IRepositoryBase<HospSettlementEntity>
    {
        /// <summary>
        /// Tra cứu tất cả bản ghi tính toán theo số nhập viện,Đối trừ các bản ghi đã quyết toán và đã xóa,Lấy dữ liệu chưa xóa
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        List<HospSettlementEntity> GetValidList(string zyh, string orgId);
    }
}
