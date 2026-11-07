using Newtouch.CIS.Compat;
using Newtouch.Domain.DTO.InputDto.Inpatient;

namespace Newtouch.Application.Interface.Inpatient
{
   public interface IDoctorserviceApp
    {
        /// <summary>
        /// Sửa ThuốcY lệnhkhi,TheoY lệnh Id Lấy Chi tiếtvàvàtriểnhiển thịđếnY lệnhnhậpgiớimặt
        /// </summary>
        /// <param name="yzId"></param>
        /// <param name="yzlx"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
         DoctorServiceparentRequestDto GetYZDetail(string zyh, string yzId, string yzlx, string orgId);
    }
}
