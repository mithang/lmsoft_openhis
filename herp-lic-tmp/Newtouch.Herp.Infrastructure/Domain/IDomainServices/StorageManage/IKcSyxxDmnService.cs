using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.Entity.VEntity;

namespace Newtouch.Herp.Domain.IDomainServices
{
    /// <summary>
    /// 损益
    /// </summary>
    public interface IKcSyxxDmnService
    {
        /// <summary>
        /// 获取损益明细
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="param"></param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<VLossAndProditEntity> SelectLossAndProditInfoList(Pagination pagination, LossAndProditSearchDTO param, string warehouseId, string organizeId);
    }
}