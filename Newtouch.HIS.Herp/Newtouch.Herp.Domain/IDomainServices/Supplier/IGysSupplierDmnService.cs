namespace Newtouch.Herp.Domain.IDomainServices
{
    /// <summary>
    /// Thao tác nhà cung cấp
    /// </summary>
    public interface IGysSupplierDmnService
    {
        /// <summary>
        /// delete supplier
        /// </summary>
        /// <param name="id">Kho Id</param>
        void DeleteSupplier(string id);
    }
}