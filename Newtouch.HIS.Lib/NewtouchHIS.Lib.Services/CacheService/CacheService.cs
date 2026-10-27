using Microsoft.Extensions.Caching.Distributed;
using NewtouchHIS.Lib.Base.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NewtouchHIS.Lib.Base.BaseEnum;

namespace NewtouchHIS.Lib.Services.CacheService
{
    public interface ICacheService
    {
        #region Thiết lập bộ nhớ đệm 
        /// <summary>
        /// Thiết lập bộ nhớ đệm
        /// </summary>
        /// <param name="key">Key bộ nhớ đệm</param>
        /// <param name="value">giá trị</param>
        void SetCache(string key, object value);
        /// <summary>
        /// Thiết lập bộ nhớ đệm
        /// </summary>
        /// <param name="key">Key bộ nhớ đệm</param>
        /// <param name="value">giá trị</param>
        Task SetCacheAsync(string key, object value);

        /// <summary>
        /// Thiết lập bộ nhớ đệm
        /// Lưu ý: kiểu hết hạn mặc định là hết hạn tuyệt đối
        /// </summary>
        /// <param name="key">Key bộ nhớ đệm</param>
        /// <param name="value">giá trị</param>
        /// <param name="timeout">Khoảng thời gian hết hạn</param>
        void SetCache(string key, object value, TimeSpan timeout);

        /// <summary>
        /// Thiết lập bộ nhớ đệm
        /// Lưu ý: kiểu hết hạn mặc định là hết hạn tuyệt đối
        /// </summary>
        /// <param name="key">Key bộ nhớ đệm</param>
        /// <param name="value">giá trị</param>
        /// <param name="timeout">Khoảng thời gian hết hạn</param>
        Task SetCacheAsync(string key, object value, TimeSpan timeout);

        /// <summary>
        /// Thiết lập bộ nhớ đệm
        /// Lưu ý: kiểu hết hạn mặc định là hết hạn tuyệt đối
        /// </summary>
        /// <param name="key">Key bộ nhớ đệm</param>
        /// <param name="value">giá trị</param>
        /// <param name="timeout">Khoảng thời gian hết hạn</param>
        /// <param name="expireType">Loại hết hạn</param>  
        void SetCache(string key, object value, TimeSpan timeout, ExpireType expireType);

        /// <summary>
        /// Thiết lập bộ nhớ đệm
        /// Lưu ý: kiểu hết hạn mặc định là hết hạn tuyệt đối
        /// </summary>
        /// <param name="key">Key bộ nhớ đệm</param>
        /// <param name="value">giá trị</param>
        /// <param name="timeout">Khoảng thời gian hết hạn</param>
        /// <param name="expireType">Loại hết hạn</param>  
        Task SetCacheAsync(string key, object value, TimeSpan timeout, ExpireType expireType);
        #endregion

        #region Lấy bộ nhớ đệm

        /// <summary>
        /// Lấy bộ nhớ đệm
        /// </summary>
        /// <param name="key">Key bộ nhớ đệm</param>
        string GetCache(string key);

        /// <summary>
        /// Lấy bộ nhớ đệm
        /// </summary>
        /// <param name="key">Key bộ nhớ đệm</param>
        Task<string> GetCacheAsync(string key);
        /// <summary>
        /// Lấy bộ nhớ đệm
        /// </summary>
        /// <param name="key">Key bộ nhớ đệm</param>
        T GetCache<T>(string key);
        /// <summary>
        /// Lấy bộ nhớ đệm
        /// </summary>
        /// <param name="key">Key bộ nhớ đệm</param>
        Task<T> GetCacheAsync<T>(string key);

        #endregion

        #region Xóa bộ nhớ đệm

        /// <summary>
        /// Xóa bộ nhớ đệm
        /// </summary>
        /// <param name="key">Key bộ nhớ đệm</param>
        void RemoveCache(string key);

        /// <summary>
        /// Xóa bộ nhớ đệm
        /// </summary>
        /// <param name="key">Key bộ nhớ đệm</param>
        Task RemoveCacheAsync(string key);

