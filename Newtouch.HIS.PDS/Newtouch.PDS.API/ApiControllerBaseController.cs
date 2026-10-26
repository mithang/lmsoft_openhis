using Autofac;
using Newtouch.HIS.API.Common.Models;

namespace Newtouch.PDS.API
{
    /// <summary>
    /// API Controll Lớp cơ sở
    /// </summary>
    public class ApiControllerBase<TControllerType> : App_Start.ApiControllerBase<TControllerType>
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