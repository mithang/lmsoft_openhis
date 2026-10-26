using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Model.Authentication;
using NewtouchHIS.Lib.Redis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NewtouchHIS.Lib.Services
{
    /// <summary>
    /// jwtdịch vụ
    /// </summary>
    public abstract class JWTBaseService 
    {
        protected readonly JWTOptions _jWTOptions;
        public JWTBaseService(IOptionsMonitor<JWTOptions> jwtOptions)
        {
            _jWTOptions = jwtOptions.CurrentValue; 
        }

        /// <summary>
        /// lấy mã ủy quyền
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public BusResult<string> GetCode(string clientId, string userName, string password)
        {
            BusResult<string> result = new BusResult<string>();

            string code = string.Empty;
            SSOAuthAppItem appItem = _jWTOptions.SSOAuthApp?.Where(s => s.AppId == clientId).FirstOrDefault();
            if (appItem == null)
            {
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "应用未授权" };
            }

            //thông tin người dùng
            UserIdentity UserIdentity = new UserIdentity
            {
                UserId = "101",
                Account = "admin",
                UserName = "张三"
            };

            //tạo mã ủy quyền
            code = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
            string key = $"AuthCode:{code}";
            string appCachekey = $"AuthCodeClientId:{code}";
            //bộ nhớ đệm mã ủy quyền
            _cachelper.SetCache<UserIdentity>(key, UserIdentity, TimeSpan.FromMinutes(10));
            //mã ủy quyền trong bộ nhớ đệm thuộc ứng dụng nào
            _cachelper.SetCache(appCachekey, appItem.AppId, TimeSpan.FromMinutes(10));
            //tạo phiên toàn cục
            string sessionCode = $"SessionCode:{code}";
            //SessionCodeUser sessionCodeUser = new SessionCodeUser
            //{
            //    expiresTime = DateTime.Now.AddHours(1),
            //    currentUser = UserIdentity
            //};
            //_cachelper.SetCache<UserIdentity>(sessionCode, UserIdentity, TimeSpan.FromDays(1));
            //thời gian hết hạn phiên toàn cục
            string sessionExpiryKey = $"SessionExpiryKey:{code}";
            DateTime sessionExpirTime = DateTime.Now.AddDays(1);
            _cachelper.SetCache<DateTime>(sessionExpiryKey, sessionExpirTime, TimeSpan.FromDays(1));
            Console.WriteLine($"登录成功，全局会话code:{code}");
            //lấy mã ủy quyền từ bộ nhớ đệmtokenthời gian hiệu lực dài nhất khi
            _cachelper.SetCache<DateTime>($"AuthCodeSessionTime:{code}", sessionExpirTime, TimeSpan.FromDays(1));

            //result.SetSuccess(code);
            return result;
        }
        /// <summary>
        /// theo phiêncodelấy mã ủy quyền
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="sessionCode"></param>
        /// <returns></returns>
        public async Task<BusResult<string>> GetCodeBySessionCode(string clientId, string sessionCode)
        {
            BusResult<string> result = new BusResult<string>();
            string code = string.Empty;
            SSOAuthAppItem appItem = _jWTOptions.SSOAuthApp.Where(s => s.AppId == clientId).FirstOrDefault();
            if (appItem == null)
            {
                //result.SetFail("ứng dụng không tồn tại");
                return result;
            }
            string codeKey = $"SessionCode:{sessionCode}";
            UserIdentity UserIdentity =await _cachelper.GetCache<UserIdentity>(codeKey);
            if (UserIdentity == null)
            {
                //return result.SetFail("phiên không tồn tại hoặc đã hết hạn", string.Empty);
            }

            //tạo mã ủy quyền
            code = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
            string key = $"AuthCode:{code}";
            string appCachekey = $"AuthCodeClientId:{code}";
            //bộ nhớ đệm mã ủy quyền
            _cachelper.SetCache<UserIdentity>(key, UserIdentity, TimeSpan.FromMinutes(10));
            //mã ủy quyền trong bộ nhớ đệm thuộc ứng dụng nào
            _cachelper.SetCache<string>(appCachekey, appItem.AppId, TimeSpan.FromMinutes(10));

            //lấy mã ủy quyền từ bộ nhớ đệmtokenthời gian hiệu lực dài nhất khi
            DateTime expirTime = await _cachelper.GetCache<DateTime>($"SessionExpiryKey:{sessionCode}");
            _cachelper.SetCache<DateTime>($"AuthCodeSessionTime:{code}", expirTime, expirTime - DateTime.Now);

          
            return result;

        }

        /// <summary>
        /// theo làm mớiTokenlấyToken
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public async Task<string> GetTokenByRefresh(string refreshToken, string clientId)
        {
            //làm mớiTokencó trong bộ nhớ đệm hay không
            UserIdentity UserIdentity =await _cachelper.GetCache<UserIdentity>($"RefreshToken:{refreshToken}");
            if (UserIdentity == null)
            {
                return String.Empty;
            }
            //làm mớitokenthời gian hết hạn
            DateTime refreshTokenExpiry =await _cachelper.GetCache<DateTime>($"RefreshTokenExpiry:{refreshToken}");
            //tokenthời gian mặc định là600s
            double tokenExpiry = 600;
            //nếu làm mớitokenthời gian hết hạn của chưa đến600sđã，tokenthời gian hết hạn là làm mớitokenthời gian hết hạn của
            if (refreshTokenExpiry > DateTime.Now && refreshTokenExpiry < DateTime.Now.AddSeconds(600))
            {
                tokenExpiry = (refreshTokenExpiry - DateTime.Now).TotalSeconds;
            }

            //tạo lạiToken
            string token = IssueToken(new JWTTokenRequest(), clientId, tokenExpiry);
            return token;

        }

        /// <summary>
        /// theo mã ủy quyền,lấyToken
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="appHSSetting"></param>
        /// <returns></returns>
        public async Task<BusResult<JWTToken>> GetTokenWithRefresh(string authCode)
        {
            BusResult<JWTToken> result = new BusResult<JWTToken>();

            string key = $"AuthCode:{authCode}";
            string clientIdCachekey = $"AuthCodeClientId:{authCode}";
            string AuthCodeSessionTimeKey = $"AuthCodeSessionTime:{authCode}";

            //lấy thông tin người dùng theo mã ủy quyền
            UserIdentity UserIdentity =await _cachelper.GetCache<UserIdentity>(key);
            if (UserIdentity == null)
            {
                throw new Exception("code无效");
            }
            //xóaauthCode，chỉ dùng được một lần
            _cachelper.RemoveCache(key);

            //lấy cấu hình ứng dụng
            string clientId =await _cachelper.GetCache<string>(clientIdCachekey);
            //làm mớitokenthời gian hết hạn
            DateTime sessionExpiryTime =await _cachelper.GetCache<DateTime>(AuthCodeSessionTimeKey);
            DateTime tokenExpiryTime = DateTime.Now.AddMinutes(10);//tokenthời gian hết hạn10phút
                                                                   //nếu làm mớitokencó thời hạn hết hạn ngắn hơntokenthời gian mặc định ngắn，(đem)tokenđặt thời gian hết hạn thành giống làm mớitokengiống nhau
            if (sessionExpiryTime > DateTime.Now && sessionExpiryTime < tokenExpiryTime)
            {
                tokenExpiryTime = sessionExpiryTime;
            }
            //lấy truy cậptoken
            string token = this.IssueToken(new JWTTokenRequest(), clientId, (tokenExpiryTime - DateTime.Now).TotalSeconds);


            TimeSpan refreshTokenExpiry;
            if (sessionExpiryTime != default(DateTime))
            {
                refreshTokenExpiry = sessionExpiryTime - DateTime.Now;
            }
            else
            {
                refreshTokenExpiry = TimeSpan.FromSeconds(60 * 60 * 24);//mặc định24giờ
            }
            //lấy làm mớitoken
            string refreshToken = this.IssueToken(new JWTTokenRequest(), clientId, refreshTokenExpiry.TotalSeconds);
            //làm mới bộ nhớ đệmtoken
            _cachelper.SetCache($"RefreshToken:{refreshToken}", UserIdentity, refreshTokenExpiry);
            //làm mới bộ nhớ đệmtokenthời gian hết hạn
            _cachelper.SetCache($"RefreshTokenExpiry:{refreshToken}", DateTime.Now.AddSeconds(refreshTokenExpiry.TotalSeconds), refreshTokenExpiry);
            //result.SetSuccess(new JWTToken() { Token = token });
            Console.WriteLine($"client_id:{clientId}获取token,有效期:{sessionExpiryTime.ToString("yyyy-MM-dd HH:mm:ss")},token:{token}");
            return result;
        }

        #region private
        /// <summary>
        /// cấp pháttoken
        /// </summary>
        /// <param name="userModel"></param>
        /// <param name="clientId"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        private string IssueToken(JWTTokenRequest userModel, string clientId, double second = 600)
        {
            var identity = userModel.authIdentity;
            var claims = new List<Claim>
            {
                    new Claim(IdentityClaimTypes.AppId, identity.AppId),
                    new Claim(IdentityClaimTypes.OrganizeId, identity.OrganizeId??""),
                    new Claim(IdentityClaimTypes.TopOrganizeId, identity.TopOrganizeId ?? ""),
                    new Claim(IdentityClaimTypes.Account, identity.Account??""),
                    new Claim(IdentityClaimTypes.UserCode, identity.UserCode??""),
                    new Claim(IdentityClaimTypes.UserName, identity.UserName??""),
                    new Claim(IdentityClaimTypes.UserId, identity.UserId??""),
                    new Claim(IdentityClaimTypes.TokenType, identity.TokenType??""),
                    new Claim(IdentityClaimTypes.Token, identity.Token??""),
                };
            //var appHSSetting = getAppInfoByAppKey(clientId);
            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appHSSetting.clientSecret));
            //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var creds = GetCreds(clientId);
            /**
             * Claims (Payload)
                Claims một phần bao gồm một số thông tin liên quan đến token thông tin quan trọng liên quan。 JWT tiêu chuẩn quy định một số trường，dưới đây trích một số trường:
                iss: The issuer of the token，chủ thể cấp phát，ai cấp
                sub: The subject of the token，token chủ đề
                aud: đối tượng nhận，cho ai
                exp: Expiration Time。 token thời gian hết hạn，Unix định dạng dấu thời gian
                iat: Issued At。 token thời gian tạo， Unix định dạng dấu thời gian
                jti: JWT ID。đối với hiện tại token định danh duy nhất của
                ngoài các trường đã quy định，có thể bao gồm bất kỳ nội dung nào khác JSON trường tương thích。
             * */
            var token = new JwtSecurityToken(
                issuer: "SSOCenter", //ai cấp
                audience: clientId, //cho ai
                claims: claims,
                expires: DateTime.Now.AddSeconds(second),//tokenthời hạn hiệu lực
                notBefore: null,//có hiệu lực ngay  DateTime.Now.AddMilliseconds(30),//30shiệu lực sau đó
                signingCredentials: creds);
            string returnToken = new JwtSecurityTokenHandler().WriteToken(token);
            return returnToken;
        }

        /// <summary>
        /// theoappKeylấy thông tin ứng dụng
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        private SSOAuthAppItem getAppInfoByAppKey(string clientId)
        {
            return _jWTOptions.SSOAuthApp.Where(s => s.AppId == clientId).FirstOrDefault();
        }
        /// <summary>
        /// lấy phương thức mã hóa
        /// </summary>
        /// <returns></returns>
        protected abstract SigningCredentials GetCreds(string clientId);

        #endregion
    }
}
