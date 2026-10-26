using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Core.Common;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysOrganizeRepository : IRepositoryBase<SysOrganizeEntity>
    {
        /// <summary>
        /// Lấy tất cả các tổ chức thuộc tổ chức
        /// </summary>
        /// <returns></returns>
        List<SysOrganizeEntity> GetListByTopOrg(string topOrganizeId);

        /// <summary>
        /// Lấy tất cả các tổ chức có hiệu lực thuộc tổ chức (top)
        /// </summary>
        /// <returns></returns>
        List<SysOrganizeEntity> GetValidListByTopOrg(string topOrganizeId);

        /// <summary>
        /// Lấy tất cả các tổ chức có hiệu lực thuộc tổ chức (parent)
        /// </summary>
        /// <returns></returns>
        List<SysOrganizeVO> GetValidListByParentOrg(string parentOrgId);

        /// <summary>
        /// Lấy tất cả các tổ chức thuộc tổ chức (parent) (bao gồm cả các mục không hiệu lực)
        /// </summary>
        /// <returns></returns>
        List<SysOrganizeVO> GetListByParentOrg(string parentOrgId);

        /// <summary>
        /// Lấy danh sách tổ chức cấp cao nhất, bao gồm cả các mục không hiệu lực
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysOrganizeEntity> GetPagintionListByTopOrg(Pagination pagination, string keyword = null);

        /// <summary>
        /// Lấy danh sách tổ chức cấp cao nhất, bao gồm cả các mục không hiệu lực
        /// </summary>
        /// <returns></returns>
        List<SysOrganizeEntity> GetTopOrgList();

        /// <summary>
        /// Lấy danh sách tổ chức cấp cao nhất
        /// </summary>
        /// <returns></returns>
        List<SysOrganizeEntity> GetValidTopOrgList();

        /// <summary>
        /// Gửi tạo mới, cập nhật thực thể
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysOrganizeEntity entity, string keyValue);

        /// <summary>
        /// Lấy Code theo OrgId
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetCodeById(string orgId);
    }
}
