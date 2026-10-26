using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Redis;
using NewtouchHIS.Lib.Services;
using System.Security.Cryptography;

namespace NewtouchHIS.AuthenticationCenter.Services
{
    public class RSJWTService
    {
        public RSJWTService(IOptionsMonitor<JWTOptions> jwtOptions) 
        {

        }
        /// <summary>
        /// tạo chứng chỉ chữ ký mã hóa bất đối xứng
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        protected SigningCredentials GetCreds(string clientId)
        {
            var appRSSetting = getAppInfoByAppKey(clientId);
            var rsa = RSA.Create();
            byte[] privateKey = Convert.FromBase64String(appRSSetting.AppSecret);//ở đây chỉ cần khóa riêng，khôngbegin,khôngend
            rsa.ImportPkcs8PrivateKey(privateKey, out _);
            var key = new RsaSecurityKey(rsa);
            var creds = new SigningCredentials(key, SecurityAlgorithms.RsaSha256);
            return creds;
        }
        /// <summary>
        /// theoappKeylấy thông tin ứng dụng
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        private SSOAuthAppItem getAppInfoByAppKey(string clientId)
        {
            return _jWTOptions.SSOAuthApp?.Where(s => s.AppId == clientId).FirstOrDefault();
        }
    }
}
