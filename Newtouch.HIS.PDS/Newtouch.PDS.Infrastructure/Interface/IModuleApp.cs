using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// Thực đơnApp
    /// </summary>
    public interface IModuleApp
    {
        /// <summary>
        /// Lấy danh sách thực đơn hiệu lực
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
        /// Xóa thực đơn
        /// </summary>
        /// <param name="keyValue"></param>
        void DeleteForm(string keyValue);


    }
}
