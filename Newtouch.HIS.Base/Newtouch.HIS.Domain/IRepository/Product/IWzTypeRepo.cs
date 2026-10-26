using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// Danh mục vật tư
    /// </summary>
    public interface IWzTypeRepo : IRepositoryBase<WzTypeEntity>
    {
        /// <summary>
        /// delete unit by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteUnitById(string id);
    }
}
