using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;
using System.Text;
using SqlSugar;
using NewtouchHIS.Lib.Base.Exceptions;
using System.Linq;
using System;
using Newtouch.OR.ManageSystem.Domain.DTO.OutputDto;

namespace Newtouch.OR.ManageSystem.Repository
{
    /// <summary>
    /// Tạo bởi:chl
    /// Ngày:2019-10-31 14:15
    /// Mô tả:Bảng từ điển phẫu thuật
    /// </summary>
    public class OROperationRepo : RepositoryBase<OROperationEntity>, IOROperationRepo
    {

		private readonly ISysConfigRepo _sysConfigRepo;

		/// <summary>
		/// Hàm tạo mặc định
		/// </summary>
		/// <param name="databaseFactory">EFFactory ngữ cảnh</param>
		public OROperationRepo(ISysConfigRepo SysConfigRepo)
        {
			_sysConfigRepo = SysConfigRepo;
		}

        /// <summary>
        /// Lấy danh sách thực thể phân trang
        /// </summary>
        /// <param name="pagination">Phân trang</param>
        /// <param name="keyword">Từ khóa lọc</param>
        public IList<OROperationEntity> GetPagintionList(Pagination pagination, string keyword,string organizeId)
        {
            var sql = new StringBuilder();
            sql.Append("select * from OR_Operation(nolock) where zt != 0");
            List <SugarParameter> pars = null;
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
                sql.Append(" or ssdm like @keyword");
                sql.Append(" or ssmc like @keyword");
                sql.Append(")");
                pars.Add(new SugarParameter("@keyword", "%" + keyword.Trim() + "%"));
            }
            return this.QueryWithPage<OROperationEntity>(sql.ToString(), pagination, pars == null ? null : pars.ToArray());
        }

        /// <summary>
        /// Lưu biểu mẫu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public int SubmitForm(OROperationEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                // G24 (theo mau G23 ORAnesthesiaRepo): sua chi trong cung co so;
                // khong tim thay -> loi ro rang thay vi NRE.
                var dbEntity = this.FindEntity(p => p.Id == keyValue && p.OrganizeId == entity.OrganizeId && p.zt == "1");
                if (dbEntity == null)
                {
                    throw new FailedException("Không tìm thấy phẫu thuật");
                }
                // G24 merge-skip-null (G2): chi kiem tra + ghi de cac truong
                // duoc gui; truong khong gui (null) giu nguyen gia tri cu.
                // Ma gui rong (chuoi rong) la loi; ma khong gui thi giu ma cu
                // nen khong the trung — bo qua dup-check trong truong hop do.
                if (entity.ssdm != null)
                {
                    if (string.IsNullOrWhiteSpace(entity.ssdm))
                    {
                        throw new FailedException("Mã phẫu thuật không được để trống");
                    }
                    if (entity.ssdm.Length > 50)
                    {
                        throw new FailedException("Mã phẫu thuật tối đa 50 ký tự");
                    }
                    // G24: dup-check chi trong cung co so + dong con hieu luc
                    // (zt=1) — ma cua dong da xoa (zt=0) duoc dung lai, ma
                    // cua co so khac khong chan.
                    if (this.IQueryable().Any(p => p.ssdm == entity.ssdm && p.Id != keyValue && p.zt == "1" && p.OrganizeId == entity.OrganizeId))
                    {
                        throw new FailedException("Mã không được trùng lặp");
                    }
                    dbEntity.ssdm = entity.ssdm;
                }
                if (entity.ssmc != null)
                {
                    if (string.IsNullOrWhiteSpace(entity.ssmc))
                    {
                        throw new FailedException("Tên phẫu thuật không được để trống");
                    }
                    if (entity.ssmc.Length > 100)
                    {
                        throw new FailedException("Tên phẫu thuật tối đa 100 ký tự");
                    }
                    dbEntity.ssmc = entity.ssmc;
                }
                if (entity.zjm != null)
                {
                    if (entity.zjm.Length > 50)
                    {
                        throw new FailedException("Chữ cái đầu tối đa 50 ký tự");
                    }
                    dbEntity.zjm = entity.zjm;
                }
                if (entity.ssjb != null)
                {
                    if (entity.ssjb.Length > 10)
                    {
                        throw new FailedException("Cấp phẫu thuật tối đa 10 ký tự");
                    }
                    dbEntity.ssjb = entity.ssjb;
                }

