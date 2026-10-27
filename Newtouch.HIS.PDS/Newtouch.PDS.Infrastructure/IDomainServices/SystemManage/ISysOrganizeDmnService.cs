using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Tổ chức DmnService
    /// </summary>
    public interface ISysOrganizeDmnService
    {
        /// <summary>
        /// Lấy loại tổ chức（Tập đoàn、Bệnh viện）
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetOrganizeTypeByOrganizeId(string orgId);

        /// <summary>
        /// Có chứa tổ chức cấp dưới không，Trả về nếu có tổ chức cấp dướitrue
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        bool IsHasLowerOrganize(string orgId);

        /// <summary>
        /// Lấy tất cả tổ chức hiệu lực thuộc tổ chức（parent）
        /// </summary>
        /// <returns></returns>
        List<SysOrganizeVEntity> GetValidListByParentOrg(string parentOrgId);

        /// <summary>
        /// Đánh giá theo loại cơ sở Có hay không Có Bệnh viện
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        bool IsHospital(string orgId);

        /// <summary>
        /// Đánh giá theo loại cơ sở Có hay không Có Phòng khám
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        bool IsClinic(string orgId);

        /// <summary>
        /// Đánh giá theo loại cơ sở Có hay không Có Cơ sở y tế（Có nghiệp vụ cụ thể）
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        bool IsMedicalOrganize(string orgId);

        /// <summary>
        /// Lấy tất cả tổ chức cấp cao nhất
        /// </summary>
        /// <returns></returns>
        IList<SysOrganizeVEntity> GetValidTopOrgList();
    }
}
