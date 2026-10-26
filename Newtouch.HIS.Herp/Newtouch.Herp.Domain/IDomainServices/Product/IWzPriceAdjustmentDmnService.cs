using System;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.Entity.VEntity;

namespace Newtouch.Herp.Domain.IDomainServices
{
    /// <summary>
    /// Điều chỉnh giá
    /// </summary>
    public interface IWzPriceAdjustmentDmnService
    {
        /// <summary>
        /// Lấy thông tin điều chỉnh giá
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyWord"></param>
        /// <param name="shzt"></param>
        /// <param name="organizeId"></param>
        /// <param name="zxbz"></param>
        /// <returns></returns>
        IList<VPriceAdjustmentInfoEntity> GetPriceAdjustmentList(Pagination pagination, string keyWord, string shzt, string organizeId, string zxbz = "");

        /// <summary>
        /// Duyệt điều chỉnh giá
        /// </summary>
        /// <param name="idList"></param>
        /// <param name="operationType"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string Approval(List<string> idList, string operationType, string userCode, string organizeId);

        /// <summary>
        /// Lấy thông tin lãi/lỗ điều chỉnh giá
        /// </summary>
        /// <param name="wztjId"></param>
        /// <param name="productId"></param>
        /// <param name="organizeid"></param>
        /// <returns></returns>
        List<VPriceAdjustmentProfitLossEntity> GetPriceAdjustmentProfitLoss(string wztjId, string productId, string organizeid);

        /// <summary>
        /// Lấy thông tin lãi/lỗ điều chỉnh giá
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        IList<VPriceAdjustmentProfitLossEntity> GetPriceAdjustmentProfitLoss(Pagination pagination, PriceAdjustmentProfitLossDTO param);

        /// <summary>
        /// Thực hiện điều chỉnh giá
        /// </summary>
        /// <param name="tjInfo"></param>
        /// <returns></returns>
        string AdjustPriceExecute(WzPriceAdjustmentEntity tjInfo);

        /// <summary>
        /// Lấy thông tin lịch sử điều chỉnh giá
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyWord"></param>
        /// <param name="organizeId"></param>
        /// <param name="bt">Thời gian bắt đầu</param>
        /// <param name="et">Thời gian kết thúc</param>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        IList<VPriceAdjustmentInfoEntity> GetPriceAdjustmentHistoryList(Pagination pagination, string keyWord, string organizeId, DateTime bt, DateTime et, string warehouseId);
    }
}