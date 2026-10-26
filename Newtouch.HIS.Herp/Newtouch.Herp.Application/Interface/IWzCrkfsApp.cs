using Newtouch.Herp.Domain.Entity;

namespace Newtouch.Herp.Application.Interface
{
    /// <summary>
    /// Hình thức nhập/xuất kho
    /// </summary>
    public interface IWzCrkfsApp
    {
        /// <summary>
        /// Gửi biểu mẫu
        /// </summary>
        /// <param name="wzCrkfsEntity"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        int SubmitForm(WzCrkfsEntity wzCrkfsEntity, string keyWord);
    }
}