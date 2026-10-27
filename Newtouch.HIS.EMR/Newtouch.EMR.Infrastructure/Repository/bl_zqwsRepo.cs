using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using System.Linq;
using NewtouchHIS.Lib.Base.Exceptions;
using System;
using SqlSugar;

namespace Newtouch.EMR.Repository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2018-08-30 11:31
    /// 描 述：住院患者信息
    /// </summary>
    public class bl_zqwsRepo : RepositoryBase<bl_zqwsEntity>, Ibl_zqwsRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>

     

        public bl_zqwsEntity bl_zqwsGetByID(string ID)
        {

            return this.FindEntity(p => p.Id == ID);
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        public void SubmitForm(bl_zqwsEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
                //properties
                dbEntity.IsLock = entity.IsLock;
                dbEntity.blxtmc_hj = entity.blxtmc_hj;
                dbEntity.LastModifierCode = entity.LastModifierCode;
                dbEntity.LastModifyTime = DateTime.Now; dbEntity.LastModifyTime = DateTime.Now;
                this.Update(dbEntity);
            }
            else
            {
        
                entity.CreateTime = DateTime.Now;
                entity.zt = "1";
                entity.IsLock = 0;
                this.Insert(entity);
            }
        }
    }
}
