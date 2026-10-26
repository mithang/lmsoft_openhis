using Newtonsoft.Json;
using StackExchange.Redis;
using System.Reflection.Metadata;
using System.Runtime.Serialization.Formatters.Binary;

namespace NewtouchHIS.Lib.Redis
{
    public class RedisCoreEx : IDisposable
    {

        #region   tham số cơ bản
        //chuỗi kết nối
        //private readonly string _ConnectStr;
        //khóa
        private static readonly object _Locker = new object();
        //đã kết nốiRedisđối tượng
        internal static IConnectionMultiplexer _ConnMultiplexer;
        //cơ sở dữ liệu
        internal static IDatabase _DB;
        #endregion

        #region   phương thức công khai

        ///// <summary>
        ///// hàm khởi tạo
        ///// </summary>
        ///// <param name="connStr">Redischuỗi kết nối của</param>
        //public static RedisCoreEx(string connStr)
        //{
        //    if (!string.IsNullOrEmpty(connStr))
        //    {
        //        _ConnectStr = connStr;
        //        //lấy được đối tượng kết nối
        //        GetConnectRedisMultiplexer();
        //        //thêm sự kiện đăng ký
        //        //AddRegisterEvent();
        //    }
        //}
        /// <summary>
        /// lấy được ... kết nốiRedisđối tượng
        /// </summary>
        /// <returns></returns>
        protected IConnectionMultiplexer GetConnectRedisMultiplexer(string ConnectStr)
        {
            if (_ConnMultiplexer == null || !_ConnMultiplexer.IsConnected)
            {
                lock (_Locker)
                {
                    if (_ConnMultiplexer == null || !_ConnMultiplexer.IsConnected)
                    {
                        _ConnMultiplexer = ConnectionMultiplexer.Connect(ConnectStr);
                    }
                }
            }
            return _ConnMultiplexer;
        }
        /// <summary>
        /// chọn cơ sở dữ liệu
        /// </summary>
        /// <param name="db">số hiệu cơ sở dữ liệu（mặc định là số hiệu0cơ sở dữ liệu của）</param>
        public static void SelectDB(int db = 0)
        {
            _DB = _ConnMultiplexer.GetDatabase(db);
        }

