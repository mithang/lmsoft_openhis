using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysChargeCategoryBaseRepo : IRepositoryBase<SysChargeCategoryBaseEntity>
    {
        /// <summary>
        /// Lấy các mục có hiệu lực
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        IList<SysChargeCategoryBaseEntity> GetValidList(string orgId);

        /// <summary>
        /// Lấy tất cả
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        IList<SysChargeCategoryBaseEntity> GetList(string orgId);

        /// <summary>
        /// Lấy dữ liệu thực thể
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        SysChargeCategoryBaseEntity GetForm(int keyValue);

        /// <summary>
        /// Gửi để lưu
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysChargeCategoryBaseEntity entity, int? keyValue);
    }
}
