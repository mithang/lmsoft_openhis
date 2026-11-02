using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Newtouch.MR.ManageSystem.Infrastructure
{
    /// <summary>
    /// Normalize view-supplied date strings to ISO form before they reach SQL
    /// Server (cycle 94). Same design as the CIS Infrastructure copy
    /// (Newtouch.CIS.Compat.SqlDateStringHelper): the DateTime binder program
    /// (cycles 21-41) only fixes DateTime-typed action params, while
    /// string-typed date params bypass the binder and the raw dd/MM/yyyy view
    /// string is compared against datetime columns under the session DATEFORMAT
    /// (sa default us_english/mdy), so '15/10/2024' fails the query and
    /// '01/08/2024' silently becomes 2024-01-08. Parses with the binder's
    /// format list (dd/MM-first, minus time-only entries) under
    /// InvariantCulture and emits ISO form. Null/empty/whitespace passes
    /// through unchanged; unparseable input throws FailedException (fail
    /// loud) — no fallback date is ever invented. Kept per-project so no
    /// shared-Lib redistribution is needed.
    /// </summary>
    internal static class SqlDateStringHelper
    {
        private static readonly string[] DateFormats = new[]
        {
            "dd/MM/yyyy", "d/M/yyyy", "dd/M/yyyy", "d/MM/yyyy",
            "dd/MM/yyyy HH:mm:ss", "d/M/yyyy H:mm:ss", "dd/MM/yyyy H:mm:ss", "d/M/yyyy HH:mm:ss",
            "dd/MM/yyyy HH:mm", "d/M/yyyy H:mm",
            "dd/MM/yyyy HH:mm:ss.fff", "d/M/yyyy H:mm:ss.fff",
            "yyyy-MM-dd", "yyyy-M-d", "yyyy-MM", "yyyy", "yyyyMM",
            "yyyy-MM-dd HH:mm:ss", "yyyy-M-d H:mm:ss", "yyyy-MM-dd H:mm:ss", "yyyy-M-d HH:mm:ss",
            "yyyy-MM-dd HH:mm", "yyyy-M-d H:mm",
            "yyyy-MM-ddTHH:mm:ss", "yyyy-MM-ddTHH:mm",
            "yyyy-MM-dd HH:mm:ss.fff",
            "yyyy/MM/dd", "yyyy/M/d",
            "yyyy/MM/dd HH:mm:ss", "yyyy/M/d H:mm:ss",
            "yyyy/MM/dd HH:mm", "yyyy/M/d H:mm",
            "MM/dd/yyyy", "M/d/yyyy", "MM/d/yyyy", "M/dd/yyyy",
            "MM/dd/yyyy HH:mm:ss", "M/d/yyyy H:mm:ss", "MM/dd/yyyy H:mm:ss", "M/d/yyyy HH:mm:ss",
            "MM/dd/yyyy HH:mm", "M/d/yyyy H:mm",
        };

        public static string NormalizeDate(string value)
        {
            return Normalize(value, "yyyy-MM-dd");
        }

        public static string NormalizeDateTime(string value)
        {
            return Normalize(value, "yyyy-MM-dd HH:mm:ss");
        }

        private static string Normalize(string value, string outputFormat)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }
            var text = Regex.Replace(value.Trim(), @"\s+", " ");
            if (DateTime.TryParseExact(text, DateFormats, CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out var parsed))
            {
                return parsed.ToString(outputFormat, CultureInfo.InvariantCulture);
            }
            throw new NewtouchHIS.Lib.Base.Exceptions.FailedException(
                $"Ngày không hợp lệ: '{value}'. Vui lòng nhập theo định dạng dd/MM/yyyy.");
        }
    }
}
