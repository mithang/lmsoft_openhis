using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// Thao tác nhà cung cấp
    /// </summary>
    public interface IGysSupplierRepo : IRepositoryBase<GysSupplierEntity>
    {
        /// <summary>
        /// get GysSupplierEntity list
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="organizeId"></param>
        /// <param name="keyWord"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        IList<GysSupplierEntity> GetList(Pagination pagination, string organizeId, string keyWord, string zt = "1");

        /// <summary>
        /// get GysSupplierEntity list
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="organizeId"></param>
        /// <param name="keyWord"></param>
        /// <param name="supplierType">0:Khác     1:Nhà sản xuất      2:Nhà phân phối</param>
        /// <param name="zt"></param>
        /// <returns></returns>
        IList<GysSupplierEntity> GetList(string organizeId, string keyWord, int supplierType, string zt = "");
    }
}