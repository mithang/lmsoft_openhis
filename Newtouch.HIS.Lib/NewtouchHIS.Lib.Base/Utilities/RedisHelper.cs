using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NewtouchHIS.Lib.Redis;
using StackExchange.Redis;
using System.DirectoryServices.ActiveDirectory;

namespace NewtouchHIS.Lib.Base.Utilities
{
    /// <summary>
    /// Redis Lớp hỗ trợ
    /// </summary>
    public class RedisHelper : RedisCoreEx
    {
        //Redisđối tượng
        //private static RedisCoreEx Redis;
        public static IConfigurationSection _config;
        public static ILogger<RedisHelper> Logger { get; set; }
        public RedisHelper(IConfigurationSection configuration)
        {
            _config = configuration;
            if(!string.IsNullOrWhiteSpace(_config["Connection"]))
            {
                GetConnectRedisMultiplexer(_config["Connection"]);
                //1-Chọn cơ sở dữ liệu
                SelectDB(Convert.ToInt32(_config["Db"]));
            }            
        }
        #region String

        /// <summary>
        /// Lấy chuỗi giá trị tương ứng với khóa
        /// </summary>
        /// <param name="redisKey">khóa</param>
        /// <param name="expiry">khoảng thời gian</param>
        /// <param name="withPrefix">Mặc định thêm tiền tố key</param>
        /// <returns>Kết quả trả về (true nghĩa là thành công)</returns>
        public static string GetString(string key, bool withPrefix = true)
        {
            if (withPrefix)
            {
                key = AddPrefixOfKey(RedisKey.OriginKeyPrefix, key);
            }
            return StringGet(key);
        }


        public static async Task<string> GetStringAsync(string key, bool withPrefix = true)
        {
            if (withPrefix)
            {
                key = AddPrefixOfKey(RedisKey.OriginKeyPrefix, key);
            }
            return await GetStringOfAsync(key);
        }



        /// <summary>
        /// Thiết lập bộ nhớ đệm
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiretime">Thời gian hết hạn, đơn vị: giây</param>
        /// <param name="withPrefix">Mặc định thêm tiền tố key</param>
        /// <returns></returns>
        public static bool SetString(string key, string value, bool withPrefix = true)
        {
            if (withPrefix)
            {
                key = AddPrefixOfKey(RedisKey.OriginKeyPrefix, key);
            }
            return StringSet(key, value);
        }
        public static async Task<bool> SetStringAsync(string key, string value, bool withPrefix = true)
        {
            if (withPrefix)
            {
                key = AddPrefixOfKey(RedisKey.OriginKeyPrefix, key);
            }
            return await SetStringOfAsync(key, value);
        }
        /// <summary>
        /// Thiết lập bộ nhớ đệm
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiretime">Thời gian hết hạn, đơn vị: giây</param>
        /// <param name="withPrefix">Mặc định thêm tiền tố key</param>
        /// <returns></returns>
        public static bool SetString(string key, string value, TimeSpan expiretime, bool withPrefix = true)
        {

            if (withPrefix)
            {
                key = AddPrefixOfKey(RedisKey.OriginKeyPrefix, key);
            }
            return StringSet(key, value, expiretime);
        }


        /// <summary>
        /// Thiết lậpStringKey
        /// Phiên bản thấpRedis
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireHour"></param>
        /// <param name="withPrefix">Mặc định thêm tiền tố key</param>
        /// <returns></returns>
        public static bool SetString(string key, string value, HourOfDay expireHour)
        {
            key = AddPrefixOfKey(RedisKey.OriginKeyPrefix, key);
            return StringSet(key, value, new TimeSpan((int)expireHour, 0, 0));
        }
        #endregion

