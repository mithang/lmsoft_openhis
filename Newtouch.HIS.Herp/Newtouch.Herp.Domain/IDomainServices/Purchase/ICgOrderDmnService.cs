using System;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Herp.Domain.Entity.VEntity;

namespace Newtouch.Herp.Domain.IDomainServices
{
    /// <summary>
    /// Đơn mua hàng
    /// </summary>
    public interface ICgOrderDmnService
    {

        /// <summary>
        /// Tra cứu thông tin đơn mua hàng
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orderNo"></param>
        /// <param name="orderType"></param>
        /// <param name="orderProcess"></param>
        /// <param name="organizeId"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <returns></returns>
        IList<VCgOrderEntity> SelectCgOrder(Pagination pagination, string orderNo, int orderType,
           int orderProcess, string organizeId, DateTime kssj, DateTime jssj);

        /// <summary>
        /// Tra cứu chi tiết đơn mua hàng
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<VCgOrderDetailEntity> SelectCgOrderDetailGroupByCgdh(string orderNo, string organizeId);

        /// <summary>
        /// Tra cứu chi tiết đơn mua hàng
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<VCgOrderDetailEntity> SelectCgOrderDetailNoCgdh(string orderNo, string organizeId);
        /// <summary>
        /// Nhập kho ngoài-Tham chiếu đơn mua hàng from Khung nhìn
        /// </summary>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="fph"></param>
        /// <param name="orderNo"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<VCgOrderRkEntity> SelectCgOrderView(DateTime kssj, DateTime jssj, string fph, string orderNo, string organizeId);

        /// <summary>
        /// Nhập kho ngoài-Tham chiếu đơn mua hàng
        /// </summary>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="fph"></param>
        /// <param name="orderNo"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<VCgOrderDetailEntity> SelectCgOrderDetail(string subOrderNo, string organizeId);
        /// <summary>
        /// Nhập chi tiết đơn mua hàng vào danh sách nhập kho
        /// </summary>
        /// <param name="subOrderNo"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<VCgOrderImportEntity> SelectCgOrderDetailImport(string subOrderNo, string organizeId);
        /// <summary>
        /// Điền bổ sung Số hóa đơn đơn mua hàng
        /// </summary>
        /// <param name="subOrderNo"></param>
        /// <param name="fph"></param>
        /// <param name="userCode"></param>
        void PurchaseDdbhUpdate(string subOrderNo, string fph, string userCode);
    }
}