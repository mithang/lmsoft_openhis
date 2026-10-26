using Newtouch.HIS.Domain.Entity.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IRepository.Purchase
{
    public interface IPurchaseLocationRepo : IRepositoryBase<PurchaseLocationEntity>
    {
        //cập nhật hoặc thêm mới
        string SubmitForm(PurchaseLocationEntity entity, string orgId, string keyValue);
        //cập nhật điểm phân phối trạng thái
        int LocationStateUpdate(string Id, string orgId, int psdzt);
    }
}
