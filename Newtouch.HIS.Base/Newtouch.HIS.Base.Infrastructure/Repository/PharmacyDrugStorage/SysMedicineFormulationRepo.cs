using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.Tools;
using Newtouch.HIS.Repository;
using NewtouchHIS.Lib.Base.Model;
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
    public class SysMedicineFormulationRepo : RepositoryBase<SysMedicineFormulationEntity>, ISysMedicineFormulationRepo
    {
        public SysMedicineFormulationRepo()
            
        {
        }

        /// <summary>
        /// Theo tra cứu dạng bào chế thuốc
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysMedicineFormulationEntity> GetValidList(string keyword = null)
        {
            var sql = @"select * from xt_ypjx(nolock) where zt = '1' and (jxCode like @searchKeyword or jxmc like @searchKeyword or py like @searchKeyword) order by py asc";
            return this.FindList<SysMedicineFormulationEntity>(sql, new SqlParameter[] {
                new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%")
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysMedicineFormulationEntity> GetPagintionList(Pagination pagination, string keyword = null)
        {
            var sql = @"select * from xt_ypjx(nolock) where zt='1'
and (jxCode like @searchKeyword or jxmc like @searchKeyword or py like @searchKeyword)";
            return this.QueryWithPage<SysMedicineFormulationEntity>(sql, pagination, new SqlParameter[] {
                    new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%") });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void submitForm(SysMedicineFormulationEntity entity, int? keyValue)
        {
            if (keyValue.HasValue && keyValue.Value > 0)
            {
                if (this.IQueryable().Any(p => p.jxId != keyValue && p.jxCode == entity.jxCode))
                {
                    throw new FailedException("Mã dạng bào chế không được trùng lặp!");
                }
                if (this.IQueryable().Any(p => p.jxId != keyValue && p.jxmc == entity.jxmc))
                {
                    throw new FailedException("Tên dạng bào chế không được trùng lặp!");
                }

                SysMedicineFormulationEntity oldEntity = null;   //Trước khi thay đổi Entity
                oldEntity = this.FindEntity(keyValue.Value);
                this.DetacheEntity(oldEntity);

                entity.Modify();
                entity.jxId = keyValue.Value;
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
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, "xt_ypjx", oldEntity.jxId.ToString());
                }
            }
            else
            {
                if (this.IQueryable().Any(p => p.jxCode == entity.jxCode))
                {
                    throw new FailedException("Mã dạng bào chế không được trùng lặp!");
                }
                if (this.IQueryable().Any(p => p.jxmc == entity.jxmc))
                {
                    throw new FailedException("Tên dạng bào chế không được trùng lặp!");
                }
                entity.Create();
                this.Insert(entity);
            }
        }

     
    }
}