        #region int
        public static double IncrKey(string key)
        {
            key = AddPrefixOfKey(RedisKey.OriginKeyPrefix, key);
            return IncrStringGet(key);
        }
        public static async Task<double> IncrKeyAsync(string key)
        {
            key = AddPrefixOfKey(RedisKey.OriginKeyPrefix, key);
            return await IncrStringGetAsync(key);
        }
        public static double SortIncrKey(string key, string member, double value)
        {
            key = AddPrefixOfKey(RedisKey.OriginKeyPrefix, key);
            return SortedSetIncrement(key, member, value);
        }
        #endregion

        #region list
        public static List<T>? GetList<T>(string key, int start, int end) where T : class
        {
            long count = GetKeyLengthOfList(key);
            if (count < end)
            {
                end = (int)count;
            }
            var arr = ListRange(key, start, end);

            List<T>? list = null;
            if (arr != null && arr.Count() > 0)
            {
                list = new List<T>();
                foreach (string item in arr)
                {
                    list.Add(JsonConvert.DeserializeObject<T>(item));
                }
            }
            return list;
        }

        public static List<T>? GetList<T>(string key) where T : class
        {
            List<T> list = new List<T>();
            long count = GetKeyLengthOfList(key);
            for (int i = 0; i < count; i += 1000)
            {
                List<T>? strList = GetList<T>(key, i, i + 999);
                if (strList != null && strList.Count > 0)
                {
                    list.AddRange(strList);
                }
            }
            if (list != null && list.Count > 0)
            {
                return list;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Toàn bộ List tạo lạiSET
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="list"></param>
        /// <param name="expiretime"></param>
        public static long SetList<T>(string key, IList<T> list, int expiretime = 0)
        {

            if (list != null && list.Count > 0)
            {
                RedisValue[] lists = new RedisValue[list.Count];
                if (list != null && list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        lists[i] = JsonConvert.SerializeObject(list[i]);
                    }
                    DeleteKey(key);
                    var result = PushKeyOfListLast(key, lists);
                    if (expiretime != 0)
                    {
                        TimeSpan span = DateTime.Now.AddSeconds(expiretime) - DateTime.Now;
                        SetKeyExpire(key, span);
                    }
                    return result;
                }
            }
            return 0;
        }

        public long AppendListItem<T>(string key, T value)
        {
            return PushKeyOfListLast(key, JsonConvert.SerializeObject(value));

        }
        public long AppendListItem(string key, string value)
        {
            return PushKeyOfListLast(key, value);
        }
        public long AppendList<T>(string key, IList<T> list)
        {
            try
            {
                RedisValue[] lists = new RedisValue[list.Count];
                if (list != null && list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        lists[i] = JsonConvert.SerializeObject(list[i]);
                    }
                    return PushKeyOfListLast(key, lists);
                }
                return 0;
            }
            catch (Exception e)
            {
                if (ConnetStatus) Dispose();

                Logger.LogError("Redis LogError:" + e.Message, e);
                return 0;
            }
        }
        #endregion

        #region Hash

        #endregion

        #region remove
        public static bool Remove(string key)
        {
            key = key = AddPrefixOfKey(RedisKey.OriginKeyPrefix, key);
            return DeleteKey(key);
        }
        public static void RemoveKeys(List<string> keys)
        {
            if (keys != null && keys.Count > 0)
            {
                foreach (string item in keys)
                {
                    DeleteKey(item);
                }
            }
        }

        //public long ListRemove<T>(string key, T value) where T : class
        //{
        //    try
        //    {
        //        return ListRemove(key, JsonConvert.SerializeObject(value));
        //    }
        //    catch (Exception e)
        //    {
        //        if (ConnetStatus) Dispose();

        //        Logger.LogError("Redis LogError:" + e.Message, e);
        //        return 0;
        //    }
        //}
        //public long ListRemove(string key, string value)
        //{
        //    try
        //    {
        //        return ListRemove(key, value);
        //    }
        //    catch (Exception e)
        //    {
        //        if (ConnetStatus) Dispose();

        //        Logger.LogError("Redis LogError:" + e.Message, e);
        //        return 0;
        //    }
        //}
        #endregion

