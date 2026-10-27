using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Services.HttpService;

namespace NewtouchHIS.WebAPI.Manage.Controllers
{
    /// <summary>
    /// APILớp cơ sở
    /// </summary>
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        protected readonly IHttpClientHelper _httpClient;
        /// <summary>
        /// Hệ thống quyết toánApi Host
        /// </summary>
        protected readonly string SettHttpUrl = ConfigInitHelper.SysConfig!.AppAPIHost!.SiteSettAPIHost ?? "";
        /// <summary>
        /// Phối hợp y hộApi Host
        /// </summary>
        protected readonly string CisHttpUrl = ConfigInitHelper.SysConfig!.AppAPIHost!.SiteCisAPIHost ?? "";
        /// <summary>
        /// Thông tin bệnh nhân
        /// (Xác thực danh tính ẩn danh)
        /// </summary>
        protected readonly string PatientAnonRoute = "api/PatientAnon";
        /// <summary>
        /// Thông tin đơn hàng
        /// (Xác thực danh tính ẩn danh)
        /// </summary>
        protected readonly string OrderMethodRoute = "api/OrderCenter";

        /// <summary>
        /// Hàm tạo
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="httpClient"></param>
        public ApiBaseController(IHttpClientHelper httpClient)
        {
            _httpClient = httpClient;
        }

        
    }
}
