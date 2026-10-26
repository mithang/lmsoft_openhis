using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.HIS.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Tổ chức DmnService
    /// </summary>
    public interface ISysOrganizeDmnService
    {
        /// <summary>
        /// Lấy tất cả tổ chức hiệu lực dưới tổ chức(parent)
        /// </summary>
        /// <returns></returns>
        List<SysOrganizeVEntity> GetValidListByParentOrg(string parentOrgId);

        /// <summary>
        /// Xác định qua loại hình cơ sở Có hay không là Bệnh viện
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        bool IsHospital(string orgId);

        /// <summary>
        /// Xác định qua loại hình cơ sở Có hay không là Phòng khám
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        bool IsClinic(string orgId);

        /// <summary>
        /// Xác định qua loại hình cơ sở Có hay không là Cơ sở y tế(Có nghiệp vụ cụ thể)
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        bool IsMedicalOrganize(string orgId);

        /// <summary>
        /// Lấy tất cả tổ chức cấp cao nhất
        /// </summary>
        /// <returns></returns>
        IList<SysOrganizeVEntity> GetValidTopOrgList();

        /// <summary>
        /// LấyOrgTên
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetNameByOrgId(string orgId);

        /// <summary>
        /// Lấy UserId Tương ứng Cơ sở y tế List
        /// </summary>
        /// <returns></returns>
        IList<SysOrganizeVEntity> GetMedicalOrganizeListByUserId(string userId);

    }
}
