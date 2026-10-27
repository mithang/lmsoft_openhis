using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SqlSugar;

namespace Newtouch.Infrastructure
{
    /// <summary>Tính chất tự trả v2 (values from Newtouch.HIS.Base Infrastructure/EnumDB.cs).</summary>
    public enum EnumZFXZv2
    {
        [System.ComponentModel.Description("Tự trả")] ZF = 1,
        [System.ComponentModel.Description("Hạng A")] J = 4,
        [System.ComponentModel.Description("Hạng B")] Y = 5,
        [System.ComponentModel.Description("Hạng C")] B = 6,
    }

    /// <summary>Sett delivery-detail DTO (shape from Sett API; only Count is used in PDS).</summary>
    public class GetTokenResponse { public string token { get; set; } }
}

namespace Newtouch.Tools
{
    /// <summary>In-memory stand-in for the Redis cache used by token helpers.</summary>
    public static class RedisHelper
    {
        private static readonly ConcurrentDictionary<string, (string Value, DateTime? Expire)> _store = new();
        public static string StringGet(string key)
        {
            if (_store.TryGetValue(key, out var v) && (v.Expire == null || v.Expire > DateTime.Now)) return v.Value;
            return null;
        }
        public static void StringSet(string key, string value, TimeSpan? expiry = null)
            => _store[key] = (value, expiry == null ? null : DateTime.Now.Add(expiry.Value));
        public static T Get<T>(string key)
        {
            var s = StringGet(key);
            if (string.IsNullOrWhiteSpace(s)) return default;
            try { return JsonConvert.DeserializeObject<T>(s); } catch { return default; }
        }
        public static void Set(string key, object value, TimeSpan? expiry = null)
            => StringSet(key, value == null ? null : JsonConvert.SerializeObject(value), expiry);
        public static void Remove(string key) => _store.TryRemove(key, out _);
        public static bool Exists(string key) => StringGet(key) != null;
    }
}

namespace Newtouch.Tools.Net
{
    /// <summary>Minimal HTTP helper replacing the Framework HttpMethods (JSON POST).</summary>
    public static class HttpMethods
    {
        private static readonly HttpClient _client = new(new HttpClientHandler { UseProxy = false }) { Timeout = TimeSpan.FromSeconds(60) };
        public static string HttpPost(string url, string json, object headers = null)
        {
            var req = new HttpRequestMessage(HttpMethod.Post, url)
            { Content = new StringContent(json ?? "", Encoding.UTF8, "application/json") };
            return _client.Send(req).Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }
        public static string HttpPostWithToken(string uri, string data, string token)
        {
            var req = new HttpRequestMessage(HttpMethod.Post, uri)
            { Content = new StringContent(data ?? "", Encoding.UTF8, "application/json") };
            if (!string.IsNullOrWhiteSpace(token)) req.Headers.Add("Authorization", "Bearer " + token);
            return _client.Send(req).Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }
    }
}

namespace Newtouch.Common.Operator
{
    public static class OperatorProvider
    {
        public static NewtouchHIS.Lib.Base.Model.OperatorModel GetCurrent()
            => NewtouchHIS.Lib.Framework.Operator.OperatorProvider.GetCurrent();
    }
}

namespace Newtouch.Core.Common.Security
{
    public static class Comm
    {
        public static string CreateNo() => Guid.NewGuid().ToString("N");
    }
    public static class DESEncrypt
    {
        public static string Encrypt(string text, string sKey) => NewtouchHIS.Lib.Base.Security.DESEncrypt.Encrypt(text, sKey);
        public static string Decrypt(string text, string sKey = null) => NewtouchHIS.Lib.Base.Security.DESEncrypt.Decrypt(text, sKey);
    }
    public static class Md5
    {
        public static string md5(string str, int code)
        {
            if (str == null) str = "";
            using var md5 = System.Security.Cryptography.MD5.Create();
            var hex = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(str))).Replace("-", "").ToLower();
            return code == 16 ? hex.Substring(8, 16) : hex;
        }
    }
}

