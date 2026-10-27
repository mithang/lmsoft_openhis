using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Repository;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// Phương thức nhập/xuất kho
    /// </summary>
    public class WzCrkfsRepo : RepositoryBase<WzCrkfsEntity>, IWzCrkfsRepo
    {
        public WzCrkfsRepo() 
        {
        }

        /// <summary>
        /// TheoIDXóa phương thức nhập/xuất kho
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteCrkfsById(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return 0;
            var entity = FindEntity(p => p.Id == id);
            return entity == null ? 0 : Delete(entity);
        }
    }
}
