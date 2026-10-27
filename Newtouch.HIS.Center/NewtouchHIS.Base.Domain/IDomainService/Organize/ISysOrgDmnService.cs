using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base;

namespace NewtouchHIS.Base.Domain.Organize
{
    public interface ISysOrgDmnService : IScopedDependency
    {
        /// <summary>
        /// Lấy hiện tạiTổ chứcDanh sách 
        /// </summary>
        /// <param name="topOrgId"></param>
        /// <returns></returns>
        Task<List<SysOrgVo>> GetOrganizeList(string? topOrgId, bool showTopOrg = false);
        /// <summary>
        /// Tổ chức Truy vấn đệ quy
        /// </summary>
        /// <param name="parentOrgId"></param>
        /// <returns></returns>
        Task<List<SysOrgVo>> GetOrganizeTree(string parentOrgId);
    }
}
