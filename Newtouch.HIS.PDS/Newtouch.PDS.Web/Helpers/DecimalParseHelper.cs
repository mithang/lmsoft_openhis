using System;
using System.Globalization;

namespace Newtouch.PDS.Web.Helpers
{
    /// <summary>
    /// Two-stage decimal parsing for PDS Web (cycle 56, mirrors the Sett.Web
    /// helper). Since commit 1d15b68c set the default thread culture to vi-VN,
    /// <see cref="Convert.ToDecimal(string)"/> parses with CurrentCulture,
    /// where '.' is the group separator — so a dot-decimal price such as
    /// "1234.56" (typed into a price text input, posted verbatim) becomes
    /// 123456. Tries invariant dot-decimal WITHOUT thousands separators
    /// first ("1234.56" -&gt; 1234.56, "1234,56" does not match and falls
    /// through), then explicit vi-VN ("1234,56" -&gt; 1234.56). Anything else
    /// fails exactly as <see cref="Convert.ToDecimal(string)"/> fails today.
    /// No value is ever invented. Kept per-project so no shared-Lib
    /// redistribution is needed. Date strings are NOT handled here: the
    /// existing DateTime binder already covers form-POST dates, and the
    /// WdatePicker dd/MM/yyyy execution date parses correctly under vi-VN.
    /// </summary>
    internal static class DecimalParseHelper
    {
        private static readonly CultureInfo ViVnCulture = new CultureInfo("vi-VN");

        private const NumberStyles DotDecimalStyles =
            NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint;

        public static bool TryParseDecimal(string s, out decimal value)
        {
            value = default;
            if (string.IsNullOrWhiteSpace(s))
            {
                return false;
            }
            var text = s.Trim();
            if (decimal.TryParse(text, DotDecimalStyles, CultureInfo.InvariantCulture, out value))
            {
                return true;
            }
            return decimal.TryParse(text, NumberStyles.Number, ViVnCulture, out value);
        }

        public static decimal ParseDecimal(string s)
        {
            if (TryParseDecimal(s, out var value))
            {
                return value;
            }
            throw new FormatException($"Input string was not in a correct format: '{s}'.");
        }
    }
}
