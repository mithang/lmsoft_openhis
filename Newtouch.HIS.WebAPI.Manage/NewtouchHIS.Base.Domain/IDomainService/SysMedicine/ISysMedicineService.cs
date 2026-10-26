using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.Model;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.Base.Domain.IDomainService
{
    /// <summary>
    /// thuốc hệ thống
    /// </summary>
    public interface ISysMedicineService : ISingletonDependency
    {
        /// <summary>
        /// từ điển thuốc hệ thống
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<List<SysMedicineVVO>> GetMedicineList(QueryParamsBase query);
        /// <summary>
        /// từ điển thuốc hệ thống(phân trang)
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<PageResponseRow<List<SysMedicineVVO>>> GetMedicinePage(OLPagination<QueryParamsBase> query);
        /// <summary>
        /// cách dùng thuốc
        /// </summary>
        /// <param name="code"></param>
        /// <param name="yplx"></param>
        /// <returns></returns>
        Task<List<DrugUsageEntity>> DrugUsageDic(string? code = null, int? yplx = null);
        /// <summary>
        /// từ điển tần suất
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<List<SysFrequencyEntity>> SysFrequencyDic(string? code = null, string? orgId = null);
        /// <summary>
        /// đơn vị thuốc
        /// </summary>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<List<DrugUnitEntity>> DrugUnitDic(string? code = null, string? name = null);
        /// <summary>
        /// cách dùng dạng bào chế thuốc
        /// </summary>
        /// <returns></returns>
        Task<List<DrugFormulationUsageVO>> GetDrugFormulationUsageList();
    }
}
