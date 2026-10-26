using Newtouch.Tools.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MRQC.Infrastructure
{
    public class HttpTpClientHelper
    {
        /// <summary>
        /// lấy bên thứ ba jwt token xác thực danh tính
        /// </summary>
        /// <param name="data"></param>
        /// <param name="uri"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string GetBearerTokenPost(string data, string uri, string token)
        {
            token = $"Bearer {token}";
            return HttpMethods.HttpPostWithToken(uri, data, token);
        }
    }
}
