using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using FrameworkBase.MultiOrg.Repository;
using FrameworkBase.MultiOrg.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 创 建: tờ 冬林
    /// Ngày kỳ:2017-10-16 11:07
    /// 描 述: menu nhanh 
    /// </summary>
    public class SysShortcutMenuRepo : RepositoryBase<SysShortcutMenuEntity>, ISysShortcutMenuRepo
    {
        /// <summary>
        /// Hàm tạo mặc đ ịnh
        /// </summary>
        /// <param name="databaseFactory">EFNhà máy ngữ cảnh</param>
        public SysShortcutMenuRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }

        /// <summary>
        /// Lấy thực thể danh sách
        /// </summary>
        /// <param name="keyword">LọcTừ khóa</param>
        public IList<SysShortcutMenuEntity> GetList(string keyword)
        {
            var sql = new StringBuilder();
            sql.Append("select * from Sys_ShortcutMenu(nolock) where 1 = 1");
            List<SqlParameter> pars = null;
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                pars = pars ?? new List<SqlParameter>();
                sql.Append(" and(1 = 2");
                sql.Append(" or Name like @keyword");
                sql.Append(" or Code like @keyword");
                sql.Append(")");
                pars.Add(new SqlParameter("@keyword", "%" + keyword.Trim() + "%"));
            }
            return this.FindList<SysShortcutMenuEntity>(sql.ToString(), pars == null ? null : pars.ToArray());
        }

        /// <summary>
        /// Lưu biểu mẫu(Thêm mới, Sửa)
        /// </summary>
        /// <param name="entity">Đ ối tư ợng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public void SubmitForm(SysShortcutMenuEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.Update(entity);
            }
            else
            {
                entity.Create(true);
                this.Insert(entity);
            }
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public void DeleteForm(string keyValue)
        {
            this.Delete(p => p.Id == keyValue);
        }

    }
}