using System.Collections.Generic;
using Newtouch.Tools;
using Newtouch.HIS.Repository;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Linq;
using NewtouchHIS.Lib.Base.Exceptions;
using System.Data.SqlClient;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysTCMSyndromeRepo : RepositoryBase<SysTCMSyndromeEntity>, ISysTCMSyndromeRepo
    {
        public SysTCMSyndromeRepo()
            
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <param name="Pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysTCMSyndromeEntity> GetCommonPagintionList(Pagination pagination, string keyword = null)
        {
            var sql = @"select * from xt_zyzh(nolock) where (OrganizeId = '*')
and (zhCode like @searchKeyword or zhmc like @searchKeyword or py like @searchKeyword)";
            return this.QueryWithPage<SysTCMSyndromeEntity>(sql, pagination, new SqlParameter[] {
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
        public IList<SysTCMSyndromeEntity> GetPagintionList(string orgId, Pagination pagination, string keyword = null)
        {
            var sql = @"select * from xt_zyzh(nolock) where (OrganizeId = '*' or OrganizeId = @orgId) 
and (zhCode like @searchKeyword or zhmc like @searchKeyword or py like @searchKeyword)";
            return this.QueryWithPage<SysTCMSyndromeEntity>(sql, pagination, new SqlParameter[] {
                    new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%")
                    ,new SqlParameter("@orgId", orgId)
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysTCMSyndromeEntity entity, int? keyValue)
        {
            if (keyValue.HasValue && keyValue.Value > 0)
            {
                var isRepeated = entity.OrganizeId == "*"
                    ? this.IQueryable().Any(p =>
                        p.zhCode == entity.zhCode && p.zhId != keyValue)
                    : this.IQueryable().Any(p => (p.OrganizeId == entity.OrganizeId || p.OrganizeId == "*")
                         && p.zhCode == entity.zhCode && p.zhId != keyValue)
                         ;
                if (isRepeated)
                {
                    throw new FailedException("Mã không được trùng lặp");
                }

                SysTCMSyndromeEntity oldEntity = null;   //Trước khi thay đổiEntity
                oldEntity = this.FindEntity(keyValue.Value);
                this.DetacheEntity(oldEntity);

                entity.Modify();
                entity.zhId = keyValue.Value;
                this.Update(entity);

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, "xt_zyzh", oldEntity.zhId.ToString());
                }
            }
            else
            {
                var isRepeated = entity.OrganizeId == "*"
                    ? this.IQueryable().Any(p =>
                        p.zhCode == entity.zhCode)
                    : this.IQueryable().Any(p => (p.OrganizeId == entity.OrganizeId || p.OrganizeId == "*")
                         && p.zhCode == entity.zhCode)
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
