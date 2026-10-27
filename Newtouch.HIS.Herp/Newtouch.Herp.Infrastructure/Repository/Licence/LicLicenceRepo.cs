using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// Bảo trì chứng chỉ (ported from Framework EF6 to SqlSugar; behaviour unchanged)
    /// </summary>
    public class LicLicenceRepo : RepositoryBase<LicLicenceEntity>, ILicLicenceRepo
    {
        public LicLicenceRepo()
        {
        }

        /// <summary>
        /// submit
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyWord"></param>
        public int SubmitForm(LicLicenceEntity entity, string keyWord)
        {
            if (string.IsNullOrWhiteSpace(keyWord))
            {
                entity.Create(true);
                return Insert(entity);
            }
            var dbOld = FindEntity(p => p.Id == keyWord);
            if (dbOld == null) return 0;
            dbOld.belongedId = entity.belongedId;
            dbOld.fileUrl = string.IsNullOrWhiteSpace(entity.fileUrl) ? dbOld.fileUrl : entity.fileUrl;
            dbOld.licenceNo = entity.licenceNo;
            dbOld.licenceTypeId = entity.licenceTypeId;
            dbOld.objectId = entity.objectId;
            dbOld.objectName = entity.objectName;
            dbOld.qxrq = entity.qxrq;
            dbOld.sxrq = entity.sxrq;
            dbOld.zt = entity.zt;
            dbOld.Modify();
            return Update(dbOld);
        }
    }
}
