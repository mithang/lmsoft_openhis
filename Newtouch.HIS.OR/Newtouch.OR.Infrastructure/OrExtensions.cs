using System;

namespace Newtouch.OR.ManageSystem.Infrastructure
{
    /// <summary>
    /// Small string helpers the originals took from Newtouch.Core.Common
    /// (no net6.0 equivalent). ToDate parses the formatted date-time strings
    /// produced by string.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", dt).
    /// </summary>
    public static class OrExtensions
    {
        public static DateTime ToDate(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return DateTime.MinValue;
            }
            if (DateTime.TryParse(s, out var dt))
            {
                return dt;
            }
            return DateTime.MinValue;
        }
    }
}
