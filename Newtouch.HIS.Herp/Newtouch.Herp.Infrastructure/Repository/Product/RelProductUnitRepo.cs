using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// 库存单位关联 (Herp DB: rel_productUnit).
    /// </summary>
    public class RelProductUnitRepo : RepositoryBase<RelProductUnitEntity>, IRelProductUnitRepo
    {
        public RelProductUnitRepo()
        {
        }

        public int DeleteById(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return 0;
            var entity = FindEntity(p => p.Id == id);
            return entity == null ? 0 : Delete(entity);
        }
    }
}
