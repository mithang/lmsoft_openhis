using System.Data;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHospMedicinFeeApp
    {
        /// <summary>
        /// theo số nhập việntra cứu tất cả các của tính bản ghi, Đ ã quyết toán và đã hủy bút toán của bản ghi tiến hành đ ối với đối trừ, nhận đ ược chưa hủy bút toán của dữ liệu
        /// </summary>
        /// <param name="zyh"></param>
        DataTable CountYPItemFree(string zyh);

        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="hospItemFeeEntity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(HospDrugBillingEntity hospMedicinFeeEntity, int? keyValue);
    }
}
