using FrameworkBase.MultiOrg.Application;
using Newtouch.HIS.Domain.DTO.OutputDto;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// Xử lý Phư ơng thức
    /// </summary>
    public class IProcessor : AppBase
    {
        /// <summary>
        /// Xác minhRequest
        /// </summary>
        protected virtual ActResult Validata()
        {
            return new ActResult();
        }

        /// <summary>
        /// Tiền xử lý
        /// </summary>
        protected virtual void BeforeAction(ActResult actResult)
        {
        }

        /// <summary>
        /// chínhxử lý
        /// </summary>
        /// <returns></returns>
        protected virtual void Action(ActResult actResult)
        {
        }

        /// <summary>
        /// Hậu xử lý
        /// </summary>
        protected virtual void AfterAction(ActResult actResult)
        {
        }

        /// <summary>
        /// bất đồng bộxử lý
        /// </summary>
        protected virtual void AsyncAction(ActResult actResult)
        {
        }
    }
}
