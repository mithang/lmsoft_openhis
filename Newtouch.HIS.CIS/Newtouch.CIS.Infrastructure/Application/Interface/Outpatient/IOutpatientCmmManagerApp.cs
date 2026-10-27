using System.Collections.Generic;
using Newtouch.CIS.Compat;

using Newtouch.CIS.Proxy.CMMPlatform.DTO;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_07;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_08;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_09;
using Newtouch.Domain.DTO;
using Newtouch.Domain.DTO.OutputDto;

namespace Newtouch.Application.Interface
{
    /// <summary>
    /// Ngoại trútrongyquánQuản lý
    /// </summary>
    public interface IOutpatientCmmManagerApp
    {
        /// <summary>
        /// Thông tin bệnh nhânTải lên
        /// </summary>
        /// <param name="jzxx"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        Result TcmHis01(TreatEntityObj jzxx, string organizeId, string userCode);

        /// <summary>
        /// điện tửHồ sơtậpthànhmẫukhối
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="jzys"></param>
        /// <param name="organizeId"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        string GetIntegrateEmrUrl(string mzh, string jzys, string organizeId, out string response);

        /// <summary>
        /// biệnchứngluậnđiều trị
        /// </summary>
        /// <param name="aeRequest"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        string GetIntegrateAeUrl(GetAeRequestDto aeRequest, out string response);

        /// <summary>
        /// biếtbiếtkho
        /// </summary>
        /// <returns></returns>
        string GetIntegrateKUrl(out string response);

        /// <summary>
        /// tậpthànhđiều trịchưabệnh
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
        /// nhắclấyđiện tửHồ sơ
        /// </summary>
        /// <param name="mzh">Số khám ngoại trú</param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        Record ExtractIntegrateEmr(string mzh, string organizeId);

        /// <summary>
        /// nhắclấyĐơn thuốc
        /// </summary>
        /// <param name="mzh">Số khám ngoại trú</param>
        /// <param name="organizeId"></param>
        /// <param name="mzzybz">Dấu hiệu ngoại trú/nội trú 0-Kho thuốc  1-Ngoại trú  2-Nhập viện  3-Chung(lấyĐơn vị ngoại trú) </param>
        /// <returns></returns>
        List<DrugDataEx> ExtractIntegrateRp(string mzh, string organizeId, string mzzybz);

        /// <summary>
        /// nhắclấyChẩn đoánthông tin
        /// </summary>
        /// <param name="mzh">Số khám ngoại trú</param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        DiagInfo ExtractIntegrateDiagnosis(string mzh, string organizeId);

        /// <summary>
        /// đẩythảo dược
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string PushMedicineInfo(string organizeId, string userCode);

        /// <summary>
        /// lắp rápxatrìnhy tếtham số
        /// </summary>
        /// <param name="blh"></param>
        /// <param name="mzh"></param>
        /// <param name="jzys"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IntegrateRcDto GetIntegrateRCRequsetParam(string blh, string mzh, string jzys, string organizeId);
    }
}