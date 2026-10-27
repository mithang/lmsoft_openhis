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
    public class PurchaseLocationRepo : RepositoryBase<PurchaseLocationEntity>, IPurchaseLocationRepo
    {
        public PurchaseLocationRepo()
        {
        }


        //Cập nhật hoặc thêm mới
        public string SubmitForm(PurchaseLocationEntity entity,string orgId, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Id = keyValue;
                entity.OrganizeId = orgId;
                entity.psdzt = 1;
                entity.zt = "1";
                entity.Modify(keyValue);
                this.Update(entity);
                return keyValue;
            }
            else
            {
                entity.Id = Guid.NewGuid().ToString();
                entity.OrganizeId = orgId;
                entity.psdzt = 1;//Đã lưu
                entity.Create(true);
                this.Insert(entity);
                return entity.Id;
            }
        }

        //Cập nhật trạng thái điểm phân phối
        public int LocationStateUpdate(string Id, string orgId,int psdzt)
        {
            var dbEntity = this.FindEntity(p => p.Id == Id);
            //properties
            dbEntity.psdzt = psdzt;//psdzt 1 Đã lưu 2 Đã báo cáo 3 Đã hủy bỏ
            dbEntity.Modify(Id);
            return this.Update(dbEntity);
        }
    }
}
