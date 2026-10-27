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
    /// 
    /// </summary>
    public class SysGlobalConfigRepo : RepositoryBase<SysGlobalConfigEntity>, ISysGlobalConfigRepo
    {
        public SysGlobalConfigRepo()
            
        {

        }

        public SysGlobalConfigEntity GetForm(string keyValue)
        {
            return this.FindEntity(keyValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysGlobalConfigEntity> GetList(string keyword)
        {
            var sql = "select * from Sys_GlobalConfig(nolock) where 1 = 1";
            var pars = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and (Name like @searchKeyword or Code like @searchKeyword)";
                pars.Add(new SqlParameter("@searchKeyword", "%" + keyword + "%"));
            }
            sql += " order by CreateTime desc";
            return this.FindList<SysGlobalConfigEntity>(sql, pars.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<SysGlobalConfigEntity> GetValidList()
        {
            var sql = "select * from Sys_GlobalConfig(nolock) where zt = '1'";
            return this.FindList<SysGlobalConfigEntity>(sql);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysGlobalConfigEntity entity, string keyValue)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                //CodeKiểm tra trùng lặp
                if (this.IQueryable().Any(p => p.Code == entity.Code && p.Id != keyValue))
                {
                    throw new FailedException("Mã không được trùng lặp");
                }

                SysGlobalConfigEntity oldEntity = null;   //Trước khi thay đổiEntity
                oldEntity = this.FindEntity(keyValue);
                this.DetacheEntity(oldEntity);

                entity.Modify(keyValue);
                this.Update(entity);

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, "Sys_GlobalConfig", oldEntity.Id);
                }
            }
            else
            {
                //CodeKiểm tra trùng lặp
                if (this.IQueryable().Any(p => p.Code == entity.Code))
                {
                    throw new FailedException("Mã không được trùng lặp");
                }
                entity.Create(true);
                this.Insert(entity);
            }
        }
    }
}
