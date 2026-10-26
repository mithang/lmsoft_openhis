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
        /// lấy tổ chức loại (集团, bệnh viện)
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetOrganizeTypeByOrganizeId(string orgId);

        /// <summary>
        /// 是否含有下级机构, 有下级机构返回true
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
        /// thông qua cơ quan loại để đánh giá có hay không có 诊所
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        bool IsClinic(string orgId);

        /// <summary>
        /// thông qua cơ quan loại để đánh giá có hay không có 医疗机构 (有具体的业务)
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
