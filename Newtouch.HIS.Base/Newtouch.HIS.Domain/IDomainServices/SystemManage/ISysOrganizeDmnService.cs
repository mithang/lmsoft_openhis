using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// DmnService tổ chức
    /// </summary>
    public interface ISysOrganizeDmnService
    {
        /// <summary>
        /// Lấy danh sách ứng dụng đã được ủy quyền cho tổ chức
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<SysApplicationEntity> GetAuthedAppListByTopOrgId(string topOrgId);

        /// <summary>
        /// Có cơ sở cấp dưới hay không, nếu có thì trả về true
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        bool IsHasLowerOrganize(string orgId);

        /// <summary>
        /// Xác định theo danh mục cơ sở có phải là bệnh viện hay không
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        bool IsHospital(string orgId);

        /// <summary>
        /// Xác định theo danh mục cơ sở có phải là phòng khám hay không
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        bool IsClinic(string orgId);

        /// <summary>
        /// Xác định theo danh mục cơ sở có phải là cơ sở y tế hay không (có nghiệp vụ cụ thể)
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        bool IsMedicalOrganize(string orgId);

        /// <summary>
        /// Lấy tên Org
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetNameByOrgId(string orgId);

        /// <summary>
        /// Lấy List cơ sở y tế tương ứng với UserId
        /// </summary>
        /// <returns></returns>
        IList<SysOrganizeEntity> GetMedicalOrganizeListByUserId(string userId);

        #region 科室

        /// <summary>
        /// Lấy danh sách khoa
        /// </summary>
        /// <returns></returns>
        List<SysDepartmentVO> GetListByOrg(string organizeId);

        /// <summary>
        /// Cập nhật khu bệnh liên kết với khoa
        /// </summary>
        /// <param name="deptId"></param>
        /// <param name="wardList">Danh sách Code khu bệnh</param>
        /// <param name="OrganizeId"></param>
        void UpdateDepartmentWard(string deptId, string[] wardList, string OrganizeId);

        #endregion
    }
}
