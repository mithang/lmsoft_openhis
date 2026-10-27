using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.Tools;
using Newtouch.HIS.Repository;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysMedicineClassificationRepo : RepositoryBase<SysMedicineClassificationEntity>, ISysMedicineClassificationRepo
    {
        public SysMedicineClassificationRepo()
            
        {
        }

        /// <summary>
        /// Tra cứu phân loại lớn thuốc theo từ khóa
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public IList<SysMedicineClassificationEntity> GetValidList(string keyword = null)
        {
            var sql = @"select * from xt_ypfl(nolock) where zt = '1' and (ypflCode like @searchKeyword or ypflmc like @searchKeyword or py like @searchKeyword) order by CreateTime desc";
            return this.FindList<SysMedicineClassificationEntity>(sql, new SqlParameter[] {
                    new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%")
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysMedicineClassificationEntity> GetPagintionList(Pagination pagination, string keyword = null)
        {
            var sql = @"select * from xt_ypfl(nolock) where zt='1'
and (ypflCode like @searchKeyword or ypflmc like @searchKeyword or py like @searchKeyword)";
            return this.QueryWithPage<SysMedicineClassificationEntity>(sql, pagination, new SqlParameter[] {
                    new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%") });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void submitForm(SysMedicineClassificationEntity entity, int? keyValue)
        {
            if (keyValue.HasValue && keyValue.Value > 0)
            {
                if (this.IQueryable().Any(p => p.ypflId != keyValue && p.ypflCode == entity.ypflCode))
                {
                    throw new FailedException("Mã không được trùng lặp!");
                }
                if (this.IQueryable().Any(p => p.ypflId != keyValue && p.ypflmc == entity.ypflmc))
                {
                    throw new FailedException("Tên không được trùng lặp!");
                }

                SysMedicineClassificationEntity oldEntity = null;   //Trước khi thay đổiEntity
                oldEntity = this.FindEntity(keyValue.Value);
                this.DetacheEntity(oldEntity);

                entity.Modify();
                entity.ypflId = keyValue.Value;
                this.Update(entity);

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, "xt_ypfl", oldEntity.ypflId.ToString());
                }
            }
            else
            {
                if (this.IQueryable().Any(p => p.ypflCode == entity.ypflCode))
                {
                    throw new FailedException("Mã không được trùng lặp!");
                }
                if (this.IQueryable().Any(p => p.ypflmc == entity.ypflmc))
                {
                    throw new FailedException("Tên không được trùng lặp!");
                }
                entity.Create();
                this.Insert(entity);
            }
        }

    }
}
