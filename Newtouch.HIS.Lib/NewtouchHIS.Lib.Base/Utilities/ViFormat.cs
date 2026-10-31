using System;
using System.Globalization;

namespace NewtouchHIS.Lib.Base
{
    /// <summary>
    /// Helper định dạng hiển thị chuẩn Việt Nam (dùng chung cho mọi phân hệ OpenHIS).
    /// <para>
    /// Cách dùng:
    /// <code>
    /// using NewtouchHIS.Lib.Base;
    ///
    /// ViFormat.Date(DateTime.Now)            // "31/10/2026"
    /// ViFormat.Date(model.NgaySinh)          // DateTime? -> "" nếu null
    /// ViFormat.DateWithTime(DateTime.Now)    // "31/10/2026 14:05:09"
    /// ViFormat.Money(1234567.5m)             // "1.234.567,5 ₫"
    /// ViFormat.Money(1234567m)               // "1.234.567 ₫"
    /// ViFormat.Money(null)                   // "" (decimal?)
    /// ViFormat.MoneyRaw(1234567.5m)          // "1.234.567,5"  (không hậu tố ₫)
    /// </code>
    /// Trong Razor: <c>@ViFormat.Date(item.NgayTao)</c>.
    /// Không dùng helper này cho chuỗi đã định dạng sẵn từ DB (đã có dấu phân cách).
    /// </para>
    /// </summary>
    public static class ViFormat
    {
        /// <summary>Culture vi-VN dùng riêng cho helper (không phụ thuộc culture của thread).</summary>
        public static readonly CultureInfo Culture = CultureInfo.GetCultureInfo("vi-VN");

        /// <summary>Mẫu ngày chuẩn HIS Việt Nam.</summary>
        public const string DatePattern = "dd/MM/yyyy";

        /// <summary>Mẫu ngày + giờ chuẩn HIS Việt Nam.</summary>
        public const string DateTimePattern = "dd/MM/yyyy HH:mm:ss";

        /// <summary>Ký hiệu tiền tệ Việt Nam.</summary>
        public const string CurrencySuffix = " ₫";

        /// <summary>Ngày dạng dd/MM/yyyy. Null -> chuỗi rỗng.</summary>
        public static string Date(DateTime value)
        {
            return value.ToString(DatePattern, Culture);
        }

        /// <summary>Ngày dạng dd/MM/yyyy. Null -> chuỗi rỗng.</summary>
        public static string Date(DateTime? value)
        {
            return value.HasValue ? Date(value.Value) : string.Empty;
        }

        /// <summary>Ngày + giờ dạng dd/MM/yyyy HH:mm:ss. Null -> chuỗi rỗng.</summary>
        public static string DateWithTime(DateTime value)
        {
            return value.ToString(DateTimePattern, Culture);
        }

        /// <summary>Ngày + giờ dạng dd/MM/yyyy HH:mm:ss. Null -> chuỗi rỗng.</summary>
        public static string DateWithTime(DateTime? value)
        {
            return value.HasValue ? DateWithTime(value.Value) : string.Empty;
        }

        /// <summary>
        /// Số tiền VND: phân cách hàng nghìn bằng dấu chấm, tối đa 2 chữ số thập phân
        /// (bỏ phần thập phân nếu là .00), hậu tố " ₫". Ví dụ: 1234567 -> "1.234.567 ₫".
        /// </summary>
        public static string Money(decimal value)
        {
            return MoneyRaw(value) + CurrencySuffix;
        }

        /// <summary>Số tiền VND có hậu tố " ₫". Null -> chuỗi rỗng.</summary>
        public static string Money(decimal? value)
        {
            return value.HasValue ? Money(value.Value) : string.Empty;
        }

        /// <summary>Số tiền không hậu tố tiền tệ. Ví dụ: 1234567.5 -> "1.234.567,5".</summary>
        public static string MoneyRaw(decimal value)
        {
            return value.ToString("#,##0.##", Culture);
        }

        /// <summary>Số tiền không hậu tố tiền tệ. Null -> chuỗi rỗng.</summary>
        public static string MoneyRaw(decimal? value)
        {
            return value.HasValue ? MoneyRaw(value.Value) : string.Empty;
        }

        /// <summary>Số lượng: phân cách hàng nghìn, tối đa 3 chữ số thập phân. Ví dụ: 1500.25 -> "1.500,25".</summary>
        public static string Quantity(decimal value)
        {
            return value.ToString("#,##0.###", Culture);
        }

        /// <summary>
        /// Parse chuỗi ngày người dùng nhập theo dd/MM/yyyy (chấp nhận d/M/yyyy, dd-MM-yyyy).
        /// Trả về null nếu không hợp lệ.
        /// </summary>
        public static DateTime? ParseDate(string? text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return null;
            }

            string[] patterns = { "d/M/yyyy", "dd/MM/yyyy", "d-M-yyyy", "dd-MM-yyyy", "yyyy-MM-dd" };
            return DateTime.TryParseExact(text.Trim(), patterns, Culture,
                DateTimeStyles.None, out var parsed) ? parsed : null;
        }
    }
}