                EntityHelper.Modify(dbEntity, keyValue);
                return Update(dbEntity);
            }
            else
            {
                // G24: tao moi — ma + ten la bat buoc, gioi han do dai theo
                // cot (ssdm varchar(50), ssmc varchar(100)); chan truoc
                // dup-check de bao loi dung nghia thay vi "Ma khong duoc
                // trung lap" (ma rong khop dong NULL san co) hay loi cat
                // chuoi cua SQL.
                if (string.IsNullOrWhiteSpace(entity.ssdm))
                {
                    throw new FailedException("Mã phẫu thuật không được để trống");
                }
                if (entity.ssdm.Length > 50)
                {
                    throw new FailedException("Mã phẫu thuật tối đa 50 ký tự");
                }
                if (string.IsNullOrWhiteSpace(entity.ssmc))
                {
                    throw new FailedException("Tên phẫu thuật không được để trống");
                }
                if (entity.ssmc.Length > 100)
                {
                    throw new FailedException("Tên phẫu thuật tối đa 100 ký tự");
                }
                if (entity.zjm != null && entity.zjm.Length > 50)
                {
                    throw new FailedException("Chữ cái đầu tối đa 50 ký tự");
                }
                if (entity.ssjb != null && entity.ssjb.Length > 10)
                {
                    throw new FailedException("Cấp phẫu thuật tối đa 10 ký tự");
                }
                if (this.IQueryable().Any(p => p.ssdm == entity.ssdm && p.zt == "1" && p.OrganizeId == entity.OrganizeId))
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
                throw new FailedException("Không tìm thấy phẫu thuật");
            }
            //properties
            dbEntity.zt = "0";
            EntityHelper.Modify(dbEntity, keyValue);
            return Update(dbEntity);
        }

        /// <summary>
        /// Lấy danh sách phẫu thuật
        /// </summary>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IList<OROperationEntity> GetOperationList(string organizeId,string keyword)
        {
            string sql = @"select * from OR_Operation(nolock) where zt != 0";
            if (!string.IsNullOrWhiteSpace(organizeId))
            {
                sql += " and organizeid = @organizeId";
            }
            //if (!string.IsNullOrWhiteSpace(keyword))
            //{
                sql += " and zjm like @keyword";
            //}
            var result = this.FindList<OROperationEntity>(sql, new SugarParameter[] {
                new SugarParameter("@organizeId", organizeId),
                new SugarParameter("@keyword", "%" + keyword.Trim() + "%"),
            });
            return result;
        }


		/// <summary>
		/// Danh sách từ điển phẫu thuật
		/// </summary>
		/// <param name="orgId"></param>
		/// <param name="keyword"></param>
		/// <returns></returns>
		public IList<SysOpListDto> OpList(string orgId, string keyword, bool type)
		{
			string sql = "";
			string dd = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd HH:mm:ss");
			var showItems = _sysConfigRepo.GetByCode("ShowOpItemsCount", orgId);

			if (type && string.IsNullOrWhiteSpace(keyword))
			{
				var topCount = 100;
				if (showItems != null && int.TryParse(showItems.Value, out var parsedTop) && parsedTop > 0) topCount = parsedTop;
				sql = "select top " + topCount + " ssdm,ssmc,zjm,ssjb from [Newtouch_OR].[dbo].[OR_Operation] a with(nolock) " +
					"where OrganizeId=@orgId and zt='1'" ;

				var list = this.FindList<SysOpListDto>(sql, new SugarParameter[] {
				new SugarParameter("@orgId",orgId),
				new SugarParameter("@date",dd) });

				if (list == null || list.Count < topCount)
				{
					sql = @"select top " + topCount + " ssdm,ssmc,zjm,ssjb from [Newtouch_OR].[dbo].[OR_Operation] with(nolock) where OrganizeId=@orgId and zt='1' ";
				}
				else
				{
					return list;
				}
			}
			else
			{
				var topCount = 100;
				if (showItems != null && int.TryParse(showItems.Value, out var parsedTop) && parsedTop > 0) topCount = parsedTop;
				sql = @"select top " + topCount + @" ssdm,ssmc,zjm,ssjb from [Newtouch_OR].[dbo].[OR_Operation] with(nolock) where OrganizeId=@orgId and zt='1' ";
			}

			if (!string.IsNullOrWhiteSpace(keyword))
			{
				sql += " and (ssdm like @kwd or ssmc like @kwd or zjm like @kwd)";
			}
			return this.FindList<SysOpListDto>(sql, new SugarParameter[] {
				new SugarParameter("@orgId",orgId),
				new SugarParameter("@kwd",string.IsNullOrWhiteSpace(keyword)==true? "":keyword+"%"),
				new SugarParameter("@date",dd)
			});
		}
	}
}