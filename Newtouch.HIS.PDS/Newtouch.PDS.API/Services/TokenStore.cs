using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtouch.PDS.API.Envelope;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;

namespace Newtouch.PDS.API.Services
{
    /// <summary>
    /// Tra cứu identity từ Redis, đúng semantics gốc (ApiControllerBaseEx.GetIdentity):
    /// token là key Redis, value là JSON identity; resolve thành công thì gia hạn 20 phút.
    /// Nếu Redis không tới được, trả null (endpoint sẽ trả lỗi auth tường minh, không 500).
    ///
    /// LƯU Ý VỀ TOKENDECRYPT: gốc đăng ký toàn cục TokenDecryptFilterAttribute (WebApiConfig)
    /// để giải mã token trước khi Authorize; thuật toán giải mã nằm trong DLL API.Common,
    /// không có source. Bản port này dùng token thô làm key Redis — giống mọi API anh em
    /// đã port. Client nào gửi token đã mã hóa DES sẽ không resolve được và nhận
    /// TOKEN_IS_EXPIRED — đây là giới hạn đã biết, ghi rõ ở README, không tự bịa thuật
    /// toán giải mã.
    /// </summary>
    public class TokenStore
    {
        private readonly string _connection;
        private readonly IConfiguration _config;
        private Lazy<ConnectionMultiplexer> _mux;

        public TokenStore(IConfiguration config)
        {
            _config = config;
            _connection = config["RedisConfig:Connection"];
            _mux = new Lazy<ConnectionMultiplexer>(() =>
                ConnectionMultiplexer.Connect(_connection));
        }

