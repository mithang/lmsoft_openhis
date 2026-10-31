using Newtouch.Domain.Entity;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.CIS.Repository;
using Newtouch.Domain.Entity.Inpatient;

namespace Newtouch.Domain.IRepository
{
    public interface IMedicalAdviceBindingFeeRepo : IRepositoryBase<MedicalAdviceBindingFeeEntity>
    {
        /// <summary>
        /// Lưubảngphiếu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">thựcthểđối tượng</param>
        /// <param name="keyValue">Khóa chínhGiá trị</param>
        void SubmitForm(MedicalAdviceBindingFeeEntity entity, string keyValue);

        /// <summary>
        /// XóaDữ liệu
        /// </summary>
        /// <param name="keyValue">Khóa chínhGiá trị</param>
        void DeleteForm(string keyValue);

    }
}
