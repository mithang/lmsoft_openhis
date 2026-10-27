using SqlSugar;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// 采购单明细
    /// </summary>
    public class CgOrderDetailRepo : RepositoryBase<CgOrderDetailEntity>, ICgOrderDetailRepo
    {
        public CgOrderDetailRepo()
        {
        }
    }
}