using System;
using System.Collections.Concurrent;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// Minimal cache-aside contract replacing Newtouch.Core.Redis.Cache.ICache.
    /// Only the Get(key, factory) shape used by Base repos is ported; the
    /// implementation is process-local (single-instance demo). Cross-app shared
    /// caching can be reintroduced later on top of RedisService.
    /// </summary>
    public interface ICache
    {
        T Get<T>(string key, Func<T> factory);
    }

    public class MemoryICache : ICache
    {
        private readonly ConcurrentDictionary<string, object> _store = new ConcurrentDictionary<string, object>();

        public T Get<T>(string key, Func<T> factory)
        {
            if (_store.TryGetValue(key, out var v) && v is T typed)
            {
                return typed;
            }
            var fresh = factory();
            _store[key] = fresh;
            return fresh;
        }
    }
}
