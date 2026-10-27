using Newtouch.Infrastructure.EF;
using FrameworkBase.MultiOrg.Application;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.PDS.Requset;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// Quản lý thông tin cơ bản bệnh nhân
    /// </summary>
    public class PatientBaseInfoApp : AppBase, IPatientBaseInfoApp
    {
        private readonly IMzCfRepo _mzCfRepo;

        /// <summary>
        /// Bổ sung thông tin cơ bản bệnh nhân ngoại trú
        /// </summary>
        /// <param name="pbi"></param>
        /// <returns></returns>
        public string SupplementMzPatientBaseInfo(SupplementPatientBaseInfoRequest pbi)
        {
            if (pbi != null)
            {
                return _mzCfRepo.UpdateGender(pbi.cfh, pbi.xb, pbi.OrganizeId) > 0 ? "" : string.Format("Theo số đơn thuốc：【{0}】、Tổ chứcID：【{1}】、Giới tính：【{2}】Không tìm thấy bản ghi có thể cập nhật", pbi.cfh, pbi.OrganizeId, pbi.xb);
            }

            return "Thông tin cơ bản bệnh nhân truyền vào không được để trống！";
        }
    }
}