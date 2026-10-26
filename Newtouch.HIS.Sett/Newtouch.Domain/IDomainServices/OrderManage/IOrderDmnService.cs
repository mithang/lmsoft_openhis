using Newtouch.HIS.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    public interface IOrderDmnService
    {
        /// <summary>
        /// Tra cứu đơn hàng
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        List<OrderInfoVO> OrderQuery(OrderInfoBaseVO vo, string orgId);
        /// <summary>
        /// Tra cứu chi tiết đơn hàng
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="orgId"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        OrderDetailVO OrderDetailQuery(string orderNo, string orgId, string appId);
        /// <summary>
        /// Chi tiết đơn hàng ngoại trú
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="orgId"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        List<OrderMzVO> OrderDetailMzQuery(string orderNo, string orgId, string appId);
        /// <summary>
        /// Chi tiết đơn hàng nội trú
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="orgId"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        List<OrderZyBaseVO> OrderDetailZyQuery(string orderNo, string orgId, string appId);
        /// <summary>
        /// Tạo đơn hàng ngoại trú
        /// </summary>
        /// <param name="kh"></param>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        OrderInfoBaseVO OrderCreateMz(string kh, string mzh, string orgId, string appId,int patId,string dzId);
        /// <summary>
        /// Tạo đơn hàng quyết toán nội trú
        /// </summary>
        /// <param name="kh"></param>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        OrderInfoBaseVO OrderCreateZy(string kh, string zyh, string orgId, string appId);
        /// <summary>
        /// Khóa đơn hàng
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="kh"></param>
        /// <param name="appId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        OrderInfoBaseVO LockOrderApply(string orderNo, decimal? orderAmt, string kh, string appId, string orgId);
        /// <summary>
        /// Trì hoãn khóa đăng ký
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="appId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        OrderInfoBaseVO LockOrderDelayedApply(string orderNo, string appId, string orgId);
        /// <summary>
        /// Thanh toán đơn hàng
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="appId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        OrderInfoBaseVO OrderPaidSuccess(OrderInfoVO vo, string appId, string orgId);
        /// <summary>
        /// Thanh toán đơn hàng ngoại trústep1:Kiểm tra đơn hàng trước thanh toán
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        OutpOrderVO PayForOutpBillOrderBefore(OrderInfoVO vo, string appId);
        /// <summary>
        /// Thanh toán đơn hàng nội trú:Kiểm tra trước thanh toán
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        InHosOrderVO PayForInHosBillOrderBefore(OrderInfoVO vo, string appId);
        /// <summary>
        /// Quyết toán thành công Hệ thống nghiệp vụ đồng bộ đẩy
        /// </summary>
        /// <param name="cfnmList"></param>
        /// <param name="cfList"></param>
        /// <param name="mzh"></param>
        /// <param name="jsnm"></param>
        /// <param name="fph"></param>
        /// <param name="sfrq"></param>
        /// <param name="appId"></param>
        /// <param name="orgId"></param>
        string PushPresInfo(IList<int> cfnmList, List<string> cfList, string mzh, int jsnm, string fph, DateTime? sfrq, string appId, string orgId, int cflx = 0);
    }
}