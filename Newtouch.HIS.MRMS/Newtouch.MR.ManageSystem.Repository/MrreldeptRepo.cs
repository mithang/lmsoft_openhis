using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.MR.ManageSystem.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using System.Linq;
using System.Data.SqlClient;

namespace Newtouch.MR.ManageSystem.Repository
{
    /// <summary>
    /// Người tạo：chl
    /// Ngày：2019-11-20 13:45
    /// Mô tả: Bảng quan hệ khoa bệnh án và khoa HIS
    /// </summary>
    public class MrreldeptRepo : RepositoryBase<MrreldeptEntity>, IMrreldeptRepo
    {
        /// <summary>
        /// Hàm tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">EFFactory ngữ cảnh</param>
        public MrreldeptRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// Lưu biểu mẫu (thêm mới, sửa đổi）
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public int SubmitForm(MrreldeptEntity entity, string keyValue)
        {
            if (this.IQueryable().Any(p => p.hisdept == entity.hisdept && p.OrganizeId == entity.OrganizeId))
            {
                //hisMã khoa đã tồn tại, cập nhật
                string sql = "update [mr_rel_dept] set baksId=@baksId ,zt='1' ,LastModifyTime=@LastModifyTime ,LastModifierCode=@LastModifierCode where 1 = 1 and hisdept = @hisdept and organizeId=@organizeId";
                SqlParameter[] para ={
                new SqlParameter("@baksId",entity.baksId?? ""),
                new SqlParameter("@LastModifyTime",entity.CreateTime),
                new SqlParameter("@LastModifierCode",entity.CreatorCode ?? ""),
                new SqlParameter("@hisdept",entity.hisdept ?? ""),
                new SqlParameter("@organizeId",entity.OrganizeId ?? ""),
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
                //hisMã khoa đã tồn tại, cập nhật
                string sql = "update [mr_rel_dept] set baksId=@baksId ,zt='1' ,LastModifyTime=@LastModifyTime ,LastModifierCode=@LastModifierCode where 1 = 1 and hisdept = @hisdept and organizeId=@organizeId";
                SqlParameter[] para ={
                new SqlParameter("@baksId",entity.baksId?? ""),
                new SqlParameter("@LastModifyTime",entity.CreateTime),
                new SqlParameter("@LastModifierCode",entity.CreatorCode ?? ""),
                new SqlParameter("@hisdept",entity.hisdept ?? ""),
                new SqlParameter("@organizeId",entity.OrganizeId ?? ""),
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