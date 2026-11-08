using System;
using Newtouch.Tools;
using Newtouch.HIS.Repository;
using NewtouchHIS.Lib.Base.Model;
using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Linq;
using NewtouchHIS.Lib.Base.Exceptions;
using System.Data.SqlClient;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.Entity.Settlement;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysDiagnosisRepo : RepositoryBase<SysDiagnosisEntity>, ISysDiagnosisRepo
    {
        public SysDiagnosisRepo()
            
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <param name="Pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysDiagnosisEntity> GetCommonPagintionList(Pagination pagination, string keyword = null)
        {
            var sql = @"select * from xt_zd(nolock) where (OrganizeId = '*')
and (zdCode like @searchKeyword or zdmc like @searchKeyword or py like @searchKeyword or icd10 like @searchKeyword)";
            return this.QueryWithPage<SysDiagnosisEntity>(sql, pagination, new SqlParameter[] {
                    new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%")
            });
        }

	

		/// <summary>
		/// 
		/// </summary>
		/// <param name="OrganizeId"></param>
		/// <param name="Pagination"></param>
		/// <param name="keyword"></param>
		/// <returns></returns>
		public IList<SysDiagnosisEntity> GetPagintionList(string orgId, Pagination pagination, string keyword = null)
        {
            var sql = @"select * from xt_zd(nolock) where (OrganizeId = '*' or OrganizeId = @orgId) 
and (zdCode like @searchKeyword or zdmc like @searchKeyword or py like @searchKeyword or icd10 like @searchKeyword)";
            return this.QueryWithPage<SysDiagnosisEntity>(sql, pagination, new SqlParameter[] {
                    new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%")
                    ,new SqlParameter("@orgId", orgId)
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysDiagnosisEntity entity, int? keyValue)
        {
            if (keyValue.HasValue && keyValue.Value > 0)
            {
                var isRepeated = entity.OrganizeId == "*"
                    ? this.IQueryable().Any(p =>
                        p.zdCode == entity.zdCode && p.zdId != keyValue)
                    : this.IQueryable().Any(p => (p.OrganizeId == entity.OrganizeId || p.OrganizeId == "*")
                         && p.zdCode == entity.zdCode && p.zdId != keyValue)
                         ;
                if (isRepeated)
                {
                    throw new FailedException("Mã không được trùng lặp");
                }

                SysDiagnosisEntity oldEntity = null;   //Trước khi thay đổi Entity
                oldEntity = this.FindEntity(keyValue.Value);
                this.DetacheEntity(oldEntity);

                entity.Modify();
                entity.zdId = keyValue.Value;
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
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, "xt_zd", oldEntity.zdId.ToString());
                }
            }
            else
            {
                var isRepeated = entity.OrganizeId == "*"
                    ? this.IQueryable().Any(p =>
                        p.zdCode == entity.zdCode)
                    : this.IQueryable().Any(p => (p.OrganizeId == entity.OrganizeId || p.OrganizeId == "*")
                         && p.zdCode == entity.zdCode)
                         ;
                if (isRepeated)
                {
                    throw new FailedException("Mã không được trùng lặp");
                }
                entity.Create();
                this.Insert(entity);
            }
        }

    }
}
