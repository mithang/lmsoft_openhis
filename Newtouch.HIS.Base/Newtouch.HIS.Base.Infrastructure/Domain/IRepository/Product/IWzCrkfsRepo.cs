using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Repository;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// Phương thức nhập/xuất kho
    /// </summary>
    public interface IWzCrkfsRepo : IRepositoryBase<WzCrkfsEntity>
    {
        /// <summary>
        /// TheoIDXóa phương thức nhập/xuất kho
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteCrkfsById(string id);
    }
}
