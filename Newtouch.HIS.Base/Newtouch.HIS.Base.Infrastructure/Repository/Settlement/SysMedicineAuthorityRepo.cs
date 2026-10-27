using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.Settlement;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.IRepository.Settlement;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
namespace Newtouch.HIS.Repository.Settlement
{
    public class SysMedicineAuthorityRepo : RepositoryBase<SysMedicineAuthorityEntity>, ISysMedicineAuthorityRepo
    {
        public SysMedicineAuthorityRepo()
            
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysMedicineAuthorityEntity GetForm(string keyValue)
        {
            return this.FindEntity(keyValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IList<SysMedicineAuthorityEntity> GetList(string orgId, string keyword = null)
        {
            var sql = @"select * from xt_qxkz(nolock) where zt=1 and OrganizeId = @orgId
and (qxCode like @searchKeyword or qxmc like @searchKeyword or rel_value like @searchKeyword)
order by CreateTime desc";

            return this.FindList<SysMedicineAuthorityEntity>(sql, new[] {
                new SqlParameter("@orgId", orgId)
                ,new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%") });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysMedicineAuthorityEntity entity, string keyValue)
        {
            //Code重复判断
            if (this.IQueryable().Any(p => p.qxCode == entity.qxCode && p.qxId != keyValue && p.OrganizeId == entity.OrganizeId))
            {
                throw new FailedException("Mã không được trùng lặp");
            }
            if (this.IQueryable().Any(p => p.qxmc == entity.qxmc && p.qxId != keyValue && p.OrganizeId == entity.OrganizeId && p.zt=="1"))
            {
                throw new FailedException("Tên quyền không được trùng lặp");
            }
            if (this.IQueryable().Any(p => p.rel_lxcode == entity.rel_lxcode && p.rel_value == entity.rel_value && p.qxId != keyValue && p.OrganizeId == entity.OrganizeId && p.zt == "1"))
            {
                throw new FailedException("Quyền không được trùng lặp");
            }

            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                entity.Modify();
                entity.qxId = keyValue;
                this.Update(entity);
            }
            else
            {
                var zt = entity.zt;
                entity.Create(true);
                entity.zt = zt;
                this.Insert(entity);
            }
        }

        public IList<SysMedicineAuthorityEntity> GetValidList(string orgId,string keyword)
        {
            var sql = "select * from xt_qxkz where zt = '1' and  OrganizeId = @orgId";
            if (!string.IsNullOrWhiteSpace(keyword)) {
                sql += " and (qxCode like @searchKeyword or qxmc like @searchKeyword)";
            }

            return this.FindList<SysMedicineAuthorityEntity>(sql, new[] {
                new SqlParameter("@orgId", orgId)
                ,new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%") });
        }

    }
}
