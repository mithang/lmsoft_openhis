using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// Thao tác kho
    /// </summary>
    public interface IKfWarehouseRepo : IRepositoryBase<KfWarehouseEntity>
    {
        /// <summary>
        /// Lấy kho anh em hoặc kho con đích
        /// </summary>
        /// <param name="kfId"></param>
        /// <param name="organizeId"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        List<KfWarehouseEntity> GetBrothersOrChildren(string kfId, string organizeId, string keyWord = "");

        /// <summary>
        /// Lấy khoa phòng(Nút cha và nút anh em)
        /// </summary>
        /// <param name="kfId"></param>
        /// <param name="keyWord"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<KfWarehouseEntity> GetParentOrBrothers(string kfId, string organizeId, string keyWord = "");
    }
}