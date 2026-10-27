using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Newtouch.EMR.Infrastructure
{
    /// <summary>
    /// Replacement for the FrameworkBase MapperTo extension used by the
    /// nursing-record services: copies every readable/writable property
    /// from source to target except the named (ignored) properties.
    /// </summary>
    public static class EmrMapper
    {
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
    }
}
