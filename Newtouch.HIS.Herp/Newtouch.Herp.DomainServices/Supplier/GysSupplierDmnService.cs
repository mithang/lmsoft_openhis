using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IDomainServices;
using EFDbTransaction = Newtouch.Infrastructure.EF.EFDbTransaction;

namespace Newtouch.Herp.DomainServices.Warehouse
{
    /// <summary>
    /// Thao tác nhà cung cấp
    /// </summary>
    public class GysSupplierDmnService : DmnServiceBase, IGysSupplierDmnService
    {
        public GysSupplierDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// delete supplier
        /// </summary>
        /// <param name="id">Kho Id</param>
        public void DeleteSupplier(string id)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                db.Delete<GysContactsEntity>(p => p.supplierId == id);
                db.Delete<GysSupplierEntity>(p => p.Id == id);
                db.Commit();
            }
        }
    }
}
