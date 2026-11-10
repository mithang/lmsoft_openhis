using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Model;
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
                // G15: base probe edit bogus Id -> Success gia (FindEntity null
                // -> Update 0 dong, controller van Success). Guard khong tim
                // thay -> Error, mau G10/G11/G12.
                PurchaseLocationEntity oldEntity = this.FindEntity(p => p.Id == keyValue);
                if (oldEntity == null) throw new FailedException("Không tìm thấy điểm giao hàng.");
                entity.Id = keyValue;
                entity.OrganizeId = orgId;
                // G15: base probe edit dong psdzt=2 (Da bao cao) -> bi reset ve
                // 1 (Da luu) ma khong qua YY — mat trang thai da truyen bao.
                // Giu psdzt cu; dong psdzt=1 sua van 1 nhu cu.
                entity.psdzt = oldEntity.psdzt;
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
                // F20-FIX: nhanh tao moi thieu zt -> INSERT loi zt NULL (cot NOT NULL).
                // Create(true) (EntityExtensions.cs:38-55) chi stamp Id/CreatorCode/
                // CreateTime/OrganizeId, khong set zt; form LocationForm cung khong post
                // zt; nhanh sua (:39) da gan entity.zt = "1". Gan giong nhanh sua.
                entity.zt = "1";
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
