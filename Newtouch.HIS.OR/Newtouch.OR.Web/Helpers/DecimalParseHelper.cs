using System;
using System.Globalization;

namespace Newtouch.OR.ManageSystem.Web.Helpers
{
    /// <summary>
    /// Two-stage decimal parsing for OR Web (cycle 56).
    /// Problem: since commit 1d15b68c set the default thread culture to vi-VN,
    /// <see cref="Convert.ToDecimal(string)"/> and stock form-POST model binding
    /// parse with CurrentCulture (vi-VN), where '.' is the group separator — so
    /// a dot-decimal value such as "1234.56" (what every view sends: JS
    /// toFixed(2) strings, regex-validated dot-decimal inputs, JSON numbers
    /// stringified by jQuery) becomes 123456, and "36.5" becomes 365.
    /// A naive invariant-only parse would break the other live value class:
    /// Razor-rendered quoted strings such as '@(Model.zhye)' render under
    /// CurrentCulture as "1234,56" and parse correctly under vi-VN today.
    /// This helper therefore tries, in order:
    ///   1. Invariant dot-decimal, WITHOUT thousands separators
    ///      (AllowLeadingSign | AllowDecimalPoint only): "1234.56" -&gt; 1234.56,
    ///      "1234" -&gt; 1234. "1234,56" and "1.234,56" do NOT match here, so
    ///      they fall through instead of being mis-parsed as 123456.
    ///   2. vi-VN (explicit culture, not CurrentCulture, so the result does
    ///      not move if the default culture ever changes again):
    ///      "1234,56" -&gt; 1234.56, "1.234,56" -&gt; 1234.56.
    /// Anything else fails exactly as <see cref="Convert.ToDecimal(string)"/>
    /// fails today: <see cref="ParseDecimal"/> throws FormatException,
    /// <see cref="TryParseDecimal"/> returns false. No value is ever invented.
    /// Kept per-project (mirroring the per-app DateTime binder copies) so no
    /// shared-Lib redistribution is needed.
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
