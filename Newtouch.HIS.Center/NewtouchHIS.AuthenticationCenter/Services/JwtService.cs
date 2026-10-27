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
    /// jwtDịch vụ
    /// </summary>
    public abstract class JWTBaseService 
    {
        protected readonly JWTOptions _jWTOptions;
        public JWTBaseService(IOptionsMonitor<JWTOptions> jwtOptions)
        {
            _jWTOptions = jwtOptions.CurrentValue; 
        }

        /// <summary>
        /// Lấy mã ủy quyền
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
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "Ứng dụng chưa được cấp quyền" };
            }

            //Thông tin người dùng
            UserIdentity UserIdentity = new UserIdentity
            {
                UserId = "101",
                Account = "admin",
                UserName = "Trương Tam"
            };

            //Tạo mã ủy quyền
            code = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
            string key = $"AuthCode:{code}";
            string appCachekey = $"AuthCodeClientId:{code}";
            //Lưu mã ủy quyền vào cache
            _cachelper.SetCache<UserIdentity>(key, UserIdentity, TimeSpan.FromMinutes(10));
            //Lưu mã ủy quyền thuộc về ứng dụng nào
            _cachelper.SetCache(appCachekey, appItem.AppId, TimeSpan.FromMinutes(10));
            //Tạo phiên toàn cục
            string sessionCode = $"SessionCode:{code}";
            //SessionCodeUser sessionCodeUser = new SessionCodeUser
            //{
            //    expiresTime = DateTime.Now.AddHours(1),
            //    currentUser = UserIdentity
            //};
            //_cachelper.SetCache<UserIdentity>(sessionCode, UserIdentity, TimeSpan.FromDays(1));
            //Thời gian hết hạn phiên toàn cục
            string sessionExpiryKey = $"SessionExpiryKey:{code}";
            DateTime sessionExpirTime = DateTime.Now.AddDays(1);
            _cachelper.SetCache<DateTime>(sessionExpiryKey, sessionExpirTime, TimeSpan.FromDays(1));
            Console.WriteLine($"Đăng nhập thành công,Phiên toàn cụccode_prefix={(code?.Length >= 8 ? code.Substring(0, 8) : code)}"); // SECURITY (G7): khong ghi ma phien day du vao log
            //Lấy mã ủy quyền từ cachetokenthời gian hiệu lực dài nhất
            _cachelper.SetCache<DateTime>($"AuthCodeSessionTime:{code}", sessionExpirTime, TimeSpan.FromDays(1));

            //result.SetSuccess(code);
            return result;
        }
        /// <summary>
        /// Theo phiêncodeLấy mã ủy quyền
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
                //result.SetFail("Ứng dụng không tồn tại");
                return result;
            }
            string codeKey = $"SessionCode:{sessionCode}";
            UserIdentity UserIdentity =await _cachelper.GetCache<UserIdentity>(codeKey);
            if (UserIdentity == null)
            {
                //return result.SetFail("Phiên không tồn tại hoặc đã hết hạn", string.Empty);
            }

            //Tạo mã ủy quyền
            code = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
            string key = $"AuthCode:{code}";
            string appCachekey = $"AuthCodeClientId:{code}";
            //Lưu mã ủy quyền vào cache
            _cachelper.SetCache<UserIdentity>(key, UserIdentity, TimeSpan.FromMinutes(10));
            //Lưu mã ủy quyền thuộc về ứng dụng nào
            _cachelper.SetCache<string>(appCachekey, appItem.AppId, TimeSpan.FromMinutes(10));

            //Lấy mã ủy quyền từ cachetokenthời gian hiệu lực dài nhất
            DateTime expirTime = await _cachelper.GetCache<DateTime>($"SessionExpiryKey:{sessionCode}");
            _cachelper.SetCache<DateTime>($"AuthCodeSessionTime:{code}", expirTime, expirTime - DateTime.Now);

          
            return result;

        }

        /// <summary>
        /// Theo làm mớiTokenLấyToken
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public async Task<string> GetTokenByRefresh(string refreshToken, string clientId)
        {
            //làm mớiTokenCó trong cache hay không
            UserIdentity UserIdentity =await _cachelper.GetCache<UserIdentity>($"RefreshToken:{refreshToken}");
            if (UserIdentity == null)
            {
                return String.Empty;
            }
            //làm mớitokenThời gian hết hạn
            DateTime refreshTokenExpiry =await _cachelper.GetCache<DateTime>($"RefreshTokenExpiry:{refreshToken}");
            //tokenThời gian mặc định là600s
            double tokenExpiry = 600;
            //Nếu refresh tokentokencó thời gian hết hạn chưa tới600s,tokenthời gian hết hạn bằng thời gian làm mớitokenthời gian hết hạn
            if (refreshTokenExpiry > DateTime.Now && refreshTokenExpiry < DateTime.Now.AddSeconds(600))
            {
                tokenExpiry = (refreshTokenExpiry - DateTime.Now).TotalSeconds;
            }

            //Tạo lạiToken
            string token = IssueToken(new JWTTokenRequest(), clientId, tokenExpiry);
            return token;

        }

        /// <summary>
        /// Theo mã ủy quyền,LấyToken
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

            //Lấy thông tin người dùng theo mã ủy quyền
            UserIdentity UserIdentity =await _cachelper.GetCache<UserIdentity>(key);
            if (UserIdentity == null)
            {
                throw new Exception("codekhông hợp lệ");
            }
            //XóaauthCode,chỉ dùng được một lần
            _cachelper.RemoveCache(key);

            //Lấy cấu hình ứng dụng
            string clientId =await _cachelper.GetCache<string>(clientIdCachekey);
            //làm mớitokenThời gian hết hạn
            DateTime sessionExpiryTime =await _cachelper.GetCache<DateTime>(AuthCodeSessionTimeKey);
            DateTime tokenExpiryTime = DateTime.Now.AddMinutes(10);//tokenThời gian hết hạn10phút
                                                                   //Nếu refresh tokentokencó thời hạn ngắn hơntokenthời gian mặc định,tokenđặt thời gian hết hạn bằng thời gian làm mớitokennhư nhau
            if (sessionExpiryTime > DateTime.Now && sessionExpiryTime < tokenExpiryTime)
            {
                tokenExpiryTime = sessionExpiryTime;
            }
            //Lấy truy cậptoken
            string token = this.IssueToken(new JWTTokenRequest(), clientId, (tokenExpiryTime - DateTime.Now).TotalSeconds);


            TimeSpan refreshTokenExpiry;
            if (sessionExpiryTime != default(DateTime))
            {
                refreshTokenExpiry = sessionExpiryTime - DateTime.Now;
            }
            else
            {
                refreshTokenExpiry = TimeSpan.FromSeconds(60 * 60 * 24);//Mặc định24giờ
            }
            //Lấy làm mớitoken
            string refreshToken = this.IssueToken(new JWTTokenRequest(), clientId, refreshTokenExpiry.TotalSeconds);
            //Lưu refresh token vào cachetoken
            _cachelper.SetCache($"RefreshToken:{refreshToken}", UserIdentity, refreshTokenExpiry);
            //Lưu refresh token vào cachetokenThời gian hết hạn
            _cachelper.SetCache($"RefreshTokenExpiry:{refreshToken}", DateTime.Now.AddSeconds(refreshTokenExpiry.TotalSeconds), refreshTokenExpiry);
            //result.SetSuccess(new JWTToken() { Token = token });
            Console.WriteLine($"client_id:{clientId}Lấytoken,Thời hạn hiệu lực:{sessionExpiryTime.ToString("yyyy-MM-dd HH:mm:ss")},token_present={!string.IsNullOrEmpty(token)},token_prefix={(token?.Length >= 8 ? token.Substring(0, 8) : token)}"); // SECURITY (G7): khong ghi JWT day du vao log
            return result;
        }

        #region private
        /// <summary>
        /// Cấp pháttoken
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
                Claims Phần này chứa một số thông tin quan trọng liên quan tới token này. Tiêu chuẩn JWT quy định một số trường, dưới đây trích một số trường:
                iss: The issuer of the token,Chủ thể cấp phát,ai cấp
                sub: The subject of the token,token Chủ đề
                aud: Đối tượng nhận,cấp cho ai
                exp: Expiration Time. token Thời gian hết hạn,Unix Định dạng timestamp
                iat: Issued At. token Thời gian tạo, Unix Định dạng timestamp
                jti: JWT ID.Đối với token mã định danh duy nhất
                Ngoài các trường đã quy định,có thể chứa bất kỳ trường tương thích JSON nào khác.
             * */
            var token = new JwtSecurityToken(
                issuer: "SSOCenter", //ai cấp
                audience: clientId, //cấp cho ai
                claims: claims,
                expires: DateTime.Now.AddSeconds(second),//tokenThời hạn hiệu lực
                notBefore: null,//Có hiệu lực ngay  DateTime.Now.AddMilliseconds(30),//30ssau mới có hiệu lực
                signingCredentials: creds);
            string returnToken = new JwtSecurityTokenHandler().WriteToken(token);
            return returnToken;
        }

        /// <summary>
        /// TheoappKeyLấy thông tin ứng dụng
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        private SSOAuthAppItem getAppInfoByAppKey(string clientId)
        {
            return _jWTOptions.SSOAuthApp.Where(s => s.AppId == clientId).FirstOrDefault();
        }
        /// <summary>
        /// Lấy phương thức mã hóa
        /// </summary>
        /// <returns></returns>
        protected abstract SigningCredentials GetCreds(string clientId);

        #endregion
    }
}
