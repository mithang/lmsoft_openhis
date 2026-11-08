using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Repository;
using Newtouch.HIS.Domain.Entity.Settlement;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysChargeCategoryTypeRelationRepo : RepositoryBase<SysChargeCategoryTypeRelationEntity>, ISysChargeCategoryTypeRelationRepo
    {
        public SysChargeCategoryTypeRelationRepo()
            
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IList<GetSfDlMc> GetList(string orgId, string keyword = null)
        {
            var sql = @"select lx.id,lx.Type,lx.dlCode,lx.CreatorCode,lx.CreateTime,lx.LastModifierCode,lx.LastModifyTime,dl.dlmc,lx.OrganizeId,lx.zt,lx.px
from  xt_sfdl_lx lx
left join xt_sfdl dl on lx.dlCode=dl.dlCode 
and dl.zt='1'
and lx.OrganizeId=dl.OrganizeId
where lx.OrganizeId = @orgId
and (lx.dlCode like @searchKeyword or Type like @searchKeyword)
order by CreateTime desc";

            return this.FindList<GetSfDlMc>(sql, new[] {
                new SqlParameter("@orgId", orgId)
                ,new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%") });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysChargeCategoryTypeRelationEntity entity, string keyValue)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                // F16-FIX D1: repo nay chua nap oldEntity; nap dong dang luu truoc
                // khi update de khoi phuc CreatorCode/CreateTime (xem comment duoi).
                SysChargeCategoryTypeRelationEntity oldEntity = this.FindEntity(keyValue);
                this.DetacheEntity(oldEntity);

                entity.Modify();
                entity.Id = keyValue;
                // F16-FIX D1: form khong post CreatorCode/CreateTime nen entity mang
                // null/default; this.Update(entity) 1 tham so bind vao overload generic
                // RepositoryBase.Update<T> (khong ignore) thay vi overload virtual co
                // ignore CreatorCode/CreateTime (RepositoryBase.cs:123 vs :331) -> UPDATE
                // gan NULL vao cot CreatorCode NOT NULL. Khoi phuc 2 cot tu dong dang
                // luu (oldEntity) truoc khi update.
                if (oldEntity != null)
                {
                    entity.CreatorCode = oldEntity.CreatorCode;
                    entity.CreateTime = oldEntity.CreateTime;
                }
                this.Update(entity);
            }
            else
            {
                entity.Create(true);
                this.Insert(entity);
            }
        }


    }
}
