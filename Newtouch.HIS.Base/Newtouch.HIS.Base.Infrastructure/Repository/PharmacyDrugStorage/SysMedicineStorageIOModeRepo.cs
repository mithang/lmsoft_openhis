using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.Tools;
using Newtouch.HIS.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysMedicineStorageIOModeRepo : RepositoryBase<SysMedicineStorageIOModeEntity>, ISysMedicineStorageIOModeRepo
    {
        public SysMedicineStorageIOModeRepo()
            
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysMedicineStorageIOModeEntity GetForm(int keyValue)
        {
            return this.FindEntity(keyValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysMedicineStorageIOModeEntity> GetList(string keyword)
        {
            var sql = "select * from xt_ypcrkfs(nolock) where 1 = 1";
            var pars = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and (crkfsmc like @searchKeyword or crkfsCode like @searchKeyword)";
                pars.Add(new SqlParameter("@searchKeyword", "%" + keyword + "%"));
            }
            sql += " order by CreateTime desc";
            return this.FindList<SysMedicineStorageIOModeEntity>(sql, pars.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<SysMedicineStorageIOModeEntity> GetValidList()
        {
            var sql = "select * from xt_ypcrkfs(nolock) where zt = '1'";
            return this.FindList<SysMedicineStorageIOModeEntity>(sql);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysMedicineStorageIOModeEntity entity, int? keyValue)
        {
            if (keyValue.HasValue && keyValue.Value > 0)
            {
                //Code重复判断
                if (this.IQueryable().Any(p => p.crkfsCode == entity.crkfsCode && p.crkfsId != keyValue))
                {
                    throw new FailedException("Mã không được trùng lặp");
                }

                SysMedicineStorageIOModeEntity oldEntity = null;   //变更前Entity
                oldEntity = this.FindEntity(keyValue.Value);
                this.DetacheEntity(oldEntity);

                entity.Modify();
                entity.crkfsId = keyValue.Value;
                this.Update(entity);

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, "xt_ypcrkfs", oldEntity.crkfsId.ToString());
                }
            }
            else
            {
                //Code重复判断
                if (this.IQueryable().Any(p => p.crkfsCode == entity.crkfsCode))
                {
                    throw new FailedException("Mã không được trùng lặp");
                }
                entity.Create();
                this.Insert(entity);
            }
        }

    }
}
