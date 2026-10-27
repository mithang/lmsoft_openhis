using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// Phương thức nhập/xuất kho
    /// </summary>
    public interface IWzCrkfsRepo : IRepositoryBase<WzCrkfsEntity>
    {
        /// <summary>
        /// Theo ID Xóa phương thức nhập/xuất kho
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteCrkfsById(string id);

        /// <summary>
        /// Truy vấn phương thức nhập/xuất kho
        /// </summary>
        /// <param name="crkbz"></param>
        /// <returns></returns>
        List<WzCrkfsEntity> SelectData(string crkbz);
    }
}