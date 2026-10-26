using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysDutyRepo : IRepositoryBase<SysDutyEntity>
    {
        /// <summary>
        /// Lấy danh sách có hiệu lực
        /// </summary>
        /// <returns></returns>
        IList<SysDutyEntity> GetValidList();

        /// <summary>
        /// Lấy tất cả
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        IList<SysDutyEntity> GetList(string keyword);

        /// <summary>
        /// Lấy dữ liệu thực thể
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        SysDutyEntity GetForm(string keyValue);

        /// <summary>
        /// Gửi để lưu
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysDutyEntity entity, string keyValue);

    }
}
