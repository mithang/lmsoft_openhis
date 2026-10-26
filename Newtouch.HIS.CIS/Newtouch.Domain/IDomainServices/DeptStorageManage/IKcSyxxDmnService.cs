using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Domain.DTO.InputDto;
using Newtouch.Domain.ValueObjects.Storage;


namespace Newtouch.Domain.IDomainServices.DeptStorageManage
{
    /// <summary>
    /// Chênh lệch
    /// </summary>
    public interface IKcSyxxDmnService
    {
        /// <summary>
        /// Lấy chi tiết chênh lệch
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="param"></param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<VLossAndProditEntity> SelectLossAndProditInfoList(Pagination pagination, LossAndProditSearchDTO param, string ks, string organizeId);
    }
}