using NewtouchHIS.Base.Domain.Model;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.Base.Domain.IDomainService.PDSData
{
    public interface IDepartmentDrugsService : IScopedDependency
    {
        /// <summary>
        /// Lấy dữ liệu phát thuốc của khoa
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<List<DepartmentDrugsVO>> GetDepartmentDrugs(QueryParamsBase query);
        Task<PageResponseRow<List<DepartmentDrugsVO>>> GetDepartmentDrugsPage(OLPagination<QueryParamsBase> query);
        Task<bool> UpdateMedicationStatus(QueryParamsBase query);
    }
}
