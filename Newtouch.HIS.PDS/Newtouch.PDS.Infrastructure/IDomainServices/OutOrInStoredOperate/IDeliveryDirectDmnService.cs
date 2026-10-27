using System;
using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects.DrugStorage;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Xuất kho trực tiếp
    /// </summary>
    public interface IDeliveryDirectDmnService
    {
        /// <summary>
        /// Gửi đơn xin xuất kho trực tiếp
        /// </summary>
        /// <param name="dj"></param>
        /// <param name="mx"></param>
        /// <returns></returns>
        string SubmitDeliveryDirect(SysMedicineStorageIOReceiptEntity dj, List<SysMedicineStorageIOReceiptDetailEntity> mx);
        /// <summary>
        /// Lấy hóa đơn nhập kho
        /// </summary>
        /// <param name="djlx"></param>
        /// <param name="fph"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <returns></returns>
        List<BillFphVo> GetRkFphData(int djlx, string fph, DateTime kssj, DateTime jssj);
        /// <summary>
        /// Chi tiết hóa đơn nhập kho thuốc
        /// </summary>
        /// <param name="djlx"></param>
        /// <param name="fph"></param>
        /// <param name="pc"></param>
        /// <returns></returns>
        List<BillFphMxVo> GetRkFphMxData(int djlx, string fph, string pc);

    }
}