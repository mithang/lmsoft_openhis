using NewtouchHIS.Base.Domain.Model;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.Base.Domain.Organize
{
    /// <summary>
    /// giao diện người dùng
    /// </summary>
    public interface ISysUserDmnService : IScopedDependency
    {
        /// <summary>
        /// người dùng hệ thống（phân trang）
        /// </summary>
        /// <param name="request"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<PageResponseRow<List<SysUserVO>>> GetPagintionUserList(OLPagination<QueryParamsBase> request, string orgId);
        /// <summary>
        /// theoOrganizeIdlấy nhân viên hệ thốngVO（đồng thời có tương ứngSysUser）
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<SysUserStaffVO>> GetSatffVOListByOrg(string orgId, string? toporgId);
        /// <summary>
        /// cây người dùng cơ quan（nhóm khoa）
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <param name="checkIds"></param>
        /// <returns></returns>
        Task<List<BsTreeSelectExtDataModel>> GetOrgUserTreeWithDeptAsync(string? orgId, string? keyword, List<string>? checkIds);
        Task<List<SysUserOrgandDmtVO>> GetSatffVOListByOrgorKeyvalue(string orgId, string? keyvalue);
    }

}
