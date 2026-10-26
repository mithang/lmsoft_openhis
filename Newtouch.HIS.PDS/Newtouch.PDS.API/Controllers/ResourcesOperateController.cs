using System;
using System.Net.Http;
using System.Web.Http;
using Autofac;
using Newtouch.HIS.API.Common;
using Newtouch.HIS.API.Common.Filter;
using Newtouch.HIS.Application.Interface;
using Newtouch.PDS.Requset.ResourcesOperate;

namespace Newtouch.PDS.API.Controllers
{
    /// <summary>
    /// Thao tác tài nguyên
    /// </summary>
    [RoutePrefix("api/ResourcesOperate")]
    public class ResourcesOperateController : ApiControllerBase<ResourcesOperateController>
    {
        private readonly IResourcesOperateApp _resourcesOperateApp;

        public ResourcesOperateController(IComponentContext com) : base(com)
        {
        }

        /// <summary>
        /// Đặt trước tài nguyên (đóng băng tồn kho)
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("OutpatientBook")]
        public HttpResponseMessage OutpatientBook(OutpatientBookRequestDTO par)
        {
            Action<OutpatientBookRequestDTO, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _resourcesOperateApp.OutpatientBook(par);
                resp.code = ResponseResultCode.SUCCESS;
            };
            var result = CommonExecute(ac, par);
            return CreateResponse(result);
        }

        /// <summary>
        /// Sửa đặt trước tài nguyên
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("OutpatientBookModify")]
        public HttpResponseMessage OutpatientBookModify(OutpatientBookModifyRequestDTO par)
        {
            Action<OutpatientBookModifyRequestDTO, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _resourcesOperateApp.OutpatientBookModify(par);
                resp.code = ResponseResultCode.SUCCESS;
            };
            var result = CommonExecute(ac, par);
            return CreateResponse(result);
        }

        /// <summary>
        /// Hủy một phần đặt trước
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("OutpatientCancelPartBook")]
        public HttpResponseMessage OutpatientCancelPartBook(OutpatientCancelPartBookRequestDTO request)
        {
            Action<OutpatientCancelPartBookRequestDTO, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _resourcesOperateApp.OutpatientCancelBook(request);
                resp.code = ResponseResultCode.SUCCESS;
            };
            var result = CommonExecute(ac, request);
            return CreateResponse(result);
        }

        /// <summary>
        /// Hủy toàn bộ đặt trước của đơn thuốc được chỉ định
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("OutpatientCancelAllBook")]
        public HttpResponseMessage OutpatientCancelAllBook(OutpatientCancelAllBookRequestDTO request)
        {
            Action<OutpatientCancelAllBookRequestDTO, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _resourcesOperateApp.OutpatientCancelBook(request);
                resp.code = ResponseResultCode.SUCCESS;
            };
            var result = CommonExecute(ac, request);
            return CreateResponse(result);
        }

        /// <summary>
        /// Sau khi HIS trả phí ngoại trú, hoàn trả tồn kho bị đóng băng
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("OutpatientCancelDjYpReturn")]
        public HttpResponseMessage OutpatientCancelDjYpReturn(OutpatientCancelAllBookRequestDTO request)
        {
            Action<OutpatientCancelAllBookRequestDTO, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _resourcesOperateApp.OutpatientCancelDjYpReturn(request);
                resp.code = ResponseResultCode.SUCCESS;
            };
            var result = CommonExecute(ac, request);
            return CreateResponse(result);
        }

        /// <summary>
        /// Xác nhận tài nguyên ngoại trú (Commit)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("OutpatientCommit")]
        public HttpResponseMessage OutpatientCommit(OutpatientCommitRequestDTO request)
        {
            Action<OutpatientCommitRequestDTO, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _resourcesOperateApp.OutpatientCommit(request);
                resp.code = ResponseResultCode.SUCCESS;
            };
            var result = CommonExecute(ac, request);
            return CreateResponse(result);
        }

        /// <summary>
        /// (Trước khi phát thuốc) trả thuốc một phần
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("OutpatientPartReturnBeforeDispensingMedicine")]
        [IgnoreTokenDecrypt]
        public HttpResponseMessage OutpatientPartReturnBeforeDispensingMedicine(OutpatientPartReturnBeforeDispensingMedicineRequestDTO request)
        {
            Action<OutpatientPartReturnBeforeDispensingMedicineRequestDTO, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _resourcesOperateApp.OutpatientPartReturnBeforeDispensingMedicine(request);
                resp.code = ResponseResultCode.SUCCESS;
            };
            var result = CommonExecute(ac, request);
            return CreateResponse(result);
        }

        /// <summary>
        /// Hủy xếp thuốc ngoại trú
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("OutpatientCancelArrangement")]
        public HttpResponseMessage OutpatientCancelArrangement(OutpatientCancelArrangementRequestDTO request)
        {
            Action<OutpatientCancelArrangementRequestDTO, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _resourcesOperateApp.OutpatientCancelArrangement(request);
                resp.code = ResponseResultCode.SUCCESS;
            };
            var result = CommonExecute(ac, request);
            return CreateResponse(result);
        }

        /// <summary>
        /// Trả thuốc nội trú
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("HospitalizatiionReturnDispensingMedicine")]
        public HttpResponseMessage HospitalizatiionReturnDispensingMedicine(HospitalizatiionReturnDispensingMedicineRequestDTO request)
        {
            Action<HospitalizatiionReturnDispensingMedicineRequestDTO, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _resourcesOperateApp.HospitalizatiionReturnDispensingMedicine(request);
                resp.code = ResponseResultCode.SUCCESS;
            };
            var result = CommonExecute(ac, request);
            return CreateResponse(result);
        }
    }
}