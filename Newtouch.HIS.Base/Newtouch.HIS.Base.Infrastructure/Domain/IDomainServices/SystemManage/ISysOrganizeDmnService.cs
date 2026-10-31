using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Tổ chứcDmnService
    /// </summary>
    public interface ISysOrganizeDmnService
    {
        /// <summary>
        /// Lấy danh sách ứng dụng tổ chức đã được ủy quyền
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<SysApplicationEntity> GetAuthedAppListByTopOrgId(string topOrgId);

        /// <summary>
        /// Có chứa tổ chức cấp dưới không,Có tổ chức cấp dưới thì trả vềtrue
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        bool IsHasLowerOrganize(string orgId);

        /// <summary>
        /// Xét theo loại tổ chức Có hay không Có Bệnh viện
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        bool IsHospital(string orgId);

        /// <summary>
        /// Xét theo loại tổ chức Có hay không Có Phòng khám
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        bool IsClinic(string orgId);

        /// <summary>
        /// Xét theo loại tổ chức Có hay không Có Cơ sở y tế(Có nghiệp vụ cụ thể)
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        bool IsMedicalOrganize(string orgId);

        /// <summary>
        /// LấyOrgTên
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetNameByOrgId(string orgId);

        /// <summary>
        /// Lấy UserId tương ứng Cơ sở y tế List
        /// </summary>
        /// <returns></returns>
        IList<SysOrganizeEntity> GetMedicalOrganizeListByUserId(string userId);

        #region Khoa

        /// <summary>
        /// Lấy danh sách khoa
        /// </summary>
        /// <returns></returns>
        List<SysDepartmentVO> GetListByOrg(string organizeId);

        /// <summary>
        /// Cập nhật khu bệnh liên kết khoa
        /// </summary>
        /// <param name="deptId"></param>
        /// <param name="wardList">Khu bệnhCode List</param>
        /// <param name="OrganizeId"></param>
        void UpdateDepartmentWard(string deptId, string[] wardList, string OrganizeId);

        #endregion
    }
}
