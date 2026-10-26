using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// App menu
    /// </summary>
    public interface IModuleApp
    {
        /// <summary>
        /// Lấy danh sách menu có hiệu lực
        /// </summary>
        /// <returns></returns>
        List<SysModuleEntity> GetValidList();

        /// <summary>
        /// Lấy thực thể
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        SysModuleEntity GetForm(string keyValue);

        /// <summary>
        /// Xóa menu
        /// </summary>
        /// <param name="keyValue"></param>
        void DeleteForm(string keyValue);

    }
}