        private IDatabase DbOrNull(int db = 0)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_connection)) return null;
                var mux = _mux.Value;
                if (!mux.IsConnected) return null;
                return mux.GetDatabase(db);
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

        /// <summary>BA-002: xác thực JWT do AuthCenter (8015) cấp cho caller
        /// nội bộ (CIS/Sett gửi qua header Bearer — xem ApiControllerBase).
        ///
        /// VÌ SAO KHÔNG KIỂM CHỮ KÝ Ở ĐÂY: JWT được ký bằng AppSecret của từng
        /// app, chỉ AuthCenter giữ; PDS.API không có secret đó (cấp secret mới
        /// cho PDS là quyết định phân phối secret — chưa có, KHÔNG tự bịa giá
        /// trị vào config). Thay vào đó, bằng chứng cấp phát là CHÍNH CHUỖI
        /// JWT ĐANG NẰM TRONG REDIS CỦA AUTHCENTER: HSJWTService.GetTokenAsync
        /// cache JWT đã cấp dưới key
        ///   OHIS:Org_{org}:SSOAuthUser{WebApi|Web}_{AppId}_{Account|"App"}
        /// (org trống → "Org*:", prefix OHIS do RedisHelper.AddPrefixOfKey,
        /// DB 1 theo RedisConfig của AuthCenter) với TTL = ExpireHours. Chỉ
        /// AuthCenter mới ghi được key đó (muốn ghi phải qua GetAppToken/
        /// GetUserToken — cần AppSecret của app đã đăng ký — hoặc ghi thẳng
        /// Redis, mà kẻ đã ghi được Redis thì vốn đã tự mint được token GUID
        /// của TokenStore nên không thêm mặt tấn công mới). Vì vậy: token chỉ
        /// hợp lệ khi (1) đúng dạng JWT 3 đoạn, (2) claim exp còn hạn (bắt
        /// buộc phải có), (3) chuỗi token khớp byte-khớp với giá trị AuthCenter
        /// đang cache (so sánh hằng-thời gian). Hết hạn/cache bị refresh đè →
        /// từ chối (fail-closed). Kiểm chứng chữ ký HS256 là bước cứng hóa
        /// tiếp theo khi có quyết định cấp AppSecret cho PDS.API — code đã
        /// chừa sẵn: nếu cấu hình AuthCenter:JwtIssuer thì claim iss phải
        /// khớp, nếu không cấu hình thì bỏ qua bước đó.</summary>
        public Identity GetIdentityFromAuthCenterJwt(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) return null;
            var parts = token.Split('.');
            if (parts.Length != 3
                || string.IsNullOrWhiteSpace(parts[0])
                || string.IsNullOrWhiteSpace(parts[1])
                || string.IsNullOrWhiteSpace(parts[2])) return null;
            JObject payload;
            try
            {
                var json = Encoding.UTF8.GetString(Base64UrlDecode(parts[1]));
                payload = JObject.Parse(json);
            }
            catch
            {
                return null;
            }
            // exp bắt buộc: JWT của AuthCenter luôn có exp (ExpireHours).
            var exp = payload["exp"]?.Value<long?>();
            if (exp == null) return null;
            try
            {
                if (DateTimeOffset.FromUnixTimeSeconds(exp.Value) <= DateTimeOffset.UtcNow) return null;
            }
            catch
            {
                return null;
            }
            // Issuer pinning (tùy chọn, fail-closed khi đã cấu hình).
            var configuredIssuer = _config?["AuthCenter:JwtIssuer"];
            if (!string.IsNullOrWhiteSpace(configuredIssuer))
            {
                var iss = payload["iss"]?.Value<string>();
                if (!string.Equals(iss, configuredIssuer, StringComparison.Ordinal)) return null;
            }
            var appId = payload["AppId"]?.Value<string>();
            if (string.IsNullOrWhiteSpace(appId)) return null;
            var account = payload["Account"]?.Value<string>() ?? "";
            var orgId = payload["OrganizeId"]?.Value<string>() ?? "";
            var acct = string.IsNullOrWhiteSpace(account) ? "App" : account;
            var candidates = new List<(string Key, string TokenType)>();
            if (!string.IsNullOrWhiteSpace(orgId))
            {
                candidates.Add(($"OHIS:Org_{orgId}:SSOAuthUserWebApi_{appId}_{acct}", "WebApi"));
                candidates.Add(($"OHIS:Org_{orgId}:SSOAuthUserWeb_{appId}_{acct}", "Web"));
            }
            else
            {
                candidates.Add(($"OHIS:Org*:SSOAuthUserWebApi_{appId}_{acct}", "WebApi"));
                candidates.Add(($"OHIS:Org*:SSOAuthUserWeb_{appId}_{acct}", "Web"));
            }
            var db1 = DbOrNull(1);
            if (db1 == null) return null;
            foreach (var (key, tokenType) in candidates)
            {
                string cached;
                try
                {
                    var val = db1.StringGet(key);
                    if (val.IsNullOrEmpty) continue;
                    cached = val.ToString();
                }
                catch
                {
                    return null;
                }
                if (FixedTimeEquals(cached, token))
                {
                    return new Identity
                    {
                        AppId = appId,
                        TokenType = tokenType,
                        UserId = payload["UserId"]?.Value<string>(),
                        Account = account,
                        UserCode = payload["UserCode"]?.Value<string>() ?? account,
                        OrganizeId = orgId,
                        TopOrganizeId = payload["TopOrganizeId"]?.Value<string>(),
                    };
                }
            }
            return null;
        }

        private static byte[] Base64UrlDecode(string input)
        {
            var s = input.Replace('-', '+').Replace('_', '/');
            switch (s.Length % 4)
            {
                case 2: s += "=="; break;
                case 3: s += "="; break;
                case 0: break;
                default: throw new FormatException("Base64Url length invalid");
            }
            return Convert.FromBase64String(s);
        }

        private static bool FixedTimeEquals(string a, string b)
        {
            var ab = Encoding.UTF8.GetBytes(a ?? "");
            var bb = Encoding.UTF8.GetBytes(b ?? "");
            if (ab.Length != bb.Length) return false;
            return CryptographicOperations.FixedTimeEquals(ab, bb);
        }
    }
}
