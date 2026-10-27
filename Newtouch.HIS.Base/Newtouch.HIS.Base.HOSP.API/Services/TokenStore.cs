using System;
using Microsoft.Extensions.Configuration;
using Newtouch.HIS.Base.HOSP.API.Envelope;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Newtouch.HIS.Base.HOSP.API.Services
{
    /// <summary>
    /// Tra cứu/lưu identity từ Redis, đúng semantics gốc: token là key Redis,
    /// value là JSON identity; resolve thành công thì gia hạn 20 phút.
    /// Nếu Redis không tới được, trả null (endpoint sẽ trả lỗi auth tường minh,
    /// không 500). Token dùng thô làm key (xem ghi chú TokenDecrypt ở base).
    /// </summary>
    public class TokenStore
    {
        private readonly string _connection;
        private Lazy<ConnectionMultiplexer> _mux;

        public TokenStore(IConfiguration config)
        {
            _connection = config["RedisConfig:Connection"];
            _mux = new Lazy<ConnectionMultiplexer>(() =>
                ConnectionMultiplexer.Connect(_connection));
        }

        private IDatabase DbOrNull()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_connection)) return null;
                var mux = _mux.Value;
                if (!mux.IsConnected) return null;
                return mux.GetDatabase(0);
            }
            catch
            {
                return null;
            }
        }

        public Identity GetIdentity(string token, string tokenType = null)
        {
            if (string.IsNullOrWhiteSpace(token)) return null;
            var db = DbOrNull();
            if (db == null) return null;
            try
            {
                var val = db.StringGet(token);
                if (val.IsNullOrEmpty) return null;
                var identity = JsonConvert.DeserializeObject<Identity>(val);
                db.KeyExpire(token, TimeSpan.FromMinutes(20));
                return identity;
            }
            catch
            {
                return null;
            }
        }

        public bool SaveIdentity(string token, Identity identity, TimeSpan expiry)
        {
            var db = DbOrNull();
            if (db == null) return false;
            try
            {
                return db.StringSet(token, JsonConvert.SerializeObject(identity), expiry);
            }
            catch
            {
                return false;
            }
        }
    }
}
