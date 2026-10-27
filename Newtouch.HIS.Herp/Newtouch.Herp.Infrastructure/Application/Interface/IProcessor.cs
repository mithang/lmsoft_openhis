using Newtouch.Herp.Domain.DTO.OutputDto;

namespace Newtouch.Herp.Application.Interface
{
    /// <summary>
    /// Phương pháp xử lý
    /// </summary>
    public class IProcessor : AppBase
    {
        /// <summary>
        /// Xác thực Request
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
        /// Xử lý chính
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
        /// Xử lý bất đồng bộ
        /// </summary>
        protected virtual void AsyncAction(ActResult actResult)
        {
        }
    }
}
