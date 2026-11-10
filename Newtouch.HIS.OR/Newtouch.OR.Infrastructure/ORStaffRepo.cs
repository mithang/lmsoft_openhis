using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;
using System.Text;
using SqlSugar;
using NewtouchHIS.Lib.Base.Exceptions;
using System.Linq;

namespace Newtouch.OR.ManageSystem.Repository
{
    /// <summary>
    /// Tạo bởi:chl
    /// Ngày:2019-11-06 10:22
    /// Mô tả:Nhân viên phẫu thuật
    /// </summary>
    public class ORStaffRepo : RepositoryBase<ORStaffEntity>, IORStaffRepo
    {
        /// <summary>
        /// Hàm tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">EFFactory ngữ cảnh</param>
        public ORStaffRepo()
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
                sql.Append(" or Code like @keyword");
                sql.Append(" or Name like @keyword");
                sql.Append(")");
                pars.Add(new SugarParameter("@keyword", "%" + keyword.Trim() + "%"));
            }
            return this.QueryWithPage<ORStaffEntity>(sql.ToString(), pagination, pars == null ? null : pars.ToArray());
        }

        /// <summary>
        /// Lưu biểu mẫu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public int SubmitForm(ORStaffEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                // G24 (theo mau G23 ORAnesthesiaRepo): sua chi trong cung co so;
                // khong tim thay -> loi ro rang thay vi NRE.
                var dbEntity = this.FindEntity(p => p.Id == keyValue && p.OrganizeId == entity.OrganizeId && p.zt == "1");
                if (dbEntity == null)
                {
                    throw new FailedException("Không tìm thấy nhân viên phẫu thuật");
                }
                // G24 merge-skip-null (G2): chi kiem tra + ghi de cac truong
                // duoc gui; truong khong gui (null) giu nguyen gia tri cu.
                if (entity.Code != null)
                {
                    if (string.IsNullOrWhiteSpace(entity.Code))
                    {
                        throw new FailedException("Mã nhân viên phẫu thuật không được để trống");
                    }
                    if (entity.Code.Length > 50)
                    {
                        throw new FailedException("Mã nhân viên phẫu thuật tối đa 50 ký tự");
                    }
                    // G24: dup-check chi trong cung co so + dong con hieu luc
                    // (zt=1) — ma cua dong da xoa (zt=0) duoc dung lai, ma
                    // cua co so khac khong chan.
                    if (this.IQueryable().Any(p => p.Code == entity.Code && p.Id != keyValue && p.zt == "1" && p.OrganizeId == entity.OrganizeId))
                    {
                        throw new FailedException("Mã không được trùng lặp");
                    }
                    dbEntity.Code = entity.Code;
                }
                if (entity.Name != null)
                {
                    if (string.IsNullOrWhiteSpace(entity.Name))
                    {
                        throw new FailedException("Tên nhân viên phẫu thuật không được để trống");
                    }
                    if (entity.Name.Length > 100)
                    {
                        throw new FailedException("Tên nhân viên phẫu thuật tối đa 100 ký tự");
                    }
                    dbEntity.Name = entity.Name;
                }
                if (entity.zjm != null)
                {
                    if (entity.zjm.Length > 50)
                    {
                        throw new FailedException("Chữ cái đầu tối đa 50 ký tự");
                    }
                    dbEntity.zjm = entity.zjm;
                }

                EntityHelper.Modify(dbEntity, keyValue);
                return Update(dbEntity);
            }
            else
            {
                // G24: tao moi — ma + ten la bat buoc, gioi han do dai theo
                // cot (Code varchar(50), Name varchar(100), zjm varchar(50)).
                if (string.IsNullOrWhiteSpace(entity.Code))
                {
                    throw new FailedException("Mã nhân viên phẫu thuật không được để trống");
                }
                if (entity.Code.Length > 50)
                {
                    throw new FailedException("Mã nhân viên phẫu thuật tối đa 50 ký tự");
                }
                if (string.IsNullOrWhiteSpace(entity.Name))
                {
                    throw new FailedException("Tên nhân viên phẫu thuật không được để trống");
                }
                if (entity.Name.Length > 100)
                {
                    throw new FailedException("Tên nhân viên phẫu thuật tối đa 100 ký tự");
                }
                if (entity.zjm != null && entity.zjm.Length > 50)
                {
                    throw new FailedException("Chữ cái đầu tối đa 50 ký tự");
                }
                if (this.IQueryable().Any(p => p.Code == entity.Code && p.zt == "1" && p.OrganizeId == entity.OrganizeId))
                {
                    throw new FailedException("Mã không được trùng lặp");
                }
                EntityHelper.Create(entity, true);
                return Insert(entity);
            }
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public int DeleteForm(string keyValue)
        {
            // G24: khoa rong/khong ton tai -> loi ro rang thay vi NRE.
            // Rang buoc co so do controller kiem tra truoc khi goi.
            var dbEntity = this.FindEntity(keyValue);
            if (dbEntity == null)
            {
                throw new FailedException("Không tìm thấy nhân viên phẫu thuật");
            }
            //properties
            dbEntity.zt = "0";
            EntityHelper.Modify(dbEntity, keyValue);
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
			var result = this.FindList<ORStaffEntity>(sql, new SugarParameter[] {
				new SugarParameter("@organizeId", organizeId),
				new SugarParameter("@keyword", "%" + keyword.Trim() + "%"),
			});
			return result;
		}


		/// <summary>
		/// Lấy danh sách nhân viên khung nổi
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
			var result = this.FindList<ORStaffEntity>(sql, new SugarParameter[] {
				new SugarParameter("@organizeId", organizeId),
				// FA-009: keyword null truoc day NRE o keyword.Trim(); null coi
				// nhu khong loc (like '%%'), giong khi UI gui keyword rong.
				new SugarParameter("@keyword", "%" + (keyword ?? "").Trim() + "%"),
			});
			return result;
		}
	}
}