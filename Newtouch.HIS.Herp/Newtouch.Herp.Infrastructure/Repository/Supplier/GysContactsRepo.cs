using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Domain.ValueObjects;
using Newtouch.Herp.Infrastructure.Enum;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// Supplier contacts. Port of the Framework GysContactsRepo to SqlSugar
    /// via the copy's RepositoryBase.
    /// </summary>
    public class GysContactsRepo : RepositoryBase<GysContactsEntity>, IGysContactsRepo
    {
        public GysContactsRepo()
        {
        }

        /// <summary>
        /// insert contact
        /// </summary>
        /// <param name="supplierContactVo"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public int InserContact(SupplierContactVO supplierContactVo, string organizeId)
        {
            if (supplierContactVo == null || string.IsNullOrWhiteSpace(supplierContactVo.contactName)) return 0;
            var entity = new GysContactsEntity
            {
                contactName = supplierContactVo.contactName,
                supplierId = supplierContactVo.keyWord,
                OrganizeId = organizeId,
                duties = supplierContactVo.duties,
                email = supplierContactVo.email,
                phone = supplierContactVo.phone,
                telphone = supplierContactVo.telphone,
                zt = ((int)Enumzt.Enable).ToString()
            };
            entity.Create(true);
            return Insert(entity);
        }

        /// <summary>
        /// update contact
        /// </summary>
        /// <param name="supplierContactVo"></param>
        /// <returns></returns>
        public int UpdateContact(SupplierContactVO supplierContactVo)
        {
            var dbContact = FindEntity(p => p.Id == supplierContactVo.id);
            if (dbContact == null)
            {
                return 0;
            }
            dbContact.contactName = supplierContactVo.contactName;
            dbContact.duties = supplierContactVo.duties;
            dbContact.email = supplierContactVo.email;
            dbContact.phone = supplierContactVo.phone;
            dbContact.telphone = supplierContactVo.telphone;
            dbContact.Modify();
            return Update(dbContact);
        }
    }
}
