using Newtonsoft.Json;
using StackExchange.Redis;
using System.Reflection.Metadata;
using System.Runtime.Serialization.Formatters.Binary;

namespace NewtouchHIS.Lib.Redis
{
    public class RedisCoreEx : IDisposable
    {

        #region   Tham số cơ sở
        //Chuỗi kết nối
        //private readonly string _ConnectStr;
        //Khóa
        private static readonly object _Locker = new object();
        //Đối tượng Redis đã kết nối
        internal static IConnectionMultiplexer _ConnMultiplexer;
        //cơ sở dữ liệu
        internal static IDatabase _DB;
        #endregion

        #region   Phương thức công khai

        ///// <summary>
        ///// Hàm khởi tạo
        ///// </summary>
        ///// <param name="connStr">Rediscủa chuỗi kết nối</param>
        //public static RedisCoreEx(string connStr)
        //{
        //    if (!string.IsNullOrEmpty(connStr))
        //    {
        //        _ConnectStr = connStr;
        //        //Lấy được đối tượng kết nối
        //        GetConnectRedisMultiplexer();
        //        //Thêm sự kiện đăng ký
        //        //AddRegisterEvent();
        //    }
        //}
        /// <summary>
        /// Lấy được đối tượng Redis đã kết nối
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
        /// Chọn cơ sở dữ liệu
        /// </summary>
        /// <param name="db">Số hiệu cơ sở dữ liệu (mặc định là cơ sở dữ liệu số 0)</param>
        public static void SelectDB(int db = 0)
        {
            _DB = _ConnMultiplexer.GetDatabase(db);
        }

        /// <summary>
        /// Lấy được trạng thái kết nối
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
        /// Thêm tiền tố cho khóa
        /// </summary>
        /// <param name="prefixName">Tên tiền tố</param>
        /// <param name="keyName">Tên khóa</param>
        /// <returns>Trả về khóa sau khi thêm tiền tố</returns>
        protected static string AddPrefixOfKey(string prefixName, string keyName)
        {
            string str = $"{prefixName}:{keyName}";
            return str;
        }

        //Kiểu tuần tự hóa
        public enum SerializeType
        {
            Binary,   //Nhị phân
            Json,     //Json
        }




        #region   String thao tác

        #region   Phương thức đồng bộ
        /// <summary>
        /// Thêm một cặp khóa - giá trị chuỗi (nếu key tồn tại thì ghi đè)
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="redisValue">giá trị</param>
        /// <param name="expiry">khoảng thời gian</param>
        /// <returns>Kết quả trả về (true nghĩa là thành công)</returns>
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
        /// Lấy chuỗi giá trị tương ứng với khóa
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="expiry">khoảng thời gian</param>
        /// <returns>Kết quả trả về (true nghĩa là thành công)</returns>
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
        /// Lưu nhiều cặp khóa - giá trị chuỗi
        /// </summary>
        /// <param name="keyValuePairs">Vùng chứa cặp khóa - giá trị</param>
        /// <returns>Kết quả trả về (true nghĩa là thành công)</returns>
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
        /// Lưu trữ một đối tượng (đối tượng này sẽ được tuần tự hóa và lưu)
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="redisValue">Giá trị (sẽ được tuần tự hóa)</param>
        /// <param name="expiry">khoảng thời gian</param>
        /// <param name="serializeType">Kiểu tuần tự hóa (mặc định tuần tự hóa thành Json)</param>
        /// <returns>Kết quả trả về (true nghĩa là thành công)</returns>
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
        /// Lấy một đối tượng (sẽ thực hiện giải tuần tự hóa)
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="expiry"></param>
        /// <param name="serializeType">Kiểu tuần tự hóa (mặc định tuần tự hóa thành Json)</param> 
        /// <returns>Kết quả trả về (true nghĩa là thành công)</returns>
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

        #region Phương thức bất đồng bộ

