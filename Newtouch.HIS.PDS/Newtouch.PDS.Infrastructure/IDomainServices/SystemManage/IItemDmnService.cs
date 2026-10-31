using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Từ điển,Mục từ điển DmnService
    /// </summary>
    public interface IItemDmnService
    {
        /// <summary>
        /// Lấy mục từ điển hiệu lực Danh sách Chỉ định tổ chức
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        IList<SysItemsDetailVEntity> GetValidItemsDetailListByTopOrgAndItemCode(string organizeId, string code);

        IList<SysItemsDetailVEntity> GetItemsDetailListByOrgIdAndItemCode(string organizeId, string code, string zt);

        /// <summary>
        /// Lấy tất cả phân loại từ điển
        /// </summary>
        /// <param name="topOrgId"></param>
        /// <returns></returns>
        IList<SysItemsVEntity> GetValidItemsList();

        /// <summary>
        /// Lấy tất cả mục từ điển
        /// </summary>
        /// <param name="topOrgId"></param>
        /// <returns></returns>
        IList<SysItemsDetailVEntity> GetValidItemsDetailListByTopOrg(string topOrgId);

    }
}
