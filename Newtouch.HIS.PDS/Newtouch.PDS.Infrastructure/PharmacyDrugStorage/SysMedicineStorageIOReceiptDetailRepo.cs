using Newtouch.Infrastructure;
using NewtouchHIS.Lib.Base.Exceptions;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// xt_yp_crkmx
    /// </summary>
    public class SysMedicineStorageIOReceiptDetailRepo : RepositoryBase<SysMedicineStorageIOReceiptDetailEntity>, ISysMedicineStorageIOReceiptDetailRepo
    {
        public SysMedicineStorageIOReceiptDetailRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// Sửa số hóa đơn
        /// </summary>
        /// <param name="crkmxId"></param>
        /// <param name="fph"></param>
        /// <param name="organizeId">Tổ chức hiện tại (chi tiết phải thuộc chứng từ của tổ chức này)</param>
        /// <returns></returns>
        public int UpdateFph(string crkmxId, string fph, string organizeId)
        {
            var mx = FindEntity(p => p.crkmxId == crkmxId);
            if (mx == null) throw new FailedException("Không tìm thấy chi tiết chứng từ");
            using (var db = new EFDbTransaction())
            {
                var dj = db.IQueryable<SysMedicineStorageIOReceiptEntity>(p => p.crkId == mx.crkId).FirstOrDefault();
                if (dj == null) throw new FailedException("Không tìm thấy chứng từ");
                if (dj.OrganizeId != organizeId) throw new FailedException("Chi tiết chứng từ không thuộc kho của bạn");
            }
            mx.Fph = fph;
            mx.Modify();
            return Update(mx);
        }
    }
}
