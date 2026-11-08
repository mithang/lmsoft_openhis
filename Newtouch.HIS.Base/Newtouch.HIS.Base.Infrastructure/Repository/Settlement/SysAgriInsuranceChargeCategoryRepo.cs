using System.Collections.Generic;
using Newtouch.Tools;
using Newtouch.HIS.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Linq;
using NewtouchHIS.Lib.Base.Exceptions;
using System.Data.SqlClient;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// Nhóm thu phí lớn bảo hiểm nông nghiệp
    /// </summary>
    public class SysAgriInsuranceChargeCategoryRepo : RepositoryBase<SysAgriInsuranceChargeCategoryEntity>, ISysAgriInsuranceChargeCategoryRepo
    {
        public SysAgriInsuranceChargeCategoryRepo()
            
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysAgriInsuranceChargeCategoryEntity GetForm(int keyValue)
        {
            return this.FindEntity(keyValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IList<SysAgriInsuranceChargeCategoryEntity> GetList(string orgId, string keyword = null)
        {
            var sql = @"select * from xt_nbsfdl(nolock) where OrganizeId = @orgId
and (dlCode like @searchKeyword or dlmc like @searchKeyword or py like @searchKeyword)
order by CreateTime desc";

            return this.FindList<SysAgriInsuranceChargeCategoryEntity>(sql, new[] {
                new SqlParameter("@orgId", orgId)
                ,new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%") });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IList<SysAgriInsuranceChargeCategoryEntity> GetValidList(string orgId)
        {
            return this.IQueryable().Where(a => a.OrganizeId == orgId && a.zt == "1").OrderBy(a => a.px).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysAgriInsuranceChargeCategoryEntity entity, int? keyValue)
        {
            if (keyValue.HasValue && keyValue.Value > 0)
            {
                //Code Kiểm tra trùng lặp
                if (this.IQueryable().Any(p => p.dlCode == entity.dlCode && p.dlId != keyValue.Value && p.OrganizeId == entity.OrganizeId))
                {
                    throw new FailedException("Mã không được trùng lặp");
                }

                SysAgriInsuranceChargeCategoryEntity oldEntity = null;   //Trước khi thay đổi Entity
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
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, "xt_nbsfdl", oldEntity.dlId.ToString());
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
