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
                if (this.IQueryable().Any(p => p.Code == entity.Code && p.Id != keyValue && p.zt == "1"))
                {
                    throw new FailedException("Mã không được trùng lặp");
                }
                var dbEntity = this.FindEntity(keyValue);
                //properties
                dbEntity.Code = entity.Code;
                dbEntity.Name = entity.Name;
                dbEntity.Addr = entity.Addr;

                EntityHelper.Modify(dbEntity, keyValue);
                return Update(dbEntity);
            }
            else
            {
                if (this.IQueryable().Any(p => p.Code == entity.Code))
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
            var dbEntity = this.FindEntity(keyValue);
            //properties
            dbEntity.zt = "0";
            EntityHelper.Modify(dbEntity, keyValue);
            return Update(dbEntity);
        }
    }
}