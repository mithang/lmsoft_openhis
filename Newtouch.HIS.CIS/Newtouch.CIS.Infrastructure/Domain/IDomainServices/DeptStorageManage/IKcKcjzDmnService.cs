using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Domain.DTO.InputDto;
using Newtouch.Domain.ValueObjects.Storage;


namespace Newtouch.Domain.IDomainServices.DeptStorageManage
{
    /// <summary>
    /// Tồn kho kết chuyển
    /// </summary>
    public interface IKcKcjzDmnService
    {
        /// <summary>
        /// Lấy kết chuyển chi tiết
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="jzsj"></param>
        /// <param name="keyWord"></param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<VCarryOverDetailEntity> SelectCarryOverDetail(Pagination pagination, string jzsj, string keyWord, string warehouseId, string organizeId);

        /// <summary>
        /// kết chuyển
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string CarryOverProduct(string warehouseId, string organizeId, string userCode);

        /// <summary>
        /// vào bán tồn tại Thống kê
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="searchParam"></param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<VPsiStatisticsEntity> GetPsiStatistics(Pagination pagination, PsiStatisticsDTO searchParam, string warehouseId, string organizeId);
    }
}
