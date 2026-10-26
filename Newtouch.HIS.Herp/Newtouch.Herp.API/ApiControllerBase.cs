using Autofac;
using Newtouch.Herp.API.Models;

namespace Newtouch.Herp.API
{
    /// <summary>
    /// API Controll Lớp cơ sở
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