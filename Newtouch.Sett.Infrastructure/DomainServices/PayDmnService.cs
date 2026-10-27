using Newtouch.Sett.Domain;
using Newtouch.Sett.Domain.DTO;
using Newtouch.Sett.Domain.Entity;
using Newtouch.Sett.Infrastructure;
using NewtouchHIS.Lib.Base.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.Sett.DomainServices
{
    /// <summary>
    /// PayManage domain service ported from Framework PayDmnService (EF6 raw SQL
    /// -&gt; SqlSugar SqlQueryable). The SQL text is unchanged except that
    /// ORDER BY is never embedded in the paged query: SqlSugar's ToPageList
    /// wraps the SQL in a derived table for the COUNT query, where ORDER BY
    /// is illegal (same gotcha as the MRQC pilot), so sorting is applied via
    /// the queryable's OrderBy with a whitelisted column list.
    /// </summary>
    public class PayDmnService : IPayDmnService
    {
        private static readonly HashSet<string> SortableColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "PayType", "OutTradeNo", "TradeNo", "OrderDate", "PayAmount", "PayStatus", "OutTradeTime"
        };

        private static DateTime ParseDate(string s, DateTime fallback)
        {
            DateTime d;
            return !string.IsNullOrWhiteSpace(s) && DateTime.TryParse(s, out d) ? d : fallback;
        }

        public IList<MicroPayTradeInfoDTO> TradePayLsit(Pagination pagination, string ksrq, string jsrq, int? payType,
            int? payStatus, string keywords, string orgId)
        {
            var para = new List<SugarParameter>();
            string sql = @"select PayType,OutTradeNo,TradeNo,OrderDate, PayAmount,PayStatus, PayMemo,PayAccount,TradeDesc, OutTradeTime,
                sum(RefundAmount) RefundAmount,fph,xm,kh,patno
                from (
                select a.PayType,a.OutTradeNo,a.TradeNo,a.OrderDate,a.Amount PayAmount,PayStatus,a.Memo PayMemo,
                a.PayAccount,a.TradeDesc,a.CreateTime OutTradeTime,b.id RefundId,
               b.Amount RefundAmount,'' fph,isnull(d.xm,f.xm)xm,isnull(d.kh,f.kh)kh,isnull(d.mzh,f.zyh) patno
                from order_payinfo a with(nolock)
                left join order_refundinfo b with(nolock) on a.outtradeno=b.outtradeno and b.zt=1  and b.refundstatus=1
                left join mz_js c with(nolock) on a.outtradeno=c.outtradeno and c.zt=1
                left join mz_gh d with(nolock) on c.organizeid=d.organizeid and c.ghnm=d.ghnm and d.zt=1
                left join zy_js e with(nolock) on a.outtradeno=e.outtradeno and e.zt=1
                left join zy_brjbxx f with(nolock) on e.zyh=f.zyh and e.organizeid=f.organizeid and f.zt=1
                where a.zt=1 and OrderDate between @ksrq and @jsrq ";

            para.Add(new SugarParameter("@ksrq", ParseDate(ksrq, DateTime.Today.AddDays(-7))));
            para.Add(new SugarParameter("@jsrq", ParseDate(jsrq, DateTime.Today).AddDays(1).AddSeconds(-1)));

            if (payType != null)
            {
                sql += " and PayType=@payType ";
                para.Add(new SugarParameter("@payType", payType));
            }

            if (payStatus != null)
            {
                sql += " and payStatus=@payStatus ";
                para.Add(new SugarParameter("@payStatus", payStatus));
            }

            if (!string.IsNullOrWhiteSpace(keywords))
            {
                sql += " and (d.xm=@keywords or d.kh=@keywords)";
                para.Add(new SugarParameter("@keywords", keywords));
            }

            if (!string.IsNullOrWhiteSpace(orgId))
            {
                sql += " and a.organizeid=@orgId ";
                para.Add(new SugarParameter("@orgId", orgId));
            }

            sql += @"
               group by a.PayType,a.OutTradeNo,a.TradeNo,a.OrderDate,a.Amount,PayStatus,a.Memo ,
                a.PayAccount,a.TradeDesc,a.CreateTime,d.xm,d.kh ,d.mzh,b.Amount,b.id,f.xm,f.kh ,f.zyh
                ) a
            group by PayType,OutTradeNo,TradeNo,OrderDate, PayAmount,PayStatus, PayMemo,PayAccount,TradeDesc, OutTradeTime,
            fph,xm,kh,patno
            ";

            string sortCol = "OutTradeNo";
            if (!string.IsNullOrWhiteSpace(pagination.sidx))
            {
                var first = pagination.sidx.Trim().Split(new[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(first) && SortableColumns.Contains(first))
                {
                    sortCol = first;
                }
            }
            string sortDir = string.Equals(pagination.sord, "asc", StringComparison.OrdinalIgnoreCase) ? "asc" : "desc";

            int total = 0;
            var page = SettDb.GetConnection().SqlQueryable<MicroPayTradeInfoDTO>(sql)
                .AddParameters(para.ToArray())
                .OrderBy(sortCol + " " + sortDir)
                .ToPageList(pagination.page ?? 1, pagination.rows ?? 15, ref total);
            pagination.records = total;
            return page;
        }

        public MicroPayTradeInfoDTO TradePayInfobyNo(string OutTradeNo, string TradeNo)
        {
            var para = new List<SugarParameter>();
            string sql = @"select PayType,OutTradeNo,TradeNo,OrderDate, PayAmount,PayStatus, PayMemo,PayAccount,TradeDesc, OutTradeTime,
                isnull(sum(RefundAmount),0.00) RefundAmount,fph,xm,kh,patno
                from (
                select a.PayType,a.OutTradeNo,a.TradeNo,a.OrderDate,a.Amount PayAmount,PayStatus,a.Memo PayMemo,
                a.PayAccount,a.TradeDesc,a.CreateTime OutTradeTime,b.id RefundId,
               b.Amount RefundAmount,'' fph,isnull(d.xm,f.xm)xm,isnull(d.kh,f.kh)kh,isnull(d.mzh,f.zyh) patno
                from order_payinfo a with(nolock)
                left join order_refundinfo b with(nolock) on a.outtradeno=b.outtradeno and b.zt=1  and b.refundstatus=1
                left join mz_js c with(nolock) on a.outtradeno=c.outtradeno and c.zt=1
                left join mz_gh d with(nolock) on c.organizeid=d.organizeid and c.ghnm=d.ghnm and d.zt=1
                left join zy_js e with(nolock) on a.outtradeno=e.outtradeno and e.zt=1
                left join zy_brjbxx f with(nolock) on e.zyh=f.zyh and e.organizeid=f.organizeid and f.zt=1
                where a.zt=1 ";

            if (!string.IsNullOrWhiteSpace(OutTradeNo))
            {
                sql += " and a.OutTradeNo=@OutTradeNo ";
                para.Add(new SugarParameter("@OutTradeNo", OutTradeNo));
            }

            if (!string.IsNullOrWhiteSpace(TradeNo))
            {
                sql += " and a.TradeNo=@TradeNo ";
                para.Add(new SugarParameter("@TradeNo", TradeNo));
            }

            sql += @"
               group by a.PayType,a.OutTradeNo,a.TradeNo,a.OrderDate,a.Amount,PayStatus,a.Memo ,
                a.PayAccount,a.TradeDesc,a.CreateTime,d.xm,d.kh ,d.mzh,b.Amount,b.id,f.xm,f.kh ,f.zyh
                ) a
            group by PayType,OutTradeNo,TradeNo,OrderDate, PayAmount,PayStatus, PayMemo,PayAccount,TradeDesc, OutTradeTime,
            fph,xm,kh,patno ";

            return SettDb.GetConnection().SqlQueryable<MicroPayTradeInfoDTO>(sql)
                .AddParameters(para.ToArray()).ToList().FirstOrDefault();
        }

        public IList<OrderRefundInfoEntity> TradeRefundList(string OutTradeNo, string TradeNo)
        {
            var para = new List<SugarParameter>();
            string sql = @"SELECT [Id]
                      ,[OutTradeNo]
                      ,[TradeNo]
                      ,[Amount]
                      ,[RefundStatus]
                      ,[Memo]
                      ,[CreatorCode]
                      ,[CreateTime]
                      ,[LastModifyTime]
                      ,[LastModifierCode]
                      ,[zt]
                      ,[RefundReason]
                      ,[RefundDate]
                      ,[OutRequestNo]
                  FROM [dbo].[Order_RefundInfo]
                where zt=1";
            if (!string.IsNullOrWhiteSpace(OutTradeNo))
            {
                sql += " and OutTradeNo=@OutTradeNo ";
                para.Add(new SugarParameter("@OutTradeNo", OutTradeNo));
            }
            if (!string.IsNullOrWhiteSpace(TradeNo))
            {
                sql += " and TradeNo=@TradeNo ";
                para.Add(new SugarParameter("@TradeNo", TradeNo));
            }

            sql += " order by CreateTime ";
            return SettDb.GetConnection().SqlQueryable<OrderRefundInfoEntity>(sql)
                .AddParameters(para.ToArray()).ToList();
        }

        public IList<MicroPayTradeInfoDTO> MicroPayTradeQuery(string ksrq, string jsrq, int? payType, int? payStatus,
            string keywords, string orgId)
        {
            var para = new List<SugarParameter>();
            string sql = @"select a.PayType,a.OutTradeNo,a.TradeNo,a.OrderDate,a.Amount PayAmount,PayStatus,a.Memo PayMemo,
                a.PayAccount,a.TradeDesc,a.CreateTime OutTradeTime,
                b.Amount RefundAmount,RefundDate,RefundReason,RefundStatus,b.Memo RefundMemo,b.CreateTime OutRefundTime,c.fph,
                d.xm,d.kh,b.OutRequestNo
                from order_payinfo a with(nolock)
                left join order_refundinfo b with(nolock) on a.outtradeno=b.outtradeno
                left join mz_js c with(nolock) on a.outtradeno=c.outtradeno
                left join mz_gh d with(nolock) on c.organizeid=d.organizeid and c.ghnm=d.ghnm
                where a.zt=1 and OrderDate between @ksrq and @jsrq ";

            para.Add(new SugarParameter("@ksrq", ParseDate(ksrq, DateTime.Today.AddDays(-7))));
            para.Add(new SugarParameter("@jsrq", ParseDate(jsrq, DateTime.Today).AddDays(1).AddSeconds(-1)));

            if (payType != null)
            {
                sql += " and PayType=@payType ";
                para.Add(new SugarParameter("@payType", payType));
            }

            if (payStatus != null)
            {
                sql += " and payStatus=@payStatus ";
                para.Add(new SugarParameter("@payStatus", payStatus));
            }

            if (!string.IsNullOrWhiteSpace(keywords))
            {
                sql += " and (d.xm=@keywords or d.kh=@keywords)";
                para.Add(new SugarParameter("@keywords", keywords));
            }

            if (!string.IsNullOrWhiteSpace(orgId))
            {
                sql += " and c.organizeid=@orgId ";
                para.Add(new SugarParameter("@orgId", orgId));
            }

            return SettDb.GetConnection().SqlQueryable<MicroPayTradeInfoDTO>(sql)
                .AddParameters(para.ToArray()).ToList();
        }
    }

    /// <summary>
    /// STUB — the Framework PayApp.TradeRefund calls the third-party payment
    /// gateways (Alipay/WeChat SDK via NewtouchPay.dll, a .NET Framework-only
    /// binary, plus the NxPay aggregator HTTP endpoint) and then writes the
    /// refund record. Neither gateway is available in this .NET 6 port, so
    /// this stub always reports Failed with an explicit message and performs
    /// NO database write. The controller surfaces the failure to the caller
    /// instead of a fake success. Replace with a real gateway client at merge
    /// time if live refunds are required.
    /// </summary>
}
