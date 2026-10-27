using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.ValueObjects;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// Người liên hệ nhà cung cấp
    /// </summary>
    public interface IGysContactsRepo : IRepositoryBase<GysContactsEntity>
    {
        /// <summary>
        /// insert contact
        /// </summary>
        /// <param name="supplierContactVo"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        int InserContact(SupplierContactVO supplierContactVo, string organizeId);

        /// <summary>
        /// update contact
        /// </summary>
        /// <param name="supplierContactVo"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        int UpdateContact(SupplierContactVO supplierContactVo);
    }
}