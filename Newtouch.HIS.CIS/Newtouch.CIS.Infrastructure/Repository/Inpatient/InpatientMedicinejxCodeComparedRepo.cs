
using Newtouch.CIS.Repository;
using Newtouch.Domain.Entity.Inpatient;
using Newtouch.Domain.IRepository.Inpatient;
using System;
using Newtouch.CIS.Compat;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Repository.Inpatient
{
   public class InpatientMedicinejxCodeComparedRepo : RepositoryBase<InpatientMedicinejxCodeComparedEntity>, IInpatientMedicinejxCodeComparedRepo
    {
        /// <summary>
        /// Mặc định Hàm khởi tạo số
        /// </summary>
        /// <param name="databaseFactory">EF trên dưới văn Nhà máy</param>
        public InpatientMedicinejxCodeComparedRepo()
            : base()
        {

        }

        /// <summary>
        /// Lưu bảng phiếu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">thực thể đối tượng</param>
        /// <param name="keyValue">Khóa chính Giá trị</param>
        public void SubmitForm(InpatientMedicinejxCodeComparedEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
                var ignoreProps = new List<string>() { "Id", "OrganizeId", "CreateTime", "CreatorCode", "LastModifyTime", "LastModifierCode" };
                entity.MapperTo(dbEntity, ignoreProps);
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
        /// Xóa Dữ liệu
        /// </summary>
        /// <param name="keyValue">Khóa chính Giá trị</param>
        public void DeleteForm(string keyValue)
        {
            this.Delete(p => p.Id == keyValue);
        }
    }
}
