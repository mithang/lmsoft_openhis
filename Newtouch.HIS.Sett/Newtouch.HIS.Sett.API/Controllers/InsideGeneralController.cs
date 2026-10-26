using System;
using System.Net.Http;
using System.Web.Http;
using Autofac;
using Newtouch.HIS.API.Common;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Sett.Request;

namespace Newtouch.HIS.Sett.API.Controllers
{
    /// <summary>
    /// Quyết toán hệ thống nội bộ chunggiao diện
    /// </summary>
    [RoutePrefix("api/InsideGeneral")]
    public class InsideGeneralController : ApiControllerBase<InsideGeneralController>
    {
        private readonly IInsideGeneralApp _insideGeneralApp;

        public InsideGeneralController(IComponentContext com) : base(com)
        {
        }

        /// <summary>
        /// mq gọi 入口
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