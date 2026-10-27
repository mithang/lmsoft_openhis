using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Newtouch.Core.Common.Utils
{
    public static class ConfigurationHelper
    {
        public static IConfiguration Config { get; set; }

        public static void Init(IConfiguration config)
        {
            Config = config;
        }

        public static string GetAppConfigValue(string key)
        {
            if (Config == null) return null;
            return Config["AppSettings:" + key]
                ?? Config["BusinessConfig:AppAPIHost:" + key]
                ?? Config["BusinessConfig:" + key];
        }

        public static int GetAppConfigIntValue(string key, int defaultValue = 0) { var v = GetAppConfigValue(key); return int.TryParse(v, out var i) ? i : defaultValue; }
        public static bool? GetAppConfigBoolValue(string key)
        {
            var v = GetAppConfigValue(key);
            if (string.IsNullOrWhiteSpace(v)) return null;
            if (bool.TryParse(v, out var b)) return b;
            if (v == "1") return true;
            if (v == "0") return false;
            return null;
        }
    }

    public static class StringExtensions
    {
        public static string EnBase64(this string s)
        {
            if (s == null) return null;
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(s));
        }

        public static string DeBase64(this string s)
        {
            if (s == null) return null;
            return Encoding.UTF8.GetString(Convert.FromBase64String(s));
        }
    }

    public static class HttpClientHelper
    {
        public enum EnumContentType { json, form, xml, text }

        private static readonly HttpClient _client = new HttpClient(new HttpClientHandler { UseProxy = false })
        {
            Timeout = TimeSpan.FromSeconds(60)
        };

        public static T HttpPostStringAndRead<T>(string url, string json, EnumContentType contentType = EnumContentType.json)
        {
            var ct = contentType == EnumContentType.json ? "application/json"
                : contentType == EnumContentType.form ? "application/x-www-form-urlencoded"
                : contentType == EnumContentType.xml ? "application/xml" : "text/plain";
            using var req = new StringContent(json ?? "", Encoding.UTF8, ct);
            var resp = _client.PostAsync(url, req).GetAwaiter().GetResult();
            var body = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return JsonConvert.DeserializeObject<T>(body);
        }

        public static T HttpPostStringAndRead<T>(string url, object reqObj)
        {
            return HttpPostStringAndRead<T>(url, JsonConvert.SerializeObject(reqObj));
        }
    }
}

namespace Newtouch.Core.Common.Interface
{
    public interface ICache
    {
        T Get<T>(string key, Func<T> factory);

        T Get<T>(string key, Func<T> factory, int cacheTimeSeconds);

        void Set<T>(string key, T value, int cacheTimeSeconds = 3600);

        void Remove(string key);
    }

    public class MemoryCacheService : ICache
    {
        private readonly IMemoryCache _cache;

        public MemoryCacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public T Get<T>(string key, Func<T> factory)
        {
            return Get(key, factory, 3600);
        }

        public T Get<T>(string key, Func<T> factory, int cacheTimeSeconds)
        {
            if (_cache.TryGetValue(key, out T v)) return v;
            var val = factory();
            _cache.Set(key, val, TimeSpan.FromSeconds(cacheTimeSeconds <= 0 ? 3600 : cacheTimeSeconds));
            return val;
        }

        public void Set<T>(string key, T value, int cacheTimeSeconds = 3600)
        {
            _cache.Set(key, value, TimeSpan.FromSeconds(cacheTimeSeconds <= 0 ? 3600 : cacheTimeSeconds));
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }
    }
}

namespace Newtouch.Core.Common.Tools
{
    public static class Json
    {
        public static string ToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static JObject ToJObject(string json)
        {
            return JObject.Parse(json);
        }
    }
}

namespace Newtouch.Tools
{
    public static class Json
    {
        public static string ToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static JObject ToJObject(string json)
        {
            return JObject.Parse(json);
        }
    }
}

namespace Newtouch.Core.Common.Tools.Net
{
    public static class HttpClientHelper
    {
        public static T HttpPostStringAndRead<T>(string url, string json, Newtouch.Core.Common.Utils.HttpClientHelper.EnumContentType contentType)
        {
            return Newtouch.Core.Common.Utils.HttpClientHelper.HttpPostStringAndRead<T>(url, json, contentType);
        }
    }
}

namespace Newtouch.HIS.API.Common.Helper
{
    public static class HttpClientHelper
    {
        public static T HttpPostStringAndRead<TV, T>(string url, TV req, Microsoft.AspNetCore.Http.HttpContext httpContext = null) where T : new()
        {
            return Newtouch.Core.Common.Utils.HttpClientHelper.HttpPostStringAndRead<T>(url, req);
        }

