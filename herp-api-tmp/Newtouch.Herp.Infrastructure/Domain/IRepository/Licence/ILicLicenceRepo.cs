using Newtouch.Herp.Domain.Entity;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// 证照维护
    /// </summary>
    public interface ILicLicenceRepo : IRepositoryBase<LicLicenceEntity>
    {
        /// <summary>
        /// submit
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyWord"></param>
        int SubmitForm(LicLicenceEntity entity, string keyWord);
    }
}