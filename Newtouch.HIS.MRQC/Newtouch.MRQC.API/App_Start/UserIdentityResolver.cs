using Newtouch.MRQC.API.Models;
using FrameworkBase.API;
using Newtouch.HIS.API.Common.Interface;
using Newtouch.HIS.API.Common.Models;

namespace Newtouch.MRQC.API.App_Start
{
    /// <summary>
    /// phân tích danh tính người dùng
    /// </summary>
    public class UserIdentityResolver : IUserIdentityResolver<Identity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Identity GetIdentity(string token)
        {
            var identity = ApiControllerBaseEx.GetIdentity<HIS.API.Common.Models.UserIdentity>(token);
            return identity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="tokenType"></param>
        /// <returns></returns>
        public Identity GetIdentity(string token, string tokenType)
        {
            var identity = ApiControllerBaseEx.GetIdentity<HIS.API.Common.Models.UserIdentity>(token, tokenType);
            return identity;
        }

    }
}