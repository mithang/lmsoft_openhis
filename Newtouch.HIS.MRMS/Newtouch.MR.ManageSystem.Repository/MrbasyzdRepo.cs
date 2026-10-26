using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.MR.ManageSystem.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;

namespace Newtouch.MR.ManageSystem.Repository
{
    /// <summary>
    /// Người tạo：chl
    /// Ngày：2019-11-20 11:05
    /// Mô tả: Bảng chẩn đoán trang đầu bệnh án
    /// </summary>
    public class MrbasyzdRepo : RepositoryBase<MrbasyzdEntity>, IMrbasyzdRepo
    {
        /// <summary>
        /// Hàm tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">EFFactory ngữ cảnh</param>
        public MrbasyzdRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }

        /// <summary>
        /// Lưu biểu mẫu (thêm mới, sửa đổi）
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public void SubmitForm(MrbasyzdEntity entity, string keyValue)
        {
            if(entity!=null)
            {

                if (!string.IsNullOrEmpty(keyValue))
                {
                    var dbEntity = this.FindEntity(keyValue);
                    dbEntity.CYQK = entity.CYQK;
                    dbEntity.CYQKMS = entity.CYQKMS;
                    dbEntity.JBDM = entity.JBDM;
                    dbEntity.JBMC = entity.JBMC;
                    dbEntity.ZDOrder = entity.ZDOrder;
                    dbEntity.RYBQ = entity.RYBQ;
                    dbEntity.RYBQMS = entity.RYBQMS;
                    dbEntity.zt = entity.zt;

                    dbEntity.Modify(keyValue);
                    Update(dbEntity);
                }
                entity.Create(true);
                Insert(entity);
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