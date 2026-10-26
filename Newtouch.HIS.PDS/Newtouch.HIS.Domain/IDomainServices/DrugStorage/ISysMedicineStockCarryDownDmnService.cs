using System;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// kết chuyển
    /// </summary>
    public interface ISysMedicineStockCarryDownDmnService
    {
        /// <summary>
        /// tra cứu đã kết chuyển thuốc
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="jzsj"></param>
        /// <param name="keyWork"></param>
        /// <returns></returns>
        IList<CarryOverMedicineVO> SelectCarryDownMedicineList(Pagination pagination, string jzsj, string keyWork);

        /// <summary>
        /// kết chuyển sẽ cần kết chuyển của thuốc tất cả chèn đến bảng kết chuyển
        /// </summary>
        /// <param name="list"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="jzsj">thời gian kết chuyển</param>
        string CarryOverMedicine(List<NeedCarryOverMedicineVO> list, string yfbmCode, string organizeId, DateTime jzsj);

        /// <summary>
        /// kết chuyển sẽ cần kết chuyển của thuốc tất cả chèn đến bảng kết chuyển
        /// </summary>
        /// <param name="list"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="jzsj"></param>
        /// <param name="userCode">thời gian kết chuyển</param>
        /// <returns></returns>
        string CarryOverMedicine(List<NeedCarryOverMedicineVO> list, string yfbmCode, string organizeId, DateTime jzsj, string userCode);

        /// <summary>
        /// kết chuyển sẽ cần kết chuyển của thuốc tất cả chèn đến bảng kết chuyển
        /// </summary>
        /// <param name="list"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="jzsj"></param>
        /// <param name="userCode">thời gian kết chuyển</param>
        /// <returns></returns>
         string CarryOverMedicine(string yfbmCode, string organizeId, DateTime jzsj, string userCode);
    }
}
