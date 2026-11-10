using Newtouch.Herp.Domain.Entity;
using SqlSugar;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Domain.Entity.Purchase;
using Newtouch.Herp.Domain.IRepository.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Herp.Repository.Purchase
{
    public class PurchaseDetailRepo : RepositoryBase<PurchaseDetailEntity>, IPurchaseDetailRepo
    {
        public PurchaseDetailRepo()
        {
        }


        public void SubmitForm(PurchaseDetailEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(p => p.cgmxId == keyValue);
                //properties
                dbEntity.Modify(keyValue);
                this.Update(dbEntity);
            }
            else
            {
                entity.cgmxId = Guid.NewGuid().ToString();
                entity.cgId = keyValue;
                entity.Create(true);
                this.Insert(entity);
            }
        }

        public int InsertItem(PurchaseDetailEntity entity, string keyValue)
        {
            entity.cgmxId = Guid.NewGuid().ToString();
            entity.cgId = keyValue;
            // G15: base probe d6-edit-real: chi tiet moi loi "Cannot insert NULL
            // into 'zt'" (cot zt char(1) NOT NULL, Create(true) khong set zt) —
            // cung lop G12 #5 / G9 #8. Khong co zt thi moi SubmitPurchase deu
            // hong o buoc insert detail.
            entity.zt = "1";
            entity.Create(true);
            return this.Insert(entity);
        }

        public int DeleteItem(string cgId, string orgId)
        {
            return ExecuteSqlCommand(@"DELETE FROM xt_wz_cgmx WHERE cgId=@cgId and organizeId=@orgId and zt=1", new SugarParameter("@cgId", cgId), new SugarParameter("@orgId", orgId));
        }

    }
}
