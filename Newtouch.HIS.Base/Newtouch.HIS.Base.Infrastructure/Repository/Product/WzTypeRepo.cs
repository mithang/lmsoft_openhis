using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Repository;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// Loại vật tư
    /// </summary>
    public class WzTypeRepo : RepositoryBase<WzTypeEntity>, IWzTypeRepo
    {
        public WzTypeRepo() 
        {
        }

        /// <summary>
        /// delete unit by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteUnitById(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return 0;
            var entity = FindEntity(p => p.Id == id);
            return entity == null ? 0 : Delete(entity);
        }
    }
}
