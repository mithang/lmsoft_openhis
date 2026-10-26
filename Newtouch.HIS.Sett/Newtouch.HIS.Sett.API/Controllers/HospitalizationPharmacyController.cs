using Autofac;
using Newtouch.HIS.API.Common;
using Newtouch.HIS.Domain.IDomainServices.HospitalizationManage;
using Newtouch.HIS.Sett.Request.HospitalizationPharmacy;
using System;
using System.Net.Http;
using System.Web.Http;

namespace Newtouch.HIS.Sett.API.Controllers
{
    /// <summary>
    /// Nhà thuốc ngoại trú
    /// </summary>
    [RoutePrefix("api/HospitalizationPharmacy")]
    public class HospitalizationPharmacyController : ApiControllerBase<HospitalizationPharmacyController>
    {
        private readonly IDispenseIndexDmnService _idispenseIndexDmnService;

        public HospitalizationPharmacyController(IComponentContext com)
            : base(com)
        {

        }
        #region 住院发药页面绑定信息

        /// <summary>
        /// Nhập viện phát thuốc khu bệnh
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("WaitingDispenseBQDispenseBRTotal")]
        public HttpResponseMessage WaitingDispenseBQDispenseBRTotal(WaitingDispenseBQMedicineDispenseRequest par)
        {
            Action<WaitingDispenseBQMedicineDispenseRequest, DefaultResponse> ac = (req, resp) =>
           {
               var data = _idispenseIndexDmnService.GetZYFYBQPagList(par.zyyf, UserIdentity.OrganizeId);
               resp.data = data;
               resp.code = ResponseResultCode.SUCCESS;
           };
            var response = base.CommonExecute(ac, par);
            return base.CreateResponse(response);
        }

