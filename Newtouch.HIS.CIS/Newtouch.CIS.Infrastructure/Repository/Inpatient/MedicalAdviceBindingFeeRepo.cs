using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;

using Newtouch.CIS.Repository;
using Newtouch.Infrastructure;
using System.Linq;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using Newtouch.CIS.Compat;
using Newtouch.Domain.ValueObjects.Outpatient;
using System.Collections.Generic;
using Newtouch.Domain.Entity.Inpatient;

namespace Newtouch.Repository
{
    /// <summary>
    /// Tác giả: 冬林
    /// Ngày:2018-06-28 13:27
    /// Mô tả:Nhập việnThông tin bệnh nhân
    /// </summary>
    public class MedicalAdviceBindingFeeRepo : RepositoryBase<MedicalAdviceBindingFeeEntity>, IMedicalAdviceBindingFeeRepo
    {
        /// <summary>
        /// Mặc địnhHàm khởi tạosố
        /// </summary>
        /// <param name="databaseFactory">EFtrêndướivănNhà máy</param>
        public MedicalAdviceBindingFeeRepo()
            : base()
        {

        }

        /// <summary>
        /// Lưubảngphiếu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">thựcthểđối tượng</param>
        /// <param name="keyValue">Khóa chínhGiá trị</param>
        public void SubmitForm(MedicalAdviceBindingFeeEntity entity, string keyValue)
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
            this.Delete(p => p.newid == keyValue);
        }
    }
}