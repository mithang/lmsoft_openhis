using NewtouchHIS.Lib.Base.Security;
using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Newtouch.Tools
{
    /// <summary>
    /// Predicate builder (original Newtouch.Core ExtLinq, LinqKit-style).
    /// And/Or rebind parameters instead of using Expression.Invoke so the
    /// resulting tree stays translatable by SqlSugar's expression parser.
    /// </summary>
    public static class ExtLinq
    {
        public static Expression<Func<T, bool>> True<T>()
        {
            return x => true;
        }

        public static Expression<Func<T, bool>> False<T>()
        {
            return x => false;
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left,
            Expression<Func<T, bool>> right)
        {
            var param = left.Parameters[0];
            var rightBody = new ParameterReplacer(right.Parameters[0], param).Visit(right.Body);
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left.Body, rightBody), param);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left,
            Expression<Func<T, bool>> right)
        {
            var param = left.Parameters[0];
            var rightBody = new ParameterReplacer(right.Parameters[0], param).Visit(right.Body);
            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(left.Body, rightBody), param);
        }

        private class ParameterReplacer : ExpressionVisitor
        {
            private readonly ParameterExpression _from;
            private readonly ParameterExpression _to;

            public ParameterReplacer(ParameterExpression from, ParameterExpression to)
            {
                _from = from;
                _to = to;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return node == _from ? _to : base.VisitParameter(node);
            }
        }
    }

    /// <summary>
    /// Object conversions (original Newtouch.Tools extensions): null-safe, so
    /// nullable-typed entity fields (int?, DateTime?) convert without guards.
    /// </summary>
    public static class ObjectExtensions
    {
        public static int ToInt(this object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return 0;
            }
            return Convert.ToInt32(value);
        }

        public static DateTime? ToDate(this object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return null;
            }
            if (value is DateTime dt)
            {
                return dt;
            }
            if (DateTime.TryParse(value.ToString(), out var parsed))
            {
                return parsed;
            }
            return null;
        }

        /// <summary>Shallow copy (original IEntity.Clone). Extension form so the
        /// inferred return type is the entity type, matching call-site usage.</summary>
        public static T Clone<T>(this T entity) where T : class
        {
            if (entity == null)
            {
                return null;
            }
            var m = typeof(object).GetMethod("MemberwiseClone",
                BindingFlags.NonPublic | BindingFlags.Instance);
            return (T)m.Invoke(entity, null);
        }
    }

    /// <summary>
    /// Legacy Framework helper shims (originally Newtouch.Core.* / Newtouch.Common, binary-only).
    /// Md5 delegates to the net6.0 Lib MD5Encrypt so password hashes match the
    /// running BaseAPI/SSO verification exactly.
    /// </summary>
    public static class Md5
    {
        public static string md5(string str, int code)
        {
            return MD5Encrypt.Md5(str, code);
        }
    }

    /// <summary>
    /// Unique-number generator (original Newtouch.Common.Comm.CreateNo).
    /// Used only as a seed for secret keys (md5'd by callers); timestamp +
    /// random suffix gives the required uniqueness.
    /// </summary>
    public static class Comm
    {
        public static string CreateNo()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssfff") + RandomNumberGenerator.GetInt32(1000, 9999);
        }
    }

    /// <summary>
    /// Entity-change audit logger (original Newtouch.Core.NLogger AppLogger).
    /// BEHAVIOR GAP: the Framework sink (log DB/file) has no net6.0 equivalent in
    /// Lib, so records are traced but NOT persisted. Medicine/user pages keep
    /// working; the audit trail itself is not stored by this port.
    /// </summary>
    public static class AppLogger
    {
        public static void WriteEntityChangeRecordLog(object oldEntity, object newEntity, string tableName, string keyValue)
        {
            try
            {
                System.Diagnostics.Trace.WriteLine(
                    $"[EntityChange] {tableName} key={keyValue} old={Newtonsoft.Json.JsonConvert.SerializeObject(oldEntity)} new={Newtonsoft.Json.JsonConvert.SerializeObject(newEntity)}");
            }
            catch
            {
                // Audit logging must never break the business operation.
            }
        }
    }

    /// <summary>
    /// Minimal API-response envelope (original Newtouch.Tools.Net APIRequestHelper),
    /// used by the PDS cross-service calls. The PDS host does not exist in this
    /// deployment, so these shapes only need to compile and deserialize; the
    /// PDS module agent owns the real API contract.
    /// </summary>
    public static class APIRequestHelper
    {
        public class DefaultResponse
        {
            public string code { get; set; }
            public string message { get; set; }
            public object data { get; set; }
        }

        public class DefaultResponse<T>
        {
            public string code { get; set; }
            public string message { get; set; }
            public T data { get; set; }
        }

        public static class ResponseResultCode
        {
            public const string SUCCESS = "success";
        }
    }
}
