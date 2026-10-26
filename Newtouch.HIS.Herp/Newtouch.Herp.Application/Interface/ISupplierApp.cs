using Newtouch.Herp.Domain.Entity;

namespace Newtouch.Herp.Application.Interface
{
    /// <summary>
    /// Xử lý nhà cung cấp
    /// </summary>
    public interface ISupplierApp
    {
        /// <summary>
        /// submit Supplier maintenance form
        /// </summary>
        /// <param name="supplierEntity"></param>
        /// <param name="keyWord"></param>
        void SubmitForm(GysSupplierEntity supplierEntity, string keyWord);

        /// <summary>
        /// Tạo nhanh nhà sản xuất  Trả về nhà sản xuất ID
        /// </summary>
        /// <param name="name"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string CreateProducerQuick(string id, string name, string organizeId);
    }
}