        /// <summary>
        /// Nhập viện phát thuốc bệnh nhân
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("WaitingDispenseDispenseBRTotal")]
        public HttpResponseMessage WaitingDispenseDispenseBRTotal(WaitingDispenseMedicineDispenseRequest par)
        {
            Action<WaitingDispenseMedicineDispenseRequest, DefaultResponse> ac = (req, resp) =>
            {
                var data = _idispenseIndexDmnService.GetZYFYBRPagList(par.zyyf, par.bq);
                resp.data = data;
                resp.code = ResponseResultCode.SUCCESS;
            };
            var response = base.CommonExecute(ac, par);
            return base.CreateResponse(response);
        }

        /// <summary>
        /// Nhập viện phát thuốc nhân sự thông tin chi tiết
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("WaitingDispenseDispenseRYXXTotal")]
        public HttpResponseMessage WaitingDispenseDispenseRYXXTotal(WaitingDispenseBRXXMedicineDispenseRequest par)
        {
            Action<WaitingDispenseBRXXMedicineDispenseRequest, DefaultResponse> ac = (req, resp) =>
            {
                //var data = _idispenseIndexDmnService.GetZYFYXXPagList(par.Bq, par.Zyh, par.Kssj,par.Jssj, par.Cl, par.Fyzt);
                var data = new
                {
                    rows = _idispenseIndexDmnService.GetZYFYXXPagList(par),
                    total = par.pagination.total,
                    page = par.pagination.page,
                    records = par.pagination.records
                };
                resp.data = data;
                resp.code = ResponseResultCode.SUCCESS;
            };
            var response = base.CommonExecute(ac, par);
            return base.CreateResponse(response);
        }
        #endregion

        #region 配药操作
        /// <summary>
        /// Tra cứu ngư ời dùng thực hiện 配药 theo thời gian bắt đ ầu thời gian kết thúc lấy nhập viện bệnh nhân y lệnh,thuốc,số lư ợngV.v.Thông tin
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("WaitingDispenseDispenseBQPYTotal")]
        public HttpResponseMessage WaitingDispenseDispenseBQPYTotal(WaitingDispenseBQBRPYMedicineDispenseRequest par)
        {
            Action<WaitingDispenseBQBRPYMedicineDispenseRequest, DefaultResponse> ac = (req, resp) =>
            {
                var data = _idispenseIndexDmnService.SetZYBRBQPY(par);
                resp.data = data;
                resp.code = ResponseResultCode.SUCCESS;
            };
            var response = base.CommonExecute(ac, par);
            return base.CreateResponse(response);
        }

        //Tra cứu ngư ời dùng thực hiện 配药 配药thao tác sau sửa phát thuốc ghi chú trạng thái
        [HttpPost]
        [Route("WaitingDispenseDispenseUPBQPYyzzxTotal")]
        public HttpResponseMessage WaitingDispenseDispenseUPBQPYyzzxTotal(WaitingDispenseBQPYyzzxZTDispenseRequest par)
        {
            Action<WaitingDispenseBQPYyzzxZTDispenseRequest, DefaultResponse> ac = (req, resp) =>
            {
                var data = _idispenseIndexDmnService.UpBQPYyzzx(par);
                resp.data = data;
                resp.code = ResponseResultCode.SUCCESS;
            };
            var response = base.CommonExecute(ac, par);
            return base.CreateResponse(response);
        }
        #endregion

        #region 发药操作


        #endregion

        #region 发药查询操作
        /// <summary>
        /// Nhập viện phát thuốc nhân sự thông tin chi tiết
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("WaitingDispenseDispenseRYCXXXTotal")]
        public HttpResponseMessage WaitingDispenseDispenseRYCXXXTotal(WaitingDispenseBRCXXXMedicineDispenseRequest par)
        {
            Action<WaitingDispenseBRCXXXMedicineDispenseRequest, DefaultResponse> ac = (req, resp) =>
            {
                //var data = _idispenseIndexDmnService.GetZYFYXXPagList(par.Bq, par.Zyh, par.Kssj,par.Jssj, par.Cl, par.Fyzt);
                var data = new
                {
                    rows = _idispenseIndexDmnService.GetZYFYCXXXPagList(par),
                    total = par.pagination.total,
                    page = par.pagination.page,
                    records = par.pagination.records
                };
                resp.data = data;
                resp.code = ResponseResultCode.SUCCESS;
            };
            var response = base.CommonExecute(ac, par);
            return base.CreateResponse(response);
        }
        #endregion

        #region 退药操作

        /// <summary>
        /// Lấy trả thuốc chi tiết trạm bác sĩ y lệnh ID y lệnh bảng ID
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("WaitingDispenseDispenseTYYszyzlist")]
        public HttpResponseMessage WaitingDispenseDispenseTYYszyzlist(WaitingDispenseTYYszyzIdDispenseRequest par)
        {
            Action<WaitingDispenseTYYszyzIdDispenseRequest, DefaultResponse> ac = (req, resp) =>
            {
                var data = _idispenseIndexDmnService.GetZYYszysList(par);
                resp.data = data;
                resp.code = ResponseResultCode.SUCCESS;
            };
            var response = base.CommonExecute(ac, par);
            return base.CreateResponse(response);
        }

        /// <summary>
        /// Lấy nhập việnkhu bệnh trả thuốc nhân sự thông tin chi tiết
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("WaitingDispenseDispenseTYRYXXTotal")]
        public HttpResponseMessage WaitingDispenseDispenseTYRYXXTotal(WaitingDispenseBRTYXXMedicineDispenseRequest par)
        {
            Action<WaitingDispenseBRTYXXMedicineDispenseRequest, DefaultResponse> ac = (req, resp) =>
            {
                var data = _idispenseIndexDmnService.GetZYTYXXPagList(par);
                resp.data = data;
                resp.code = ResponseResultCode.SUCCESS;
            };
            var response = base.CommonExecute(ac, par);
            return base.CreateResponse(response);
        }
        #endregion
    }
}
