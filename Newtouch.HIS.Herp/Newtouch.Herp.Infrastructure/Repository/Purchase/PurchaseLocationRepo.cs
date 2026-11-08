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
                // F19-FIX D1: form LocationForm khong post CreatorCode/CreateTime nen
                // entity mang null/default; Herp RepositoryBase chi co DUNG 1 overload
                // Update(T) (full-column, RepositoryBase.cs:133-136), khong co overload
                // ignore 2 cot nhu Base -> UPDATE gan NULL vao cot CreatorCode NOT NULL
                // (xt_wz_psd). Nap oldEntity tu DB va khoi phuc 2 cot truoc khi update.
                // Khong can DetacheEntity: Herp khong co method do, va SqlSugar
                // Updateable khong track entity nhu EF (FindEntity tra POCO tach roi);
                // cung mau LocationStateUpdate trong file nay FindEntity roi Update
                // truc tiep, khong detach.
                PurchaseLocationEntity oldEntity = this.FindEntity(p => p.Id == keyValue);
                entity.Id = keyValue;
                entity.OrganizeId = orgId;
                entity.psdzt = 1;
                entity.zt = "1";
                entity.Modify(keyValue);
                if (oldEntity != null)
                {
                    entity.CreatorCode = oldEntity.CreatorCode;
                    entity.CreateTime = oldEntity.CreateTime;
                }
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
