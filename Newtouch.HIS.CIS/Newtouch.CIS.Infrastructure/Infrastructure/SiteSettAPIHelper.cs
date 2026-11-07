using Newtouch.CIS.Compat;
using Newtouch.CIS.Compat;
using Newtouch.Common.Web;
using System;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// Sett API yêu cầugiúpgiúploại
    /// </summary>
    public class SiteSettAPIHelper : SiteAPIRequestHelperBase<SiteSettAPIHelper>
    {
        /// <summary>
        /// lĩnh vực Addrcấu hình của Key
        /// </summary>
        private readonly static string _domainConfigKey = "SiteSettAPIHost";

        /// <summary>
        /// API Name
        /// </summary>
        private readonly static string _apiName = "Sett";
        private static APIRequestHelper.DefaultResponse apiResp;

        /// <summary>
        /// Đồng bộ Thông tin bệnh nhân cho CIS
        /// </summary>
        /// <param name="dto"></param>
        public static APIRequestHelper.DefaultResponse UpdateInpatientBasicInfo(InpatientPatientInfoDTO dto)
        {
            var reqObj = new
            {
                zyh = dto.zyh,
                zybz = dto.zybz,
                bq = dto.bq,
                cw = dto.cw,
                cyrq = dto.cyrq,
                doctor = dto.doctor,
                cyzd = dto.cyzd,
            };
            var apiResp = Request<APIRequestHelper.DefaultResponse>(
                "api/Patient/UpdateInpatientOutInfo", reqObj);
            return apiResp;
        }

        /// <summary>
        /// đãquyết toán Đơn thuốcchi tiết Tra cứu
        /// </summary>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="fph"></param>
        /// <param name="kh"></param>
        /// <param name="yfCode">Cách dùng Mã</param>
        /// <returns></returns>
        public static APIRequestHelper.DefaultResponse OutpatientSettledRpQuery(DateTime kssj, DateTime jssj, string fph, string kh, string yfCode,string mzh)
        {
            var reqObj = new
            {
                kssj = kssj,
                jssj = jssj,
                fph = fph,
                kh = kh,
                yfCode = yfCode,
                mzh=mzh
            };
            return Request<APIRequestHelper.DefaultResponse>("api/OutpatientPharmacy/OutpatientSettledRpQuery", reqObj);
        }
    }

    /// <summary>
    /// Đồng bộ Nhập viện Thông tin bệnh nhân
    /// </summary>
    public class InpatientPatientInfoDTO
    {
        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// ởviện Cờ
        /// </summary>
        public int zybz { get; set; }
        /// <summary>
        /// Khu bệnh
        /// </summary>
        public string bq { get; set; }
        /// <summary>
        /// giường ký tự
        /// </summary>
        public string cw { get; set; }
        /// <summary>
        /// Ngày xuất viện
        /// </summary>
        public DateTime? cyrq { get; set; }
        /// <summary>
        /// Bác sĩ
        /// </summary>
        public string doctor { get; set; }
        /// <summary>
        /// Xuất viện Chẩn đoán
        /// </summary>
        public string cyzd { get; set; }
    }
}