        /// <summary>
        /// Lưu một cặp khóa - giá trị chuỗi (phương thức bất đồng bộ)
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="redisValue">giá trị</param>
        /// <param name="expiry">khoảng thời gian</param>
        /// <returns>Kết quả trả về (true nghĩa là thành công)</returns>
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
        /// Lấy một giá trị duy nhất
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="redisValue">giá trị</param>
        /// <param name="expiry">khoảng thời gian</param>
        /// <returns>Kết quả trả về (true nghĩa là thành công)</returns>
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
        /// Lưu một nhóm giá trị chuỗi
        /// </summary>
        /// <param name="keyValuePairs">Vùng chứa cặp khóa - giá trị</param>
        /// <returns>Kết quả trả về (true nghĩa là thành công)</returns>
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
        /// Lưu trữ một đối tượng (đối tượng này sẽ được tuần tự hóa và lưu)
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="redisValue">giá trị</param>
        /// <param name="expiry">Khoảng thời gian</param>
        /// <param name="serializeType">Kiểu tuần tự hóa (mặc định tuần tự hóa thành Json)</param> 
        /// <returns>Kết quả trả về (true nghĩa là thành công)</returns>
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
        /// Lấy một đối tượng (sẽ thực hiện giải tuần tự hóa)
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="expiry">Khoảng thời gian</param>
        /// <param name="serializeType">Kiểu tuần tự hóa (mặc định tuần tự hóa thành Json)</param> 
        /// <returns>Kết quả trả về (true nghĩa là thành công)</returns>
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

        #region   Phương thức đồng bộ
        /// <summary>
        /// Xác định trường này có tồn tại trong hash không
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="hashField">trường</param>
        /// <returns>Kết quả trả về (true: nghĩa là thành công)</returns>
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
        /// Xóa trường chỉ định khỏi hash
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="hashField">trường</param>
        /// <returns>Kết quả trả về (true: nghĩa là thành công)</returns>
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
        /// Xóa trường chỉ định khỏi hash
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="hashFields">trường</param>
        /// <returns>Kết quả trả về (-1: nghĩa là thất bại)</returns>
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
        /// Đặt giá trị trong hash
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="hashField">trường</param>
        /// <param name="value">giá trị</param>
        /// <returns>Kết quả trả về (true: nghĩa là thành công)</returns>
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
        /// Đặt giá trị trong hash
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="hashFields">Vùng chứa trường</param>
        public static void SetHash(string redisKey, IEnumerable<KeyValuePair<string, string>> hashFields)
        {
            if (ConnetStatus)
            {
                var entries = hashFields.Select(x => new HashEntry(x.Key, x.Value));

                _DB.HashSet(redisKey, entries.ToArray());
            }

        }

        /// <summary>
        /// Lấy giá trị trong hash
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="hashField">trường</param>
        /// <returns>Kết quả trả về</returns>
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
        /// Lấy giá trị trong hash
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="hashFields">trường</param>
        /// <returns>Kết quả trả về</returns>
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
        /// Trả về tất cả các giá trị trường từ hash
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <returns>Kết quả trả về</returns>
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
        /// Lấy tất cả các giá trị trong hash theo khóa
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <returns>Trả về kết quả hash</returns>
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
        /// Đặt giá trị trong hash (tuần tự hóa)
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="hashField">trường</param>
        /// <param name="redisValue">giá trị</param>
        /// <param name="serializeType">Kiểu tuần tự hóa (mặc định là Json)</param>
        /// <returns>Kết quả trả về (true:nghĩa là thành công)</returns>
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
        /// Lấy giá trị trong hash (giải tuần tự hóa)
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <param name="serializeType">Kiểu tuần tự hóa (mặc định là Json)</param>
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

        #region   Phương thức bất đồng bộ

        /// <summary>
        /// Xác định trường này có tồn tại trong hash không (phương thức bất đồng bộ)
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="hashField">trường</param>
        /// <returns>Kết quả trả về (true: nghĩa là tồn tại)</returns>
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
        /// Xóa trường chỉ định khỏi hash
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="hashField">trường</param>
        /// <returns>Kết quả trả về (true: nghĩa là thành công)</returns>
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
        /// Xóa trường chỉ định khỏi hash
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="hashFields">trường</param>
        /// <returns>Trả về kết quả xóa (-1 nghĩa là thất bại)</returns>
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
        /// Đặt giá trị trong hash
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="hashField">trường</param>
        /// <param name="value">giá trị</param>
        /// <returns>Kết quả trả về (true:nghĩa là thành công)</returns>
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
        /// Đặt giá trị trong hash
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
        /// Lấy giá trị trong hash
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="hashField">trường</param>
        /// <returns>Kết quả trả về</returns>
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
        /// Lấy giá trị trong hash
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="hashFields">trường</param>
        /// <param name="value">giá trị</param>
        /// <returns>Kết quả trả về</returns>
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
        /// Trả về tất cả các giá trị trường từ hash
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <returns>Trả về giá trị trường hash tương ứng</returns>
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
        /// Trả về tất cả các giá trị trong hash
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <returns>Trả về tất cả các giá trị trong hash</returns>
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
        /// Đặt giá trị trong hash (tuần tự hóa)
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="hashField">trường</param>
        /// <param name="value">giá trị</param>
        /// <param name="serializeType">Kiểu tuần tự hóa (mặc định là Json)</param>
        /// <returns>Kết quả trả về (true:nghĩa là thành công)</returns>
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
        /// Lấy giá trị trong hash (giải tuần tự hóa)
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <param name="serializeType">Kiểu tuần tự hóa (mặc định là Json)</param>
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

