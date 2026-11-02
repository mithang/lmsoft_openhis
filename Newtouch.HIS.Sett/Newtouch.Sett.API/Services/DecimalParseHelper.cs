using System;
using System.Globalization;

namespace Newtouch.Sett.API.Services
{
    /// <summary>
    /// Two-stage decimal parsing for Sett.API (cycle 61, mirroring the
    /// Sett.Web copy from cycle 56).
    /// Problem: since commit 1d15b68c set the default thread culture to vi-VN,
    /// <see cref="decimal.TryParse(string, out decimal)"/> parses with
    /// CurrentCulture (vi-VN), where '.' is the group separator — so a
    /// dot-decimal value such as "1234.56" becomes 123456. The values reaching
    /// <see cref="ParaData.Get"/> are invariant JSON-number strings
    /// (<c>t.ToString(Formatting.None)</c>), e.g. "1234.56" for a fractional
    /// order amount sent by WebAPI.Manage as a JSON number; the old
    /// culture-less TryParse mis-read every fractional amount, and the
    /// culture-independent <c>SettDb.Dec</c> cross-check then failed loudly.
    /// This helper therefore tries, in order:
    ///   1. Invariant dot-decimal, WITHOUT thousands separators
    ///      (AllowLeadingSign | AllowDecimalPoint only): "1234.56" -&gt; 1234.56,
    ///      "1234" -&gt; 1234. "1234,56" and "1.234,56" do NOT match here, so
    ///      they fall through instead of being mis-parsed as 123456.
    ///   2. vi-VN (explicit culture, not CurrentCulture, so the result does
    ///      not move if the default culture ever changes again):
    ///      "1234,56" -&gt; 1234.56, "1.234,56" -&gt; 1234.56.
    /// Anything else fails exactly as <see cref="decimal.TryParse(string, out decimal)"/>
    /// fails today: <see cref="TryParseDecimal"/> returns false,
    /// <see cref="ParseDecimal"/> throws FormatException. No value is ever invented.
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
