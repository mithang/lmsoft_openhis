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
    /// Tác giả: 冬林
    /// Ngày:2018-06-28 13:27
    /// Mô tả:Nhập việngiường ký tựSử dụngghi chépkho
    /// </summary>
    public class InpatientBedUseRecordRepo : RepositoryBase<InpatientBedUseRecordEntity>, IInpatientBedUseRecordRepo
    {
        /// <summary>
        /// Mặc địnhHàm khởi tạosố
        /// </summary>
        /// <param name="databaseFactory">EFtrêndướivănNhà máy</param>
        public InpatientBedUseRecordRepo()
            : base()
        {
            
        }

        /// <summary>
        /// Lưubảngphiếu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">thựcthểđối tượng</param>
        /// <param name="keyValue">Khóa chínhGiá trị</param>
        public void SubmitForm(InpatientBedUseRecordEntity entity, string keyValue)
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
        /// XóaDữ liệu
        /// </summary>
        /// <param name="keyValue">Khóa chínhGiá trị</param>
        public void DeleteForm(string keyValue)
        {
            this.Delete(p => p.Id == keyValue);
        }

    }
}