using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Repository;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysChargeCategoryRepo : IRepositoryBase<SysChargeCategoryEntity>
    {
        /// <summary>
        /// Lấy hiệu lực
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        IList<SysChargeCategoryEntity> GetValidList(string orgId);

        /// <summary>
        /// Lấy tất cả
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        IList<SysChargeCategoryEntity> GetList(string orgId,string zt);

        /// <summary>
        /// Lấy dữ liệu thực thể
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        SysChargeCategoryEntity GetForm(int keyValue,string orgId = null);

        /// <summary>
        /// Gửi lưu
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysChargeCategoryEntity entity, int? keyValue);
    }
}
