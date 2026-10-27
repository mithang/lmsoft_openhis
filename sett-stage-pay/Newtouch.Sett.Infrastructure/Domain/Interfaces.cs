using Newtouch.Sett.Domain.DTO;
using Newtouch.Sett.Domain.Entity;
using NewtouchHIS.Lib.Base.Model;
using System.Collections.Generic;

namespace Newtouch.Sett.Domain
{
    /// <summary>PayManage domain service (same interface names as Framework).</summary>
    public interface IPayDmnService
    {
        IList<MicroPayTradeInfoDTO> MicroPayTradeQuery(string ksrq, string jsrq, int? payType, int? payStatus,
            string keywords, string orgId);

        IList<MicroPayTradeInfoDTO> TradePayLsit(Pagination pagination, string ksrq, string jsrq, int? payType, int? payStatus,
            string keywords, string orgId);

        IList<OrderRefundInfoEntity> TradeRefundList(string OutTradeNo, string TradeNo);

        MicroPayTradeInfoDTO TradePayInfobyNo(string OutTradeNo, string TradeNo);
    }

    /// <summary>PayManage application service (same interface name as Framework).</summary>
    public interface IPayApp
    {
        int TradeRefund(string outTradeNo, decimal refundAmount, string refundReason, string outRequestNo,
            out string errorMsg);
    }

    public interface IOrderPayInfoRepo
    {
        void SubmitInfo(OrderPayInfoEntity entity, string keyValue = null);

        OrderPayInfoEntity GetSuccessRecordByOutTradeNo(string outTradeNo);
    }

    public interface IOrderRefundInfoRepo
    {
        void SubmitInfo(OrderRefundInfoEntity entity, string keyValue);

        decimal GetRefundedSumByOutTradeNo(string outTradeNo);
    }
}
