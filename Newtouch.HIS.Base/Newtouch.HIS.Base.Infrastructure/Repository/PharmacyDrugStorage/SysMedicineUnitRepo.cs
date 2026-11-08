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
    /// 
    /// </summary>
    public class SysMedicineUnitRepo : RepositoryBase<SysMedicineUnitEntity>, ISysMedicineUnitRepo
    {
        public SysMedicineUnitRepo()
            
        {
        }

        /// <summary>
        /// Tra cứu đơn vị thuốc
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysMedicineUnitEntity> GetValidList(string keyword = null)
        {
            var sql = @"select * from xt_ypdw where zt = '1' and ypdwmc like @searchKeyword order by ypdwmc asc";
            return this.FindList<SysMedicineUnitEntity>(sql, new SqlParameter[] {
                new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%")
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysMedicineUnitEntity> GetList(string keyword = null)
        {
            var sql = @"select * from xt_ypdw where
                (ypdwCode like @searchKeyword or ypdwmc like @searchKeyword)";
            return this.FindList<SysMedicineUnitEntity>(sql, new SqlParameter[] {
                new SqlParameter("@searchKeyword","%" + (keyword ?? "") + "%")
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void submitForm(SysMedicineUnitEntity entity, int? keyValue)
        {
            if (keyValue.HasValue && keyValue.Value > 0)
            {
                if (this.IQueryable().Any(p => p.ypdwId != keyValue && p.ypdwCode == entity.ypdwCode))
                {
                    throw new FailedException("Mã đơn vị không được trùng lặp!");
                }
                if (this.IQueryable().Any(p => p.ypdwId != keyValue && p.ypdwmc == entity.ypdwmc))
                {
                    throw new FailedException("Tên đơn vị không được trùng lặp!");
                }

                SysMedicineUnitEntity oldEntity = null;   //Trước khi thay đổi Entity
                oldEntity = this.FindEntity(keyValue.Value);
                this.DetacheEntity(oldEntity);

                entity.Modify();
                entity.ypdwId = keyValue.Value;
                // F19-FIX D1: form khong post CreatorCode/CreateTime nen entity mang
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
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, "xt_ypdw", oldEntity.ypdwId.ToString());
                }
            }
            else
            {
                if (this.IQueryable().Any(p => p.ypdwCode == entity.ypdwCode))
                {
                    throw new FailedException("Mã đơn vị không được trùng lặp!");
                }
                if (this.IQueryable().Any(p => p.ypdwmc == entity.ypdwmc))
                {
                    throw new FailedException("Tên đơn vị không được trùng lặp!");
                }
                entity.Create();
                this.Insert(entity);
            }
        }
    }
}
