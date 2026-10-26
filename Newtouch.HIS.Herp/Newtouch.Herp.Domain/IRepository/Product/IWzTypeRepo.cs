using Newtouch.Herp.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// Loai vat tu
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