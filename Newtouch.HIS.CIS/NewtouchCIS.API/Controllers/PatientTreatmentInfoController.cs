using System;
using System.Net.Http;
using System.Web.Http;
using Autofac;
using Newtouch.CIS.APIRequest.Dto;
using Newtouch.Domain.IDomainServices;
using Newtouch.HIS.API.Common;

namespace NewtouchCIS.API.Controllers
{
    /// <summary>
    /// Xác thực ủy quyền
    /// </summary>
    [RoutePrefix("api/PatientSeekingInfo")]
    public class PatientTreatmentInfoController : ApiControllerBase<PatientTreatmentInfoController>
    {
        private readonly ITreatmentDmnService _treatmentDmnService;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="com"></param>
        public PatientTreatmentInfoController(IComponentContext com) : base(com)
        {
        }

        /// <summary>
        /// Tra cứu thông tin khám bệnh của bệnh nhân
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PatientTreatmentInfoQuery")]
        public HttpResponseMessage PatientTreatmentInfoQuery(PatientTreatmentInfoQueryRequestDTO request)
        {
            Action<PatientTreatmentInfoQueryRequestDTO, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _treatmentDmnService.SelectTreatmentEntities(req.mzh, req.OrganizeId);
                resp.code = ResponseResultCode.SUCCESS;
            };
            var response = CommonExecute(ac, request);
            return CreateResponse(response);
        }
    }
}