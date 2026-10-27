using Newtouch.Herp.Domain.Entity;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// Loại vật tư
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