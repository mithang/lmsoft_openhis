using System;
using Microsoft.Extensions.Configuration;
using Newtouch.CIS.API.Envelope;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Newtouch.CIS.API.Services
{
    /// <summary>
    /// Tra cứu identity từ Redis, đúng semantics gốc (ApiControllerBaseEx):
    /// token là key Redis, value là JSON identity; resolve thành công thì gia hạn 20 phút.
    /// Nếu Redis không tới được, trả null (endpoint sẽ trả lỗi auth tường minh, không 500).
    ///
    /// LƯU Ý VỀ TOKENDECRYPT: gốc đăng ký toàn cục TokenDecryptFilterAttribute để giải mã
    /// token trước khi Authorize (trừ action [IgnoreTokenDecrypt]); thuật toán giải mã nằm
    /// trong DLL API.Common, không có source. Bản port này dùng token thô làm key Redis.
    /// Client nào gửi token đã mã hóa DES sẽ không resolve được và nhận TOKEN_IS_EXPIRED —
    /// đây là giới hạn đã biết, ghi rõ ở README, không tự bịa thuật toán giải mã.
    /// UA GetUserInfo vốn đã dùng token thô (IgnoreTokenDecrypt) nên không bị ảnh hưởng.
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

        /// <summary>Tra cứu thô cho UA GetUserInfo (đúng code gốc: StringGet + gia hạn 120 phút).</summary>
        public (string Account, string OrganizeId) GetUaUser(string token)
        {
            var db = DbOrNull();
            if (db == null) return (null, null);
            try
            {
                var val = db.StringGet(token);
                if (val.IsNullOrEmpty || val == "SIDELINED") return (null, null);
                var jObj = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(val);
                string account = null, orgId = null;
                foreach (var prop in jObj.Properties())
                {
                    if (prop.Name == "UserCode") account = prop.Value.ToString();
                    else if (prop.Name == "OrganizeId") orgId = prop.Value.ToString();
                }
                if (string.IsNullOrWhiteSpace(account)) return (null, null);
                db.KeyExpire(token, TimeSpan.FromMinutes(120));
                return (account, orgId);
            }
            catch
            {
                return (null, null);
            }
        }
    }
}
