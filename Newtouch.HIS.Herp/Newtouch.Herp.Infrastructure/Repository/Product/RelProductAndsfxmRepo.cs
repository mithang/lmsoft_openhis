using Newtouch.Herp.Domain.Entity.Product;
using Newtouch.Herp.Domain.IRepository;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// Bảng đối chiếu hạng mục thu phí vật tư (Herp DB: rel_productAndsfxm).
    /// </summary>
    public class RelProductAndsfxmRepo : RepositoryBase<RelProductAndsfxmEntity>, IRelProductAndsfxmRepo
    {
        public RelProductAndsfxmRepo()
        {
        }
    }
}
