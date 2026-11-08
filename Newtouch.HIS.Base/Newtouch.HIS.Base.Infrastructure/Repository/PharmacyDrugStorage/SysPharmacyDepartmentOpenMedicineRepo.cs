using System;
using Newtouch.HIS.Repository;
using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Data.SqlClient;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysPharmacyDepartmentOpenMedicineRepo : RepositoryBase<SysPharmacyDepartmentOpenMedicineEntity>, ISysPharmacyDepartmentOpenMedicineRepo
    {
        public SysPharmacyDepartmentOpenMedicineRepo()
            
        {

        }  

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void submitForm(SysPharmacyDepartmentOpenMedicineEntity entity, string keyValue)
        {
            if (string.IsNullOrWhiteSpace(keyValue))
            {
                entity.Id = Guid.NewGuid().ToString();
                entity.Create();
                this.Insert(entity);
            }
            else
            {
                // F19-FIX D1: repo nay chua nap oldEntity; nap dong dang luu truoc
                // khi update de khoi phuc CreatorCode/CreateTime (xem comment duoi).
                SysPharmacyDepartmentOpenMedicineEntity oldEntity = this.FindEntity(keyValue);
                this.DetacheEntity(oldEntity);

                entity.Modify();
                entity.Id = keyValue;
                // F19-FIX D1: form khong post CreatorCode/CreateTime nen entity mang
                // null/default; this.Update(entity) 1 tham so bind vao overload generic
                // RepositoryBase.Update<T> (khong ignore) thay vi overload virtual co
                // ignore CreatorCode/CreateTime (RepositoryBase.cs:123 vs :331) -> UPDATE
                // gan NULL vao cot CreatorCode NOT NULL. Khoi phuc 2 cot tu dong dang
                // luu (oldEntity vua nap o tren) truoc khi update.
                if (oldEntity != null)
                {
                    entity.CreatorCode = oldEntity.CreatorCode;
                    entity.CreateTime = oldEntity.CreateTime;
                }
                this.Update(entity);
            }
        }
    }
}
