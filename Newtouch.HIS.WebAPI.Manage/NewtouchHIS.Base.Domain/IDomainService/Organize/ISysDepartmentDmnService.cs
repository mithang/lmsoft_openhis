using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Lib.Base;

namespace NewtouchHIS.Base.Domain.Organize
{
    public interface ISysDepartmentDmnService : IScopedDependency
    {
        /// <summary>
        /// theoCodelấy tên khoa
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<string> GetNameByCode(string code, string orgId);
        /// <summary>
        /// lấy danh sách thực thể
        /// </summary>
        /// <param name="keyword">từ khóa lọc</param>
        Task<List<SysDepartmentEntity>> GetList(string keyword = null);
        /// <summary>
        /// lấy danh sách thực thể
        /// </summary>
        /// <param name="keyword">từ khóa lọc</param>
        Task<List<SysDepartmentEntity>> GetListByOrg(string organizeId);
    }
}
