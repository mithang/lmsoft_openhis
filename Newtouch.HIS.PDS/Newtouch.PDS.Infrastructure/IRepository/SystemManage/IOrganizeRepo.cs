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
        /// Lấy tất cả tổ chức thuộc tổ chức
        /// </summary>
        /// <returns></returns>
        List<OrganizeEntity> GetListByTopOrg(string topOrganizeId);

        /// <summary>
        /// Lấy tất cả tổ chức hiệu lực thuộc tổ chức
        /// </summary>
        /// <returns></returns>
        List<OrganizeEntity> GetValidListByTopOrg(string topOrganizeId);

    }
}
