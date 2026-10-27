using System;

namespace Newtouch.EMR.Web.Helpers
{
    /// <summary>
    /// Conversions ported from the Framework-era Newtouch helper extensions
    /// (binary FrameworkBase/Newtouch.Common), which have no Core equivalent.
    /// All are total (never throw): unparsable input yields 0 / MinValue / null,
    /// matching the original helpers' forgiving behaviour.
    /// </summary>
    public static class ConvertExtensions
    {
        public static int ToInt(this string value)
        {
            return int.TryParse(value, out var result) ? result : 0;
        }

        public static DateTime ToDate(this string value)
        {
            return DateTime.TryParse(value, out var result) ? result : DateTime.MinValue;
        }

        public static DateTime ToDate(this DateTime value)
        {
            return value;
        }

        public static DateTime ToDate(this DateTime? value)
        {
            return value ?? DateTime.MinValue;
        }

        public static DateTime? AsDateTime(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }
            return DateTime.TryParse(value, out var result) ? result : null;
        }
    }
}
