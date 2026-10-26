using System.Data;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHospItemFeeApp
    {
        /// <summary>
        /// theo số nhập viện tính tính phí hạng mục
        /// </summary>
        /// <param name="zyh"></param>
        DataTable CountZYItemFree(string zyh);

        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="hospItemFeeEntity"></param>
        /// <param name="keyValue"></param>
         void SubmitForm(HospItemBillingEntity hospItemFeeEntity, int? keyValue);
    }
}
