using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity.V;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// phương thức nhập/xuất kho
    /// </summary>
    public interface ISysMedicineCrkfsDmnService
    {
        /// <summary>
        /// lấy phương thức nhập/xuất kho
        /// </summary>
        /// <param name="crkbz">cờ nhập/xuất kho 0: nhập kho  1: xuất kho</param>
        /// <returns></returns>
        List<SysMedicineCrkfsVEntity> GetList(string crkbz);
    }
}