using Newtouch.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// Hạng mục thu phí
    /// </summary>
    public interface ISysChargeItemBaseRepo : IRepositoryBase<SysChargeItemBaseEntity>
    {
        /// <summary>
        /// Lấy tất cả
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        IList<SysChargeItemBaseEntity> GetList(string keyValue, string orgId);
        
        /// <summary>
        /// Lấy dữ liệu thực thể
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        SysChargeItemBaseEntity GetForm(int keyValue);

        /// <summary>
        /// Gửi để lưu
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysChargeItemBaseEntity entity, int? keyValue);

        /// <summary>
        /// Thời gian tải lên cuối cùng khi đồng bộ sửa đổi bảo hiểm y tế
        /// </summary>
        /// <param name="ypId"></param>
        /// <returns></returns>
        bool YibaoUpload(int sfxmId, out string error);
    }
}
