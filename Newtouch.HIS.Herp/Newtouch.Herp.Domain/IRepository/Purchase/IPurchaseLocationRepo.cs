using Newtouch.Herp.Domain.Entity.Purchase;
using Newtouch.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Herp.Domain.IRepository.Purchase
{
    public interface IPurchaseLocationRepo : IRepositoryBase<PurchaseLocationEntity>
    {
        //Cập nhật hoặc thêm mới
        string SubmitForm(PurchaseLocationEntity entity, string orgId, string keyValue);
        //Cập nhật trạng thái điểm giao hàng
        int LocationStateUpdate(string Id, string orgId, int psdzt);
    }
}