        #region   Phương thức đồng bộ

        /// <summary>
        /// Xóa và trả về phần tử đầu tiên được lưu trong danh sách của khóa này
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
        /// Xóa và trả về phần tử cuối cùng được lưu trong danh sách của khóa này
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
        /// Xóa phần tử giống với giá trị đó trên khóa chỉ định của danh sách
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="redisValue">giá trị</param>
        /// <returns>Kết quả trả về (-1 nghĩa là thất bại)</returns>
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
        /// Chèn giá trị vào cuối danh sách. Nếu khóa không tồn tại, tạo trước rồi chèn giá trị
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="redisValue">giá trị</param>
        /// <returns>Kết quả trả về (-1 nghĩa là thất bại)</returns>
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
        /// Chèn giá trị vào đầu danh sách. Nếu khóa không tồn tại, tạo trước rồi chèn giá trị
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="redisValue">giá trị</param>
        /// <returns>Kết quả trả về (-1 nghĩa là thất bại)</returns>
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
        /// Trả về độ dài của khóa này trên danh sách, nếu không tồn tại, trả về 0
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <returns>Kết quả trả về (-1 nghĩa là thất bại)</returns>
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
        /// Trả về phần tử tương ứng với khóa trên danh sách này
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="start">Điểm bắt đầu</param>
        /// <param name="stop">Điểm dừng</param>
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
        /// Xóa và trả về đối tượng phần tử đầu tiên được lưu trong danh sách của khóa này
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="serializeType">Kiểu tuần tự hóa (mặc định là Json)</param>
        /// <returns>Trả về đối tượng sau khi giải tuần tự hóa</returns>
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
        /// Xóa và trả về đối tượng phần tử cuối cùng được lưu trong danh sách của khóa này
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="serializeType">Kiểu tuần tự hóa (mặc định là Json)</param>
        /// <returns>Trả về đối tượng sau khi giải tuần tự hóa</returns>
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
        /// Chèn giá trị vào cuối danh sách. Nếu khóa không tồn tại, tạo trước rồi chèn giá trị
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns>Kết quả trả về (-1 nghĩa là thất bại)</returns>
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
        /// Chèn giá trị vào đầu danh sách. Nếu khóa không tồn tại, tạo trước rồi chèn giá trị
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns>Kết quả trả về (-1 nghĩa là thất bại)</returns>
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

        #region    Phương thức bất đồng bộ

        /// <summary>
        /// Xóa và trả về phần tử đầu tiên được lưu trong danh sách của khóa này
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
        /// Xóa và trả về phần tử cuối cùng được lưu trong danh sách của khóa này
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
        /// Xóa phần tử giống với giá trị đó trên khóa chỉ định của danh sách
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="redisValue">giá trị</param>
        /// <returns>Kết quả trả về (-1 nghĩa là thất bại)</returns>
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
        /// Chèn giá trị vào cuối danh sách. Nếu khóa không tồn tại, tạo trước rồi chèn giá trị
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="redisValue">giá trị</param>
        /// <returns>Kết quả trả về (-1 nghĩa là thất bại)</returns>
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
        /// Chèn giá trị vào đầu danh sách. Nếu khóa không tồn tại, tạo trước rồi chèn giá trị
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="redisValue">giá trị</param>
        /// <returns>Kết quả trả về (-1 nghĩa là thất bại)</returns>
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
        /// Trả về độ dài của khóa này trên danh sách, nếu không tồn tại, trả về 0
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <returns>Kết quả trả về (-1 nghĩa là thất bại)</returns>
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
        /// Trả về phần tử tương ứng với khóa trên danh sách này
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
        /// Xóa và trả về đối tượng phần tử đầu tiên được lưu trong danh sách của khóa này
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="serializeType">Kiểu tuần tự hóa (mặc định là Json)</param>
        /// <returns>Trả về đối tượng sau khi giải tuần tự hóa</returns>
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
        /// Xóa và trả về đối tượng phần tử cuối cùng được lưu trong danh sách của khóa này
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="serializeType">Kiểu tuần tự hóa (mặc định là Json)</param>
        /// <returns>Trả về đối tượng sau khi giải tuần tự hóa</returns>
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
        /// Chèn giá trị vào cuối danh sách. Nếu khóa không tồn tại, tạo trước rồi chèn giá trị
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="redisValue">giá trị</param>
        /// <returns>Kết quả trả về (-1 nghĩa là thất bại)</returns>
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
        /// Chèn giá trị vào đầu danh sách. Nếu khóa không tồn tại, tạo trước rồi chèn giá trị
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="redisValue">giá trị</param>
        /// <returns>Kết quả trả về (-1 nghĩa là thất bại)</returns>
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

