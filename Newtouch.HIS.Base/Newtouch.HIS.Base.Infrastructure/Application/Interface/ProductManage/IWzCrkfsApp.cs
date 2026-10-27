using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// Phương thức nhập/xuất kho
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
