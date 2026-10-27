using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Lib.Base;

namespace NewtouchHIS.Base.Domain.Organize
{
    public interface ISysDepartmentDmnService : IScopedDependency
    {
        /// <summary>
        /// TheoCodeLấy tên khoa
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<string> GetNameByCode(string code, string orgId);
        /// <summary>
        /// Lấy danh sách thực thể
        /// </summary>
        /// <param name="keyword">Từ khóa lọc</param>
        Task<List<SysDepartmentEntity>> GetList(string keyword = null);
        /// <summary>
        /// Lấy danh sách thực thể
        /// </summary>
        /// <param name="keyword">Từ khóa lọc</param>
        Task<List<SysDepartmentEntity>> GetListByOrg(string organizeId);
    }
}
