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
    /// Mô tả: Nhập viện thông tin bệnh nhân
    /// </summary>
    public class MedicalAdviceBindingFeeRepo : RepositoryBase<MedicalAdviceBindingFeeEntity>, IMedicalAdviceBindingFeeRepo
    {
        /// <summary>
        /// Hàm khởi tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">Nhà máy ngữ cảnh EF</param>
        public MedicalAdviceBindingFeeRepo()
            : base()
        {

        }

        /// <summary>
        /// Lưu bảng phiếu (Thêm mới, Sửa)
        /// </summary>
        /// <param name="entity">thực thể đối tượng</param>
        /// <param name="keyValue">Khóa chính giá trị</param>
        public void SubmitForm(MedicalAdviceBindingFeeEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
                var ignoreProps = new List<string>() { "newid", "OrganizeId", "CreateTime", "CreatorCode", "LastModifyTime", "LastModifierCode" };
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
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Khóa chính giá trị</param>
        public void DeleteForm(string keyValue)
        {
            this.Delete(p => p.newid == keyValue);
        }
    }
}