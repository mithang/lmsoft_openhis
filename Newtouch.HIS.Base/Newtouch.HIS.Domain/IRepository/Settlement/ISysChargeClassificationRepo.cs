using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// Phân loại thu phí
    /// </summary>
    public interface ISysChargeClassificationRepo : IRepositoryBase<SysChargeClassificationEntity>
    {
        /// <summary>
        /// Lấy tất cả
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        IList<SysChargeClassificationEntity> GetList(string keyValue, string orgId);

        /// <summary>
        /// Lấy các mục có hiệu lực
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        IList<SysChargeClassificationEntity> GetValidList(string orgId);

        /// <summary>
        /// Lấy dữ liệu thực thể
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        SysChargeClassificationEntity GetForm(int keyValue);

        /// <summary>
        /// Gửi để lưu
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysChargeClassificationEntity entity, int? keyValue);


    }
}
