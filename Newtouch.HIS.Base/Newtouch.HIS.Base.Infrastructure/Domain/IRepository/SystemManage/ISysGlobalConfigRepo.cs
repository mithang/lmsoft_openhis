using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Repository;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysGlobalConfigRepo : IRepositoryBase<SysGlobalConfigEntity>
    {
        /// <summary>
        /// Lấy tất cả
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        IList<SysGlobalConfigEntity> GetList(string keyword);

        /// <summary>
        /// Lấy dữ liệu thực thể
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        SysGlobalConfigEntity GetForm(string keyValue);

        /// <summary>
        /// Gửi lưu
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysGlobalConfigEntity entity, string keyValue);
    }
}
