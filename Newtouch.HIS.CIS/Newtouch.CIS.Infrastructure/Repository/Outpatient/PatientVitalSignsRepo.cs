
using Newtouch.CIS.Repository;
using Newtouch.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System;
using System.Collections.Generic;
using Newtouch.CIS.Compat;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class PatientvitalSignsRepo : RepositoryBase<PatientVitalSignsEntity>, IPatientVitalSignsRepo
    {
        public PatientvitalSignsRepo()
            : base()
        {
        }

        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(PatientVitalSignsEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                // G2: keyValue khong ton tai -> Update 0 dong nhung controller van
                // tra Success (thanh cong gia) -> loi trung thuc.
                var oldEntity = this.FindEntity(keyValue);
                if (oldEntity == null)
                {
                    throw new FailedException("Không tìm thấy bản ghi dấu hiệu sinh tồn cần sửa, thao tác thất bại.");
                }
                // G2: gop chi cac field form-post co gia tri (khac null) vao ban ghi
                // cu, giu nguyen field khong sua. MapperTo mau T1 khong dung duoc
                // o day vi no copy ca null (ServiceCompat.MapperTo) -> field khong
                // co trong form bi ghi de thanh null (da probe live baseline).
                var ignoreProps = new HashSet<string>(new[]
                {
                    "Id", "OrganizeId", "CreateTime", "CreatorCode", "LastModifyTime", "LastModifierCode"
                }, System.StringComparer.OrdinalIgnoreCase);
                foreach (var prop in typeof(PatientVitalSignsEntity).GetProperties())
                {
                    if (!prop.CanWrite || !prop.CanRead || ignoreProps.Contains(prop.Name)) continue;
                    var value = prop.GetValue(entity);
                    if (value == null) continue;
                    prop.SetValue(oldEntity, value);
                }
                oldEntity.Modify(keyValue);
                this.Update(oldEntity);
            }
            else
            {
                entity.Id = Guid.NewGuid().ToString();
                entity.Create(true);
                this.Insert(entity);
            }
        }
    }
}