        #region   Phương thức đồng bộ
        /// <summary>
        /// SortedSet Thêm mới
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
        /// Trả về các phần tử trong phạm vi chỉ định trong tập hợp có thứ tự, mặc định từ thấp đến cao
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
        /// Trả về số lượng phần tử của tập hợp có thứ tự
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns>Kết quả trả về (-1 nghĩa là thất bại)</returns>
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
        /// Trả về số lượng phần tử của tập hợp có thứ tự
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="memebr"></param>
        /// <returns>Kết quả trả về (true:nghĩa là thành công)</returns>
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
        /// SortedSet Thêm mới
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns>Kết quả trả về (true:nghĩa là thành công)</returns>
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
        /// Thành viên trong tập hợp được sắp xếp theo điểm tăng dần lưu trữ khóa - giá trị theo mức tăng
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="value"></param>
        /// <returns>Kết quả trả về (-1 nghĩa là thất bại)</returns>
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

        #region  Phương thức bất đồng bộ

        /// <summary>
        /// SortedSet Thêm mới
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns>Kết quả trả về (true:nghĩa là thành công)</returns>
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
        /// Trả về các phần tử trong phạm vi chỉ định trong tập hợp có thứ tự, mặc định từ thấp đến cao.
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
        /// Trả về số lượng phần tử của tập hợp có thứ tự
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns>Kết quả trả về (-1 nghĩa là thất bại)</returns>
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
        /// Trả về số lượng phần tử của tập hợp có thứ tự
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="memebr"></param>
        /// <returns>Kết quả trả về (true:nghĩa là thành công)</returns>
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
        /// SortedSet Thêm mới
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
        /// Thành viên trong tập hợp được sắp xếp theo điểm tăng dần lưu trữ khóa - giá trị theo mức tăng
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="value"></param>
        /// <returns>Kết quả trả về (-1 nghĩa là thất bại)</returns>
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
        /// Redis Loại sắp xếp
        /// </summary>
        public enum OrderType
        {
            Asc,
            Desc
        }

        #endregion


        #region   Key thao tác

        #region   Phương thức đồng bộ

        /// <summary>
        /// Xóa chỉ địnhKey
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <returns>Kết quả trả về (true: nghĩa là thành công)</returns>
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
        /// Xóa danh sách Key chỉ định
        /// </summary>
        /// <param name="redisKeys">Vùng chứa khóa</param>
        /// <returns>Số lượng khóa chỉ định đã xóa(-1:nghĩa là thất bại)</returns>
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
        /// Kiểm tra Key có tồn tại không
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <returns>Kết quả trả về (true: nghĩa là thành công)</returns>
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
        /// Đổi tênKey
        /// </summary>
        /// <param name="redisKey">Khóa ban đầu</param>
        /// <param name="redisNewKey">Tên khóa mới</param>
        /// <returns>Kết quả trả về (true: nghĩa là thành công)</returns>
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
        /// Thiết lập thời gian của Key
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="expiry">khoảng thời gian</param>
        /// <returns>Kết quả trả về (true: nghĩa là thành công)</returns>
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

        #region   Phương thức bất đồng bộ

