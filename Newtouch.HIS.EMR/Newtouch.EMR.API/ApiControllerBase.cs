using Autofac;
using Newtouch.EMR.API.Models;

namespace Newtouch.EMR.API
{
    /// <summary>
    /// Lớp cơ sở API Controller
    /// </summary>
    public class ApiControllerBase<TControllerType> : FrameworkBase.API.ApiControllerBase<TControllerType>
    {
        public ApiControllerBase(IComponentContext com)
            : base(com)
        {
        }

        /// <summary>
        /// Danh tính người dùng
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