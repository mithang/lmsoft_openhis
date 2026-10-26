using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using System.Collections.Generic;
using Newtouch.Core.Common;
using System.Data.SqlClient;
using System.Text;
using FrameworkBase.MultiOrg.Repository;

namespace Newtouch.Repository
{
    /// <summary>
    /// Tạo bởi: 周珏琦
    /// Ngày: 2019-01-28 13:27
    /// Mô tả: Bảng sắp xếp phẫu thuật
    /// </summary>
    public class InpatientOperationArrangementRepo : RepositoryBase<InpatientOperationArrangementEntity>, IInpatientOperationArrangementRepo
    {
        /// <summary>
        /// Hàm khởi tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">Factory ngữ cảnh EF</param>
        public InpatientOperationArrangementRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// Lưu biểu mẫu (thêm mới, sửa đổi)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
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
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public void DeleteForm(string keyValue)
        {
            this.Delete(p => p.Id == keyValue);
        }

    }
}