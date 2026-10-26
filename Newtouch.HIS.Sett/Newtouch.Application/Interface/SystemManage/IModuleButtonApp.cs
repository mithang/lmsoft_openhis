using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// nút App
    /// </summary>
    public interface IModuleButtonApp
    {
        /// <summary>
        /// Lấy thực thể
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        SysModuleButtonEntity GetForm(string keyValue);

        /// <summary>
        /// Xóa thực thể
        /// </summary>
        /// <param name="keyValue"></param>
        void DeleteForm(string keyValue);

    }
}
