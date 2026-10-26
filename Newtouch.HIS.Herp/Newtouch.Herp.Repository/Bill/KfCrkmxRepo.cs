using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// Chi tiết chứng từ nhập/xuất kho
    /// </summary>
    public class KfCrkmxRepo : RepositoryBase<KfCrkmxEntity>, IKfCrkmxRepo
    {
        public KfCrkmxRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// Sửa số hóa đơn
        /// </summary>
        /// <param name="crkmxId"></param>
        /// <param name="fph"></param>
        /// <returns></returns>
        public int UpdateFph(long crkmxId, string fph)
        {
            var mx = FindEntity(p => p.Id == crkmxId);
            if (mx != null) mx.fph = fph;
            mx.Modify();
            return Update(mx);
        }
    }
}
