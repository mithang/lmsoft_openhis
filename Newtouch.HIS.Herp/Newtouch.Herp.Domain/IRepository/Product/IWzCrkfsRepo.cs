using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// Hình thức nhập/xuất kho
    /// </summary>
    public interface IWzCrkfsRepo : IRepositoryBase<WzCrkfsEntity>
    {
        /// <summary>
        /// Theo ID Xóa hình thức nhập/xuất kho
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteCrkfsById(string id);

        /// <summary>
        /// Tra cứu hình thức nhập/xuất kho
        /// </summary>
        /// <param name="crkbz"></param>
        /// <returns></returns>
        List<WzCrkfsEntity> SelectData(string crkbz);
    }
}