using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using System.Collections.Generic;
using Newtouch.Core.Common;
using System.Text;
using System.Data.SqlClient;
using Newtouch.Core.Common.Exceptions;
using System.Linq;

namespace Newtouch.OR.ManageSystem.Repository
{
    /// <summary>
    /// Tạo lập：chl
    /// Ngày tạo：2019-11-06 10:22
    /// Mô tả：Nhân viên phẫu thuật
    /// </summary>
    public class ORStaffRepo : RepositoryBase<ORStaffEntity>, IORStaffRepo
    {
        /// <summary>
        /// Hàm khởi tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">EFFactory ngữ cảnh</param>
        public ORStaffRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }

        /// <summary>
        /// Lấy danh sách thực thể phân trang
        /// </summary>
        /// <param name="pagination">Phân trang</param>
        /// <param name="keyword">Từ khóa lọc</param>
        public IList<ORStaffEntity> GetPagintionList(Pagination pagination, string keyword,string organizeId)
        {
            var sql = new StringBuilder();
            sql.Append("select * from OR_Staff(nolock) where zt != 0");
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
                sql.Append(" or Code like @keyword");
                sql.Append(" or Name like @keyword");
                sql.Append(")");
                pars.Add(new SqlParameter("@keyword", "%" + keyword.Trim() + "%"));
            }
            return this.QueryWithPage<ORStaffEntity>(sql.ToString(), pagination, pars == null ? null : pars.ToArray());
        }

        /// <summary>
        /// Lưu biểu mẫu（Thêm mới、Sửa）
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public int SubmitForm(ORStaffEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                if (this.IQueryable().Any(p => p.Code == entity.Code && p.Id != keyValue && p.zt == "1"))
                {
                    throw new FailedException("编号不可重复");
                }
                var dbEntity = this.FindEntity(keyValue);
                //properties
                dbEntity.Code = entity.Code;
                dbEntity.Name = entity.Name;
                dbEntity.zjm = entity.zjm;

                dbEntity.Modify(keyValue);
                return Update(dbEntity);
            }
            else
            {
                if (this.IQueryable().Any(p => p.Code == entity.Code))
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
		/// Lấy danh sách bác sĩ
		/// </summary>
		/// <param name="organizeId"></param>
		/// <returns></returns>
		public IList<ORStaffEntity> GetStaffList(string organizeId, string keyword)
		{
			string sql = @"select * from NewtouchHIS_Base.V_C_Sys_StaffWard where zt != 0";
			if (!string.IsNullOrWhiteSpace(organizeId))
			{
				sql += " and organizeid = @organizeId";
			}
			//if (!string.IsNullOrWhiteSpace(keyword))
			//{
			sql += " and staffPY like @keyword";
			//}
			var result = this.FindList<ORStaffEntity>(sql, new SqlParameter[] {
				new SqlParameter("@organizeId", organizeId),
				new SqlParameter("@keyword", "%" + keyword.Trim() + "%"),
			});
			return result;
		}


		/// <summary>
		/// Lấy danh sách nhân viên cửa sổ nổi
		/// </summary>
		/// <param name="organizeId"></param>
		/// <returns></returns>
		public IList<ORStaffEntity> GetFloatStaffList(string organizeId, string keyword)
		{
			//string sql = @"select distinct staffId Id,staffGh code, staffName name,staffPY zjm,zt,organizeId,dateadd(dd,0,'1900-1-1')  CreateTime,''CreatorCode,dateadd(dd,0,'1900-1-1') LastModifyTime,''LastModifierCode  from NewtouchHIS_Base..V_C_Sys_StaffWard(nolock) where zt != 0";
			string sql = @"select distinct staffId Id,staffGh code, staffName name,staffPY zjm,zt,organizeId,dateadd(dd,0,'1900-1-1')  CreateTime,''CreatorCode,dateadd(dd,0,'1900-1-1') LastModifyTime,''LastModifierCode  
from NewtouchHIS_Base..V_C_Sys_StaffWard(nolock) 
where zt != 0";
			if (!string.IsNullOrWhiteSpace(organizeId))
			{
				sql += " and OrganizeId = @organizeId";
			}
			//if (!string.IsNullOrWhiteSpace(keyword))
			//{
			sql += " and StaffGh like @keyword";
			//}
			var result = this.FindList<ORStaffEntity>(sql, new SqlParameter[] {
				new SqlParameter("@organizeId", organizeId),
				new SqlParameter("@keyword", "%" + keyword.Trim() + "%"),
			});
			return result;
		}
	}
}