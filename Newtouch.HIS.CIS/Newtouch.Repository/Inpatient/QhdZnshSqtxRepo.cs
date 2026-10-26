using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository.Inpatient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Repository.Inpatient
{
    public class QhdZnshSqtxRepo : RepositoryBase<QhdZnshSqtxEntity>, IQhdZnshSqtxRepo
    {
        /// <summary>
        /// Hàm khởi tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">Factory ngữ cảnh EF</param>
        public QhdZnshSqtxRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        /// <summary>
        /// Lưu (thêm mới, sửa đổi)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
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
