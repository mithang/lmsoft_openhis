using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFinancialInvoiceRepo : IRepositoryBase<FinancialInvoiceEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lyry"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<FinancialInvoiceEntity> GetListByUserCode(string lyry, string orgId);

        /// <summary>
        /// Dùng hóa đơn,Thay đổi CSDL Đồng bộ số hóa đơn hiện tại
        /// </summary>
        /// <param name="fph"></param>
        /// <param name="lyry"></param>
        /// <param name="updateEntity"></param>
        /// <param name="insertEntity"></param>
        /// <param name="orgId"></param>
        void UpdateCurrentGetEntitys(string fph, string lyry, out FinancialInvoiceEntity updateEntity, out FinancialInvoiceEntity insertEntity, string orgId);

        /// <summary>
        /// Lấy danh sách tất cả hóa đơn
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<FinancialInvoiceEntity> GetFinancialInvoiceList(string keyValue, string organizeId);

        /// <summary>
        /// Trang sửa đổi,Lấy thực thể theo khóa chính
        /// </summary>
        /// <param name="fpdm"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        FinancialInvoiceEntity GetFinancialInvoiceEntity(string fpdm,string orgId);

        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="financialInvoiceEntity"></param>
        /// <param name="fpdm"></param>
        void SubmitForm(FinancialInvoiceEntity financialInvoiceEntity, string fpdm);

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="fpdm"></param>
        /// <param name="orgId"></param>
        void DeleteForm(string fpdm, string orgId);

        void VlidateRepeat(FinancialInvoiceEntity entity, string orgId, string Id);
        }
}
