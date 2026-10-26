using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysCommercialInsuranceFilingRepo : IRepositoryBase<SysCommercialInsuranceFilingEntity>
    {
        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="sbbabId"></param>
        void SubmitForm(SysCommercialInsuranceFilingEntity entity, string sbbabId);

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="sbbabId"></param>
        /// <param name="orgId"></param>
        void DeleteForm(string sbbabId, string orgId);
    }
}
