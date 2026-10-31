using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Repository;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysItemsRepository : IRepositoryBase<SysItemsEntity>
    {
        /// <summary>
        /// Lấy phân loại từ điển(Bao gồm cả vô hiệu)
        /// </summary>
        /// <returns></returns>
        IList<SysItemsEntity> GetList();

        /// <summary>
        /// Lấy phân loại từ điển hiệu lực
        /// </summary>
        /// <returns></returns>
        IList<SysItemsEntity> GetValidList();

        /// <summary>
        /// Gửi tạo mới,Cập nhật Thực thể
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysItemsEntity entity, string keyValue);

    }
}
