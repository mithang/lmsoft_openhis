using Newtouch.HIS.Domain.DTO.OutputDto;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// Hủy quyết toán nhập viện Giao diện
    /// </summary>
    public interface IHospSettCancelApp
    {
        /// <summary>
        /// Hủy quyết toán nhập viện,XemBệnh nhânNhập viện Trạng tháiThông tin
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="kh"></param>
        /// <returns></returns>
        HospSettCancelPatStatusDetailDto GetPatHospStatusDetail(string zyh, string kh);

        /// <summary>
        /// Hủy quyết toán nhập viện GửiLưuThao tác
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="expectedjsnm"></param>
        /// <param name="cancelReason"></param>
        void DoCancel(string zyh, int expectedjsnm, string cancelReason);

    }
}
