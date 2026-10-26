using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysMedicalInsuranceFilingRepo : IRepositoryBase<SysMedicalInsuranceFilingEntity>
    {
        /// <summary>
        /// Lưu
        /// </summary>
        void SubmitForm(SysMedicalInsuranceFilingEntity entity, string ybbabId);

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="keyValue"></param>
        void DeleteForm(string ybbabId, string orgId);
    }
}
