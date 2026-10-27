using Newtouch.Herp.Domain.Entity.Purchase;

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
        //Cập nhật trạng thái điểm phân phối
        int LocationStateUpdate(string Id, string orgId, int psdzt);
    }
}
