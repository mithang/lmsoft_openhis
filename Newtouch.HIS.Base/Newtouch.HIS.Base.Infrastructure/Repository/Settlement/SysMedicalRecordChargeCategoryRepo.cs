using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.Tools;
using Newtouch.HIS.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// Nhóm thu phí lớn của bệnh án
    /// </summary>
    public class SysMedicalRecordChargeCategoryRepo : RepositoryBase<SysMedicalRecordChargeCategoryEntity>, ISysMedicalRecordChargeCategoryRepo
    {
        public SysMedicalRecordChargeCategoryRepo()
            
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysMedicalRecordChargeCategoryEntity GetForm(int keyValue)
        {
            return this.FindEntity(keyValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IList<SysMedicalRecordChargeCategoryEntity> GetList(string orgId, string keyword = null)
        {
            var sql = @"select * from xt_basfdl(nolock) where OrganizeId = @orgId
and (dlCode like @searchKeyword or dlmc like @searchKeyword or py like @searchKeyword)
order by CreateTime desc";

            return this.FindList<SysMedicalRecordChargeCategoryEntity>(sql, new[] {
                new SqlParameter("@orgId", orgId)
                ,new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%") });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IList<SysMedicalRecordChargeCategoryEntity> GetValidList(string orgId)
        {
            return this.IQueryable().Where(a => a.OrganizeId == orgId && a.zt == "1").OrderByDescending(a => a.dlId).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysMedicalRecordChargeCategoryEntity entity, int? keyValue)
        {
            if (keyValue.HasValue && keyValue.Value > 0)
            {
                //Code Kiểm tra trùng lặp
                if (this.IQueryable().Any(p => p.dlCode == entity.dlCode && p.dlId != keyValue.Value && p.OrganizeId == entity.OrganizeId))
                {
                    throw new FailedException("Mã không được trùng lặp");
                }

                SysMedicalRecordChargeCategoryEntity oldEntity = null;   //Trước khi thay đổi Entity
                oldEntity = this.FindEntity(keyValue.Value);
                this.DetacheEntity(oldEntity);

                entity.Modify();
                entity.dlId = keyValue.Value;
                // F16-FIX D1: form khong post CreatorCode/CreateTime nen entity mang
                // null/default; this.Update(entity) 1 tham so bind vao overload generic
                // RepositoryBase.Update<T> (khong ignore) thay vi overload virtual co
                // ignore CreatorCode/CreateTime (RepositoryBase.cs:123 vs :331) -> UPDATE
                // gan NULL vao cot CreatorCode NOT NULL. Khoi phuc 2 cot tu dong dang
                // luu (oldEntity da nap san cho log thay doi) truoc khi update.
                if (oldEntity != null)
                {
                    entity.CreatorCode = oldEntity.CreatorCode;
                    entity.CreateTime = oldEntity.CreateTime;
                }
                this.Update(entity);

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, "xt_basfdl", oldEntity.dlId.ToString());
                }
            }
            else
            {
                //Code Kiểm tra trùng lặp
                if (this.IQueryable().Any(p => p.dlCode == entity.dlCode && p.OrganizeId == entity.OrganizeId))
                {
                    throw new FailedException("Mã không được trùng lặp");
                }
                entity.Create();
                this.Insert(entity);
            }
        }
    }
}
