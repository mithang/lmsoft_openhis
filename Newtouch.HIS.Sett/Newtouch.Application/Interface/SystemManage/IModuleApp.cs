using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// menu App
    /// </summary>
    public interface IModuleApp
    {
        /// <summary>
        /// Lấy hiệu lực menu danh sách
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
