using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.MR.ManageSystem.Domain.IRepository;
using System.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SqlSugar;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.MR.ManageSystem.Infrastructure;

namespace Newtouch.MR.ManageSystem.Repository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2019-11-20 13:45
    /// 描 述：病案科室与his科室关系表
    /// </summary>
    public class MrreldeptRepo : RepositoryBase<MrreldeptEntity>, IMrreldeptRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public MrreldeptRepo()
            
        {

        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        public int SubmitForm(MrreldeptEntity entity, string keyValue)
        {
            if (this.IQueryable().Any(p => p.hisdept == entity.hisdept && p.OrganizeId == entity.OrganizeId))
            {
                //his科室代码存在 更新
                string sql = "update [mr_rel_dept] set baksId=@baksId ,zt='1' ,LastModifyTime=@LastModifyTime ,LastModifierCode=@LastModifierCode where 1 = 1 and hisdept = @hisdept and organizeId=@organizeId";
                SugarParameter[] para ={
                new SugarParameter("@baksId",entity.baksId?? ""),
                new SugarParameter("@LastModifyTime",entity.CreateTime),
                new SugarParameter("@LastModifierCode",entity.CreatorCode ?? ""),
                new SugarParameter("@hisdept",entity.hisdept ?? ""),
                new SugarParameter("@organizeId",entity.OrganizeId ?? ""),
                };
                return this.ExecuteSqlCommand(sql, para);
            }
            else
            {//新增
                entity.Create(true);
                return Insert(entity);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Save(MrreldeptEntity entity) {
            if (this.IQueryable().Any(p => p.hisdept == entity.hisdept && p.OrganizeId == entity.OrganizeId))
            {
                //his科室代码存在 更新
                string sql = "update [mr_rel_dept] set baksId=@baksId ,zt='1' ,LastModifyTime=@LastModifyTime ,LastModifierCode=@LastModifierCode where 1 = 1 and hisdept = @hisdept and organizeId=@organizeId";
                SugarParameter[] para ={
                new SugarParameter("@baksId",entity.baksId?? ""),
                new SugarParameter("@LastModifyTime",entity.CreateTime),
                new SugarParameter("@LastModifierCode",entity.CreatorCode ?? ""),
                new SugarParameter("@hisdept",entity.hisdept ?? ""),
                new SugarParameter("@organizeId",entity.OrganizeId ?? ""),
                };
                return this.ExecuteSqlCommand(sql, para);
            }
            else
            {//新增
                entity.Create(true);
                return Insert(entity);
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        public int DeleteForm(string keyValue)
        {
            return Delete(p => p.Id == keyValue);
        }
    }
}