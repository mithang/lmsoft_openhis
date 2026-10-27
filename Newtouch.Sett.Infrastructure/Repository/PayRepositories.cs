using Newtouch.Sett.Domain;
using Newtouch.Sett.Domain.Entity;
using Newtouch.Sett.Infrastructure;
using Newtouch.Sett.Repository;
using System;
using System.Linq;

namespace Newtouch.Sett.Repository
{
    public class OrderPayInfoRepo : RepositoryBase<OrderPayInfoEntity>, IOrderPayInfoRepo
    {
        public void SubmitInfo(OrderPayInfoEntity entity, string keyValue = null)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                var ety = Db.Queryable<OrderPayInfoEntity>().Where(p => p.Id == keyValue).ToList().FirstOrDefault();
                if (ety != null)
                {
                    Db.Updateable(entity).ExecuteCommand();
                }
            }
            else
            {
                entity.Id = Guid.NewGuid().ToString();
                entity.CreateTime = DateTime.Now;
                Db.Insertable(entity).ExecuteCommand();
            }
        }

        public OrderPayInfoEntity GetSuccessRecordByOutTradeNo(string outTradeNo)
        {
            var success = (int)Newtouch.Sett.Infrastructure.EnumPayStatus.Success;
            return IQueryable().Where(p => p.OutTradeNo == outTradeNo && p.PayStatus == success).ToList().FirstOrDefault();
        }
    }

    public class OrderRefundInfoRepo : RepositoryBase<OrderRefundInfoEntity>, IOrderRefundInfoRepo
    {
        public void SubmitInfo(OrderRefundInfoEntity entity, string keyValue)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                var ety = Db.Queryable<OrderRefundInfoEntity>().Where(p => p.Id == keyValue).ToList().FirstOrDefault();
                if (ety != null)
                {
                    Db.Updateable(entity).ExecuteCommand();
                }
            }
            else
            {
                entity.Id = Guid.NewGuid().ToString();
                entity.CreateTime = DateTime.Now;
                Db.Insertable(entity).ExecuteCommand();
            }
        }

        public decimal GetRefundedSumByOutTradeNo(string outTradeNo)
        {
            var success = (int)Newtouch.Sett.Infrastructure.EnumRefundStatus.Success;
            var unknown = (int)Newtouch.Sett.Infrastructure.EnumRefundStatus.UnKnown;
            return IQueryable().Where(p => p.OutTradeNo == outTradeNo && (p.RefundStatus == success || p.RefundStatus == unknown))
                .Select(p => p.Amount).ToList().Sum();
        }
    }
}
