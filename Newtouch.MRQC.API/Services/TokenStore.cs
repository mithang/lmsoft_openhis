using System;
using Microsoft.Extensions.Configuration;
using Newtouch.MRQC.API.Envelope;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Newtouch.MRQC.API.Services
{
    /// <summary>
    /// Tra cứu identity từ Redis, đúng semantics gốc (ApiControllerBaseEx):
    /// token là key Redis, value là JSON identity; resolve thành công thì gia hạn 20 phút.
    /// Nếu Redis không tới được, trả null (endpoint sẽ trả lỗi auth tường minh, không 500).
    ///
    /// LƯU Ý VỀ TOKENDECRYPT: gốc đăng ký toàn cục TokenDecryptFilterAttribute để giải mã
    /// token trước khi Authorize; thuật toán giải mã nằm trong DLL API.Common, không có
    /// source. Bản port này dùng token thô làm key Redis. Client nào gửi token đã mã hóa
    /// DES sẽ không resolve được và nhận TOKEN_IS_EXPIRED — đây là giới hạn đã biết,
    /// ghi rõ ở README, không tự bịa thuật toán giải mã.
    /// (MRQC không có endpoint UA GetUserInfo kiểu CIS nên không cần tra cứu thô riêng.)
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

        /// <summary>
        /// Cấp token: mint GUID thô làm key Redis, value là JSON identity, TTL
        /// 20 phút (GetIdentity gia hạn 20 phút mỗi lần đọc thành công — trượt).
        /// Trả null khi Redis không ghi được; caller phải từ chối cấp token.
        /// </summary>
        public string SaveIdentity(Identity identity)
        {
            var db = DbOrNull();
            if (db == null) return null;
            try
            {
                var token = Guid.NewGuid().ToString();
                if (!db.StringSet(token, JsonConvert.SerializeObject(identity), TimeSpan.FromMinutes(20)))
                    return null;
                return token;
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
    }
}
