using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.MR.ManageSystem.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Newtouch.Core.Common;
using System.Linq;
using Newtouch.Core.Common.Exceptions;

namespace Newtouch.MR.ManageSystem.Repository
{
    /// <summary>
    /// Người tạo：chl
    /// Ngày：2019-11-20 13:45
    /// Mô tả: Khoa bệnh án
    /// </summary>
    public class MrdicdeptRepo : RepositoryBase<MrdicdeptEntity>, IMrdicdeptRepo
    {
        /// <summary>
        /// Hàm tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">EFFactory ngữ cảnh</param>
        public MrdicdeptRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }

        /// <summary>
        /// Lưu biểu mẫu (thêm mới, sửa đổi）
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public int SubmitForm(MrdicdeptEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                if (this.IQueryable().Any(p => p.ksbm == entity.ksbm && p.Id != keyValue && p.zt == "1"))
                {
                    throw new FailedException("编号不可重复");
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
                    throw new FailedException("编号不可重复");
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
        /// Lấy trong hộp thả xuốngKhoa bệnh ánDanh sách
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
            var result = this.FindList<MrdicdeptEntity>(sql, new SqlParameter[] {
                new SqlParameter("@organizeId", organizeId)
            });
            return result;
        }

        /// <summary>
        /// Lấy danh sách bệnh án theo trang
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<MrdicdeptEntity> GetPagintionDicDeptList(Pagination pagination, string organizeId, string keyword)
        {

            var sql = new StringBuilder();
            sql.Append("select * from mr_dic_dept(nolock) where zt != 0");
            List<SqlParameter> pars = null;
            if (!string.IsNullOrWhiteSpace(organizeId))
            {
                pars = pars ?? new List<SqlParameter>();
                sql.Append(" and organizeid = @organizeId");
                pars.Add(new SqlParameter("@organizeId", organizeId));
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                pars = pars ?? new List<SqlParameter>();
                sql.Append(" and(1 = 2");
                sql.Append(" or ksbm like @keyword");
                sql.Append(" or ksmc like @keyword");
                sql.Append(")");
                pars.Add(new SqlParameter("@keyword", "%" + keyword.Trim() + "%"));
            }
            return this.QueryWithPage<MrdicdeptEntity>(sql.ToString(), pagination, pars == null ? null : pars.ToArray());
        }
    }
}