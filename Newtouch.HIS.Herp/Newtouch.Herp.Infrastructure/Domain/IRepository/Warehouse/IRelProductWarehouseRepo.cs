using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// Vật tư kho
    /// </summary>
    public interface IRelProductWarehouseRepo : IRepositoryBase<RelProductWarehouseEntity>
    {
        /// <summary>
        /// Xóa hàng loạt
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        int BatchDelete(List<RelProductWarehouseEntity> list);
    }
}
