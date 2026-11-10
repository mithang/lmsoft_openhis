using System;
using System.Collections.Generic;
using System.Reflection;

namespace Newtouch.MR.ManageSystem.Infrastructure
{
    /// <summary>
    /// Merge cac truong da post tu form vao entity da co, bo qua gia tri
    /// chua post (null / gia tri mac dinh cua value-type). Tien le G2/G18:
    /// form chi post mot phan field — dung MapperTo se copy null va xoa
    /// mat du lieu chua post. Trade-off (ghi ro, nhu G2): khong the xoa
    /// mot field ve null / 0 / DateTime.MinValue qua form.
    /// Khong copy: Id, OrganizeId, CreateTime, CreatorCode,
    /// LastModifyTime, LastModifierCode (khoa audit, do Create/Modify lo).
    /// </summary>
    public static class MrmsMergeHelper
    {
        private static readonly HashSet<string> SkipProps = new HashSet<string>(StringComparer.Ordinal)
        {
            "Id", "OrganizeId", "CreateTime", "CreatorCode", "LastModifyTime", "LastModifierCode"
        };

        public static void MergeSkipNull<T>(T source, T target) where T : class
        {
            if (source == null || target == null) return;
            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in props)
            {
                if (!prop.CanRead || !prop.CanWrite) continue;
                if (prop.GetIndexParameters().Length > 0) continue;
                if (SkipProps.Contains(prop.Name)) continue;
                var propType = prop.PropertyType;
                if (typeof(System.Collections.IEnumerable).IsAssignableFrom(propType) && propType != typeof(string)) continue;
                object value;
                try { value = prop.GetValue(source); } catch { continue; }
                if (value == null) continue;
                if (propType == typeof(DateTime) && (DateTime)value == DateTime.MinValue) continue;
                if (propType.IsValueType && !propType.IsEnum && Nullable.GetUnderlyingType(propType) == null)
                {
                    var defaultValue = Activator.CreateInstance(propType);
                    if (Equals(value, defaultValue)) continue;
                }
                try { prop.SetValue(target, value); } catch { /* kieu khong khop: bo qua */ }
            }
        }
    }
}
