using Autofac;
using Newtouch.MRQC.API.Models;

namespace Newtouch.MRQC.API
{
    /// <summary>
    /// API Controll lớp cơ sở
    /// </summary>
    public class ApiControllerBase<TControllerType> : FrameworkBase.API.ApiControllerBase<TControllerType>
    {
        public ApiControllerBase(IComponentContext com)
            : base(com)
        {
        }

        /// <summary>
        /// danh tính người dùng
        /// </summary>
        public UserIdentity UserIdentity
        {
            get
            {
                return this.Identity as UserIdentity;
            }
        }

    }

}