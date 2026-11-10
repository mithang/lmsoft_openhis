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
            // telphone NOT NULL trong DB, UI cũng bắt buộc nhập di động.
            if (string.IsNullOrWhiteSpace(supplierContactVo.telphone)) return 0;
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
            // Chỉ copy field khác null: telphone/contactName NOT NULL trong
            // DB, POST thiếu field mà copy null sẽ lỗi SQL thô.
            if (supplierContactVo.contactName != null) dbContact.contactName = supplierContactVo.contactName;
            if (supplierContactVo.duties != null) dbContact.duties = supplierContactVo.duties;
            if (supplierContactVo.email != null) dbContact.email = supplierContactVo.email;
            if (supplierContactVo.phone != null) dbContact.phone = supplierContactVo.phone;
            if (supplierContactVo.telphone != null) dbContact.telphone = supplierContactVo.telphone;
            dbContact.Modify();
            return Update(dbContact);
        }
    }
}
