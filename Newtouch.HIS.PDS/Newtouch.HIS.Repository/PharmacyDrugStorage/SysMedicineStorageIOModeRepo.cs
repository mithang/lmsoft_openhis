using System.Linq;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysMedicineStorageIOModeRepo : RepositoryBase<SysMedicineStorageIOModeEntity>, ISysMedicineStorageIOModeRepo
    {
        public SysMedicineStorageIOModeRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// Lấy phương thức xuất nhập kho
        /// </summary>
        /// <returns></returns>
        public object GetStorageIOMode()
        {
            var list = this.IQueryable().Where(a => a.zt == "1" && a.crkbz == "0")
                .Select(p => new
                {
                    crkfsId = p.crkfsId,
                    crkfsCode = p.crkfsCode,
                    crkfsmc = p.crkfsmc
                }).ToList();
            return list;
        }
    }
}
