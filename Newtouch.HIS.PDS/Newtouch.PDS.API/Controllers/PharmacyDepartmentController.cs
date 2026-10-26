using System;
using System.Net.Http;
using System.Web.Http;
using Autofac;
using Newtouch.HIS.API.Common;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.PDS.Requset.PharmacyDepartment;

namespace Newtouch.PDS.API.Controllers
{
    /// <summary>
    /// Bộ phận kho
    /// </summary>
    [RoutePrefix("api/PharmacyDepartment")]
    public class PharmacyDepartmentController : ApiControllerBase<PharmacyDepartmentController>
    {
        private readonly ISysPharmacyDepartmentDmnService _sysPharmacyDepartmentDmnService;
        private readonly ISysPharmacyDepartmentApp _sysPharmacyDepartmentApp;

        public PharmacyDepartmentController(IComponentContext com) : base(com)
        {
        }

        /// <summary>
        /// Tra cứu bộ phận nhà thuốc đã được cấp quyền đối với thuốc đó
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("EmpowermentPharmacyDepartmentQuery")]
        public HttpResponseMessage EmpowermentPharmacyDepartmentQuery(SysPharmacyDepartmentRequestDto par)
        {
            Action<SysPharmacyDepartmentRequestDto, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _sysPharmacyDepartmentDmnService.SelectEmpowermentYfbmByYp(par.ypId, par.organizeId);
                resp.code = ResponseResultCode.SUCCESS;
            };
            return CreateResponse(CommonExecute(ac, par));
        }

        /// <summary>
        /// Bộ phận nhà thuốc được ủy quyền thuốc
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("EmpowermentPharmacyDepartment")]
        public HttpResponseMessage EmpowermentPharmacyDepartment(EmpowermentPharmacyDepartmentRequestDto par)
        {
            Action<EmpowermentPharmacyDepartmentRequestDto, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _sysPharmacyDepartmentApp.EmpowermentPharmacyDepartment(par);
                resp.code = ResponseResultCode.SUCCESS;
            };
            return CreateResponse(CommonExecute(ac, par));
        }

        /// <summary>
        /// Bộ phận nhà thuốc được ủy quyền thuốc
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("EmpowermentPharmacyDepartmentAndRemoveOld")]
        public HttpResponseMessage EmpowermentPharmacyDepartmentAndRemoveOld(EmpowermentPharmacyDepartmentAndRemoveOldRequestDto par)
        {
            Action<EmpowermentPharmacyDepartmentAndRemoveOldRequestDto, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _sysPharmacyDepartmentApp.EmpowermentPharmacyDepartmentAndRemoveOld(par);
                resp.code = ResponseResultCode.SUCCESS;
            };
            return CreateResponse(CommonExecute(ac, par));
        }
    }
}