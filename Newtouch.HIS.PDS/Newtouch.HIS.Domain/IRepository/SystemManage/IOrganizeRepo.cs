using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOrganizeRepo : IRepositoryBase<OrganizeEntity>
    {
        /// <summary>
        /// lấy tổ chức dưới của tất cả tổ chức
        /// </summary>
        /// <returns></returns>
        List<OrganizeEntity> GetListByTopOrg(string topOrganizeId);

        /// <summary>
        /// lấy tổ chức dưới của tất cả còn hiệu lực tổ chức
        /// </summary>
        /// <returns></returns>
        List<OrganizeEntity> GetValidListByTopOrg(string topOrganizeId);

    }
}
