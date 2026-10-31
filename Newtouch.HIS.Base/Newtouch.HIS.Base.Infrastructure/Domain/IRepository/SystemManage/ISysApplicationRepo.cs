using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Repository;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysApplicationRepo : IRepositoryBase<SysApplicationEntity>
    {
        /// <summary>
        /// Lấy danh sách(Bao gồm cả vô hiệu)
        /// </summary>
        /// <returns></returns>
        IList<SysApplicationEntity> GetList();

        /// <summary>
        /// Lấy danh sách hiệu lực
        /// </summary>
        /// <returns></returns>
        IList<SysApplicationEntity> GetValidList();

        /// <summary>
        /// Gửi tạo mới,Cập nhật Thực thể
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
