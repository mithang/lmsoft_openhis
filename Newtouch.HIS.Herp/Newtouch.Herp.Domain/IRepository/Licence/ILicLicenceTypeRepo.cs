using Newtouch.Herp.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// Bao tri loai giay phep
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