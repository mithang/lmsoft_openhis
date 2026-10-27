using NewtouchHIS.Lib.Base.Security;

namespace NewtouchHIS.Lib.Services.AuthSecurity
{
    public class AuthSecurityServices
    {
        /// <summary>
        /// Tạo thông tin ủy quyền ứng dụngkey
        /// </summary>
        /// <param name="keyStr"></param>
        /// <returns></returns>
        public static string AppAuthKeyGen(string keyStr)
        {
            var md5 = MD5Encrypt.Encrypt(keyStr);
            return DESEncrypt.Encrypt(keyStr, md5);
        }
    }
}
