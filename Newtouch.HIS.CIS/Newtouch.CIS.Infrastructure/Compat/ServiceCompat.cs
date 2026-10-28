using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Newtonsoft.Json;
using NewtouchHIS.Lib.Framework.Operator;
using SqlSugar;
using DbType = System.Data.DbType;

namespace Newtouch.CIS.Compat
{
    /// <summary>System.Data.SqlClient.SqlParameter -> SqlSugar parameter conversion.</summary>
    public static class SqlParam
    {
        public static SugarParameter[] ToSugar(SqlParameter[] parameters)
        {
            if (parameters == null)
            {
                return new SugarParameter[0];
            }
            var result = new SugarParameter[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                var p = parameters[i];
                var value = p.Value == DBNull.Value ? null : p.Value;
                result[i] = new SugarParameter(p.ParameterName, value, ToDbType(p.SqlDbType), p.Direction, p.Size);
            }
            return result;
        }
        public static SugarParameter ToSugar(System.Data.Common.DbParameter p)
        {
            if (p == null) return null;
            var sp = new SugarParameter(p.ParameterName, p.Value);
            sp.DbType = p.DbType;
            sp.Direction = p.Direction;
            if (p.Size > 0) sp.Size = p.Size;
            return sp;
        }
        public static SugarParameter[] ToSugar(System.Data.Common.DbParameter[] parameters)
        {
            if (parameters == null) return null;
            return parameters.Select(x => ToSugar(x)).ToArray();
        }

        public static void CopyBack(SqlParameter[] original, SugarParameter[] sugar)
        {
            if (original == null || sugar == null)
            {
                return;
            }
            for (int i = 0; i < original.Length && i < sugar.Length; i++)
            {
                if (original[i].Direction != ParameterDirection.Input)
                {
                    original[i].Value = sugar[i].Value ?? DBNull.Value;
                }
            }
        }

        private static DbType ToDbType(SqlDbType sqlDbType)
        {
            return sqlDbType switch
            {
                SqlDbType.BigInt => DbType.Int64,
                SqlDbType.Binary => DbType.Binary,
                SqlDbType.Bit => DbType.Boolean,
                SqlDbType.Char => DbType.StringFixedLength,
                SqlDbType.Date => DbType.Date,
                SqlDbType.DateTime => DbType.DateTime,
                SqlDbType.DateTime2 => DbType.DateTime2,
                SqlDbType.DateTimeOffset => DbType.DateTimeOffset,
                SqlDbType.Decimal => DbType.Decimal,
                SqlDbType.Float => DbType.Double,
                SqlDbType.Image => DbType.Binary,
                SqlDbType.Int => DbType.Int32,
                SqlDbType.Money => DbType.Currency,
                SqlDbType.NChar => DbType.StringFixedLength,
                SqlDbType.NText => DbType.String,
                SqlDbType.NVarChar => DbType.String,
                SqlDbType.Real => DbType.Single,
                SqlDbType.SmallDateTime => DbType.DateTime,
                SqlDbType.SmallInt => DbType.Int16,
                SqlDbType.SmallMoney => DbType.Currency,
                SqlDbType.Text => DbType.String,
                SqlDbType.Time => DbType.Time,
                SqlDbType.Timestamp => DbType.Binary,
                SqlDbType.TinyInt => DbType.Byte,
                SqlDbType.UniqueIdentifier => DbType.Guid,
                SqlDbType.VarBinary => DbType.Binary,
                SqlDbType.VarChar => DbType.String,
                SqlDbType.Xml => DbType.Xml,
                _ => DbType.Object,
            };
        }
    }

    /// <summary>
    /// Minimal stand-in for FrameworkBase ICache (Get with factory +
    /// Remove). Backed by the deployment Redis so cached reference data
    /// survives app restarts; entries expire after 60 minutes unless
    /// explicitly removed.
    /// </summary>
    public interface ICache
    {
        T Get<T>(string key, Func<T> factory);
        void Remove(string key);
    }

