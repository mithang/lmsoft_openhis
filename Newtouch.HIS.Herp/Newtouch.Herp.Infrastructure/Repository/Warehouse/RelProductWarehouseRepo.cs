using System.Collections.Generic;
using System.Linq;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// Vật tư kho
    /// </summary>
    public class RelProductWarehouseRepo : RepositoryBase<RelProductWarehouseEntity>, IRelProductWarehouseRepo
    {
        public RelProductWarehouseRepo()
        {
        }

        /// <summary>
        /// Xóa hàng loạt
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int BatchDelete(List<RelProductWarehouseEntity> list)
        {
            if (list == null || list.Count <= 0) return 0;
            using (var db = new Newtouch.Infrastructure.EF.EFDbTransaction(Db).BeginTrans())
            {
                list.ForEach(p =>
                {
                    db.Delete(p);
                });
                return db.Commit();
            }
        }
    }
}
