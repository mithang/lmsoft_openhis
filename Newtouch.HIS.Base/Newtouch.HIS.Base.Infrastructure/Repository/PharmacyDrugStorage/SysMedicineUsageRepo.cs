using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.Tools;
using Newtouch.HIS.Repository;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Data.SqlClient;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysMedicineUsageRepo : RepositoryBase<SysMedicineUsageEntity>, ISysMedicineUsageRepo
    {
        public SysMedicineUsageRepo()
            
        {
        }
        
        /// <summary>
        /// Lưu
        /// </summary>
        public void SubmitForm(SysMedicineUsageEntity entity, int? yfId)
        {
            if (yfId!=null && yfId > 0)
            {
                //Code Kiểm tra trùng lặp
                if (this.IQueryable().Any(a => a.yfCode == entity.yfCode && a.yfId != yfId))
                {
                    throw new FailedException("Mã không được trùng lặp");
                }

                SysMedicineUsageEntity oldEntity = null;   //Trước khi thay đổi Entity
                oldEntity = this.FindEntity(yfId.Value);
                this.DetacheEntity(oldEntity);

                entity.Modify();
                entity.yfId = yfId.Value;
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
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, "xt_ypyf", oldEntity.yfId.ToString());
                }
            }
            else
            {
                //Code Kiểm tra trùng lặp
                if (this.IQueryable().Any(a => a.yfCode == entity.yfCode))
                {
                    throw new FailedException("Mã không được trùng lặp");
                }
                entity.Create();
                this.Insert(entity);

            }

        }

        /// <summary>
        /// Lấy tất cả danh sách
        /// </summary>
        public List<SysMedicineUsageEntity> GetMedicineUsageList(string keyword = null)
        {
            List<SysMedicineUsageEntity> list = null;
            if (!string.IsNullOrEmpty(keyword))
            {
                list = this.IQueryable().Where(a => a.yfmc.Contains(keyword) || a.py.Contains(keyword) || a.yfCode.Contains(keyword)).ToList();
            }
            else
            {
                list = this.IQueryable().ToList();
            }
            return list;
        }

        /// <summary>
        /// Sửa form
        /// </summary>
        public SysMedicineUsageEntity GetMedicineUsageEntity(int? yfId)
        {
            var entity = this.FindEntity(yfId);
            return entity;
        }

        /// <summary>
        /// Xóa
        /// </summary>
        public void DeleteForm(int yfId)
        {
            this.Delete(a => a.yfId == yfId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysMedicineUsageEntity> GetPagintionList(Pagination pagination, string keyword = null)
        {
            var sql = "select * from xt_ypyf(nolock) where  1=1";
            var pars = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and (yfCode like @searchKeyword or yfmc like @searchKeyword or py like @searchKeyword)";
                pars.Add(new SqlParameter("@searchKeyword", "%" + keyword + "%"));
            }
            return this.QueryWithPage<SysMedicineUsageEntity>(sql, pagination, pars.ToArray());
        }
        
    }
}