        ///// <summary>
        ///// LISTMột Item nào đó trong đó tạo lạiSET
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="key"></param>
        ///// <param name="index"></param>
        ///// <param name="value"></param>
        //public void SetListItem<T>(string key, int index, T value)
        //{
        //    try
        //    {

        //        ListSetByIndex(key, index, JsonConvert.SerializeObject(value));
        //    }
        //    catch (Exception e)
        //    {
        //        if (ConnetStatus) Dispose();

        //        Logger.LogError("Redis LogError:" + e.Message, e);
        //    }
        //}

        //public string? Get(string key)
        //{
        //    try
        //    {
        //        key = $"{RedisKey.KeyPrefix}{key}";
        //        var result = StringGet(key);
        //        return result.ToString();
        //    }
        //    catch (Exception e)
        //    {
        //        if (ConnetStatus) Dispose();

        //        Logger.LogError("Redis LogError:" + e.Message, e);
        //        return "";
        //    }
        //}
        //public T? Get<T>(string key) where T : class
        //{
        //    try
        //    {
        //        key = $"{RedisKey.KeyPrefix}{key}";
        //        var resultStr = StringGet(key);
        //        return JsonConvert.DeserializeObject<T>(resultStr.ToString());
        //    }
        //    catch (Exception e)
        //    {
        //        if (ConnetStatus) Dispose();

        //        Logger.LogError("Redis LogError:" + e.Message, e);
        //        return null;
        //    }
        //}
        ///// <summary>
        ///// HashExists
        ///// </summary>
        ///// <param name="key"></param>
        ///// <param name="value"></param>
        ///// <param name="flags"></param>
        ///// <returns>true: Successfully locked,false:Failed to lock</returns>
        //public bool HashExists(string key, string value, CommandFlags flags)
        //{
        //    try
        //    {
        //        return HashExists(key, value, flags);
        //    }
        //    catch (Exception e)
        //    {
        //        if (ConnetStatus) Dispose();

        //        Logger.LogError("Redis LogError:" + e.Message, e);
        //        return false;
        //    }
        //}

        //public void SetHash(string key, HashEntry[] value, int expiretime = 0)
        //{
        //    try
        //    {
        //        HashSet(key, value);
        //        if (expiretime != 0)
        //        {
        //            TimeSpan span = DateTime.Now.AddSeconds(expiretime) - DateTime.Now;
        //            KeyExpire(key, span);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        if (ConnetStatus) Dispose();

        //        Logger.LogError("Redis LogError:" + e.Message, e);
        //    }
        //}

        //public HashEntry[] GetHash(string key)
        //{
        //    try
        //    {
        //        return HashGetAll(key);
        //    }
        //    catch (Exception e)
        //    {
        //        if (ConnetStatus) Dispose();

        //        Logger.LogError("Redis LogError:" + e.Message, e);
        //        return null;
        //    }
        //}

        //public static  List<T>? GetList<T>(string key, int start, int end) where T : class
        //{
        //    try
        //    {
        //        long count = ListLength(key);
        //        if (count < end)
        //        {
        //            end = (int)count;
        //        }
        //        RedisValue[] arr = ListRange(key, start, end);

        //        List<T>? list = null;
        //        if (arr != null && arr.Length > 0)
        //        {
        //            list = new List<T>();
        //            foreach (string item in arr)
        //            {
        //                list.Add(JsonConvert.DeserializeObject<T>(item));
        //            }
        //        }
        //        return list;
        //    }
        //    catch (Exception e)
        //    {
        //        if (ConnetStatus) Dispose();

        //        Logger.LogError("Redis LogError:" + e.Message, e);
        //        return null;
        //    }
        //}

        //public RedisValue[] GetList(string key, int start, int end)
        //{
        //    try
        //    {
        //        long count = ListLength(key);
        //        if (count < end)
        //        {
        //            end = (int)count;
        //        }
        //        return ListRange(key, start, end);
        //    }
        //    catch (Exception e)
        //    {
        //        if (ConnetStatus) Dispose();

