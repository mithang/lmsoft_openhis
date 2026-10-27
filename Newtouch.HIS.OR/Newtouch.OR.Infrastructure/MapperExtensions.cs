using System;
using System.Collections.Generic;
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
