using System;
using System.Net.Http;
using System.Web.Http;
using Autofac;
using Newtouch.HIS.Application.Implementation;
using Newtouch.HIS.API.Common;
using Newtouch.PDS.Requset.ClinicTfOperate;

namespace Newtouch.PDS.API.Controllers
{
    /// <summary>
    /// Hoàn phí ngoại trú cập nhật đơn thuốc chờ phát
    /// </summary>
    [RoutePrefix("api/ClinicTfOperate")]
    public class ClinicTfOperateController : ApiControllerBase<ClinicTfOperateController>
    {
        public ClinicTfOperateController(IComponentContext com) : base(com)
        {
        }

        /// <summary>
        /// Cập nhật đơn thuốcbyHoàn phí
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [Route("UpdateCfByTf")]
        [HttpPost]
        public HttpResponseMessage UpdateCfByTf(ClinicTfOperateRequest par)
        {
            Action<ClinicTfOperateRequest, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = new ClinicTfOperateApp(par).Process();
                resp.code = ResponseResultCode.SUCCESS;
            };
            return CreateResponse(CommonExecute(ac, par));
        }
    }
}
