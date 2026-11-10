using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Newtouch.OR.ManageSystem.Infrastructure
{
    /// <summary>
    /// Replaces Newtouch.Core.Common's MapperTo&lt;TSrc, TDst&gt; (binary,
    /// Framework-only, no source): copies same-name properties whose types
    /// are assignable; incompatible members are skipped (callers set those
    /// explicitly afterwards, as the original code does for ssdm/ssmcn).
    /// </summary>
    public static class MapperExtensions
    {
        /// <summary>
        /// G23 (theo mau EmrMapper, G18): copy cung kieu source -> target
        /// da ton tai, tru cac prop bi loai — dung cho nhanh edit cua repo
        /// SubmitForm (D-T1-FIX-001). Khong copy null (xem MapperToSkipNull).
        /// </summary>
        public static void MapperTo<T>(this T source, T target, IList<string> ignoreProps) where T : class
        {
            if (source == null || target == null)
            {
                return;
            }
            var ignore = new HashSet<string>(ignoreProps ?? new List<string>(), StringComparer.OrdinalIgnoreCase);
            var targetProps = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanWrite).ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase);
            foreach (var prop in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!prop.CanRead || ignore.Contains(prop.Name))
                {
                    continue;
                }
                if (targetProps.TryGetValue(prop.Name, out var targetProp)
                    && targetProp.PropertyType.IsAssignableFrom(prop.PropertyType))
                {
                    targetProp.SetValue(target, prop.GetValue(source));
                }
            }
        }

        /// <summary>
        /// G23 (theo mau EmrMapper.MapperToSkipNull, G18): nhu MapperTo
        /// nhung BO QUA gia tri null o nguon — dung cho edit form thieu
        /// field: field khong duoc post len giu gia tri cu thay vi bi wipe
        /// ve null. Gia tri "" VAN duoc copy. Danh doi: khong clear duoc
        /// mot field ve null qua form (form khong post null bao gio).
        /// </summary>
        public static void MapperToSkipNull<T>(this T source, T target, IList<string> ignoreProps) where T : class
        {
            if (source == null || target == null)
            {
                return;
            }
            var ignore = new HashSet<string>(ignoreProps ?? new List<string>(), StringComparer.OrdinalIgnoreCase);
            var targetProps = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanWrite).ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase);
            foreach (var prop in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!prop.CanRead || ignore.Contains(prop.Name))
                {
                    continue;
                }
                var value = prop.GetValue(source);
                if (value == null)
                {
                    continue;
                }
                if (targetProps.TryGetValue(prop.Name, out var targetProp)
                    && targetProp.PropertyType.IsAssignableFrom(prop.PropertyType))
                {
                    targetProp.SetValue(target, value);
                }
            }
        }

        public static TDst MapperTo<TSrc, TDst>(this TSrc src) where TDst : new()
        {
            if (src == null)
            {
                return default(TDst);
            }
            var dst = new TDst();
            var dstProps = new Dictionary<string, PropertyInfo>(StringComparer.OrdinalIgnoreCase);
            foreach (var p in typeof(TDst).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (p.CanWrite)
                {
                    dstProps[p.Name] = p;
                }
            }
            foreach (var p in typeof(TSrc).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!p.CanRead)
                {
                    continue;
                }
                if (dstProps.TryGetValue(p.Name, out var dp) && dp.PropertyType.IsAssignableFrom(p.PropertyType))
                {
                    try
                    {
                        dp.SetValue(dst, p.GetValue(src));
                    }
                    catch
                    {
                        // best-effort copy; a single bad member must not break mapping
                    }
                }
            }
            return dst;
        }
    }
}
