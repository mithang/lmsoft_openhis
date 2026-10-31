
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
        /// Mặc địnhHàm khởi tạosố
        /// </summary>
        /// <param name="databaseFactory">EFtrêndướivănNhà máy</param>
        public QhdZnshSqtxRepo()
            : base()
        {

        }
        /// <summary>
        /// Lưu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">thựcthểđối tượng</param>
        /// <param name="keyValue">Khóa chínhGiá trị</param>
        public void SubmitForm(QhdZnshSqtxEntity entity,out string rzid, string keyValue=null)
        {
            var keyId = "";
            try {
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
               
            }
            catch (Exception er) { }
            rzid = keyId;
        }
    }
}
