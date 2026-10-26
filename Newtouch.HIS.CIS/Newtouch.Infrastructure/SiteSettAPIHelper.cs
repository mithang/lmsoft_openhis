using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Common.Web;
using System;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// Lớp trợ giúp yêu cầu Sett API
    /// </summary>
    public class SiteSettAPIHelper : SiteAPIRequestHelperBase<SiteSettAPIHelper>
    {
        /// <summary>
        /// Key cấu hình Addr của miền
        /// </summary>
        private readonly static string _domainConfigKey = "SiteSettAPIHost";

        /// <summary>
        /// API Name
        /// </summary>
        private readonly static string _apiName = "Sett";
        private static APIRequestHelper.DefaultResponse apiResp;

        /// <summary>
        /// Đồng bộ thông tin bệnh nhân cho CIS
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
        /// Tra cứu chi tiết đơn thuốc đã thanh toán
        /// </summary>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="fph"></param>
        /// <param name="kh"></param>
        /// <param name="yfCode">Mã cách dùng</param>
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
    /// Đồng bộ thông tin bệnh nhân nội trú
    /// </summary>
    public class InpatientPatientInfoDTO
    {
        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// Cờ đang nằm viện
        /// </summary>
        public int zybz { get; set; }
        /// <summary>
        /// Khu bệnh
        /// </summary>
        public string bq { get; set; }
        /// <summary>
        /// Giường bệnh
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
        /// Chẩn đoán xuất viện
        /// </summary>
        public string cyzd { get; set; }
    }
}