        //        Logger.LogError("Redis LogError:" + e.Message, e);
        //        return null;
        //    }
        //}



        //public List<RedisValue>? GetList(string key)
        //{
        //    try
        //    {
        //        var list = new List<RedisValue>();
        //        long count = ListLength(key);
        //        for (int i = 0; i < count; i += 1000)
        //        {
        //            var strList = GetList(key, i, i + 999);
        //            if (strList != null && strList.Length > 0)
        //            {
        //                list.AddRange(strList.ToList() ?? new List<RedisValue>());
        //            }
        //        }
        //        return list;
        //    }
        //    catch (Exception e)
        //    {
        //        if (ConnetStatus) Dispose();

        //        Logger.LogError("Redis LogError:" + e.Message, e);
        //        return null;
        //    }
        //}

        //public T? GetListItem<T>(string key, int index) where T : class
        //{
        //    try
        //    {
        //        long count = ListLength(key);
        //        if (count <= index)
        //        {
        //            return null;
        //        }
        //        RedisValue item = ListGetByIndex(key, index);
        //        return JsonConvert.DeserializeObject<T>(item);
        //    }
        //    catch (Exception e)
        //    {
        //        if (ConnetStatus) Dispose();

        //        Logger.LogError("Redis LogError:" + e.Message, e);
        //        return null;
        //    }
        //}


        //public List<T>? ListLeftPop<T>(string key, int count = 1) where T : class
        //{
        //    try
        //    {

        //        long len = ListLength(key);
        //        if (count > len)
        //        {
        //            count = Convert.ToInt32(len);
        //        }
        //        if (count > 0)
        //        {
        //            List<T> list = new List<T>();
        //            for (int i = 0; i < count; i++)
        //            {
        //                String value = ListLeftPop(key);
        //                if (String.IsNullOrEmpty(value))
        //                {
        //                    break;
        //                }
        //                list.Add(JsonConvert.DeserializeObject<T>(value));
        //            }
        //            return list;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        if (ConnetStatus) Dispose();

        //        Logger.LogError("Redis LogError:" + e.Message, e);
        //        return null;
        //    }
        //}
        //public List<T>? ListRightPop<T>(string key, int count = 1) where T : class
        //{
        //    try
        //    {

        //        long len = ListLength(key);
        //        if (count > len)
        //        {
        //            count = Convert.ToInt32(len);
        //        }
        //        if (count > 0)
        //        {
        //            List<T> list = new List<T>();
        //            for (int i = 0; i < count; i++)
        //            {
        //                String value = ListRightPop(key);
        //                if (String.IsNullOrEmpty(value))
        //                {
        //                    break;
        //                }
        //                list.Add(JsonConvert.DeserializeObject<T>(ListRightPop(value)));
        //            }
        //            return list;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        if (ConnetStatus) Dispose();

        //        Logger.LogError("Redis LogError:" + e.Message, e);
        //        return null;
        //    }
        //}
        /// <summary>
        /// Thiết lập thời gian của Key
        /// đơn vị:phút 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiretime"></param>
        /// <param name="withPrefix">Mặc định thêm tiền tố key</param>
        public static void KeyExpire(string key, int expiretime, bool withPrefix = true)
        {
            if (withPrefix)
            {
                key = AddPrefixOfKey(RedisKey.OriginKeyPrefix, key);
            }
            TimeSpan span = DateTime.Now.AddMinutes(expiretime) - DateTime.Now;
            SetKeyExpire(key, span);
        }
        /// <summary>
        /// Thiết lập thời gian của Key
        /// đơn vị:phút
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiretime"></param>
        /// <param name="withPrefix"></param>
        /// <returns></returns>
        public static async Task KeyExpireAsync(string key, int expiretime, bool withPrefix = true)
        {
            if (withPrefix)
            {
                key = AddPrefixOfKey(RedisKey.OriginKeyPrefix, key);
            }
            TimeSpan span = DateTime.Now.AddMinutes(expiretime) - DateTime.Now;
            await SetKeyExpireAsync(key, span);
        }
    }


