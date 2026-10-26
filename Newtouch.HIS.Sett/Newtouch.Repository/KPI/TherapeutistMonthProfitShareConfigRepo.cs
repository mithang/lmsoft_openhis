using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 创 建: tờ 冬林
    /// Ngày kỳ:2018-01-29 15:52
    /// 描 述:Chuyên viên trị liệu利润提成cấu hình
    /// </summary>
    public class TherapeutistMonthProfitShareConfigRepo : RepositoryBase<TherapeutistMonthProfitShareConfigEntity>, ITherapeutistMonthProfitShareConfigRepo
    {
        /// <summary>
        /// Hàm tạo mặc đ ịnh
        /// </summary>
        /// <param name="databaseFactory">EFNhà máy ngữ cảnh</param>
        public TherapeutistMonthProfitShareConfigRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// Lưu biểu mẫu(Thêm mới, Sửa)
        /// </summary>
        /// <param name="entity">Đ ối tư ợng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public void SubmitForm(TherapeutistMonthProfitShareConfigEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
                //properties

                dbEntity.Modify(keyValue);
                this.Update(dbEntity);
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