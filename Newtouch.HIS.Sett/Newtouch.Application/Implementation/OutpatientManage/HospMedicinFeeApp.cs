using System.Data;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using FrameworkBase.MultiOrg.Application;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class HospMedicinFeeApp : AppBase, IHospMedicinFeeApp
    {
        private readonly IHospDrugBillingRepo _hospMedicinFeeRepository;

        /// <summary>
        /// theo số nhập việntra cứu tất cả các của tính bản ghi, Đ ã quyết toán và đã hủy bút toán của bản ghi tiến hành đ ối với đối trừ, nhận đ ược chưa hủy bút toán của dữ liệu
        /// </summary>
        /// <param name="zyh"></param>
        public DataTable CountYPItemFree(string zyh)
        {
            return this.CountYPItemFree(zyh);
        }

        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="hospItemFeeEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(HospDrugBillingEntity hospMedicinFeeEntity, int? keyValue)
        {
            _hospMedicinFeeRepository.SubmitForm(hospMedicinFeeEntity, keyValue);
        }
    }
}
