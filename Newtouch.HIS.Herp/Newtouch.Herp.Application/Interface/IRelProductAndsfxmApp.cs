using Newtouch.Herp.Domain.Entity.Product;

namespace Newtouch.Herp.Application.Interface
{
    /// <summary>
    /// Đối chiếu khoản thu vật tư
    /// </summary>
    public interface IRelProductAndsfxmApp
    {
        /// <summary>
        /// Gửi đối chiếu khoản thu vật tư
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        string SubmitRelProductAndsfxm(RelProductAndsfxmEntity entity);
    }
}