namespace Newtouch.HIS.API.Common.Helper
{
    public class MqGeneralTaskRequestDto { public DateTime Timestamp { get; set; } public string body { get; set; } }
    public class MqGeneralTaskBody { public string OperationType { get; set; } public string Content { get; set; } }
    public class PatientBaseInfoQueryRequest { public string cfh { get; set; } public string OrganizeId { get; set; } }
    /// <summary>MQ publish stub: no broker in this deployment; the call is fire-and-forget upstream.</summary>
    public static class MqClientHelper
    {
        public static void SettGeneralTask(MqGeneralTaskRequestDto rq) { /* no-op stub */ }
    }
}

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>Sett DeliveryRpDetailQuery item DTO; PDS only checks Count, so no props are required.</summary>
    public class fyCfYpInfo { }
}

namespace NewtouchHIS.Lib.Base.Extension
{
    /// <summary>Extensions that lived in the Framework Tools assembly (ToJson/GetDescription already exist in Lib.Base).</summary>
    public static class PdsMissingExtensions
    {
        public static DataTable ToDataTable<T>(this System.Collections.Generic.IEnumerable<T> list)
        {
            var dt = new DataTable();
            if (list == null) return dt;
            var props = typeof(T).GetProperties();
            foreach (var p in props) dt.Columns.Add(p.Name, Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType);
            foreach (var item in list)
            {
                var row = dt.NewRow();
                foreach (var p in props) row[p.Name] = p.GetValue(item) ?? DBNull.Value;
                dt.Rows.Add(row);
            }
            return dt;
        }
        public static T Clone<T>(this T obj)
        {
            if (obj == null) return default;
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj));
        }
        public static int ToInt(this object value)
        {
            if (value == null || value == DBNull.Value) return 0;
            try { return Convert.ToInt32(value); } catch { return 0; }
        }
        public static int ToInt(this int? value) => value ?? 0;
        public static DateTime ToDate(this DateTime? value) => value ?? default;
        public static DateTime ToDate(this object value)
        {
            if (value == null || value == DBNull.Value) return default;
            try { return Convert.ToDateTime(value); } catch { return default; }
        }
    }
}

namespace NewtouchHIS.Lib.Base.Utilities
{
    /// <summary>Predicate starter (the referenced Lib.Base DLL predates this type).</summary>
    public static class PredicateBuilder
    {
        public static System.Linq.Expressions.Expression<System.Func<T, bool>> GetTrue<T>() { return f => true; }
        public static System.Linq.Expressions.Expression<System.Func<T, bool>> And<T>(this System.Linq.Expressions.Expression<System.Func<T, bool>> first, System.Linq.Expressions.Expression<System.Func<T, bool>> second)
        {
            var invoked = System.Linq.Expressions.Expression.Invoke(second, first.Parameters);
            return System.Linq.Expressions.Expression.Lambda<System.Func<T, bool>>(System.Linq.Expressions.Expression.AndAlso(first.Body, invoked), first.Parameters);
        }
        public static System.Linq.Expressions.Expression<System.Func<T, bool>> Or<T>(this System.Linq.Expressions.Expression<System.Func<T, bool>> first, System.Linq.Expressions.Expression<System.Func<T, bool>> second)
        {
            var invoked = System.Linq.Expressions.Expression.Invoke(second, first.Parameters);
            return System.Linq.Expressions.Expression.Lambda<System.Func<T, bool>>(System.Linq.Expressions.Expression.OrElse(first.Body, invoked), first.Parameters);
        }
    }
}

namespace Newtouch.Core.Redis
{
    public static class RedisHelper
    {
        public static string StringGet(string key) => Newtouch.Tools.RedisHelper.StringGet(key);
        public static void StringSet(string key, string value, TimeSpan? expiry = null) => Newtouch.Tools.RedisHelper.StringSet(key, value, expiry);
        public static T Get<T>(string key) => Newtouch.Tools.RedisHelper.Get<T>(key);
        public static void Set(string key, object value, TimeSpan? expiry = null) => Newtouch.Tools.RedisHelper.Set(key, value, expiry);
        public static void Remove(string key) => Newtouch.Tools.RedisHelper.Remove(key);
        public static bool Exists(string key) => Newtouch.Tools.RedisHelper.Exists(key);
    }
}

namespace Newtouch.Core.Attributes
{
    /// <summary>Marker: skips the authorization filter (Framework semantics; the Core filter treats all actions uniformly).</summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class HandlerAuthorizeIgnoreAttribute : Attribute { }
}
