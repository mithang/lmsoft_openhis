using Newtouch.Herp.Domain.Entity;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// Bảo trì loại chứng chỉ
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