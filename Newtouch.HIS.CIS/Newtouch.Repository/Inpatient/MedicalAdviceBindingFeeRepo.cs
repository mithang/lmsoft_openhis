using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Infrastructure;
using System.Linq;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Domain.ValueObjects.Outpatient;
using System.Collections.Generic;
using Newtouch.Domain.Entity.Inpatient;

namespace Newtouch.Repository
{
    /// <summary>
    /// Tạo bởi: 张冬林
    /// Ngày: 2018-06-28 13:27
    /// Mô tả: Thông tin bệnh nhân nội trú
    /// </summary>
    public class MedicalAdviceBindingFeeRepo : RepositoryBase<MedicalAdviceBindingFeeEntity>, IMedicalAdviceBindingFeeRepo
    {
        /// <summary>
        /// Hàm khởi tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">Factory ngữ cảnh EF</param>
        public MedicalAdviceBindingFeeRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// Lưu biểu mẫu (thêm mới, sửa đổi)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
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
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public void DeleteForm(string keyValue)
        {
            this.Delete(p => p.newid == keyValue);
        }
    }
}