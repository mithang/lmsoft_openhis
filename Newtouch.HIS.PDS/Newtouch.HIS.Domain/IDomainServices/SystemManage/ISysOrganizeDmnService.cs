using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// tổ chức DmnService
    /// </summary>
    public interface ISysOrganizeDmnService
    {
        /// <summary>
        /// lấy tổ chức loại (tập đoàn, bệnh viện)
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetOrganizeTypeByOrganizeId(string orgId);

        /// <summary>
        /// Có chứa tổ chức cấp dưới không, có tổ chức cấp dưới thì trả về true
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        bool IsHasLowerOrganize(string orgId);

        /// <summary>
        /// lấy tổ chức dưới của tất cả còn hiệu lực tổ chức (parent)
        /// </summary>
        /// <returns></returns>
        List<SysOrganizeVEntity> GetValidListByParentOrg(string parentOrgId);

        /// <summary>
        /// thông qua cơ quan loại để đánh giá có hay không có bệnh viện
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        bool IsHospital(string orgId);

        /// <summary>
        /// thông qua cơ quan loại để đánh giá có hay không có phòng khám
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        bool IsClinic(string orgId);

        /// <summary>
        /// thông qua cơ quan loại để đánh giá có hay không có cơ sở y tế (có nghiệp vụ cụ thể)
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        bool IsMedicalOrganize(string orgId);

        /// <summary>
        /// lấy tất cả của tổ chức cấp cao nhất
        /// </summary>
        /// <returns></returns>
        IList<SysOrganizeVEntity> GetValidTopOrgList();
    }
}
