using Newtouch.CIS.Compat;

using Newtouch.CIS.Repository;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;

namespace Newtouch.Repository
{
    public class InpatientBedLevelChargeItemRepo : RepositoryBase<InpatientBedLevelChargeItemEntity>, IInpatientBedLevelChargeItemRepo
    {
        /// <summary>
        /// Mặc địnhHàm khởi tạosố
        /// </summary>
        /// <param name="databaseFactory">EFtrêndướivănNhà máy</param>
        public InpatientBedLevelChargeItemRepo()
            : base()
        {
            
        }

        /// <summary>
        /// Lưubảngphiếu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">thựcthểđối tượng</param>
        /// <param name="keyValue">Khóa chínhGiá trị</param>
        public void SubmitForm(InpatientBedLevelChargeItemEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
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
        /// XóaDữ liệu
        /// </summary>
        /// <param name="keyValue">Khóa chínhGiá trị</param>
        public void DeleteForm(string levelCode,string orgId)
        {
            this.Delete(p => p.Id == levelCode && p.zt=="1"&&p.OrganizeId==orgId);
        }

    }
}