        /// <summary>
        /// lấy được trạng thái kết nối
        /// </summary>
        public static bool ConnetStatus
        {
            get
            {
                if (_ConnMultiplexer != null)
                {
                    return _ConnMultiplexer.IsConnected;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// thêm tiền tố cho khóa
        /// </summary>
        /// <param name="prefixName">tên tiền tố</param>
        /// <param name="keyName">tên khóa</param>
        /// <returns>trả về khóa sau khi thêm tiền tố</returns>
        protected static string AddPrefixOfKey(string prefixName, string keyName)
        {
            string str = $"{prefixName}:{keyName}";
            return str;
        }

        //kiểu tuần tự hóa
        public enum SerializeType
        {
            Binary,   //nhị phân
            Json,     //Json
        }




        #region   String thao tác

        #region   phương thức đồng bộ
        /// <summary>
        /// thêm một cặp khóa-giá trị chuỗi（keynếu tồn tại thì ghi đè）
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="redisValue">giá trị</param>
        /// <param name="expiry">khoảng thời gian</param>
        /// <returns>kết quả trả về（truebiểu thị thành công）</returns>
        public static bool StringSet(string redisKey, string redisValue, TimeSpan? expiry = null)
        {
            if (ConnetStatus)
            {
                return _DB.StringSet(redisKey, redisValue, expiry);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// lấy chuỗi giá trị tương ứng với khóa
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="expiry">khoảng thời gian</param>
        /// <returns>kết quả trả về（truebiểu thị thành công）</returns>
        public static string StringGet(string redisKey, TimeSpan? expiry = null)
        {
            if (ConnetStatus)
            {
                return _DB.StringGet(redisKey);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// lưu nhiều chuỗi cặp khóa-giá trị
        /// </summary>
        /// <param name="keyValuePairs">vùng chứa cặp khóa-giá trị</param>
        /// <returns>kết quả trả về（truebiểu thị thành công）</returns>
        public static bool StringSet(IEnumerable<KeyValuePair<string, string>> keyValuePairs)
        {
            if (ConnetStatus)
            {
                var pairs = keyValuePairs.Select(x => new KeyValuePair<RedisKey, RedisValue>(x.Key, x.Value));
                return _DB.StringSet(pairs.ToArray());
            }
            else
            {
                return false;
            }
        }



        /// <summary>
        /// lưu trữ một đối tượng（đối tượng này sẽ được tuần tự hóa và lưu lại）
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="redisValue">giá trị（sẽ bị tuần tự hóa）</param>
        /// <param name="expiry">khoảng thời gian</param>
        /// <param name="serializeType">kiểu tuần tự hóa（tuần tự hóa mặc định thànhJson）</param>
        /// <returns>kết quả trả về（truebiểu thị thành công）</returns>
        public static bool SetObjString<T>(string redisKey, T redisValue, TimeSpan? expiry = null,
            SerializeType serializeType = SerializeType.Json)
        {
            if (ConnetStatus)
            {
                switch (serializeType)
                {
                    case SerializeType.Binary:
                        var binaryValue = Serialize(redisValue);
                        return _DB.StringSet(redisKey, binaryValue, expiry);

                    case SerializeType.Json:
                        var jsonValue = SerializeJson(redisValue);
                        return _DB.StringSet(redisKey, jsonValue, expiry);

                    default:
                        return false;

                }
            }
            else
            {
                return false;
            }

        }


        /// <summary>
        /// lấy một đối tượng（sẽ thực hiện giải tuần tự hóa）
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="expiry"></param>
        /// <param name="serializeType">kiểu tuần tự hóa（tuần tự hóa mặc định thànhJson）</param> 
        /// <returns>kết quả trả về（truebiểu thị thành công）</returns>
        public static T GetObjString<T>(string redisKey, TimeSpan? expiry = null,
            SerializeType serializeType = SerializeType.Json)
        {
            if (ConnetStatus)
            {
                switch (serializeType)
                {
                    case SerializeType.Binary:
                        return Deserialize<T>(_DB.StringGet(redisKey));

                    case SerializeType.Json:
                        return DeserializeJson<T>(_DB.StringGet(redisKey));

                    default:

                        return default(T);
                }
            }
            else
            {

                return default(T);
            }

        }


        public static double IncrStringGet(string redisKey)
        {
            if (ConnetStatus)
            {
                return _DB.StringIncrement(redisKey);
            }
            else
            {
                return -1;
            }
        }
        public static async Task<double> IncrStringGetAsync(string redisKey)
        {
            if (ConnetStatus)
            {
                return await _DB.StringIncrementAsync(redisKey);
            }
            else
            {
                return -1;
            }
        }
        #endregion

        #region phương thức bất đồng bộ

        /// <summary>
        /// lưu một chuỗi cặp khóa-giá trị（phương thức bất đồng bộ）
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="redisValue">giá trị</param>
        /// <param name="expiry">khoảng thời gian</param>
        /// <returns>kết quả trả về（truebiểu thị thành công）</returns>
        public static async Task<bool> SetStringOfAsync(string redisKey, string redisValue, TimeSpan? expiry = null)
        {
            if (ConnetStatus)
            {
                return await _DB.StringSetAsync(redisKey, redisValue, expiry);
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// lấy một giá trị
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="redisValue">giá trị</param>
        /// <param name="expiry">khoảng thời gian</param>
        /// <returns>kết quả trả về（truebiểu thị thành công）</returns>
        public static async Task<string> GetStringOfAsync(string redisKey)
        {
            if (ConnetStatus)
            {
                return await _DB.StringGetAsync(redisKey);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// lưu một nhóm giá trị chuỗi
        /// </summary>
        /// <param name="keyValuePairs">vùng chứa cặp khóa-giá trị</param>
        /// <returns>kết quả trả về（truebiểu thị thành công）</returns>
        public static async Task<bool> SetStringOfAsync(IEnumerable<KeyValuePair<string, string>> keyValuePairs)
        {
            if (ConnetStatus)
            {
                var pairs = keyValuePairs.Select(x => new KeyValuePair<RedisKey, RedisValue>(x.Key, x.Value));
                return await _DB.StringSetAsync(pairs.ToArray());
            }
            else
            {
                return false;
            }
        }



        /// <summary>
        /// lưu trữ một đối tượng（đối tượng này sẽ được tuần tự hóa và lưu lại）
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="redisValue">giá trị</param>
        /// <param name="expiry">khoảng thời gian</param>
        /// <param name="serializeType">kiểu tuần tự hóa（tuần tự hóa mặc định thànhJson）</param> 
        /// <returns>kết quả trả về（truebiểu thị thành công）</returns>
        public static async Task<bool> SetObjStringOfAsync<T>(string redisKey, T redisValue, TimeSpan? expiry = null,
            SerializeType serializeType = SerializeType.Json)
        {
            if (ConnetStatus)
            {
                switch (serializeType)
                {
                    case SerializeType.Binary:
                        var binaryValue = Serialize(redisValue);
                        return await _DB.StringSetAsync(redisKey, binaryValue, expiry);

                    case SerializeType.Json:
                        var jsonValue = SerializeJson(redisValue);
                        return await _DB.StringSetAsync(redisKey, jsonValue, expiry);

                    default:
                        break;
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// lấy một đối tượng（sẽ thực hiện giải tuần tự hóa）
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="expiry">khoảng thời gian</param>
        /// <param name="serializeType">kiểu tuần tự hóa（tuần tự hóa mặc định thànhJson）</param> 
        /// <returns>kết quả trả về（truebiểu thị thành công）</returns>
        public static async Task<T> GetObjStringOfAsync<T>(string redisKey, TimeSpan? expiry = null,
            SerializeType serializeType = SerializeType.Json)
        {
            if (ConnetStatus)
            {
                switch (serializeType)
                {
                    case SerializeType.Binary:
                        return Deserialize<T>(await _DB.StringGetAsync(redisKey));

                    case SerializeType.Json:
                        return DeserializeJson<T>(await _DB.StringGetAsync(redisKey));

                    default:
                        break;
                }
                return default(T);
            }
            else
            {
                return default(T);
            }

        }

        #endregion

        #endregion


        #region  Hash thao tác

        #region   phương thức đồng bộ
        /// <summary>
        /// xác định trường đó có tồn tại hay khônghash
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="hashField">trường</param>
        /// <returns>kết quả trả về（true：biểu thị thành công）</returns>
        public static bool ExistsHash(string redisKey, string hashField)
        {
            if (ConnetStatus)
            {
                return _DB.HashExists(redisKey, hashField);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// từ hash xóa trường chỉ định trong
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="hashField">trường</param>
        /// <returns>kết quả trả về（true：biểu thị thành công）</returns>
        public static bool DeleteHash(string redisKey, string hashField)
        {
            if (ConnetStatus)
            {
                return _DB.HashDelete(redisKey, hashField);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// từ hash xóa trường chỉ định trong
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="hashFields">trường</param>
        /// <returns>kết quả trả về（-1：biểu thị thất bại）</returns>
        public static long DeleteHash(string redisKey, IEnumerable<string> hashFields)
        {
            if (ConnetStatus)
            {
                var fields = hashFields.Select(x => (RedisValue)x);

                return _DB.HashDelete(redisKey, fields.ToArray());
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// trong hash đặt giá trị
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="hashField">trường</param>
        /// <param name="value">giá trị</param>
        /// <returns>kết quả trả về（true：biểu thị thành công）</returns>
        public static bool SetHash(string redisKey, string hashField, string value)
        {
            if (ConnetStatus)
            {
                return _DB.HashSet(redisKey, hashField, value);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// trong hash đặt giá trị trong
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="hashFields">vùng chứa trường</param>
        public static void SetHash(string redisKey, IEnumerable<KeyValuePair<string, string>> hashFields)
        {
            if (ConnetStatus)
            {
                var entries = hashFields.Select(x => new HashEntry(x.Key, x.Value));

                _DB.HashSet(redisKey, entries.ToArray());
            }

        }

        /// <summary>
        /// trong hash lấy giá trị trong
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="hashField">trường</param>
        /// <returns>kết quả trả về</returns>
        public static string GetHash(string redisKey, string hashField)
        {
            if (ConnetStatus)
            {
                return _DB.HashGet(redisKey, hashField);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// tronghashlấy giá trị
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="hashFields">trường</param>
        /// <returns>kết quả trả về</returns>
        public static IEnumerable<string> GetHash(string redisKey, IEnumerable<string> hashFields)
        {
            if (ConnetStatus)
            {
                var fields = hashFields.Select(x => (RedisValue)x);

                return ConvertStrings(_DB.HashGet(redisKey, fields.ToArray()));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// từ hash trả về tất cả các giá trị trường
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <returns>kết quả trả về</returns>
        public static IEnumerable<string> HashKeys(string redisKey)
        {
            if (ConnetStatus)
            {
                return ConvertStrings(_DB.HashKeys(redisKey));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// lấy theo khóahashtất cả các giá trị trong
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <returns>trả vềhashkết quả</returns>
        public static IEnumerable<string> HashValues(string redisKey)
        {
            if (ConnetStatus)
            {
                return ConvertStrings(_DB.HashValues(redisKey));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// trong hash đặt giá trị（tuần tự hóa）
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="hashField">trường</param>
        /// <param name="redisValue">giá trị</param>
        /// <param name="serializeType">kiểu tuần tự hóa（mặc định làJson）</param>
        /// <returns>kết quả trả về（true:biểu thị thành công）</returns>
        public static bool SetObjHash<T>(string redisKey, string hashField, T redisValue,
            SerializeType serializeType = SerializeType.Json)
        {
            if (ConnetStatus)
            {
                switch (serializeType)
                {
                    case SerializeType.Binary:
                        var binaryValue = Serialize(redisValue);
                        return _DB.HashSet(redisKey, hashField, binaryValue);

                    case SerializeType.Json:
                        var jsonValue = SerializeJson(redisValue);
                        return _DB.HashSet(redisKey, hashField, jsonValue);

                    default:
                        return false;

                }

            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// trong hash lấy giá trị trong（giải tuần tự hóa）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <param name="serializeType">kiểu tuần tự hóa（mặc định làJson）</param>
        /// <returns></returns>
        public static T GetObjHash<T>(string redisKey, string hashField,
            SerializeType serializeType = SerializeType.Json)
        {
            if (ConnetStatus)
            {
                switch (serializeType)
                {
                    case SerializeType.Binary:
                        return Deserialize<T>(_DB.HashGet(redisKey, hashField));

                    case SerializeType.Json:
                        return DeserializeJson<T>(_DB.HashGet(redisKey, hashField));

                    default:
                        return default(T);
                }

            }
            else
            {
                return default(T);
            }
        }

        #endregion

        #region   phương thức bất đồng bộ

        /// <summary>
        /// xác định trường đó có tồn tại hay khônghash（phương thức bất đồng bộ）
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="hashField">trường</param>
        /// <returns>kết quả trả về（true：biểu thị tồn tại）</returns>
        public static async Task<bool> ExistsHashOfAsync(string redisKey, string hashField)
        {
            if (ConnetStatus)
            {
                return await _DB.HashExistsAsync(redisKey, hashField);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// từhashxóa trường chỉ định trong
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="hashField">trường</param>
        /// <returns>kết quả trả về（true：biểu thị thành công）</returns>
        public static async Task<bool> DeleteHashOfAsync(string redisKey, string hashField)
        {
            if (ConnetStatus)
            {
                return await _DB.HashDeleteAsync(redisKey, hashField);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// từhashxóa trường chỉ định trong
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="hashFields">trường</param>
        /// <returns>trả về kết quả xóa（-1 biểu thị thất bại）</returns>
        public static async Task<long> DeleteHashOfAsync(string redisKey, IEnumerable<string> hashFields)
        {
            if (ConnetStatus)
            {
                var fields = hashFields.Select(x => (RedisValue)x);

                return await _DB.HashDeleteAsync(redisKey, fields.ToArray());
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// trong hash đặt giá trị
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="hashField">trường</param>
        /// <param name="value">giá trị</param>
        /// <returns>kết quả trả về（true:biểu thị thành công）</returns>
        public static async Task<bool> SetHashOfAsync(string redisKey, string hashField, string value)
        {
            if (ConnetStatus)
            {
                return await _DB.HashSetAsync(redisKey, hashField, value);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// trong hash đặt giá trị trong
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashFields"></param>
        public static async Task SetHashOfAsync(string redisKey, IEnumerable<KeyValuePair<string, string>> hashFields)
        {
            if (ConnetStatus)
            {
                var entries = hashFields.Select(x => new HashEntry(x.Key, x.Value));
                await _DB.HashSetAsync(redisKey, entries.ToArray());
            }
            else
            {
                return;
            }

        }

        /// <summary>
        /// trong hash lấy giá trị trong
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="hashField">trường</param>
        /// <returns>kết quả trả về</returns>
        public static async Task<string> GetHashOfAsync(string redisKey, string hashField)
        {
            if (ConnetStatus)
            {
                return await _DB.HashGetAsync(redisKey, hashField);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// trong hash lấy giá trị trong
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="hashFields">trường</param>
        /// <param name="value">giá trị</param>
        /// <returns>kết quả trả về</returns>
        public static async Task<IEnumerable<string>> GetHashOfAsync(string redisKey, IEnumerable<string> hashFields,
            string value)
        {
            if (ConnetStatus)
            {
                var fields = hashFields.Select(x => (RedisValue)x);

                return ConvertStrings(await _DB.HashGetAsync(redisKey, fields.ToArray()));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// từ hash trả về tất cả các giá trị trường
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <returns>trả về ... tương ứnghashgiá trị trường</returns>
        public static async Task<IEnumerable<string>> HashKeysOfAsync(string redisKey)
        {
            if (ConnetStatus)
            {
                return ConvertStrings(await _DB.HashKeysAsync(redisKey));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// trả về hash tất cả các giá trị trong
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <returns>trả vềhashtất cả các giá trị trong</returns>
        public static async Task<IEnumerable<string>> HashValuesAsync(string redisKey)
        {
            if (ConnetStatus)
            {
                return ConvertStrings(await _DB.HashValuesAsync(redisKey));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// trong hash đặt giá trị（tuần tự hóa）
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="hashField">trường</param>
        /// <param name="value">giá trị</param>
        /// <param name="serializeType">kiểu tuần tự hóa（mặc định làJson）</param>
        /// <returns>kết quả trả về（true:biểu thị thành công）</returns>
        public static async Task<bool> SetObjHashAsync<T>(string redisKey, string hashField, T value,
            SerializeType serializeType = SerializeType.Json)
        {
            if (ConnetStatus)
            {
                switch (serializeType)
                {
                    case SerializeType.Binary:
                        var binaryValue = Serialize(value);
                        return await _DB.HashSetAsync(redisKey, hashField, binaryValue);

                    case SerializeType.Json:
                        var jsonValue = SerializeJson(value);
                        return await _DB.HashSetAsync(redisKey, hashField, jsonValue);
                    default:
                        return false;
                }

            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// trong hash lấy giá trị trong（giải tuần tự hóa）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <param name="serializeType">kiểu tuần tự hóa（mặc định làJson）</param>
        /// <returns></returns>
        public static async Task<T> GetObjHashAsync<T>(string redisKey, string hashField,
            SerializeType serializeType = SerializeType.Json)
        {
            if (ConnetStatus)
            {
                switch (serializeType)
                {
                    case SerializeType.Binary:
                        return Deserialize<T>(await _DB.HashGetAsync(redisKey, hashField));
                    case SerializeType.Json:
                        return DeserializeJson<T>(await _DB.HashGetAsync(redisKey, hashField));
                    default:
                        return default(T);
                }

            }
            else
            {
                return default(T);
            }

        }

        #endregion

        #endregion


        #region   List thao tác

        #region   phương thức đồng bộ

        /// <summary>
        /// xóa và trả về phần tử đầu tiên được lưu trong danh sách của khóa đó
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <returns></returns>
        public static string PopFirtKeyOfList(string redisKey)
        {
            if (ConnetStatus)
            {
                return _DB.ListLeftPop(redisKey);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// xóa và trả về phần tử cuối cùng được lưu trong danh sách của khóa đó
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <returns></returns>
        public static string PopLastKeyOfList(string redisKey)
        {
            if (ConnetStatus)
            {
                return _DB.ListRightPop(redisKey);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// xóa phần tử trên khóa chỉ định của danh sách có giá trị giống giá trị đó
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="redisValue">giá trị</param>
        /// <returns>kết quả trả về（-1 biểu thị thất bại）</returns>
        public static long PopSameKeyOfList(string redisKey, string redisValue)
        {
            if (ConnetStatus)
            {
                return _DB.ListRemove(redisKey, redisValue);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// chèn giá trị vào cuối danh sách。nếu khóa không tồn tại，tạo trước rồi mới chèn giá trị
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="redisValue">giá trị</param>
        /// <returns>kết quả trả về（-1 biểu thị thất bại）</returns>
        public static long PushKeyOfListLast(string redisKey, string redisValue)
        {
            if (ConnetStatus)
            {
                return _DB.ListRightPush(redisKey, redisValue);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// chèn giá trị vào đầu danh sách。nếu khóa không tồn tại，tạo trước rồi mới chèn giá trị
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="redisValue">giá trị</param>
        /// <returns>kết quả trả về（-1 biểu thị thất bại）</returns>
        public static long PushKeyOfListFirst(string redisKey, string redisValue)
        {
            if (ConnetStatus)
            {
                return _DB.ListLeftPush(redisKey, redisValue);
            }
            else
            {
                return -1;
            }

        }

        /// <summary>
        /// trả về độ dài của khóa đó trong danh sách，nếu không tồn tại，trả về 0
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <returns>kết quả trả về（-1 biểu thị thất bại）</returns>
        public static long GetKeyLengthOfList(string redisKey)
        {
            if (ConnetStatus)
            {
                return _DB.ListLength(redisKey);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// trả về phần tử tương ứng với khóa trên danh sách đó
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="start">điểm bắt đầu</param>
        /// <param name="stop">điểm dừng</param>
        /// <returns></returns>
        public static IEnumerable<string> ListRange(string redisKey, long start = 0L, long stop = -1L)
        {
            if (ConnetStatus)
            {
                return ConvertStrings(_DB.ListRange(redisKey, start, stop));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// xóa và trả về đối tượng phần tử đầu tiên được lưu trong danh sách của khóa đó
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="serializeType">kiểu tuần tự hóa（mặc định làJson）</param>
        /// <returns>trả về đối tượng sau khi giải tuần tự hóa</returns>
        public static T PopFirstKeyObjOfList<T>(string redisKey, SerializeType serializeType = SerializeType.Json)
        {
            if (ConnetStatus)
            {
                switch (serializeType)
                {
                    case SerializeType.Binary:
                        return Deserialize<T>(_DB.ListLeftPop(redisKey));

                    case SerializeType.Json:
                        return DeserializeJson<T>(_DB.ListLeftPop(redisKey));
                    default:
                        return default(T);
                }

            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// xóa và trả về đối tượng phần tử cuối cùng được lưu trong danh sách của khóa đó
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="serializeType">kiểu tuần tự hóa（mặc định làJson）</param>
        /// <returns>trả về đối tượng sau khi giải tuần tự hóa</returns>
        public static T PopLastKeyObjOfList<T>(string redisKey, SerializeType serializeType = SerializeType.Json)
        {
            if (ConnetStatus)
            {
                switch (serializeType)
                {
                    case SerializeType.Binary:
                        return Deserialize<T>(_DB.ListRightPop(redisKey));
                    case SerializeType.Json:
                        return DeserializeJson<T>(_DB.ListRightPop(redisKey));
                    default:
                        return default(T);
                }
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// chèn giá trị vào cuối danh sách。nếu khóa không tồn tại，tạo trước rồi mới chèn giá trị
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns>kết quả trả về（-1 biểu thị thất bại）</returns>
        public static long PushKeyOfListLast<T>(string redisKey, T redisValue)
        {
            if (ConnetStatus)
            {
                return _DB.ListRightPush(redisKey, Serialize(redisValue));
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// chèn giá trị vào đầu danh sách。nếu khóa không tồn tại，tạo trước rồi mới chèn giá trị
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns>kết quả trả về（-1 biểu thị thất bại）</returns>
        public static long PushKeyOfListHead<T>(string redisKey, T redisValue)
        {
            if (ConnetStatus)
            {
                return _DB.ListLeftPush(redisKey, Serialize(redisValue));
            }
            else
            {
                return -1;
            }
        }

        #endregion

        #region    phương thức bất đồng bộ

        /// <summary>
        /// xóa và trả về phần tử đầu tiên được lưu trong danh sách của khóa đó
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public static async Task<string> PopFirtKeyOfListAsync(string redisKey)
        {
            if (ConnetStatus)
            {
                return await _DB.ListLeftPopAsync(redisKey);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// xóa và trả về phần tử cuối cùng được lưu trong danh sách của khóa đó
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <returns></returns>
        public static async Task<string> PopLastKeyOfListAsync(string redisKey)
        {
            if (ConnetStatus)
            {
                return await _DB.ListRightPopAsync(redisKey);
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// xóa phần tử trên khóa chỉ định của danh sách có giá trị giống giá trị đó
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="redisValue">giá trị</param>
        /// <returns>kết quả trả về（-1 biểu thị thất bại）</returns>
        public static async Task<long> PopSameKeyOfListAsync(string redisKey, string redisValue)
        {
            if (ConnetStatus)
            {
                return await _DB.ListRemoveAsync(redisKey, redisValue);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// chèn giá trị vào cuối danh sách。nếu khóa không tồn tại，tạo trước rồi mới chèn giá trị
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="redisValue">giá trị</param>
        /// <returns>kết quả trả về（-1 biểu thị thất bại）</returns>
        public static async Task<long> PushKeyOfListLastAsync(string redisKey, string redisValue)
        {
            if (ConnetStatus)
            {
                return await _DB.ListRightPushAsync(redisKey, redisValue);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// chèn giá trị vào đầu danh sách。nếu khóa không tồn tại，tạo trước rồi mới chèn giá trị
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="redisValue">giá trị</param>
        /// <returns>kết quả trả về（-1 biểu thị thất bại）</returns>
        public static async Task<long> PushKeyOfListHeadAsync(string redisKey, string redisValue)
        {
            if (ConnetStatus)
            {
                return await _DB.ListLeftPushAsync(redisKey, redisValue);
            }
            else
            {
                return -1;
            }

        }

        /// <summary>
        /// trả về độ dài của khóa đó trong danh sách，nếu không tồn tại，trả về 0
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <returns>kết quả trả về（-1 biểu thị thất bại）</returns>
        public static async Task<long> GetKeyLengthOfListAsync(string redisKey)
        {
            if (ConnetStatus)
            {
                return await _DB.ListLengthAsync(redisKey);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// trả về phần tử tương ứng với khóa trên danh sách đó
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<string>> ListRangeAsync(string redisKey, long start = 0L, long stop = -1L)
        {
            if (ConnetStatus)
            {
                var query = await _DB.ListRangeAsync(redisKey, start, stop);
                return query.Select(x => x.ToString());
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// xóa và trả về đối tượng phần tử đầu tiên được lưu trong danh sách của khóa đó
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="serializeType">kiểu tuần tự hóa（mặc định làJson）</param>
        /// <returns>trả về đối tượng sau khi giải tuần tự hóa</returns>
        public static async Task<T> PopFirstKeyObjOfListAsync<T>(string redisKey, SerializeType serializeType = SerializeType.Json)
        {
            if (ConnetStatus)
            {
                switch (serializeType)
                {
                    case SerializeType.Binary:
                        return Deserialize<T>(await _DB.ListLeftPopAsync(redisKey));
                    case SerializeType.Json:
                        return DeserializeJson<T>(await _DB.ListLeftPopAsync(redisKey));
                    default:
                        return default(T);
                }

            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// xóa và trả về đối tượng phần tử cuối cùng được lưu trong danh sách của khóa đó
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="serializeType">kiểu tuần tự hóa（mặc định làJson）</param>
        /// <returns>trả về đối tượng sau khi giải tuần tự hóa</returns>
        public static async Task<T> PopLastKeyObjOfListAsync<T>(string redisKey, SerializeType serializeType = SerializeType.Json)
        {
            if (ConnetStatus)
            {
                switch (serializeType)
                {
                    case SerializeType.Binary:
                        return Deserialize<T>(await _DB.ListRightPopAsync(redisKey));
                    case SerializeType.Json:
                        return DeserializeJson<T>(await _DB.ListRightPopAsync(redisKey));
                    default:
                        return default(T);
                }

            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// chèn giá trị vào cuối danh sách。nếu khóa không tồn tại，tạo trước rồi mới chèn giá trị
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="redisValue">giá trị</param>
        /// <returns>kết quả trả về（-1 biểu thị thất bại）</returns>
        public static async Task<long> PushKeyOfListLastAsync<T>(string redisKey, T redisValue)
        {
            if (ConnetStatus)
            {
                return await _DB.ListRightPushAsync(redisKey, Serialize(redisValue));
            }
            else
            {
                return -1;
            }

        }

        /// <summary>
        /// chèn giá trị vào đầu danh sách。nếu khóa không tồn tại，tạo trước rồi mới chèn giá trị
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="redisValue">giá trị</param>
        /// <returns>kết quả trả về（-1 biểu thị thất bại）</returns>
        public static async Task<long> PushKeyOfListHeadAsync<T>(string redisKey, T redisValue)
        {
            if (ConnetStatus)
            {
                return await _DB.ListLeftPushAsync(redisKey, Serialize(redisValue));
            }
            else
            {
                return -1;
            }
        }

        #endregion


        #endregion


        #region SortedSet thao tác

        #region   phương thức đồng bộ
        /// <summary>
        /// SortedSet thêm mới
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public static bool SortedSetAdd(string redisKey, string member, double score)
        {
            if (ConnetStatus)
            {
                return _DB.SortedSetAdd(redisKey, member, score);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// trả về các phần tử trong phạm vi chỉ định của tập hợp có thứ tự，trong trường hợp mặc định từ thấp đến cao
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static IEnumerable<string> SortedSetRangeByRank(string redisKey, long start = 0L, long stop = -1L,
            OrderType order = OrderType.Asc)
        {
            if (ConnetStatus)
            {
                return _DB.SortedSetRangeByRank(redisKey, start, stop, (Order)order).Select(x => x.ToString());
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// trả về số phần tử của tập hợp có thứ tự
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns>kết quả trả về（-1 biểu thị thất bại）</returns>
        public static long SortedSetLength(string redisKey)
        {
            if (ConnetStatus)
            {
                return _DB.SortedSetLength(redisKey);
            }
            else
            {
                return -1;
            }

        }

        /// <summary>
        /// trả về số phần tử của tập hợp có thứ tự
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="memebr"></param>
        /// <returns>kết quả trả về（true:biểu thị thành công）</returns>
        public static bool SortedSetLength(string redisKey, string memebr)
        {
            if (ConnetStatus)
            {
                return _DB.SortedSetRemove(redisKey, memebr);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// SortedSet thêm mới
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns>kết quả trả về（true:biểu thị thành công）</returns>
        public static bool SortedSetAdd<T>(string redisKey, T member, double score)
        {
            if (ConnetStatus)
            {
                var json = Serialize(member);
                return _DB.SortedSetAdd(redisKey, json, score);
            }
            else
            {
                return false;
            }


        }

        /// <summary>
        /// các thành viên của tập hợp sắp xếp theo điểm tăng dần
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="value"></param>
        /// <returns>kết quả trả về（-1 biểu thị thất bại）</returns>
        public static double SortedSetIncrement(string redisKey, string member, double value = 1)
        {
            if (ConnetStatus)
            {
                return _DB.SortedSetIncrement(redisKey, member, value);
            }
            else
            {
                return -1;
            }

        }

        #endregion

        #region  phương thức bất đồng bộ

        /// <summary>
        /// SortedSet thêm mới
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns>kết quả trả về（true:biểu thị thành công）</returns>
        public static async Task<bool> SortedSetAddAsync(string redisKey, string member, double score)
        {
            if (ConnetStatus)
            {
                return await _DB.SortedSetAddAsync(redisKey, member, score);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// trả về các phần tử trong phạm vi chỉ định của tập hợp có thứ tự，trong trường hợp mặc định từ thấp đến cao。
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<string>> SortedSetRangeByRankAsync(string redisKey)
        {
            if (ConnetStatus)
            {
                return ConvertStrings(await _DB.SortedSetRangeByRankAsync(redisKey));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// trả về số phần tử của tập hợp có thứ tự
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns>kết quả trả về（-1 biểu thị thất bại）</returns>
        public static async Task<long> SortedSetLengthAsync(string redisKey)
        {
            if (ConnetStatus)
            {
                return await _DB.SortedSetLengthAsync(redisKey);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// trả về số phần tử của tập hợp có thứ tự
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="memebr"></param>
        /// <returns>kết quả trả về（true:biểu thị thành công）</returns>
        public static async Task<bool> SortedSetRemoveAsync(string redisKey, string memebr)
        {
            if (ConnetStatus)
            {
                return await _DB.SortedSetRemoveAsync(redisKey, memebr);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// SortedSet thêm mới
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public static async Task<bool> SortedSetAddAsync<T>(string redisKey, T member, double score)
        {
            if (ConnetStatus)
            {
                var json = Serialize(member);
                return await _DB.SortedSetAddAsync(redisKey, json, score);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// các thành viên của tập hợp sắp xếp theo điểm tăng dần
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="value"></param>
        /// <returns>kết quả trả về（-1 biểu thị thất bại）</returns>
        public static async Task<double> SortedSetIncrementAsync(string redisKey, string member, double value = 1)
        {
            if (ConnetStatus)
            {
                return await _DB.SortedSetIncrementAsync(redisKey, member, value);
            }
            else
            {
                return -1;
            }
        }

        #endregion

        /// <summary>
        /// Redis kiểu sắp xếp
        /// </summary>
        public enum OrderType
        {
            Asc,
            Desc
        }

        #endregion


        #region   Key thao tác

        #region   phương thức đồng bộ

        /// <summary>
        /// xóa chỉ địnhKey
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <returns>kết quả trả về（true：biểu thị thành công）</returns>
        public static bool DeleteKey(string redisKey)
        {
            if (ConnetStatus)
            {
                return _DB.KeyDelete(redisKey);
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// xóa chỉ địnhKeydanh sách
        /// </summary>
        /// <param name="redisKeys">vùng chứa khóa</param>
        /// <returns>số lượng khóa chỉ định bị xóa(-1:biểu thị thất bại)</returns>
        public static long DeleteKey(IEnumerable<string> redisKeys)
        {
            if (ConnetStatus)
            {
                var keys = redisKeys.Select(x => (RedisKey)x);
                return _DB.KeyDelete(keys.ToArray());
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// kiểm traKeycó tồn tại hay không
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <returns>kết quả trả về（true：biểu thị thành công）</returns>
        public static bool ExistsKey(string redisKey)
        {
            if (ConnetStatus)
            {
                return _DB.KeyExists(redisKey);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// đổi tênKey
        /// </summary>
        /// <param name="redisKey">khóa ban đầu</param>
        /// <param name="redisNewKey">tên khóa mới</param>
        /// <returns>kết quả trả về（true：biểu thị thành công）</returns>
        public static bool RenameKey(string redisKey, string redisNewKey)
        {
            if (ConnetStatus)
            {
                return _DB.KeyRename(redisKey, redisNewKey);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// thiết lập Key thời gian của
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="expiry">khoảng thời gian</param>
        /// <returns>kết quả trả về（true：biểu thị thành công）</returns>
        public static bool SetKeyExpire(string redisKey, TimeSpan? expiry)
        {
            if (ConnetStatus)
            {
                return _DB.KeyExpire(redisKey, expiry);
            }
            else
            {
                return false;
            }

        }

        #endregion

        #region   phương thức bất đồng bộ

        /// <summary>
        /// xóa chỉ định Key
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <returns>kết quả trả về（true：biểu thị thành công）</returns>
        public static async Task<bool> DeleteKeyAsync(string redisKey)
        {
            if (ConnetStatus)
            {
                return await _DB.KeyDeleteAsync(redisKey);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// xóa chỉ định Key danh sách
        /// </summary>
        /// <param name="redisKeys">khóa</param>
        /// <returns>kết quả trả về（-1 biểu thị thất bại）</returns>
        public static async Task<long> DeleteKeyAsync(IEnumerable<string> redisKeys)
        {
            if (ConnetStatus)
            {
                var keys = redisKeys.Select(x => (RedisKey)x);
                return await _DB.KeyDeleteAsync(keys.ToArray());
            }
            else
            {
                return -1;
            }

        }

        /// <summary>
        /// kiểm tra Key có tồn tại hay không
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <returns>kết quả trả về（true：biểu thị thành công）</returns>
        public static async Task<bool> ExistsKeyAsync(string redisKey)
        {
            if (ConnetStatus)
            {
                return await _DB.KeyExistsAsync(redisKey);
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// đổi tên Key
        /// </summary>
        /// <param name="redisKey">tên khóa cũ</param>
        /// <param name="redisNewKey">tên khóa mới</param>
        /// <returns>kết quả trả về（true：biểu thị thành công）</returns>
        public static async Task<bool> RenameKeyAsync(string redisKey, string redisNewKey)
        {
            if (ConnetStatus)
            {
                return await _DB.KeyRenameAsync(redisKey, redisNewKey);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// thiết lập Key thời gian của
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="expiry">khoảng thời gian</param>
        /// <returns>kết quả trả về（true：biểu thị thành công）</returns>
        public static async Task<bool> SetKeyExpireAsync(string redisKey, TimeSpan? expiry)
        {
            if (ConnetStatus)
            {
                return await _DB.KeyExpireAsync(redisKey, expiry);
            }
            else
            {
                return false;
            }

        }

        #endregion

        #endregion


        #region phát hành - đăng ký

        #region   phương thức đồng bộ
        ///// <summary>
        ///// đăng ký
        ///// </summary>
        ///// <param name="channel"></param>
        ///// <param name="handle"></param>
        //public static void Subscribe(RedisChannel channel, Action<RedisChannel, RedisValue> handle)
        //{
        //    var sub = _ConnMultiplexer.GetSubscriber();
        //    sub.Subscribe(channel, handle);
        //}

        ///// <summary>
        ///// phát hành
        ///// </summary>
        ///// <param name="channel"></param>
        ///// <param name="message"></param>
        ///// <returns></returns>
        //public static long Publish(RedisChannel channel, RedisValue message)
        //{
        //    var sub = _ConnMultiplexer.GetSubscriber();
        //    return sub.Publish(channel, message);
        //}

        ///// <summary>
        ///// phát hành（dùng tuần tự hóa）
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="channel"></param>
        ///// <param name="message"></param>
        ///// <returns></returns>
        //public static long Publish<T>(RedisChannel channel, T message)
        //{
        //    var sub = _ConnMultiplexer.GetSubscriber();
        //    return sub.Publish(channel, Serialize(message));
        //}

        #endregion

        #region phương thức bất đồng bộ

        ///// <summary>
        ///// đăng ký
        ///// </summary>
        ///// <param name="channel"></param>
        ///// <param name="handle"></param>
        //public static async Task SubscribeAsync(RedisChannel channel, Action<RedisChannel, RedisValue> handle)
        //{
        //    var sub = _ConnMultiplexer.GetSubscriber();
        //    await sub.SubscribeAsync(channel, handle);
        //}

        ///// <summary>
        ///// phát hành
        ///// </summary>
        ///// <param name="channel"></param>
        ///// <param name="message"></param>
        ///// <returns></returns>
        //public static async Task<long> PublishAsync(RedisChannel channel, RedisValue message)
        //{
        //    var sub = _ConnMultiplexer.GetSubscriber();
        //    return await sub.PublishAsync(channel, message);
        //}

        ///// <summary>
        ///// phát hành（dùng tuần tự hóa）
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="channel"></param>
        ///// <param name="message"></param>
        ///// <returns></returns>
        //public static async Task<long> PublishAsync<T>(RedisChannel channel, T message)
        //{
        //    var sub = _ConnMultiplexer.GetSubscriber();
        //    return await sub.PublishAsync(channel, Serialize(message));
        //}

        #endregion

        #endregion


        #endregion


        #region   phương thức riêng tư
        


        #region sự kiện đăng ký

        /// <summary>
        /// thêm sự kiện đăng ký
        /// </summary>
        private static void AddRegisterEvent()
        {
            _ConnMultiplexer.ConnectionRestored += ConnMultiplexer_ConnectionRestored;
            _ConnMultiplexer.ConnectionFailed += ConnMultiplexer_ConnectionFailed;
            _ConnMultiplexer.ErrorMessage += ConnMultiplexer_ErrorMessage;
            _ConnMultiplexer.ConfigurationChanged += ConnMultiplexer_ConfigurationChanged;
            _ConnMultiplexer.HashSlotMoved += ConnMultiplexer_HashSlotMoved;
            _ConnMultiplexer.InternalError += ConnMultiplexer_InternalError;
            _ConnMultiplexer.ConfigurationChangedBroadcast += ConnMultiplexer_ConfigurationChangedBroadcast;
        }

        /// <summary>
        /// khi cấu hình lại broadcast（thường có nghĩa là thay đổi đồng bộ chính - phụ）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_ConfigurationChangedBroadcast(object sender, EndPointEventArgs e)
        {
            Console.WriteLine($"{nameof(ConnMultiplexer_ConfigurationChangedBroadcast)}: {e.EndPoint}");
        }

        /// <summary>
        /// khi xảy ra lỗi nội bộ（chủ yếu dùng để gỡ lỗi）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_InternalError(object sender, InternalErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(ConnMultiplexer_InternalError)}: {e.Exception}");
        }

        /// <summary>
        /// khi thay đổi cụm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_HashSlotMoved(object sender, HashSlotMovedEventArgs e)
        {
            Console.WriteLine(
                $"{nameof(ConnMultiplexer_HashSlotMoved)}: {nameof(e.OldEndPoint)}-{e.OldEndPoint} To {nameof(e.NewEndPoint)}-{e.NewEndPoint}, ");
        }

        /// <summary>
        /// khi cấu hình thay đổi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_ConfigurationChanged(object sender, EndPointEventArgs e)
        {
            Console.WriteLine($"{nameof(ConnMultiplexer_ConfigurationChanged)}: {e.EndPoint}");
        }

        /// <summary>
        /// khi xảy ra lỗi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_ErrorMessage(object sender, RedisErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(ConnMultiplexer_ErrorMessage)}: {e.Message}");
        }

        /// <summary>
        /// khi kết nối vật lý thất bại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_ConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            Console.WriteLine($"{nameof(ConnMultiplexer_ConnectionFailed)}: {e.Exception}");
        }

        /// <summary>
        /// khi thiết lập kết nối vật lý
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_ConnectionRestored(object sender, ConnectionFailedEventArgs e)
        {
            Console.WriteLine($"{nameof(ConnMultiplexer_ConnectionRestored)}: {e.Exception}");
        }

        #endregion sự kiện đăng ký


        /// <summary>
        /// chuyển đổi thành chuỗi
        /// </summary>
        /// <typeparam name="T">kiểu dữ liệu</typeparam>
        /// <param name="list">danh sách dữ liệu</param>
        /// <returns>trả về danh sách dữ liệu sau khi chuyển thành chuỗi</returns>
        private static IEnumerable<string> ConvertStrings<T>(IEnumerable<T> list) where T : struct
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            return list.Select(x => x.ToString());
        }

        /// <summary>
        /// tuần tự hóa（nhị phân）
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static byte[] Serialize(object obj)
        {
            if (obj == null)
                return null;

            var binaryFormatter = new BinaryFormatter();
            using (var memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, obj);
                var data = memoryStream.ToArray();
                return data;
            }
        }


        /// <summary>
        /// giải tuần tự hóa（nhị phân）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        private static T Deserialize<T>(byte[] data)
        {
            if (data == null)
                return default(T);

            var binaryFormatter = new BinaryFormatter();
            using (var memoryStream = new MemoryStream(data))
            {
                var result = (T)binaryFormatter.Deserialize(memoryStream);
                return result;
            }
        }


        /// <summary>
        /// tuần tự hóa đối tượng thànhJsonchuỗi
        /// </summary>
        /// <typeparam name="T">kiểu thực thể</typeparam>
        /// <param name="obj">đối tượng thực thể</param>
        /// <returns>trả về chuỗi đã tuần tự hóa</returns>
        private static string SerializeJson<T>(T obj)
        {
            string tmp = null;
            if (obj != null)
            {
                tmp = JsonConvert.SerializeObject(obj);
            }
            return tmp;
        }

        /// <summary>
        /// giải tuần tự hóaJsonchuỗi là đối tượng
        /// </summary>
        /// <typeparam name="T">kiểu thực thể</typeparam>
        /// <param name="strjson">chuỗi</param>
        /// <returns></returns>
        private static T DeserializeJson<T>(string strjson)
        {
            T tmp = default(T);
            if (!string.IsNullOrEmpty(strjson))
            {
                tmp = JsonConvert.DeserializeObject<T>(strjson);
            }

            return tmp;
        }

        public void Dispose()
        {
            _ConnMultiplexer.Dispose();
        }


        #endregion

    }//Class_end

}

#region 

/***
*	Title："thu thập dữ liệu" dự án
*		chủ đề：Redislớp trợ giúp
*	Description：
*		chức năng：
*		    1、thiết lập chuỗi kết nối、chọn cơ sở dữ liệu、lấy được trạng thái kết nối
*		    2、thêm tiền tố cho khóa
*		    3、Stringthao tác
*		     【đồng bộ（①lưu một cặp khóa-giá trị）（②lấy giá trị tương ứng theo khóa）（③đồng thời lưu nhiều cặp khóa-giá trị）（④lưu trữ một đối tượng）（⑤lấy một đối tượng）】
*		     
*		     【bất đồng bộ（①lưu một cặp khóa-giá trị）（②lấy giá trị tương ứng theo khóa）（③đồng thời lưu nhiều cặp khóa-giá trị）（④lưu trữ một đối tượng）（⑤lấy một đối tượng）】
*		    4、Hashthao tác
*		     【đồng bộ（①xác định một cặp khóa-giá trị có tồn tại hay khôngHash）（②từHashxóa một cặp khóa-giá trị trong）（③từHashđồng thời xóa danh sách nhiều trường tương ứng với một khóa trong）（④trongHashchỉ định giá trị trong）
*		     （⑤trongHashđồng thời thêm danh sách nhiều trường do một khóa chỉ định trong）（⑥trongHashlấy một khóa trong、giá trị tương ứng với trường）（⑦trongHashđồng thời lấy trường chỉ định tương ứng với một khóa trong）
*		     （⑧trongHashlấy tất cả các trường tương ứng với một khóa trong）（⑨trongHashlấy tất cả các giá trị tương ứng với một khóa trong）（⑩trongHashthêm một giá trị đối tượng cho một trường của một khóa trong）（⑩①trongHashlấy giá trị đối tượng tương ứng với một trường của một khóa trong）】
*		     
*		     【bất đồng bộ（①xác định một cặp khóa-giá trị có tồn tại hay khôngHash）（②từHashxóa một cặp khóa-giá trị trong）（③từHashđồng thời xóa danh sách nhiều trường tương ứng với một khóa trong）（④trongHashchỉ định giá trị trong）
*		     （⑤trongHashđồng thời thêm danh sách nhiều trường do một khóa chỉ định trong）（⑥trongHashlấy một khóa trong、giá trị tương ứng với trường）（⑦trongHashđồng thời lấy trường chỉ định tương ứng với một khóa trong）
*		     （⑧trongHashlấy tất cả các trường tương ứng với một khóa trong）（⑨trongHashlấy tất cả các giá trị tương ứng với một khóa trong）（⑩trongHashthêm một giá trị đối tượng cho một trường của một khóa trong）（⑩①trongHashlấy giá trị đối tượng tương ứng với một trường của một khóa trong）】
*		    5、Listthao tác
*		     【đồng bộ（①xóa và trả về phần tử đầu tiên tương ứng với khóa đó trong danh sách）(②xóa và trả về phần tử cuối cùng tương ứng với khóa đó trong danh sách)（③xóa phần tử trong danh sách có khóa chỉ định và giá trị giống giá trị đó）
*		     （④chèn giá trị vào cuối danh sách。nếu khóa không tồn tại，tạo trước rồi mới chèn giá trị）（⑤chèn giá trị vào đầu danh sách。nếu khóa không tồn tại，tạo trước rồi mới chèn giá trị）（⑥trả về độ dài của khóa đó trong danh sách，nếu không tồn tại，trả về 0）
*		     （⑦trả về phần tử từ điểm bắt đầu đến điểm kết thúc tương ứng với khóa trên danh sách đó）（⑧xóa và trả về đối tượng phần tử đầu tiên được lưu trong danh sách của khóa đó）（⑨xóa và trả về đối tượng phần tử cuối cùng được lưu trong danh sách của khóa đó）
*		     （⑩chèn giá trị vào cuối danh sách。nếu khóa không tồn tại，tạo trước rồi mới chèn giá trị）（⑩①chèn giá trị vào đầu danh sách。nếu khóa không tồn tại，tạo trước rồi mới chèn giá trị）】
*		     
*		     【bất đồng bộ（①xóa và trả về phần tử đầu tiên tương ứng với khóa đó trong danh sách）(②xóa và trả về phần tử cuối cùng tương ứng với khóa đó trong danh sách)（③xóa phần tử trong danh sách có khóa chỉ định và giá trị giống giá trị đó）
*		     （④chèn giá trị vào cuối danh sách。nếu khóa không tồn tại，tạo trước rồi mới chèn giá trị）（⑤chèn giá trị vào đầu danh sách。nếu khóa không tồn tại，tạo trước rồi mới chèn giá trị）（⑥trả về độ dài của khóa đó trong danh sách，nếu không tồn tại，trả về 0）
*		     （⑦trả về phần tử từ điểm bắt đầu đến điểm kết thúc tương ứng với khóa trên danh sách đó）（⑧xóa và trả về đối tượng phần tử đầu tiên được lưu trong danh sách của khóa đó）（⑨xóa và trả về đối tượng phần tử cuối cùng được lưu trong danh sách của khóa đó）
*		     （⑩chèn giá trị vào cuối danh sách。nếu khóa không tồn tại，tạo trước rồi mới chèn giá trị）（⑩①chèn giá trị vào đầu danh sách。nếu khóa không tồn tại，tạo trước rồi mới chèn giá trị）】
*		    6、SortedSetthao tác
*		     【đồng bộ（① SortedSet thêm mới）（②trả về các phần tử trong phạm vi chỉ định của tập hợp có thứ tự，trong trường hợp mặc định từ thấp đến cao）（③trả về số phần tử của tập hợp có thứ tự）（④trả về số phần tử của tập hợp có thứ tự）
*		     （⑤SortedSet thêm mới）（⑥các thành viên của tập hợp sắp xếp theo điểm tăng dần）】
*		     
*		     【bất đồng bộ（① SortedSet thêm mới）（②trả về các phần tử trong phạm vi chỉ định của tập hợp có thứ tự，trong trường hợp mặc định từ thấp đến cao）（③trả về số phần tử của tập hợp có thứ tự）（④trả về số phần tử của tập hợp có thứ tự）
*		     （⑤SortedSet thêm mới）（⑥các thành viên của tập hợp sắp xếp theo điểm tăng dần）】
*		    7、Keythao tác
*		     【đồng bộ（①xóa chỉ địnhKey）（②xóa chỉ địnhKeydanh sách）（③kiểm traKeycó tồn tại hay không）（④đổi tênKey）（⑤thiết lậpKeythời gian của）】
*		     
*		     【bất đồng bộ（①xóa chỉ địnhKey）（②xóa chỉ địnhKeydanh sách）（③kiểm traKeycó tồn tại hay không）（④đổi tênKey）（⑤thiết lậpKeythời gian của）】
*		    8、phát hành - đăng ký
*		     【đồng bộ（①đăng ký）（②phát hành）】
*		     
*		     【bất đồng bộ（①đăng ký）（②phát hành）】
*		     
*	Date：2021
*	Version：0.1phiên bản
*	Author：Coffee
*	Modify Recoder：
————————————————
tuyên bố bản quyền：bài viết này làCSDNchủ blog「cà phê sữa13」bài viết gốc của，tuân theoCC 4.0 BY-SAthỏa thuận bản quyền，khi đăng lại vui lòng kèm liên kết nguồn gốc văn bản và tuyên bố này。
liên kết văn bản gốc：https://blog.csdn.net/xiaochenXIHUA/article/details/117589909*/
#endregion