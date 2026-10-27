using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity.V;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Phương thức xuất nhập kho
    /// </summary>
    public interface ISysMedicineCrkfsDmnService
    {
        /// <summary>
        /// Lấy phương thức xuất nhập kho
        /// </summary>
        /// <param name="crkbz">Cờ xuất nhập kho 0：Nhập kho  1：Xuất kho</param>
        /// <returns></returns>
        List<SysMedicineCrkfsVEntity> GetList(string crkbz);
    }
}