using System.Collections;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Herp.Domain.ValueObjects;

namespace Newtouch.Herp.Domain.IDomainServices
{
    /// <summary>
    /// Liên kết hạng mục thu phí vật tư
    /// </summary>
    public interface IRelProductAndsfxmDmnService
    {

        /// <summary>
        /// Truy vấn quản lý đối chiếu hạng mục thu phí vật tư
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="sfdlCode"></param>
        /// <param name="productTypeId"></param>
        /// <param name="organizeId"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        IList<RelProductAndsfxmVo> SelectProductAndsfxm(Pagination pagination, string sfdlCode, string productTypeId, string organizeId, string zt);

        /// <summary>
        /// Truy vấn quản lý đối chiếu hạng mục thu phí vật tư
        /// </summary>
        /// <param name="relId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        RelProductAndsfxmVo SelectProductAndsfxm(string relId, string organizeId);
    }
}