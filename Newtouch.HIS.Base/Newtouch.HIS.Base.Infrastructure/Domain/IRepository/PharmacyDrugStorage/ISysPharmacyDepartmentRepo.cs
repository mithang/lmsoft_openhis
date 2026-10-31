using System.Collections.Generic;
using Newtouch.HIS.Repository;
using NewtouchHIS.Lib.Base.Model;
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
        /// Lấy nhà thuốc và kho dược hiệu lực
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="yjbmjb"></param>
        /// <returns></returns>
        IList<SysPharmacyDepartmentEntity> GetEffectiveList(string orgId, byte? yjbmjb);

        /// <summary>
        /// Lấy thông tin khoa dược
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<SysPharmacyDepartmentEntity> GetPagintionList(Pagination pagination, string organizeId, string keyword = null);

        /// <summary>
        /// Sửa và thêm khoa dược
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void submitForm(SysPharmacyDepartmentEntity entity, int? keyValue);

        /// <summary>
        /// LấyUserIdcái đã liên kết hiện tại cầnyfbmCode (Không bao gồm tổ chức con)
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        IList<string> GetCurPharmacyList(string userId, string orgId);

        /// <summary>
        /// Lấy nhà thuốc thuộc tổ chức/Kho thuốc (Không bao gồm tổ chức con)
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        IList<SysPharmacyDepartmentEntity> GetPharmacyListByOrg(string OrganizeId);
    }
}
