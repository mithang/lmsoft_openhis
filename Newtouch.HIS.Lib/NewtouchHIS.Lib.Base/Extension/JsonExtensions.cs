/*******************************************************************************
 * Copyright © 2023 Newtouch 版权所有
 * Author: Newtouch
 * Description: 
*********************************************************************************/
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using NewtouchHIS.Lib.Base.Utilities.Json;
using System.Data;

namespace NewtouchHIS.Lib.Base.Extension
{
    /// <summary>
    /// JsonLớp hỗ trợ tuần tự hóa
    /// </summary>
    public static class Json
    {
        /// <summary>
        /// stringGiải tuần tự hóa thành object
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static object? ToJson(this string json)
        {
            return json == null ? null : JsonConvert.DeserializeObject(json);
        }

        /// <summary>
        /// tuần tự hóa
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            return obj.ToJson("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// tuần tự hóa
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="datetimeformats">Định dạng ngày</param>
        /// <returns></returns>
        public static string ToJson(this object obj, string? datetimeformats)
        {
            if (string.IsNullOrWhiteSpace(datetimeformats))
            {
                var timeConverter = new IsoDateTimeConverter { DateTimeFormat = datetimeformats };
                return JsonConvert.SerializeObject(obj, timeConverter);
            }
            else
            {
                return JsonConvert.SerializeObject(obj);
            }
        }

        /// <summary>
        /// tuần tự hóa
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="retainProps">Chỉ định trường tuần tự hóa</param>
        /// <param name="dateFormatString"></param>
        /// <returns></returns>
        public static string ToJson(this object obj, string[] retainProps, string dateFormatString = "yyyy-MM-dd HH:mm:ss")
        {
            var jsetting = new JsonSerializerSettings();
            jsetting.ContractResolver = new LimitPropsContractResolver(retainProps);
            if (!string.IsNullOrWhiteSpace(dateFormatString))
            {
                jsetting.DateFormatString = dateFormatString;
            }
            return JsonConvert.SerializeObject(obj, Formatting.Indented, jsetting);
        }

        /// <summary>
        /// Giải tuần tự hóa
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Json"></param>
        /// <returns></returns>
        public static T? ToObject<T>(this string Json)
        {
            return Json == null ? default : JsonConvert.DeserializeObject<T>(Json);
        }

        /// <summary>
        /// Giải tuần tự hóa
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static List<T>? ToList<T>(this string json)
        {
            return json == null ? null : JsonConvert.DeserializeObject<List<T>>(json);
        }

        /// <summary>
        /// tuần tự hóa
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string TableToJson(this DataTable data)
        {
            return JsonConvert.SerializeObject(data);
        }

        /// <summary>
        /// Giải tuần tự hóa
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static DataTable? ToTable(this string json)
        {
            return json == null ? null : JsonConvert.DeserializeObject<DataTable>(json);
        }

        /// <summary>
        /// tuần tự hóa
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static JObject ToJObject(this string json)
        {
            return json == null ? JObject.Parse("{}") : JObject.Parse(json.Replace("&nbsp;", ""));
        }

        /// <summary>
        /// tuần tự hóa
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static IEnumerable<JProperty> ToJProperties(this string json)
        {
            return json.ToJObject().Properties();
        }

        /// <summary>
        /// Thêm item vào json object, và trả về sau khi tuần tự hóastring
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string? Merge(object obj, object content)
        {
            try
            {
                var reqStr = obj!.ToJson();
                JObject? jobject = string.IsNullOrWhiteSpace(reqStr) ? null : (JObject)JsonConvert.DeserializeObject(reqStr);
                if (content is JObject)
                {
                    jobject!.Merge(content);
                }
                else
                {
                    jobject!.Merge(JObject.FromObject(content));
                }
                return jobject.ToJson();
            }
            catch
            {
                //Không phải định dạng json
                return obj == null ? "" : obj.ToString();
            }
        }
        /// <summary>
        /// Giải tuần tự hóa chuỗi json thành kiểu từ điển
        /// </summary>
        /// <typeparam name="TKey">từ điểnkey</typeparam>
        /// <typeparam name="TValue">từ điểnvalue</typeparam>
        /// <param name="jsonStr">jsonchuỗi</param>
        /// <returns>Dữ liệu từ điển</returns>
        public static Dictionary<TKey, TValue>? JsonStringToDictionary<TKey, TValue>(string jsonStr)
        {
            if (string.IsNullOrEmpty(jsonStr))
                return null;

            Dictionary<TKey, TValue> jsonDict = JsonConvert.DeserializeObject<Dictionary<TKey, TValue>>(jsonStr);
            return jsonDict;

        }
    }
}
