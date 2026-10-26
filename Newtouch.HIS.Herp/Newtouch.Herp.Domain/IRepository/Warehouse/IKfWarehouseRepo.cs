using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// Thao tac kho
    /// </summary>
    public interface IKfWarehouseRepo : IRepositoryBase<KfWarehouseEntity>
    {
        /// <summary>
        /// Lay kho anh em hoac kho con dich
        /// </summary>
        /// <param name="kfId"></param>
        /// <param name="organizeId"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        List<KfWarehouseEntity> GetBrothersOrChildren(string kfId, string organizeId, string keyWord = "");

        /// <summary>
        /// Lay khoa phong(Nut cha va nut anh em)
        /// </summary>
        /// <param name="kfId"></param>
        /// <param name="keyWord"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<KfWarehouseEntity> GetParentOrBrothers(string kfId, string organizeId, string keyWord = "");
    }
}