        #endregion

        #region Làm mới bộ nhớ đệm
        /// <summary>
        /// Làm mới bộ nhớ đệm
        /// </summary>
        /// <param name="key">Key bộ nhớ đệm</param>
        void RefreshCache(string key);
        /// <summary>
        /// Làm mới bộ nhớ đệm
        /// </summary>
        /// <param name="key">Key bộ nhớ đệm</param>
        Task RefreshCacheAsync(string key);
        #endregion
    }



    public class CacheService : ICacheService
    {

        readonly IDistributedCache _cache;

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        protected string BuildKey(string idKey)
        {
            return $"OHIS_{GetType().FullName}_{idKey}";
        }
        public void SetCache(string key, object value)
        {
            string cacheKey = BuildKey(key);
            _cache.SetString(cacheKey, value.ToJson());
        }

        public async Task SetCacheAsync(string key, object value)
        {
            string cacheKey = BuildKey(key);
            await _cache.SetStringAsync(cacheKey, value.ToJson());
        }

        public void SetCache(string key, object value, TimeSpan timeout)
        {
            string cacheKey = BuildKey(key);
            _cache.SetString(cacheKey, value.ToJson(), new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = new DateTimeOffset(DateTime.Now + timeout)
            });
        }

        public async Task SetCacheAsync(string key, object value, TimeSpan timeout)
        {
            string cacheKey = BuildKey(key);
            await _cache.SetStringAsync(cacheKey, value.ToJson(), new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = new DateTimeOffset(DateTime.Now + timeout)
            });
        }

        public void SetCache(string key, object value, TimeSpan timeout, ExpireType expireType)
        {
            string cacheKey = BuildKey(key);
            if (expireType == ExpireType.Absolute)
            {
                //Ở đây chưa chuyển đổi thời gian chuẩn, múi giờ Linux sẽ có vấn đề?
                _cache.SetString(cacheKey, value.ToJson(), new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = new DateTimeOffset(DateTime.Now + timeout)
                });
            }
            else
            {
                _cache.SetString(cacheKey, value.ToJson(), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = timeout
                });
            }
        }

        public async Task SetCacheAsync(string key, object value, TimeSpan timeout, ExpireType expireType)
        {
            string cacheKey = BuildKey(key);
            if (expireType == ExpireType.Absolute)
            {
                //Ở đây chưa chuyển đổi thời gian chuẩn, múi giờ Linux sẽ có vấn đề?
                await _cache.SetStringAsync(cacheKey, value.ToJson(), new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = new DateTimeOffset(DateTime.Now + timeout)
                });
            }
            else
            {
                await _cache.SetStringAsync(cacheKey, value.ToJson(), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = timeout
                });
            }
        }

        public string GetCache(string idKey)
        {
            if (string.IsNullOrEmpty(idKey))
            {
                return null;
            }
            string cacheKey = BuildKey(idKey);
            var cache = _cache.GetString(cacheKey);
            return cache;
        }
        public async Task<string> GetCacheAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }
            string cacheKey = BuildKey(key);
            var cache = await _cache.GetStringAsync(cacheKey);
            return cache;
        }

        public T GetCache<T>(string key)
        {
            var cache = GetCache(key);
            if (!string.IsNullOrEmpty(cache))
            {
                return cache.ToObject<T>();
            }
            return default(T);
        }

        public async Task<T> GetCacheAsync<T>(string key)
        {
            var cache = await GetCacheAsync(key);
            if (!string.IsNullOrEmpty(cache))
            {
                return cache.ToObject<T>();
            }
            return default(T);
        }

        public void RemoveCache(string key)
        {
            _cache.Remove(BuildKey(key));
        }

        public async Task RemoveCacheAsync(string key)
        {
            await _cache.RemoveAsync(BuildKey(key));
        }

        public void RefreshCache(string key)
        {
            _cache.Refresh(BuildKey(key));
        }

        public async Task RefreshCacheAsync(string key)
        {
            await _cache.RefreshAsync(BuildKey(key));
        }


    }
}
