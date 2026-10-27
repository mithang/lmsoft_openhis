using Newtouch.Herp.Domain.Entity;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// 证照类型维护
    /// </summary>
    public interface ILicLicenceTypeRepo : IRepositoryBase<LicLicenceTypeEntity>
    {

        /// <summary>
        /// submit
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyWord"></param>
        int SubmitForm(LicLicenceTypeEntity entity, string keyWord);
    }

}