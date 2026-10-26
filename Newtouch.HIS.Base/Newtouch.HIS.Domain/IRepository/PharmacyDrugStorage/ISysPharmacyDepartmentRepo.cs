using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysPharmacyDepartmentRepo : IRepositoryBase<SysPharmacyDepartmentEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="yjbmjb"></param>
        /// <returns></returns>
        IList<SysPharmacyDepartmentEntity> GetList(string orgId, byte? yjbmjb);

        /// <summary>
        /// Lấy phòng dược và kho thuốc có hiệu lực
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="yjbmjb"></param>
        /// <returns></returns>
        IList<SysPharmacyDepartmentEntity> GetEffectiveList(string orgId, byte? yjbmjb);

        /// <summary>
        /// Lấy thông tin phòng dược
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<SysPharmacyDepartmentEntity> GetPagintionList(Pagination pagination, string organizeId, string keyword = null);

        /// <summary>
        /// Sửa đổi và thêm phòng dược
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void submitForm(SysPharmacyDepartmentEntity entity, int? keyValue);

        /// <summary>
        /// Lấy mã yfbmCode mà UserId hiện đã liên kết (không bao gồm cơ sở con)
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        IList<string> GetCurPharmacyList(string userId, string orgId);

        /// <summary>
        /// Lấy phòng dược/kho thuốc thuộc tổ chức (không bao gồm cơ sở con)
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        IList<SysPharmacyDepartmentEntity> GetPharmacyListByOrg(string OrganizeId);
    }
}
