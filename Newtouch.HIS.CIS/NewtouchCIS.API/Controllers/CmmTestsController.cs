using System.Web.Mvc;
using Autofac;
using Newtouch.Application.Interface;
using Newtouch.Domain.DTO.OutputDto;
using Newtouch.Tools;

namespace NewtouchCIS.API.Controllers
{
    /// <summary>
    /// Kiểm tra cấu trúc phòng khám Đông y
    /// </summary>
    [System.Web.Http.RoutePrefix("api/cmmtests")]
    public class CmmTestsController : ApiControllerBase<CmmTestsController>
    {
        private readonly IOutpatientCmmManagerApp _outpatientCmmManagerApp;

        public CmmTestsController(IComponentContext com) : base(com)
        {
        }

        ///// <summary>
        ///// Tra cứu đặt lịch hẹn khám ngoại trú của bệnh nhân hiện tại
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //public string TCM_HIS_01(TcmHis01Request request)
        //{
        //    var result = _outpatientCmmManagerApp.TcmHis01(request.jzId, request.organizeId);
        //    return result.ToJson();
        //}

        /// <summary>
        /// Tra cứu đặt lịch hẹn khám ngoại trú của bệnh nhân hiện tại
        /// </summary>
        /// <param name="jzxx"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("TCM_HIS_01")]
        public JsonResult TCM_HIS_01(TreatEntityObj jzxx, string organizeId)
        {
            var result = _outpatientCmmManagerApp.TcmHis01(jzxx, organizeId, "interfacetest");
            return new JsonResult { Data = result.ToJson() }; ;
        }

    }
}