using Newtouch.HIS.Domain.Entity.V;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices.DrugStorage
{
    /// <summary>
    /// Thao tác tồn kho
    /// </summary>
    public interface ISysMedicineStockInfoDmnService
    {

        /// <summary>
        /// Nhập kho từ ngoài Trừ tồn kho(Hủy bỏ)
        /// </summary>
        /// <param name="sl">Số lượng đơn vị nhỏ nhất</param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="ypdm"></param>
        /// <param name="organizeId"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string WbrkKkc(int sl, string pc, string ph, string ypdm, string organizeId, string yfbmCode, string userCode);

        /// <summary>
        /// Đóng băng tồn kho，và trả về thông tin lô bị đóng băng
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