    public class CacheService : ICache
    {
        public T Get<T>(string key, Func<T> factory)
        {
            var cached = RedisHelper.StringGet(key);
            if (!string.IsNullOrWhiteSpace(cached))
            {
                try
                {
                    return JsonConvert.DeserializeObject<T>(cached);
                }
                catch
                {
                    // corrupt entry: fall through and rebuild
                }
            }
            var value = factory();
            if (value != null)
            {
                RedisHelper.StringSet(key, JsonConvert.SerializeObject(value), TimeSpan.FromMinutes(60));
            }
            return value;
        }

        public void Remove(string key)
        {
            try
            {
                RedisHelper.KeyDelete(key);
            }
            catch
            {
                // cache unavailable: removal is best-effort
            }
        }
    }

    /// <summary>
    /// Stand-in for FrameworkBase entity audit methods (EntityBase.Create /
    /// Modify). The ported entities are POCOs without the Framework base
    /// class, so this sets the audit properties only when the entity
    /// actually declares them; otherwise it is a no-op and the insert
    /// proceeds without audit columns.
    /// </summary>
    public static class EntityExtensions
    {
        public static void Create(this object entity) { Create(entity, true); }
        public static void Create(this object entity, bool isNew, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var kp = entity.GetType().GetProperties().FirstOrDefault(x => x.GetCustomAttribute<System.ComponentModel.DataAnnotations.KeyAttribute>() != null) ?? entity.GetType().GetProperty("Id");
                if (kp != null && kp.CanWrite)
                {
                    try { kp.SetValue(entity, System.Convert.ChangeType(keyValue, System.Nullable.GetUnderlyingType(kp.PropertyType) ?? kp.PropertyType)); } catch { }
                }
            }
            else if (isNew)
            {
                // FIX-2 B-CIS-02: theo mau Herp (EntityExtensions.Create): tao moi
                // phai sinh Id khi chua co; truoc day Create(bool) chi dat audit
                // nen INSERT vao cot Id NOT NULL bi NULL (xt_fzcd).
                GenerateKeyIfEmpty(entity);
            }
            SetAudit(entity, true);
        }
        public static void Create(this object entity, bool isNew)
        {
            // FIX-2 B-CIS-02: nhu tren — sinh Guid Id khi tao moi va khoa dang trong.
            if (isNew)
            {
                GenerateKeyIfEmpty(entity);
            }
            SetAudit(entity, true);
        }

        /// <summary>Sinh khoa chinh khi dang trong: string/Guid nhan Guid moi;
        /// khoa int (DB tu sinh) thi de nguyen — khong tu gan Id.</summary>
        private static void GenerateKeyIfEmpty(object entity)
        {
            if (entity == null) return;
            var kp = entity.GetType().GetProperties().FirstOrDefault(x => x.GetCustomAttribute<System.ComponentModel.DataAnnotations.KeyAttribute>() != null) ?? entity.GetType().GetProperty("Id");
            if (kp == null || !kp.CanWrite) return;
            var target = System.Nullable.GetUnderlyingType(kp.PropertyType) ?? kp.PropertyType;
            try
            {
                if (target == typeof(string))
                {
                    if (string.IsNullOrWhiteSpace(kp.GetValue(entity) as string))
                        kp.SetValue(entity, System.Guid.NewGuid().ToString());
                }
                else if (target == typeof(System.Guid))
                {
                    if (System.Guid.Empty.Equals(kp.GetValue(entity)))
                        kp.SetValue(entity, System.Guid.NewGuid());
                }
            }
            catch { }
        }

