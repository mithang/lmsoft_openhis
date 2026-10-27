using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// Bảo trì loại chứng chỉ (ported from Framework EF6 to SqlSugar; behaviour unchanged)
    /// </summary>
    public class LicLicenceTypeRepo : RepositoryBase<LicLicenceTypeEntity>, ILicLicenceTypeRepo
    {
        public LicLicenceTypeRepo()
        {
        }

        /// <summary>
        /// submit
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyWord"></param>
        public int SubmitForm(LicLicenceTypeEntity entity, string keyWord)
        {
            if (string.IsNullOrWhiteSpace(keyWord))
            {
                entity.Create(true);
                return Insert(entity);
            }
            var dbOld = FindEntity(p => p.Id == keyWord);
            if (dbOld == null) return 0;
            dbOld.belongedId = entity.belongedId;
            dbOld.typeName = entity.typeName;
            dbOld.zt = entity.zt;
            dbOld.Modify();
            return Update(dbOld);
        }
    }
}