        /// <summary>
        /// Xóa chỉ định Key
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <returns>Kết quả trả về (true: nghĩa là thành công)</returns>
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
        /// Xóa danh sách Key chỉ định
        /// </summary>
        /// <param name="redisKeys">khóa</param>
        /// <returns>Kết quả trả về (-1 nghĩa là thất bại)</returns>
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
        /// Kiểm tra Key có tồn tại không
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <returns>Kết quả trả về (true: nghĩa là thành công)</returns>
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
        /// Đổi tên Key
        /// </summary>
        /// <param name="redisKey">Tên khóa cũ</param>
        /// <param name="redisNewKey">Tên khóa mới</param>
        /// <returns>Kết quả trả về (true: nghĩa là thành công)</returns>
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
        /// Thiết lập thời gian của Key
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="expiry">Khoảng thời gian</param>
        /// <returns>Kết quả trả về (true: nghĩa là thành công)</returns>
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


        #region Xuất bản - Đăng ký nhận

        #region   Phương thức đồng bộ
        ///// <summary>
        ///// Đăng ký nhận
        ///// </summary>
        ///// <param name="channel"></param>
        ///// <param name="handle"></param>
        //public static void Subscribe(RedisChannel channel, Action<RedisChannel, RedisValue> handle)
        //{
        //    var sub = _ConnMultiplexer.GetSubscriber();
        //    sub.Subscribe(channel, handle);
        //}

        ///// <summary>
        ///// Xuất bản
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
        ///// Xuất bản (sử dụng tuần tự hóa)
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

        #region Phương thức bất đồng bộ

        ///// <summary>
        ///// Đăng ký nhận
        ///// </summary>
        ///// <param name="channel"></param>
        ///// <param name="handle"></param>
        //public static async Task SubscribeAsync(RedisChannel channel, Action<RedisChannel, RedisValue> handle)
        //{
        //    var sub = _ConnMultiplexer.GetSubscriber();
        //    await sub.SubscribeAsync(channel, handle);
        //}

        ///// <summary>
        ///// Xuất bản
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
        ///// Xuất bản (sử dụng tuần tự hóa)
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


        #region   Phương thức riêng tư
        


        #region Đăng ký sự kiện

        /// <summary>
        /// Thêm sự kiện đăng ký
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
        /// Khi phát sóng cấu hình lại (thường có nghĩa là thay đổi đồng bộ chính - phụ)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_ConfigurationChangedBroadcast(object sender, EndPointEventArgs e)
        {
            Console.WriteLine($"{nameof(ConnMultiplexer_ConfigurationChangedBroadcast)}: {e.EndPoint}");
        }

        /// <summary>
        /// Khi xảy ra lỗi nội bộ (chủ yếu dùng để gỡ lỗi)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_InternalError(object sender, InternalErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(ConnMultiplexer_InternalError)}: {e.Exception}");
        }

        /// <summary>
        /// Khi thay đổi cụm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_HashSlotMoved(object sender, HashSlotMovedEventArgs e)
        {
            Console.WriteLine(
                $"{nameof(ConnMultiplexer_HashSlotMoved)}: {nameof(e.OldEndPoint)}-{e.OldEndPoint} To {nameof(e.NewEndPoint)}-{e.NewEndPoint}, ");
        }

        /// <summary>
        /// Khi cấu hình thay đổi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_ConfigurationChanged(object sender, EndPointEventArgs e)
        {
            Console.WriteLine($"{nameof(ConnMultiplexer_ConfigurationChanged)}: {e.EndPoint}");
        }

