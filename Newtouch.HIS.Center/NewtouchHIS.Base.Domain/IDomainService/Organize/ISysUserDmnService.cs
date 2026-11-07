using NewtouchHIS.Base.Domain.Model;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.Base.Domain.Organize
{
    /// <summary>
    /// Giao diện người dùng
    /// </summary>
    public interface ISysUserDmnService : IScopedDependency
    {
        /// <summary>
        /// Người dùng hệ thống(Phân trang)
        /// </summary>
        /// <param name="request"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<PageResponseRow<List<SysUserVO>>> GetPagintionUserList(OLPagination<QueryParamsBase> request, string orgId);
        /// <summary>
        /// Theo OrganizeId Lấy Nhân viên hệ thống VO(và có tương ứng SysUser)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<SysUserStaffVO>> GetSatffVOListByOrg(string orgId, string? toporgId);
        /// <summary>
        /// Tổ chức Cây người dùng(Nhóm theo khoa)
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <param name="checkIds"></param>
        /// <returns></returns>
        Task<List<BsTreeSelectExtDataModel>> GetOrgUserTreeWithDeptAsync(string? orgId, string? keyword, List<string>? checkIds);
        Task<List<SysUserOrgandDmtVO>> GetSatffVOListByOrgorKeyvalue(string orgId, string? keyvalue);
    }

}
