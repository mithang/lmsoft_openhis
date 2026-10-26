using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Services.HttpService;

namespace NewtouchHIS.WebAPI.Manage.Controllers
{
    /// <summary>
    /// APIlớp cơ sở
    /// </summary>
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        protected readonly IHttpClientHelper _httpClient;
        /// <summary>
        /// hệ thống thanh toánApi Host
        /// </summary>
        protected readonly string SettHttpUrl = ConfigInitHelper.SysConfig!.AppAPIHost!.SiteSettAPIHost ?? "";
        /// <summary>
        /// phối hợp y tá – bác sĩApi Host
        /// </summary>
        protected readonly string CisHttpUrl = ConfigInitHelper.SysConfig!.AppAPIHost!.SiteCisAPIHost ?? "";
        /// <summary>
        /// thông tin bệnh nhân
        /// （xác thực danh tính ẩn danh）
        /// </summary>
        protected readonly string PatientAnonRoute = "api/PatientAnon";
        /// <summary>
        /// thông tin đơn hàng
        /// （xác thực danh tính ẩn danh）
        /// </summary>
        protected readonly string OrderMethodRoute = "api/OrderCenter";

        /// <summary>
        /// hàm khởi tạo
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="httpClient"></param>
        public ApiBaseController(IHttpClientHelper httpClient)
        {
            _httpClient = httpClient;
        }

        
    }
}
