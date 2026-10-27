using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// Bảo trì đơn vị trực thuộc của chứng chỉ (ported from Framework EF6 to SqlSugar; behaviour unchanged)
    /// </summary>
    public class LicLicenceBelongedRepo : RepositoryBase<LicLicenceBelongedEntity>, ILicLicenceBelongedRepo
    {
        public LicLicenceBelongedRepo()
        {
        }

        /// <summary>
        /// submit
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyWord"></param>
        public int SubmitForm(LicLicenceBelongedEntity entity, string keyWord)
        {
            if (string.IsNullOrWhiteSpace(keyWord))
            {
                entity.Create(true);
                return Insert(entity);
            }
            var dbOld = FindEntity(p => p.Id == keyWord);
            if (dbOld == null) return 0;
            dbOld.belonged = entity.belonged;
            dbOld.zt = entity.zt;
            dbOld.Modify();
            return Update(dbOld);
        }
    }
}
