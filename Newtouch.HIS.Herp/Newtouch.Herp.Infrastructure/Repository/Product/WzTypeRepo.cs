using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure;
using SqlSugar;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// 物资类别 (Base DB: wz_type).
    /// </summary>
    public class WzTypeRepo : RepositoryBase<WzTypeEntity>, IWzTypeRepo
    {
        public WzTypeRepo()
        {
        }

        protected override SqlSugarProvider Db => HerpDb.GetBaseConnection();

        public int DeleteUnitById(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return 0;
            var entity = FindEntity(p => p.Id == id);
            return entity == null ? 0 : Delete(entity);
        }
    }
}
