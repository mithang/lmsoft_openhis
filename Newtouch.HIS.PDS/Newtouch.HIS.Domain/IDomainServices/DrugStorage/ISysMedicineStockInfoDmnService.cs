using Newtouch.HIS.Domain.Entity.V;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices.DrugStorage
{
    /// <summary>
    /// tồn kho thao tác
    /// </summary>
    public interface ISysMedicineStockInfoDmnService
    {

        /// <summary>
        /// nhập kho từ bên ngoài trừ tồn kho(hủy bỏ)
        /// </summary>
        /// <param name="sl">số lượng đơn vị nhỏ nhất</param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="ypdm"></param>
        /// <param name="organizeId"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string WbrkKkc(int sl, string pc, string ph, string ypdm, string organizeId, string yfbmCode, string userCode);

        /// <summary>
        /// tồn kho bị đóng băng, và trả về thông tin lô bị đóng băng
        /// </summary>
        /// <param name="kcsl"></param>
        /// <param name="ypdm"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string FrozenStork(int kcsl, string ypdm, string yfbmCode, string organizeId, string userCode, out List<FrozenBatchesDetailVEntity> frozenInfo);
    }
}