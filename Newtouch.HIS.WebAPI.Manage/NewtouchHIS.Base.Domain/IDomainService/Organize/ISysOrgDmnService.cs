using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base;

namespace NewtouchHIS.Base.Domain.Organize
{
    public interface ISysOrgDmnService : IScopedDependency
    {
        /// <summary>
        /// lấy danh sách tổ chức hiện tại 
        /// </summary>
        /// <param name="topOrgId"></param>
        /// <returns></returns>
        Task<List<SysOrgVo>> GetOrganizeList(string? topOrgId, bool showTopOrg = false);
        /// <summary>
        /// tổ chức truy vấn đệ quy
        /// </summary>
        /// <param name="parentOrgId"></param>
        /// <returns></returns>
        Task<List<SysOrgVo>> GetOrganizeTree(string parentOrgId);
    }
}
