using System;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Herp.Domain.Entity;

namespace Newtouch.Herp.Domain.IDomainServices
{
    /// <summary>
    /// Quản lý phiếu lĩnh
    /// </summary>
    public interface IKfApplyOrderDmnService
    {
        /// <summary>
        /// Lấy thông tin chính của phiếu lĩnh
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="sldh"></param>
        /// <param name="applyType"></param>
        /// <param name="applyProcess"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<VApplyBillInfoEntity> SelectApplyBillInfo(Pagination pagination, string sldh, int applyType,
           int applyProcess, DateTime kssj, DateTime jssj, string organizeId);

        /// <summary>
        /// Lấy thông tin chính của phiếu lĩnh
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="sldh"></param>
        /// <param name="applyType"></param>
        /// <param name="applyProcess"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="organizeId"></param>
        /// <param name="warehouseId"></param>
        /// <param name="applyProcesses">Tập hợp trạng thái xử lý có thể chọn</param>
        /// <returns></returns>
        IList<VApplyBillInfoEntity> SelectApplyBillInfo(Pagination pagination, string sldh, int applyType,
            int applyProcess, DateTime kssj, DateTime jssj, string organizeId, string warehouseId, List<int> applyProcesses);

        /// <summary>
        /// Lấy chi tiết chính của phiếu lĩnh
        /// </summary>
        /// <param name="sldh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<VApplyBillDetailEntity> SelectApplyBillDetail(string sldh, string organizeId);

        /// <summary>
        /// Lấy chi tiết chính của phiếu lĩnh
        /// </summary>
        /// <param name="sldhs"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<VApplyBillDetailEntity> SelectApplyBillDetailBySldhs(string sldhs, string organizeId);
    }
}