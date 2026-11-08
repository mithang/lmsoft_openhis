
using Newtouch.CIS.Repository;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository.Inpatient;
using System;
using Newtouch.CIS.Compat;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Repository.Inpatient
{
    public class QhdZnshSqtxRepo : RepositoryBase<QhdZnshSqtxEntity>, IQhdZnshSqtxRepo
    {
        /// <summary>
        /// Mặc định Hàm khởi tạo số
        /// </summary>
        /// <param name="databaseFactory">EF trên dưới văn Nhà máy</param>
        public QhdZnshSqtxRepo()
            : base()
        {

        }
        /// <summary>
        /// Lưu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">thực thể đối tượng</param>
        /// <param name="keyValue">Khóa chính Giá trị</param>
        public void SubmitForm(QhdZnshSqtxEntity entity,out string rzid, string keyValue=null)
        {
            var keyId = "";
            // C130: bo catch (Exception) { } — loi Insert/Update truoc day bi nuot, caller tra
            // Success gia trong khi khong ghi duoc gi; de exception toi global handler.
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
                dbEntity.XmlResponse = entity.XmlResponse;
                dbEntity.Modify(keyValue);
                this.Update(dbEntity);
                keyId = dbEntity.Id;
            }
            else
            {
                entity.Create(true);
                this.Insert(entity);
                keyId = entity.Id;
            }
            rzid = keyId;
        }
    }
}
