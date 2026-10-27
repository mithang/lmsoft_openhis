using Newtouch.Herp.Domain.Entity;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// Bảo trì đơn vị trực thuộc của chứng chỉ
    /// </summary>
    public interface ILicLicenceBelongedRepo : IRepositoryBase<LicLicenceBelongedEntity>
    {
        /// <summary>
        /// submit
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyWord"></param>
        int SubmitForm(LicLicenceBelongedEntity entity, string keyWord);
    }
}