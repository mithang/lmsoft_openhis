using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.MR.ManageSystem.Domain.IRepository;
using System.Collections.Generic;
using System.Text;
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
    /// Mô tả: Khoa bệnh án
    /// </summary>
    public class MrdicdeptRepo : RepositoryBase<MrdicdeptEntity>, IMrdicdeptRepo
    {
        /// <summary>
        /// Hàm tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">Factory ngữ cảnh EF</param>
        public MrdicdeptRepo()
            
        {
            
        }

        /// <summary>
        /// Lưu biểu mẫu(Thêm mới, Sửa)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public int SubmitForm(MrdicdeptEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                if (this.IQueryable().Any(p => p.ksbm == entity.ksbm && p.Id != keyValue && p.zt == "1"))
                {
                    throw new FailedException("Mã không được trùng lặp");
                }
                var dbEntity = this.FindEntity(keyValue);
                //properties
                dbEntity.ksbm = entity.ksbm;
                dbEntity.ksmc = entity.ksmc;

                dbEntity.Modify(keyValue);
                return Update(dbEntity);
            }
            else
            {
                if (this.IQueryable().Any(p => p.ksbm == entity.ksbm))
                {
                    throw new FailedException("Mã không được trùng lặp");
                }
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
            var dbEntity = this.FindEntity(keyValue);
            //properties
            dbEntity.zt = "0";
            dbEntity.Modify(keyValue);
            return Update(dbEntity);
        }

        /// <summary>
        /// Lấy danh sách khoa bệnh án trong hộp thả xuống
        /// </summary>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IList<MrdicdeptEntity> GetDicDeptList(string organizeId)
        {
            string sql = @"select * from mr_dic_dept(nolock) where zt != 0";
            if (!string.IsNullOrWhiteSpace(organizeId))
            {
                sql += " and organizeid = @organizeId";
            }
            var result = this.FindList<MrdicdeptEntity>(sql, new SugarParameter[] {
                new SugarParameter("@organizeId", organizeId)
            });
            return result;
        }

        /// <summary>
        /// Lấy danh sách bệnh án phân trang
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<MrdicdeptEntity> GetPagintionDicDeptList(Pagination pagination, string organizeId, string keyword)
        {

            var sql = new StringBuilder();
            sql.Append("select * from mr_dic_dept(nolock) where zt != 0");
            List<SugarParameter> pars = null;
            if (!string.IsNullOrWhiteSpace(organizeId))
            {
                pars = pars ?? new List<SugarParameter>();
                sql.Append(" and organizeid = @organizeId");
                pars.Add(new SugarParameter("@organizeId", organizeId));
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                pars = pars ?? new List<SugarParameter>();
                sql.Append(" and(1 = 2");
                sql.Append(" or ksbm like @keyword");
                sql.Append(" or ksmc like @keyword");
                sql.Append(")");
                pars.Add(new SugarParameter("@keyword", "%" + keyword.Trim() + "%"));
            }
            return this.QueryWithPage<MrdicdeptEntity>(sql.ToString(), pagination, pars == null ? null : pars.ToArray());
        }
    }
}