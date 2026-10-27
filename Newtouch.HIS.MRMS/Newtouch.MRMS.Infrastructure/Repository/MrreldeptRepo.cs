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
    /// Tạo: chl
    /// Ngày: 2019-11-20 13:45
    /// Mô tả: Bảng quan hệ khoa bệnh án và khoa HIS
    /// </summary>
    public class MrreldeptRepo : RepositoryBase<MrreldeptEntity>, IMrreldeptRepo
    {
        /// <summary>
        /// Hàm tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">Factory ngữ cảnh EF</param>
        public MrreldeptRepo()
            
        {

        }

        /// <summary>
        /// Lưu biểu mẫu(Thêm mới, Sửa)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public int SubmitForm(MrreldeptEntity entity, string keyValue)
        {
            if (this.IQueryable().Any(p => p.hisdept == entity.hisdept && p.OrganizeId == entity.OrganizeId))
            {
                //Mã khoa HIS đã tồn tại Cập nhật
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
            {//Thêm mới
                entity.Create(true);
                return Insert(entity);
            }
        }

        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Save(MrreldeptEntity entity) {
            if (this.IQueryable().Any(p => p.hisdept == entity.hisdept && p.OrganizeId == entity.OrganizeId))
            {
                //Mã khoa HIS đã tồn tại Cập nhật
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
            {//Thêm mới
                entity.Create(true);
                return Insert(entity);
            }
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public int DeleteForm(string keyValue)
        {
            return Delete(p => p.Id == keyValue);
        }
    }
}