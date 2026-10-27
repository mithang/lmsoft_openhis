using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.DomainServices;
using EFDbTransaction = Newtouch.Infrastructure.EF.EFDbTransaction;

namespace Newtouch.Herp.DomainServices.Supplier
{
    /// <summary>
    /// Supplier operations. Port of the Framework GysSupplierDmnService
    /// (which lived in the Warehouse namespace in the original) to the
    /// copy's DmnServiceBase + EFDbTransaction.
    /// </summary>
    public class GysSupplierDmnService : DmnServiceBase, IGysSupplierDmnService
    {
        public GysSupplierDmnService()
        {
        }

        /// <summary>
        /// delete supplier (plus its contacts)
        /// </summary>
        /// <param name="id">supplier Id</param>
        public void DeleteSupplier(string id)
        {
            using (var db = new EFDbTransaction(Db).BeginTrans())
            {
                db.Delete<GysContactsEntity>(p => p.supplierId == id);
                db.Delete<GysSupplierEntity>(p => p.Id == id);
                db.Commit();
            }
        }
    }
}
