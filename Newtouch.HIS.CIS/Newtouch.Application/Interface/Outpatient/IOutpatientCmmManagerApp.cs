using System.Collections.Generic;
using FrameworkBase.MultiOrg.Domain.ValueObjects;
using Newtouch.CIS.Proxy.CMMPlatform.DTO;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_07;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_08;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_09;
using Newtouch.Domain.DTO;
using Newtouch.Domain.DTO.OutputDto;

namespace Newtouch.Application.Interface
{
    /// <summary>
    /// Quản lý phòng khám Đông y ngoại trú
    /// </summary>
    public interface IOutpatientCmmManagerApp
    {
        /// <summary>
        /// Tải lên thông tin bệnh nhân
        /// </summary>
        /// <param name="jzxx"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        Result TcmHis01(TreatEntityObj jzxx, string organizeId, string userCode);

        /// <summary>
        /// Mô-đun tích hợp bệnh án điện tử
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="jzys"></param>
        /// <param name="organizeId"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        string GetIntegrateEmrUrl(string mzh, string jzys, string organizeId, out string response);

        /// <summary>
        /// Biện chứng luận trị
        /// </summary>
        /// <param name="aeRequest"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        string GetIntegrateAeUrl(GetAeRequestDto aeRequest, out string response);

        /// <summary>
        /// Cơ sở tri thức
        /// </summary>
        /// <returns></returns>
        string GetIntegrateKUrl(out string response);

        /// <summary>
        /// Tích hợp trị vị bệnh
        /// </summary>
        /// <param name="blh"></param>
        /// <param name="mzh"></param>
        /// <param name="zjlx"></param>
        /// <param name="zjh"></param>
        /// <param name="organizeId"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        string GetIntegrateHEALUrl(string blh, string mzh, string zjlx, string zjh, string organizeId, out string response);

        /// <summary>
        /// Trích xuất bệnh án điện tử
        /// </summary>
        /// <param name="mzh">Số ngoại trú</param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        Record ExtractIntegrateEmr(string mzh, string organizeId);

        /// <summary>
        /// Trích xuất đơn thuốc
        /// </summary>
        /// <param name="mzh">Số ngoại trú</param>
        /// <param name="organizeId"></param>
        /// <param name="mzzybz">Cờ ngoại trú/nội trú 0-kho thuốc 1-ngoại trú 2-nội trú 3-dùng chung (lấy đơn vị ngoại trú) </param>
        /// <returns></returns>
        List<DrugDataEx> ExtractIntegrateRp(string mzh, string organizeId, string mzzybz);

        /// <summary>
        /// Trích xuất thông tin chẩn đoán
        /// </summary>
        /// <param name="mzh">Số ngoại trú</param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        DiagInfo ExtractIntegrateDiagnosis(string mzh, string organizeId);

        /// <summary>
        /// Đẩy thảo dược
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string PushMedicineInfo(string organizeId, string userCode);

        /// <summary>
        /// Ghép tham số y tế từ xa
        /// </summary>
        /// <param name="blh"></param>
        /// <param name="mzh"></param>
        /// <param name="jzys"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IntegrateRcDto GetIntegrateRCRequsetParam(string blh, string mzh, string jzys, string organizeId);
    }
}