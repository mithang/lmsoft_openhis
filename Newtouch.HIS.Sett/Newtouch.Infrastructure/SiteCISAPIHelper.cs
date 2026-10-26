using System;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Common.Web;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// CIS API yêu cầu giúp đ ỡ loại 
    /// </summary>
    public class SiteCISAPIHelper : SiteAPIRequestHelperBase<SiteCISAPIHelper>
    {
        /// <summary>
        /// lĩnh vực Addr cấu hình của Key
        /// </summary>
        private readonly static string _domainConfigKey = "SiteCISAPIHost";

        /// <summary>
        /// API Name
        /// </summary>
        private readonly static string _apiName = "CIS";

        /// <summary>
        /// Đ ồng bộ thông tin bệnh nhân cho CIS
        /// </summary>
        /// <param name="dto"></param>
        public static APIRequestHelper.DefaultResponse UpdateInpatientBasicInfo(InpatientPatientInfoDTO dto)
        {
            var reqObj = new
            {
                zyh = dto.zyh,
                zybz = dto.zybz,
                ryzd = dto.ryzd,
                ryzdmc = dto.ryzdmc,
                brxz = dto.brxz,
                brxzmc = dto.brxzmc,
                lxr = dto.lxr,
                lxrgx = dto.lxrgx,
                lxrdh = dto.lxrdh,
            };
            var apiResp = Request<APIRequestHelper.DefaultResponse>(
                "api/PatInfo/UpdateInpatientBasicInfo", reqObj);
            return apiResp;
        }
        /// <summary>
        /// Đ ồng bộ tính chất bệnh nhân cho CIS
        /// </summary>
        /// <param name="dto"></param>
        public static APIRequestHelper.DefaultResponse UpdatebrxzInfo(UpdatebrxzRequest dto)
        {
            var apiResp = Request<APIRequestHelper.DefaultResponse>(
                "api/PatInfo/UpdatebrxzInfo", dto);
            return apiResp;
        }
        /// <summary>
        /// Ngoại trú đ ặt lịch tra cứu không 分 đ ặt lịch ngày, khoa, chuyên gia
        /// </summary>
        /// <param name="zjlx"></param>
        /// <param name="zjh"></param>
        /// <param name="blh"></param>
        /// <param name="kh"></param>
        /// <returns></returns>
        public static APIRequestHelper.DefaultResponse MzBespeakRegisterQuery(int? zjlx, string zjh, string blh, string kh)
        {
            var reqObj = new
            {
                zjlx = zjlx,
                zjh = zjh,
                blh = blh,
                kh = kh
            };
            var apiResp = Request<APIRequestHelper.DefaultResponse>(
                "api/MzBespeakRegister/MzBespeakRegisterQuery", reqObj);
            return apiResp;
        }

        /// <summary>
        /// Ngoại trú lịch trực đ ặt lịch tra cứu
        /// </summary>
        /// <param name="ksCode"></param>
        /// <param name="ysgh"></param>
        /// <param name="regDate"></param>
        /// <param name="regTime"></param>
        /// <returns></returns>
        public static APIRequestHelper.DefaultResponse MzBespeakRegisterSchedulingQuery(string ksCode, string ysgh, DateTime regDate, string regTime)
        {
            var reqObj = new
            {
                ksCode = ksCode,
                ysgh = ysgh,
                regDate = regDate,
                regTime = regTime
            };
            var apiResp = Request<APIRequestHelper.DefaultResponse>(
                "api/MzBespeakRegister/MzBespeakRegisterSchedulingQuery", reqObj);
            return apiResp;
        }

        /// <summary>
        /// Đ ã đ ặt lịch Đ ăng ký khám ngoại trú thông tintra cứu
        /// </summary>
        /// <param name="ksCode"></param>
        /// <param name="ysgh"></param>
        /// <param name="regDate"></param>
        /// <param name="regTime"></param>
        /// <returns></returns>
        public static APIRequestHelper.DefaultResponse MzAlreadyBespeakRegisterCountQuery(string ksCode, string ysgh, DateTime regDate, string regTime)
        {
            var reqObj = new
            {
                ksCode = ksCode,
                ysgh = ysgh,
                regDate = regDate,
                regTime = regTime
            };
            var apiResp = Request<APIRequestHelper.DefaultResponse>("api/MzBespeakRegister/MzAlreadyBespeakRegisterCountQuery", reqObj);
            return apiResp;
        }

        /// <summary>
        /// 赴约
        /// </summary>
        /// <param name="mzyyghid">Đ ặt lịch đ ăng ký khám ngoại trú ID</param>
        /// <param name="arrivalDate">赴约thời gian</param>
        /// <param name="arrivalOpereater">Đ ăng ký khám ngoại trú thao tác viên </param>
        /// <returns></returns>
        public static APIRequestHelper.DefaultResponse KeepAnAppointment(string mzyyghId, DateTime arrivalDate, string arrivalOpereater)
        {
            var reqObj = new
            {
                mzyyghId = mzyyghId,
                arrivalDate = arrivalDate,
                arrivalOpereater = arrivalOpereater
            };
            var apiResp = Request<APIRequestHelper.DefaultResponse>("api/MzBespeakRegister/KeepAnAppointment", reqObj);
            return apiResp;
        }

        /// <summary>
        /// LấyKhám bệnhThông tin
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public static APIRequestHelper.DefaultResponse PatientTreatmentInfoQuery(string mzh, string organizeId)
        {
            var reqObj = new
            {
                mzh = mzh,
                OrganizeId = organizeId
            };
            return Request<APIRequestHelper.DefaultResponse>("api/PatientSeekingInfo/PatientTreatmentInfoQuery", reqObj);
        }

    }

    /// <summary>
    /// Đ ồng bộ thông tin bệnh nhân nhập viện
    /// </summary>
    public class InpatientPatientInfoDTO
    {
        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// Dấu hiệu đang nằm viện
        /// </summary>
        public string zybz { get; set; }
        /// <summary>
        /// Chẩn đoán nhập viện
        /// </summary>
        public string ryzd { get; set; }
        /// <summary>
        /// Chẩn đoán nhập việnTên
        /// </summary>
        public string ryzdmc { get; set; }
        /// <summary>
        /// Tính chất bệnh nhân
        /// </summary>
        public string brxz { get; set; }
        /// <summary>
        /// Tính chất bệnh nhânTên
        /// </summary>
        public string brxzmc { get; set; }
        /// <summary>
        /// liên hệ 人
        /// </summary>
        public string lxr { get; set; }
        /// <summary>
        /// liên hệ 人 quan hệ 
        /// </summary>
        public string lxrgx { get; set; }
        /// <summary>
        /// liên hệ 人điện thoại
        /// </summary>
        public string lxrdh { get; set; }
    }
    public class UpdatebrxzRequest
    {
        public string zyh { get; set; }
        public string mzh { get; set; }
        public string brxzCode { get; set; }
        public string brxzmc { get; set; }
    }
}
