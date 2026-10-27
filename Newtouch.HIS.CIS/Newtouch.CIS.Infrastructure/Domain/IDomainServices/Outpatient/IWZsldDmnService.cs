using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Domain.DTO;
using Newtouch.Domain.ValueObjects;
using Newtouch.Domain.ValueObjects.Storage;

namespace Newtouch.Domain.IDomainServices
{
    public interface IWZsldDmnService
    {

        /// <summary>
        /// Danh sách thả xuốngvật tưthông tin
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        List<VSelProductInfoVO> DepartmentStockListQuery(DepartmentStockListQueryParamDTO param);

        /// <summary>
        /// Lấyvật tưSố lôLôthông tin  top 20
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <param name="gysId"></param>
        /// <param name="deliveryNo"></param>
        /// <returns></returns>
        List<VProductBatchInfoVO> ProductBatchQuery(string productId, string warehouseId, string organizeId,  string keyword = "");

        List<RelWarehouseVO> GetList(string organizeId, string keyword);
        List<RelWarehouseVO> GetDeptList(string organizeId, string keyword);

        #region  Khoavật tưQuản lý
        List<WzTypeVo> GetWzTreeSelectJson();

        IList<VProductStorageEntity> GetProductStorage(Pagination pagination, string ks, string organizeId, string keyWord, string lbId, string xslkc);
        
        IList<VProductStorageEntity> GetExpiredProductStorage(Pagination pagination, string ks, string organizeId, string keyWord, string lbId, string xslkc);

        IList<VProductStorageDetailEntity> GetProductStorageDetail(string ks, string OrganizeId, string proId, string zt);

        int UpdateZt(string mxId, string OrganizeId, string zt);

        void UpdateSyncWz(string OrganizeId, string userCode);
        #endregion

    }
}
