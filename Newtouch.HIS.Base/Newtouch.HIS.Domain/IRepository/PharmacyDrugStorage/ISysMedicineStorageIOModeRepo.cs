using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysMedicineStorageIOModeRepo : IRepositoryBase<SysMedicineStorageIOModeEntity>
    {
        /// <summary>
        /// Lấy tất cả
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        IList<SysMedicineStorageIOModeEntity> GetList(string keyword);

        /// <summary>
        /// Lấy dữ liệu thực thể
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        SysMedicineStorageIOModeEntity GetForm(int keyValue);

        /// <summary>
        /// Gửi để lưu
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysMedicineStorageIOModeEntity entity, int? keyValue);
    }
}