        /// <summary>
        /// Khi xảy ra lỗi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_ErrorMessage(object sender, RedisErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(ConnMultiplexer_ErrorMessage)}: {e.Message}");
        }

        /// <summary>
        /// Khi kết nối vật lý thất bại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_ConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            Console.WriteLine($"{nameof(ConnMultiplexer_ConnectionFailed)}: {e.Exception}");
        }

        /// <summary>
        /// Khi thiết lập kết nối vật lý
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_ConnectionRestored(object sender, ConnectionFailedEventArgs e)
        {
            Console.WriteLine($"{nameof(ConnMultiplexer_ConnectionRestored)}: {e.Exception}");
        }

        #endregion Đăng ký sự kiện


        /// <summary>
        /// Chuyển đổi thành chuỗi
        /// </summary>
        /// <typeparam name="T">Loại dữ liệu</typeparam>
        /// <param name="list">Danh sách dữ liệu</param>
        /// <returns>Trả về danh sách dữ liệu sau khi chuyển thành chuỗi</returns>
        private static IEnumerable<string> ConvertStrings<T>(IEnumerable<T> list) where T : struct
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            return list.Select(x => x.ToString());
        }

        /// <summary>
        /// Tuần tự hóa (nhị phân)
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
        /// Giải tuần tự hóa (nhị phân)
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
        /// Tuần tự hóa đối tượng thành chuỗi Json
        /// </summary>
        /// <typeparam name="T">Loại thực thể</typeparam>
        /// <param name="obj">Đối tượng thực thể</param>
        /// <returns>Trả về chuỗi đã tuần tự hóa</returns>
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
        /// Giải tuần tự hóa chuỗi Json thành đối tượng
        /// </summary>
        /// <typeparam name="T">Loại thực thể</typeparam>
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
*	Title:"Thu thập dữ liệu" dự án
*		Chủ đề: Lớp hỗ trợ Redis
*	Description:
*		Chức năng:
*		    1, Thiết lập chuỗi kết nối, chọn cơ sở dữ liệu, lấy trạng thái kết nối
*		    2, Thêm tiền tố cho khóa
*		    3, Thao tác String
*		     [Đồng bộ ((1)Lưu một cặp khóa - giá trị duy nhất)((2)Lấy giá trị tương ứng theo khóa)((3)Đồng thời lưu nhiều cặp khóa - giá trị)((4)Lưu trữ một đối tượng)((5)Lấy một đối tượng)]
*		     
*		     [Bất đồng bộ ((1)Lưu một cặp khóa - giá trị duy nhất)((2)Lấy giá trị tương ứng theo khóa)((3)Đồng thời lưu nhiều cặp khóa - giá trị)((4)Lưu trữ một đối tượng)((5)Lấy một đối tượng)]
*		    4, Thao tác Hash
*		     [Đồng bộ ((1)Xác định một cặp khóa - giá trị duy nhất có tồn tại trong Hash không)((2)Xóa một cặp khóa - giá trị duy nhất khỏi Hash)((3)Đồng thời xóa danh sách nhiều trường tương ứng với một khóa duy nhất khỏi Hash)((4)Chỉ định giá trị trong Hash)
*		     ((5)Đồng thời thêm danh sách nhiều trường do một khóa duy nhất chỉ định trong Hash)((6)Lấy giá trị tương ứng với khóa, trường duy nhất trong Hash)((7)Đồng thời lấy trường chỉ định tương ứng với khóa duy nhất trong Hash)
*		     ((8)Lấy tất cả các trường tương ứng với khóa duy nhất trong Hash)((9)Lấy tất cả các giá trị tương ứng với khóa duy nhất trong Hash)((10)Thêm một giá trị đối tượng cho một trường duy nhất của một khóa duy nhất trong Hash)((11)Lấy giá trị đối tượng tương ứng với một trường duy nhất của một khóa duy nhất trong Hash)]
*		     
*		     [Bất đồng bộ ((1)Xác định một cặp khóa - giá trị duy nhất có tồn tại trong Hash không)((2)Xóa một cặp khóa - giá trị duy nhất khỏi Hash)((3)Đồng thời xóa danh sách nhiều trường tương ứng với một khóa duy nhất khỏi Hash)((4)Chỉ định giá trị trong Hash)
*		     ((5)Đồng thời thêm danh sách nhiều trường do một khóa duy nhất chỉ định trong Hash)((6)Lấy giá trị tương ứng với khóa, trường duy nhất trong Hash)((7)Đồng thời lấy trường chỉ định tương ứng với khóa duy nhất trong Hash)
*		     ((8)Lấy tất cả các trường tương ứng với khóa duy nhất trong Hash)((9)Lấy tất cả các giá trị tương ứng với khóa duy nhất trong Hash)((10)Thêm một giá trị đối tượng cho một trường duy nhất của một khóa duy nhất trong Hash)((11)Lấy giá trị đối tượng tương ứng với một trường duy nhất của một khóa duy nhất trong Hash)]
*		    5, Thao tác List
*		     [Đồng bộ ((1)Xóa và trả về phần tử đầu tiên tương ứng với khóa này trong danh sách)((2)Xóa và trả về phần tử cuối cùng tương ứng với khóa này trong danh sách)((3)Xóa phần tử giống với giá trị đó trên khóa chỉ định trong danh sách)
*		     ((4)Chèn giá trị vào cuối danh sách. Nếu khóa không tồn tại, tạo trước rồi chèn giá trị)((5)Chèn giá trị vào đầu danh sách. Nếu khóa không tồn tại, tạo trước rồi chèn giá trị)((6)Trả về độ dài của khóa này trên danh sách, nếu không tồn tại, trả về 0)
*		     ((7)Trả về phần tử điểm bắt đầu - điểm kết thúc tương ứng với khóa trên danh sách này)((8)Xóa và trả về đối tượng phần tử đầu tiên được lưu trong danh sách của khóa này)((9)Xóa và trả về đối tượng phần tử cuối cùng được lưu trong danh sách của khóa này
*		     ((10)Chèn giá trị vào cuối danh sách. Nếu khóa không tồn tại, tạo trước rồi chèn giá trị)((11)Chèn giá trị vào đầu danh sách. Nếu khóa không tồn tại, tạo trước rồi chèn giá trị)]
*		     
*		     [Bất đồng bộ ((1)Xóa và trả về phần tử đầu tiên tương ứng với khóa này trong danh sách)((2)Xóa và trả về phần tử cuối cùng tương ứng với khóa này trong danh sách)((3)Xóa phần tử giống với giá trị đó trên khóa chỉ định trong danh sách)
*		     ((4)Chèn giá trị vào cuối danh sách. Nếu khóa không tồn tại, tạo trước rồi chèn giá trị)((5)Chèn giá trị vào đầu danh sách. Nếu khóa không tồn tại, tạo trước rồi chèn giá trị)((6)Trả về độ dài của khóa này trên danh sách, nếu không tồn tại, trả về 0)
*		     ((7)Trả về phần tử điểm bắt đầu - điểm kết thúc tương ứng với khóa trên danh sách này)((8)Xóa và trả về đối tượng phần tử đầu tiên được lưu trong danh sách của khóa này)((9)Xóa và trả về đối tượng phần tử cuối cùng được lưu trong danh sách của khóa này
*		     ((10)Chèn giá trị vào cuối danh sách. Nếu khóa không tồn tại, tạo trước rồi chèn giá trị)((11)Chèn giá trị vào đầu danh sách. Nếu khóa không tồn tại, tạo trước rồi chèn giá trị)]
*		    6, Thao tác SortedSet
*		     [Đồng bộ ((1) SortedSet Thêm mới)((2)Trả về các phần tử trong phạm vi chỉ định trong tập hợp có thứ tự, mặc định từ thấp đến cao)((3)Trả về số lượng phần tử của tập hợp có thứ tự)((4)Trả về số lượng phần tử của tập hợp có thứ tự)
*		     ((5)SortedSet Thêm mới)((6)Thành viên trong tập hợp được sắp xếp theo điểm tăng dần lưu trữ khóa - giá trị theo mức tăng)]
*		     
*		     [Bất đồng bộ ((1) SortedSet Thêm mới)((2)Trả về các phần tử trong phạm vi chỉ định trong tập hợp có thứ tự, mặc định từ thấp đến cao)((3)Trả về số lượng phần tử của tập hợp có thứ tự)((4)Trả về số lượng phần tử của tập hợp có thứ tự)
*		     ((5)SortedSet Thêm mới)((6)Thành viên trong tập hợp được sắp xếp theo điểm tăng dần lưu trữ khóa - giá trị theo mức tăng)]
*		    7, Thao tác Key
*		     [Đồng bộ ((1)Xóa Key chỉ định)((2)Xóa danh sách Key chỉ định)((3)Kiểm tra Key có tồn tại không)((4)Đổi tên Key)((5)Thiết lập thời gian của Key)]
*		     
*		     [Bất đồng bộ ((1)Xóa Key chỉ định)((2)Xóa danh sách Key chỉ định)((3)Kiểm tra Key có tồn tại không)((4)Đổi tên Key)((5)Thiết lập thời gian của Key)]
*		    8, Xuất bản - Đăng ký nhận
*		     [Đồng bộ ((1)Đăng ký nhận)((2)Xuất bản)]
*		     
*		     [Bất đồng bộ ((1)Đăng ký nhận)((2)Xuất bản)]
*		     
*	Date:2021
*	Version: 0.1 phiên bản
*	Author:Coffee
*	Modify Recoder:
————————————————
版权声明：本文为CSDN博主「牛奶咖啡13」的原创文章，遵循CC 4.0 BY-SA版权协议，转载请附上原文出处链接及本声明。
原文链接：https://blog.csdn.net/xiaochenXIHUA/article/details/117589909*/
#endregion