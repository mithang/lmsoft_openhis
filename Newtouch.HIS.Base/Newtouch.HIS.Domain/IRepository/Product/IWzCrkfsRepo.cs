using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// Phương thức nhập/xuất kho
    /// </summary>
    public interface IWzCrkfsRepo : IRepositoryBase<WzCrkfsEntity>
    {
        /// <summary>
        /// Xóa phương thức nhập/xuất kho theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteCrkfsById(string id);
    }
}
