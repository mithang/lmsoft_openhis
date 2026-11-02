using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace NewtouchHIS.WebAPI.Manage.Converters
{
    /// <summary>
    /// Newtonsoft <see cref="JsonConverter{T}"/> cho DateTime/DateTime? của WebAPI.Manage (8016, cycle 52).
    /// Vấn đề: Newtonsoft deserialise chuỗi ngày bằng InvariantCulture theo mặc
    /// định, nên "01/10/2024" trong JSON body bị đọc nhầm thành 10/01/2024
    /// (MM/dd) — lặng lẽ, không lỗi; "13/10/2024" thì fail deserialise
    /// (vòng 51 đã chứng minh trên cổng tạm với envelope Timestamp).
    /// Converter này dùng ĐÚNG danh sách format của DateTimeModelBinder bên
    /// Sett.Web (vòng 21) và của các converter Sett/CIS/EMR/PDS (vòng 44-47),
    /// theo thứ tự:
    ///   1. dd/MM/yyyy (+ giờ) — định dạng giao diện Việt Nam gửi; thử trước
    ///      nên giá trị ngày/tháng mơ hồ được hiểu theo nghĩa của giao diện.
    ///   2. ISO yyyy-MM-dd (+ giờ, + 'T') và giá trị năm/tháng.
    ///   3. US MM/dd/yyyy (+ giờ) — dạng invariant binder cũ từng chấp nhận,
    ///      giữ lại để giá trị US-form vẫn bind như trước.
    ///   4. Chỉ giờ.
    /// Chuỗi không khớp format nào thì ném <see cref="JsonSerializationException"/>
    /// (input formatter ghi model error, action nhận req null và trả envelope
    /// Fail — đúng hành vi hiện tại với body lỗi, không ngày nào được bịa ra).
    /// ĐIỂM KHÁC BIỆT DUY NHẤT so với converter vòng 44-47 — WriteJson:
    /// 8016 là host Newtonsoft DUY NHẤT pin DateFormatString =
    /// "yyyy-MM-dd HH:mm:ss.fff" trong AddNewtonsoftJson, và response đi qua
    /// AppResultFilter → ObjectResult → formatter Newtonsoft toàn cục, nên
    /// converter đăng ký trong AddNewtonsoftJson CŨNG serialize DateTime
    /// trong response. WriteJson vì vậy PHẢI tái tạo đúng format đã pin
    /// (.fff, không ISO round-trip) — đã kiểm chứng byte-identical với
    /// output của Newtonsoft khi pin format này trên mọi Kind (Unspecified/
    /// Utc/Local) và mọi phần thập phân bằng harness offline trước khi probe
    /// live. Cố tình KHÔNG dùng SerializerSettings.Culture = vi-VN toàn cục.
    /// </summary>
    public class ViDateTimeJsonConverter : JsonConverter<DateTime>
    {
        internal static readonly string[] DateFormats = new[]
        {
            "dd/MM/yyyy", "d/M/yyyy", "dd/M/yyyy", "d/MM/yyyy",
            "dd/MM/yyyy HH:mm:ss", "d/M/yyyy H:mm:ss", "dd/MM/yyyy H:mm:ss", "d/M/yyyy HH:mm:ss",
            "dd/MM/yyyy HH:mm", "d/M/yyyy H:mm",
            "yyyy-MM-dd", "yyyy-M-d", "yyyy-MM", "yyyy", "yyyyMM",
            "yyyy-MM-dd HH:mm:ss", "yyyy-M-d H:mm:ss", "yyyy-MM-dd H:mm:ss", "yyyy-M-d HH:mm:ss",
            "yyyy-MM-dd HH:mm", "yyyy-M-d H:mm",
            "yyyy-MM-ddTHH:mm:ss", "yyyy-MM-ddTHH:mm",
            "MM/dd/yyyy", "M/d/yyyy", "MM/d/yyyy", "M/dd/yyyy",
            "MM/dd/yyyy HH:mm:ss", "M/d/yyyy H:mm:ss", "MM/dd/yyyy H:mm:ss", "M/d/yyyy HH:mm:ss",
            "MM/dd/yyyy HH:mm", "M/d/yyyy H:mm",
            "HH:mm:ss", "H:mm:ss", "HH:mm", "H:mm",
        };

        /// <summary>Format ngày đã pin trong AddNewtonsoftJson của 8016; WriteJson phải tái tạo đúng format này.</summary>
        internal const string PinnedDateFormat = "yyyy-MM-dd HH:mm:ss.fff";

        public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                throw new JsonSerializationException("Cannot convert null value to System.DateTime.");
            }
            if (reader.TokenType == JsonToken.Date)
            {
                // Reader đã parse chuỗi ISO thành DateTime (vd "2024-10-01T00:00:00"):
                // giữ nguyên giá trị, đúng hành vi mặc định hôm nay.
                return (DateTime)reader.Value;
            }
            var value = reader.Value == null ? null : reader.Value.ToString();
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new JsonSerializationException("Cannot convert empty value to System.DateTime.");
            }
            value = Regex.Replace(value.Trim(), @"\s+", " ");
            if (DateTime.TryParseExact(value, DateFormats, CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out var parsed))
            {
                return parsed;
            }
            throw new JsonSerializationException($"Could not convert string to DateTime: {value}.");
        }

        public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString(PinnedDateFormat, CultureInfo.InvariantCulture));
        }
    }

    /// <summary>Phiên bản nullable của <see cref="ViDateTimeJsonConverter"/>: null/missing giữ null.</summary>
    public class ViNullableDateTimeJsonConverter : JsonConverter<DateTime?>
    {
        private static readonly ViDateTimeJsonConverter Inner = new ViDateTimeJsonConverter();

        public override DateTime? ReadJson(JsonReader reader, Type objectType, DateTime? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null || reader.TokenType == JsonToken.Undefined)
            {
                return null;
            }
            return Inner.ReadJson(reader, typeof(DateTime), default, hasExistingValue, serializer);
        }

        public override void WriteJson(JsonWriter writer, DateTime? value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                Inner.WriteJson(writer, value.Value, serializer);
            }
        }
    }
}