    /// <summary>
    /// Thời gian bộ nhớ đệm, đơn vị giây
    /// </summary>
    public class RedisCacheTime
    {
        /// <summary>
        /// Một giờ
        /// </summary>
        public const int Hour = 3600;

        /// <summary>
        /// Một ngày
        /// </summary>
        public const int Day = 86400;

        /// <summary>
        /// Một tuần
        /// </summary>
        public const int Week = 604800;

        /// <summary>
        /// Một tháng (30 ngày)
        /// </summary>
        public const int Month = 2592000;

        /// <summary>
        /// 1phút
        /// </summary>
        public const int Minute1 = 60;

        /// <summary>
        /// 5phút
        /// </summary>
        public const int Minute5 = 300;

        /// <summary>
        /// 10 phút
        /// </summary>
        public const int Minute10 = 600;

        /// <summary>
        /// 30phút
        /// </summary>
        public const int HalfHour = 1800;

        /// <summary>
        /// Hết hạn khi giờ hiện tại kết thúc
        /// </summary>
        /// <returns></returns>
        public static int NowHour
        {
            get
            {
                DateTime now = DateTime.Now;
                int endSecond = (now.Hour + 1) * 60 * 60;
                int nowSecond = (now.Hour * 60 * 60) + (now.Minute * 60) + now.Second;
                return endSecond - nowSecond;
            }
        }

        /// <summary>
        /// Hết hạn khi ngày hiện tại kết thúc
        /// </summary>
        /// <returns></returns>
        public static int NowDay
        {
            get
            {
                DateTime now = DateTime.Now;
                int endSecond = 24 * 60 * 60;
                int nowSecond = (now.Hour * 60 * 60) + (now.Minute * 60) + now.Second;
                return endSecond - nowSecond;
            }
        }

        /// <summary>
        /// Hết hạn khi tháng hiện tại kết thúc
        /// </summary>
        /// <returns></returns>
        public static int NowMonth
        {
            get
            {
                DateTime now = DateTime.Now;
                int days = DateTime.DaysInMonth(now.Year, now.Month); // Số ngày trong tháng hiện tại
                int endSecond = days * 24 * 60 * 60;
                int nowSecond = (now.Day * 24 * 60 * 60) + (now.Minute * 60) + now.Second;
                return endSecond - nowSecond;
            }
        }
    }

    public class RedisKey
    {
        /// <summary>
        /// Tiền tố mặc định
        /// </summary>
        public const string OriginKeyPrefix = "OHIS";
        /// <summary>
        /// Người dùng đăng nhập hệ thống ứng dụng
        /// </summary>
        public const string LoginUserPrefix = "AppUser";
        /// <summary>
        /// Người dùng đăng nhập một lần
        /// </summary>
        public const string SsoLoginUserPrefix = "SSOAuthUser";
        public const string SsoPrefix = "SSOAuth";
        /// <summary>
        /// Số phòng phòng khám từ xa
        /// </summary>
        public const string TreatedRoomNo = "TreatedRoomNo";
        /// <summary>
        /// Thông tin phòng khám từ xa
        /// </summary>
        public const string TreatedRoom = "TreatedRoom:{0}";
        /// <summary>
        /// Bộ nhớ đệm Token phòng khám đám mây
        /// </summary>
        public const string TokenOfOhClinic = $"{OriginKeyPrefix}:Clinic:Token";

        #region Method
        /// <summary>
        /// Hội nghị hội chẩn phân biệt tổ chức
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public static string TreatedRoomOrg(string orgId)
        {
            return string.Format(TreatedRoom, orgId);
        }


        public static string AccountInfo(string account)
        {
            return string.Concat(OriginKeyPrefix, "account_", account);
        }

        #endregion
    }
}
