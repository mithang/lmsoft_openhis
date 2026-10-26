using NewtouchHIS.Base.Domain.Model;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.Base.Domain.IDomainService
{
    public interface IChargeItemService : IScopedDependency
    {
        /// <summary>
        /// nhóm thu phí lớn
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<List<SysChargeCategoryVO>> GetChargeCategory(QueryParamsBase query);
        /// <summary>
        /// danh sách hạng mục thu phí
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<List<SysChargeItemVO>> GetChargeItemList(QueryParamsBase query);
        /// <summary>
        /// danh sách hạng mục thu phí(phân trang)
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<PageResponseRow<List<SysChargeItemVO>>> GetChargeItemPage(OLPagination<QueryParamsBase> query);
        Task<List<SysChargeItemVO>> GetMaterialItemList(QueryParamsBase query);
        Task<PageResponseRow<List<SysChargeItemVO>>> GetMaterialItemPage(OLPagination<QueryParamsBase> query);
        
    }
}
