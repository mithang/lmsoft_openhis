using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// Nhóm thu phí lớn bảo hiểm nông nghiệp
    /// </summary>
    public interface ISysAgriInsuranceChargeCategoryRepo : IRepositoryBase<SysAgriInsuranceChargeCategoryEntity>
    {
        /// <summary>
        /// Lấy tất cả
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        IList<SysAgriInsuranceChargeCategoryEntity> GetList(string orgId, string keyword = null);

        /// <summary>
        /// Lấy các mục có hiệu lực
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        IList<SysAgriInsuranceChargeCategoryEntity> GetValidList(string orgId);

        /// <summary>
        /// Lấy dữ liệu thực thể
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        SysAgriInsuranceChargeCategoryEntity GetForm(int keyValue);

        /// <summary>
        /// Gửi để lưu
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysAgriInsuranceChargeCategoryEntity entity, int? keyValue);


    }
}
