using Autofac;
using Newtouch.CIS.APIRequest.Dto;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.HIS.API.Common;
using System;
using System.Net.Http;
using System.Web.Http;

namespace NewtouchCIS.API.Controllers
{
    /// <summary>
    /// Xác thực ủy quyền
    /// </summary>
    [RoutePrefix("api/MzBespeakRegister")]
    public class MzBespeakRegisterController : ApiControllerBase<MzBespeakRegisterController>
    {
        private readonly IMzyyghRepo _mzyyghRepo;
        private readonly IMzyyghDmnService _mzyyghDmnService;
        private readonly ISysBespeakRegisterDmnService _sysBespeakRegisterDmnService;

        public MzBespeakRegisterController(IComponentContext com) : base(com)
        {
        }

        /// <summary>
        /// Tra cứu đặt lịch hẹn khám ngoại trú của bệnh nhân hiện tại
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("MzBespeakRegisterQuery")]
        public HttpResponseMessage MzBespeakRegisterQuery(MzBespeakRegisterQueryRequestDTO request)
        {
            Action<MzBespeakRegisterQueryRequestDTO, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _mzyyghDmnService.MzBespeakRegisterQuery(req.blh, req.kh, req.zjh, UserIdentity.OrganizeId);
                resp.code = ResponseResultCode.SUCCESS;
            };
            var response = base.CommonExecute(ac, request);
            return base.CreateResponse(response);
        }

        /// <summary>
        /// Thông tin xếp lịch đặt lịch hẹn khám của khoa hoặc chuyên gia
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("MzBespeakRegisterSchedulingQuery")]
        public HttpResponseMessage MzBespeakRegisterSchedulingQuery(SysBespeakRegisterQueryRequestDTO request)
        {
            Action<SysBespeakRegisterQueryRequestDTO, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _sysBespeakRegisterDmnService.SelectSysBespeakregister(req.ksCode, req.ysgh, UserIdentity.OrganizeId, req.regDate, req.regTime);
                resp.code = ResponseResultCode.SUCCESS;
            };
            var response = base.CommonExecute(ac, request);
            return base.CreateResponse(response);
        }

        /// <summary>
        /// Tra cứu tổng số đã đặt lịch của khoa hoặc chuyên gia
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("MzAlreadyBespeakRegisterCountQuery")]
        public HttpResponseMessage MzAlreadyBespeakRegisterCountQuery(MzAlreadyBespeakRegisterCountQueryRequestDTO request)
        {
            Action<MzAlreadyBespeakRegisterCountQueryRequestDTO, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _mzyyghDmnService.SelectAlreadyBespeakRegisterCount(req.ksCode, req.ysgh, req.regDate, req.regTime, UserIdentity.OrganizeId);
                resp.code = ResponseResultCode.SUCCESS;
            };
            var response = base.CommonExecute(ac, request);
            return base.CreateResponse(response);
        }

        /// <summary>
        /// Đến hẹn
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("KeepAnAppointment")]
        public HttpResponseMessage KeepAnAppointment(KeepAnAppointmentRequestDTO request)
        {
            Action<KeepAnAppointmentRequestDTO, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _mzyyghRepo.KeepAnAppointment(req.mzyyghId, req.arrivalDate, req.arrivalOpereater);
                resp.code = ResponseResultCode.SUCCESS;
            };
            var response = base.CommonExecute(ac, request);
            return base.CreateResponse(response);
        }
    }
}