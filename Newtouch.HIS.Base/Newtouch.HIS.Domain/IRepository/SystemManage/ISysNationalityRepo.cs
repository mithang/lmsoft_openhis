using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysNationalityRepo : IRepositoryBase<SysNationalityEntity>
    {
        /// <summary>
        /// Lấy tất cả
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        IList<SysNationalityEntity> GetList(string keyword);

        /// <summary>
        /// Lấy dữ liệu thực thể
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        SysNationalityEntity GetForm(int keyValue);

        /// <summary>
        /// Gửi để lưu
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysNationalityEntity entity, int? keyValue);
    }
}
