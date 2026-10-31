using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;

using System.Collections.Generic;
using Newtouch.CIS.Compat;
using NewtouchHIS.Lib.Base.Model;
using System.Data.SqlClient;
using System.Text;
using Newtouch.CIS.Repository;

namespace Newtouch.Repository
{
    /// <summary>
    /// Tác giả: 周珏琦
    /// Ngày:2019-01-28 13:27
    /// Mô tả:Sắp xếp phẫu thuậtbảng
    /// </summary>
    public class InpatientOperationArrangementRepo : RepositoryBase<InpatientOperationArrangementEntity>, IInpatientOperationArrangementRepo
    {
        /// <summary>
        /// Mặc địnhHàm khởi tạosố
        /// </summary>
        /// <param name="databaseFactory">EFtrêndướivănNhà máy</param>
        public InpatientOperationArrangementRepo()
            : base()
        {

        }

        /// <summary>
        /// Lưubảngphiếu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">thựcthểđối tượng</param>
        /// <param name="keyValue">Khóa chínhGiá trị</param>
        public bool SubmitForm(InpatientOperationArrangementEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
                if (dbEntity != null)
                {
                    //properties
                    dbEntity.aprq = entity.aprq;
                    dbEntity.ssAddr = entity.ssAddr;
                    dbEntity.urgent = entity.urgent;
                    dbEntity.surgeonId = entity.surgeonId;
                    dbEntity.surgeonName = entity.surgeonName;
                    dbEntity.assistant = entity.assistant;
                    dbEntity.assistantName = entity.assistantName;
                    dbEntity.anesthesiaType = entity.anesthesiaType;
                    dbEntity.remark = entity.remark;
                    dbEntity.zt = entity.zt;
                    dbEntity.Modify(keyValue);
                    return this.Update(dbEntity) > 0;
                }
            }
            else
            {
                entity.Create(true);
                return this.Insert(entity) > 0;
            }
            return false;
        }

        /// <summary>
        /// XóaDữ liệu
        /// </summary>
        /// <param name="keyValue">Khóa chínhGiá trị</param>
        public void DeleteForm(string keyValue)
        {
            this.Delete(p => p.Id == keyValue);
        }

    }
}