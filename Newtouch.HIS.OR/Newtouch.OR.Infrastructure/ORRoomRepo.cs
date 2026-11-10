using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;
using System.Text;
using SqlSugar;
using System.Linq;
using NewtouchHIS.Lib.Base.Exceptions;

namespace Newtouch.OR.ManageSystem.Repository
{
    /// <summary>
    /// Tạo bởi:chl
    /// Ngày:2019-11-06 10:22
    /// Mô tả:Phòng phẫu thuật
    /// </summary>
    public class ORRoomRepo : RepositoryBase<ORRoomEntity>, IORRoomRepo
    {
        /// <summary>
        /// Hàm tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">EFFactory ngữ cảnh</param>
        public ORRoomRepo()
        {
            
        }

        /// <summary>
        /// Lấy danh sách thực thể phân trang
        /// </summary>
        /// <param name="pagination">Phân trang</param>
        /// <param name="keyword">Từ khóa lọc</param>
        public IList<ORRoomEntity> GetPagintionList(Pagination pagination, string keyword,string organizeId)
        {
            var sql = new StringBuilder();
            sql.Append("select * from OR_Room(nolock) where zt != 0");
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
                sql.Append(" or Addr like @keyword");
                sql.Append(")");
                pars.Add(new SugarParameter("@keyword", "%" + keyword.Trim() + "%"));
            }
            return this.QueryWithPage<ORRoomEntity>(sql.ToString(), pagination, pars == null ? null : pars.ToArray());
        }

        /// <summary>
        /// Lưu biểu mẫu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public int SubmitForm(ORRoomEntity entity, string keyValue)
        {

            if (!string.IsNullOrEmpty(keyValue))
            {
                // G24 (theo mau G23 ORAnesthesiaRepo): sua chi trong cung co so;
                // khong tim thay -> loi ro rang thay vi NRE.
                var dbEntity = this.FindEntity(p => p.Id == keyValue && p.OrganizeId == entity.OrganizeId && p.zt == "1");
                if (dbEntity == null)
                {
                    throw new FailedException("Không tìm thấy phòng phẫu thuật");
                }
                // G24 merge-skip-null (G2): chi kiem tra + ghi de cac truong
                // duoc gui; truong khong gui (null) giu nguyen gia tri cu.
                if (entity.Code != null)
                {
                    if (string.IsNullOrWhiteSpace(entity.Code))
                    {
                        throw new FailedException("Mã phòng phẫu thuật không được để trống");
                    }
                    if (entity.Code.Length > 50)
                    {
                        throw new FailedException("Mã phòng phẫu thuật tối đa 50 ký tự");
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
                        throw new FailedException("Tên phòng phẫu thuật không được để trống");
                    }
                    if (entity.Name.Length > 100)
                    {
                        throw new FailedException("Tên phòng phẫu thuật tối đa 100 ký tự");
                    }
                    dbEntity.Name = entity.Name;
                }
                if (entity.Addr != null)
                {
                    if (entity.Addr.Length > 200)
                    {
                        throw new FailedException("Địa chỉ tối đa 200 ký tự");
                    }
                    dbEntity.Addr = entity.Addr;
                }

                EntityHelper.Modify(dbEntity, keyValue);
                return Update(dbEntity);
            }
            else
            {
                // G24: tao moi — ma + ten la bat buoc, gioi han do dai theo
                // cot (Code varchar(50), Name varchar(100), Addr varchar(200)).
                if (string.IsNullOrWhiteSpace(entity.Code))
                {
                    throw new FailedException("Mã phòng phẫu thuật không được để trống");
                }
                if (entity.Code.Length > 50)
                {
                    throw new FailedException("Mã phòng phẫu thuật tối đa 50 ký tự");
                }
                if (string.IsNullOrWhiteSpace(entity.Name))
                {
                    throw new FailedException("Tên phòng phẫu thuật không được để trống");
                }
                if (entity.Name.Length > 100)
                {
                    throw new FailedException("Tên phòng phẫu thuật tối đa 100 ký tự");
                }
                if (entity.Addr != null && entity.Addr.Length > 200)
                {
                    throw new FailedException("Địa chỉ tối đa 200 ký tự");
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
                throw new FailedException("Không tìm thấy phòng phẫu thuật");
            }
            //properties
            dbEntity.zt = "0";
            EntityHelper.Modify(dbEntity, keyValue);
            return Update(dbEntity);
        }
    }
}