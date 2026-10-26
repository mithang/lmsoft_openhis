using Autofac;
using Newtouch.HIS.API.Common.Models;

namespace Newtouch.HIS.Sett.API
{
    /// <summary>
    /// API Controller loại cơ sở 
    /// </summary>
    public class ApiControllerBase<TControllerType> : FrameworkBase.API.ApiControllerBase<TControllerType>
    {
        public ApiControllerBase(IComponentContext com)
            : base(com)
        {
        }

        /// <summary>
        /// ngư ời dùng thân phận 
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
