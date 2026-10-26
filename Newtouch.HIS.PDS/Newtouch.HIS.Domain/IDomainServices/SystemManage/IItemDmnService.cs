using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// từ điển, mục từ điển DmnService
    /// </summary>
    public interface IItemDmnService
    {
        /// <summary>
        /// lấy còn hiệu lực mục từ điển danh sách 指定组织机构
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        IList<SysItemsDetailVEntity> GetValidItemsDetailListByTopOrgAndItemCode(string organizeId, string code);

        /// <summary>
        /// lấy tất cả từ điển phần loại
        /// </summary>
        /// <param name="topOrgId"></param>
        /// <returns></returns>
        IList<SysItemsVEntity> GetValidItemsList();

        /// <summary>
        /// lấy tất cả mục từ điển
        /// </summary>
        /// <param name="topOrgId"></param>
        /// <returns></returns>
        IList<SysItemsDetailVEntity> GetValidItemsDetailListByTopOrg(string topOrgId);

    }
}
