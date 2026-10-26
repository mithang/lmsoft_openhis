using Newtouch.Domain.DTO.InputDto.Inpatient;

namespace Newtouch.Application.Interface.Inpatient
{
   public interface IDoctorserviceApp
    {
        /// <summary>
        /// Khi sửa y lệnh thuốc, lấy chi tiết theo Id y lệnh và hiển thị lên giao diện nhập y lệnh
        /// </summary>
        /// <param name="yzId"></param>
        /// <param name="yzlx"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
         DoctorServiceparentRequestDto GetYZDetail(string zyh, string yzId, string yzlx, string orgId);
    }
}