        public static void Modify(this object entity) { Modify(entity, (object)null); }
        public static void Modify(this object entity, object keyValue)
        {
            // FIX-2 B-CIS-03 (phan chung): Modify(keyValue) truoc day bo qua keyValue
            // nen entity tu form post (khong co Id) duoc Update voi khoa trong, trong
            // khi Update dung WhereColumns(khoa). Theo mau Herp Modify(id): gan Id
            // tu keyValue khi co, de WHERE dung dong can sua.
            if (keyValue != null && !string.IsNullOrWhiteSpace(keyValue.ToString()))
            {
                var kp = entity.GetType().GetProperties().FirstOrDefault(x => x.GetCustomAttribute<System.ComponentModel.DataAnnotations.KeyAttribute>() != null) ?? entity.GetType().GetProperty("Id");
                if (kp != null && kp.CanWrite)
                {
                    try { kp.SetValue(entity, System.Convert.ChangeType(keyValue, System.Nullable.GetUnderlyingType(kp.PropertyType) ?? kp.PropertyType)); } catch { }
                }
            }
            SetAudit(entity, false);
        }

        private static void SetAudit(object entity, bool isCreate)
        {
            if (entity == null)
            {
                return;
            }
            string userCode = null;
            try
            {
                userCode = OperatorProvider.GetCurrent()?.UserCode;
            }
            catch
            {
                userCode = null;
            }
            var type = entity.GetType();
            SetDateTime(type, entity, isCreate
                ? new[] { "CreateTime", "CreatorTime", "F_CreatorTime" }
                : new[] { "ModifyTime", "LastModifyTime", "F_LastModifyTime" });
            if (isCreate)
            {
                SetString(type, entity, new[] { "CreatorCode", "CreatorUserId", "F_CreatorUserId" }, userCode);
            }
            else
            {
                SetString(type, entity, new[] { "ModifyUserId", "LastModifyUserId", "F_LastModifyUserId" }, userCode);
            }
        }

        private static void SetDateTime(Type type, object entity, string[] names)
        {
            foreach (var name in names)
            {
                var prop = type.GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
                if (prop != null && prop.CanWrite)
                {
                    var now = DateTime.Now;
                    if (prop.PropertyType == typeof(DateTime?))
                    {
                        prop.SetValue(entity, (DateTime?)now);
                    }
                    else if (prop.PropertyType == typeof(DateTime))
                    {
                        prop.SetValue(entity, now);
                    }
                    return;
                }
            }
        }

        private static void SetString(Type type, object entity, string[] names, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }
            foreach (var name in names)
            {
                var prop = type.GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
                if (prop != null && prop.CanWrite && prop.PropertyType == typeof(string))
                {
                    prop.SetValue(entity, value);
                    return;
                }
            }
        }
    }
}

namespace Newtouch.CIS.Compat
{
    /// <summary>Stand-in for Newtouch.Tools.Json (Newtonsoft-backed).</summary>
    public static class Tools
    {
        public static class Json
        {
            public static List<T> ToList<T>(string json)
            {
                if (string.IsNullOrWhiteSpace(json)) return new List<T>();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
            }
            public static T ToObject<T>(string json)
            {
                if (string.IsNullOrWhiteSpace(json)) return default(T);
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
            }
        public static string ToJson(object obj) { return Newtonsoft.Json.JsonConvert.SerializeObject(obj); }
        }
    }
    /// <summary>Newtouch.Tools numeric conversions.</summary>
    public static class NumExt
    {
        public static double ToDouble(this int? v) { return v ?? 0; }
        public static double ToDouble(this decimal? v) { return (double)(v ?? 0); }
        public static double ToDouble(this int v) { return v; }
        public static double ToDouble(this decimal v) { return (double)v; }
        public static int ToInt(this string s)
        {
            if (int.TryParse(s, out int v)) return v;
            if (decimal.TryParse(s, out decimal d)) return (int)d;
            return 0;
        }
        public static int ToInt(this int? v) { return v ?? 0; }
        public static int ToInt(this double v) { return (int)v; }
    }
    /// <summary>FrameworkBase AppLogger stand-in (log-only).</summary>
    public sealed class AppLogger
    {
        public static readonly AppLogger Instance = new AppLogger();
        private AppLogger() { }
        public void Warn(string message) { System.Diagnostics.Trace.WriteLine("[WARN] " + message); }
        public static void Info(string message) { System.Diagnostics.Trace.WriteLine("[INFO] " + message); }
        public void Error(string message) { System.Diagnostics.Trace.WriteLine("[ERROR] " + message); }
    }
}

