using Newtouch.Herp.Domain.Entity;
using SqlSugar;
using Newtouch.Herp.Domain.DTO.InputDto.Purchase;
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
    public class PurchaseBillRepo : RepositoryBase<PurchaseBillEntity>, IPurchaseBillRepo
    {

        public PurchaseBillRepo()
        {
        }

        public int InsertItem(PurchaseMainYY132 entity,string orgId)
        {
            PurchaseBillEntity dbEntity = new PurchaseBillEntity();
            dbEntity.Id = Guid.NewGuid().ToString();
            dbEntity.OrganizeId = orgId;
            dbEntity.fpId = entity.FPID;
            dbEntity.fpdm = entity.FPDM;
            dbEntity.fph = entity.FPH;
            dbEntity.qybm = entity.QYBM;
            dbEntity.fpysjg = entity.FPYSJG;
            dbEntity.fphszje = entity.FPHSZJE;
            // G15 (fix theo code, chua probe live): cot xt_wz_fp.zt char(1)
            // NOT NULL nhung InsertItem khong set zt — nhanh success cua YY132
            // (chua cham duoc vi YY vang) se loi zt NULL, cung lop G12 #5 /
            // G9 #8 (tien le G9 Wbck fix theo code). KHONG ghi PASS live.
            dbEntity.zt = "1";
            dbEntity.Create(true);
            return this.Insert(dbEntity);
        }
    }
}