        public static T HttpPostStringAndRead<T>(string url, string json, Microsoft.AspNetCore.Http.HttpContext httpContext = null)
        {
            return Newtouch.Core.Common.Utils.HttpClientHelper.HttpPostStringAndRead<T>(url, json);
        }
    }
}

namespace Newtouch.Common.Web
{
    public static class APIRequestHelper
    {
        public enum ResponseResultCode { SUCCESS, FAILED, ERROR, UNAUTHORIZED }

        public class DefaultResponse
        {
            public ResponseResultCode code { get; set; }
            public object data { get; set; }
            public string message { get; set; }
            public string msg { get => message; set => message = value; }
            public string sub_msg { get; set; }
        }
        public class DefaultResponse<T>
        {
            public ResponseResultCode code { get; set; }
            public T data { get; set; }
            public string message { get; set; }
            public string msg { get => message; set => message = value; }
            public string sub_msg { get; set; }
        }
        public class TokenClass
        {
            public string Token { get; set; }
            public DateTime LastRequestTime { get; set; }
        }
    }
}

namespace Newtouch.Tools.Excel
{
    public class ExcelColumn
    {
        public string Heading { get; set; }
        public string Name { get; set; }
        public int Width { get; set; }
        public int? NumberDigits { get; set; }
        public string DateTimeFormat { get; set; }
        public Func<object, object> FuncGetCellValue { get; set; }
        public double WidthTimes { get; set; }
    }

    public class ExcelSheet
    {
        public string Title { get; set; }
        public IList<ExcelColumn> columns { get; set; }
        public Dictionary<string, string> filters { get; set; }
    }

    public static class ExcelExtensions
    {
        public static bool ToExcel<T>(this IList<T> list, string filePath, ExcelSheet sheet)
        {
            return ToExcel((IEnumerable<T>)list, filePath, sheet);
        }

        public static bool ToExcel<T>(this IEnumerable<T> list, string filePath, ExcelSheet sheet)
        {
            try
            {
                var dir = System.IO.Path.GetDirectoryName(filePath);
                if (!string.IsNullOrWhiteSpace(dir)) System.IO.Directory.CreateDirectory(dir);
                IWorkbook wb = filePath.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase)
                    ? (IWorkbook)new XSSFWorkbook() : new HSSFWorkbook();
                var sh = wb.CreateSheet(string.IsNullOrWhiteSpace(sheet?.Title) ? "Sheet1" : sheet.Title);
                var cols = sheet?.columns ?? new List<ExcelColumn>();
                var header = sh.CreateRow(0);
                for (int i = 0; i < cols.Count; i++)
                {
                    header.CreateCell(i).SetCellValue(cols[i].Heading ?? cols[i].Name ?? "");
                }
                int r = 1;
                foreach (var item in list ?? new List<T>())
                {
                    var row = sh.CreateRow(r++);
                    for (int i = 0; i < cols.Count; i++)
                    {
                        var col = cols[i];
                        var cell = row.CreateCell(i);
                        object val = null;
                        if (col.FuncGetCellValue != null)
                        {
                            try { val = col.FuncGetCellValue(item); } catch { }
                        }
                        else if (!string.IsNullOrWhiteSpace(col.Name))
                        {
                            var prop = typeof(T).GetProperty(col.Name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                            if (prop != null) val = prop.GetValue(item);
                        }
                        if (val == null) continue;
                        if (val is DateTime dt)
                        {
                            cell.SetCellValue(dt);
                            if (!string.IsNullOrWhiteSpace(col.DateTimeFormat))
                            {
                                var style = wb.CreateCellStyle();
                                style.DataFormat = wb.CreateDataFormat().GetFormat(col.DateTimeFormat);
                                cell.CellStyle = style;
                            }
                        }
                        else if (col.NumberDigits.HasValue && double.TryParse(val.ToString(), out var d))
                        {
                            cell.SetCellValue(d);
                            var style = wb.CreateCellStyle();
                            style.DataFormat = wb.CreateDataFormat().GetFormat("0." + new string('0', col.NumberDigits.Value));
                            cell.CellStyle = style;
                        }
                        else
                        {
                            cell.SetCellValue(val.ToString());
                        }
                    }
                }
                using var fs = System.IO.File.Create(filePath);
                wb.Write(fs);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
