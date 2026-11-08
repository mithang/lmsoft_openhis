using Newtouch.CIS.Compat;
using Newtouch.Domain.DTO.InputDto.Inpatient;

namespace Newtouch.Application.Interface.Inpatient
{
   public interface IDoctorserviceApp
    {
        /// <summary>
        /// Sửa ThuốcY lệnh khi,TheoY lệnh Id Lấy Chi tiết và và triển hiển thị đếnY lệnh nhập giới mặt
        /// </summary>
        /// <param name="yzId"></param>
        /// <param name="yzlx"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
         DoctorServiceparentRequestDto GetYZDetail(string zyh, string yzId, string yzlx, string orgId);
    }
}
