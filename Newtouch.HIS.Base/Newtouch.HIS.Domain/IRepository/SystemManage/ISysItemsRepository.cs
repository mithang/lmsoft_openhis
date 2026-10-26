using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysItemsRepository : IRepositoryBase<SysItemsEntity>
    {
        /// <summary>
        /// Lấy phân loại từ điển (bao gồm cả các mục không hiệu lực)
        /// </summary>
        /// <returns></returns>
        IList<SysItemsEntity> GetList();

        /// <summary>
        /// Lấy phân loại từ điển có hiệu lực
        /// </summary>
        /// <returns></returns>
        IList<SysItemsEntity> GetValidList();

        /// <summary>
        /// Gửi tạo mới, cập nhật thực thể
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysItemsEntity entity, string keyValue);

    }
}
