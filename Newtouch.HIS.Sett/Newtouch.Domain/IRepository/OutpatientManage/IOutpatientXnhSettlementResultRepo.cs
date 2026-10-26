using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// Quyết toán Bảo hiểm nông thôn mới ngoại trú
    /// </summary>
    public interface IOutpatientXnhSettlementResultRepo : IRepositoryBase<OutpatientXnhSettlementResultEntity>
    {

        /// <summary>
        /// TheooutpIdLấy dữ liệu
        /// </summary>
        /// <param name="outpId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<OutpatientXnhSettlementResultEntity> SelectData(string outpId, string organizeId);

        /// <summary>
        /// Sửa cờ quyết toán
        /// </summary>
        /// <param name="jszt">Trạng thái quyết toán 1-Đã quyết toán 2-Đã trả lại   Mặc định đã quyết toán</param>
        /// <param name="outpId"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        int UpdateJszt(int jszt, string outpId, string userCode, string organizeId);
    }
}