using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysApplicationRepo : IRepositoryBase<SysApplicationEntity>
    {
        /// <summary>
        /// Lấy danh sách (bao gồm cả các mục không hiệu lực)
        /// </summary>
        /// <returns></returns>
        IList<SysApplicationEntity> GetList();

        /// <summary>
        /// Lấy danh sách có hiệu lực
        /// </summary>
        /// <returns></returns>
        IList<SysApplicationEntity> GetValidList();

        /// <summary>
        /// Gửi tạo mới, cập nhật thực thể
        /// </summary>
        /// <returns></returns>
        void SubmitForm(SysApplicationEntity entity, string keyValue);

        /// <summary>
        /// Trả về một thực thể
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        SysApplicationEntity GetEntity(string appId);
    }
}
