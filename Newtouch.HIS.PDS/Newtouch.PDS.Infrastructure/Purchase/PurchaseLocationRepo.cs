using Newtouch.Infrastructure;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity.Purchase;
using Newtouch.HIS.Domain.IRepository.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Repository.Purchase
{
    public class PurchaseLocationRepo : RepositoryBase<PurchaseLocationEntity>, IPurchaseLocationRepo
    {
        public PurchaseLocationRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }


        //Cập nhật hoặc thêm mới
        public string SubmitForm(PurchaseLocationEntity entity, string orgId, string keyValue)
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

        //Cập nhật trạng thái điểm giao hàng
        public int LocationStateUpdate(string Id, string orgId, int psdzt)
        {
            var dbEntity = this.FindEntity(Id);
            //properties
            dbEntity.psdzt = psdzt;//psdzt 1Đã lưu 2Đã truyền báo 3Đã hủy bỏ
            dbEntity.Modify(Id);
            return this.Update(dbEntity);
        }
    }
}
