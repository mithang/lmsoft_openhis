using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// Chi tiết chứng từ nhập/xuất kho
    /// </summary>
    public class KfCrkmxRepo : RepositoryBase<KfCrkmxEntity>, IKfCrkmxRepo
    {
        

        /// <summary>
        /// Sửa số hóa đơn
        /// </summary>
        /// <param name="crkmxId"></param>
        /// <param name="fph"></param>
        /// <returns></returns>
        public int UpdateFph(long crkmxId, string fph)
        {
            var mx = FindEntity(p => p.Id == crkmxId);
            // PORT NOTE: original called mx.Modify() unconditionally after a
            // null check, NRE-ing on a missing detail row. Guarded here.
            if (mx == null) return 0;
            mx.fph = fph;
            mx.Modify();
            return Update(mx);
        }
    }
}