namespace Newtouch.CIS.Compat
{
    public static class MiscExt
    {
        public static System.DateTime ToDate(this string s)
        {
            if (System.DateTime.TryParse(s, out var d)) return d;
            return System.DateTime.MinValue;
        }
        public static System.DateTime AsDateTime(this string s)
        {
            if (System.DateTime.TryParse(s, out var d)) return d;
            return System.DateTime.MinValue;
        }
        public static bool ToBool(this bool v) { return v; }
        public static bool ToBool(this string s)
        {
            if (bool.TryParse(s, out bool v)) return v;
            return s == "1";
        }
        public static T MapperTo<T>(this object src)
        {
            if (src == null) return default(T);
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(src);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }
        public static TTarget MapperTo<TSource, TTarget>(this TSource src)
        {
            if (src == null) return default(TTarget);
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(src);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<TTarget>(json);
        }
        public static TTarget MapperTo<TTarget>(this object src, TTarget target, System.Collections.Generic.List<string> ignoreProps) { return MapperTo(src, target, ignoreProps == null ? null : ignoreProps.ToArray()); }
        public static TTarget MapperTo<TTarget>(this object src, TTarget target, params string[] ignoreProps)
        {
            if (src == null || target == null) return target;
            var ignore = new System.Collections.Generic.HashSet<string>(ignoreProps ?? new string[0], System.StringComparer.OrdinalIgnoreCase);
            var srcProps = src.GetType().GetProperties();
            var tgtProps = target.GetType().GetProperties();
            foreach (var tp in tgtProps)
            {
                if (!tp.CanWrite || ignore.Contains(tp.Name)) continue;
                var sp = srcProps.FirstOrDefault(x => string.Equals(x.Name, tp.Name, System.StringComparison.OrdinalIgnoreCase) && x.CanRead);
                if (sp == null) continue;
                try
                {
                    var v = sp.GetValue(src);
                    if (v == null) { tp.SetValue(target, null); continue; }
                    if (tp.PropertyType.IsAssignableFrom(sp.PropertyType)) tp.SetValue(target, v);
                    else tp.SetValue(target, System.Convert.ChangeType(v, System.Nullable.GetUnderlyingType(tp.PropertyType) ?? tp.PropertyType));
                }
                catch { /* best-effort property copy */ }
            }
            return target;
        }
        public static System.Data.DataTable ToDataTable<T>(this System.Collections.Generic.IList<T> list)
        {
            var dt = new System.Data.DataTable();
            var props = typeof(T).GetProperties();
            foreach (var pr in props) dt.Columns.Add(pr.Name, System.Nullable.GetUnderlyingType(pr.PropertyType) ?? pr.PropertyType);
            if (list != null)
            {
                foreach (var item in list)
                {
                    var row = dt.NewRow();
                    foreach (var pr in props) row[pr.Name] = pr.GetValue(item) ?? System.DBNull.Value;
                    dt.Rows.Add(row);
                }
            }
            return dt;
        }
        public static string ToDateString(this System.DateTime? dt) { return dt == null ? "" : dt.Value.ToString("yyyy-MM-dd"); }
        public static string ToDateTimeString(this System.DateTime? dt) { return dt == null ? "" : dt.Value.ToString("yyyy-MM-dd HH:mm:ss"); }
        public static bool IsEmpty(this System.DateTime? dt) { return !dt.HasValue; }
        public static int ToInt(this System.Enum e) { return System.Convert.ToInt32(e); }
        public static T Clone<T>(this T src)
        {
            if (src == null) return default(T);
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(src);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }
    }
}
