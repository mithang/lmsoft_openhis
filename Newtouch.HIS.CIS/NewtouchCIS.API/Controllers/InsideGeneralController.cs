using System;
using System.Net.Http;
using System.Web.Http;
using Autofac;
using Newtouch.Application.Interface;
using Newtouch.CIS.APIRequest.Dto;
using Newtouch.HIS.API.Common;

namespace NewtouchCIS.API.Controllers
{
    /// <summary>
    /// Giao diện chung nội bộ
    /// </summary>
    [RoutePrefix("api/InsideGeneral")]
    public class InsideGeneralController : ApiControllerBase<InsideGeneralController>
    {
        private readonly IInsideGeneralApp _insideGeneralApp;
        public InsideGeneralController(IComponentContext com) : base(com)
        {
        }

        /// <summary>
        /// Điểm vào gọi mq
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("MqGeneralTask")]
        public HttpResponseMessage MqGeneralTask(MqGeneralTaskRequestDto requestDto)
        {
            Action<MqGeneralTaskRequestDto, DefaultResponse> ac = (req, resp) =>
            {
                _insideGeneralApp.PublicPortal(requestDto);
                resp.code = ResponseResultCode.SUCCESS;
            };
            return CreateResponse(CommonExecute(ac, requestDto));
        }
    }
}