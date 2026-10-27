using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Repository;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// Hạng mục thu phí
    /// </summary>
    public interface ISysChargeItemRepo : IRepositoryBase<SysChargeItemEntity>
    {
        /// <summary>
        /// Lấy tất cả
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        IList<SysChargeItemEntity> GetList(string keyValue, string orgId);
        
        /// <summary>
        /// Lấy dữ liệu thực thể
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        SysChargeItemEntity GetForm(int keyValue);

        /// <summary>
        /// Gửi lưu
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysChargeItemEntity entity, int? keyValue);

        /// <summary>
        /// Đồng bộ BHYT sửa thời gian tải lên cuối cùng
        /// </summary>
        /// <param name="ypId"></param>
        /// <returns></returns>
        bool YibaoUpload(int sfxmId, out string error);
    }
}
