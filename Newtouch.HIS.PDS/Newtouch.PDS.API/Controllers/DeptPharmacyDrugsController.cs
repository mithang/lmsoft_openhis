using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Autofac;
using Newtouch.HIS.API.Common;
using Newtouch.PDS.Requset.DeptPharmacyDrugs;

namespace Newtouch.PDS.API.Controllers
{
    /// <summary>
    /// Thuốc nhà thuốc của bộ phận
    /// </summary>
    [RoutePrefix("api/DeptPharmacyDrugs")]
    public class DeptPharmacyDrugsController : ApiControllerBase<DeptPharmacyDrugsController>
    {
        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="com"></param>
        public DeptPharmacyDrugsController(IComponentContext com) : base(com)
        {
        }

        /// <summary>
        /// Thông tin tổng hợp xếp thuốc ngoại trú (chờ xếp thuốc）
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetDeptPharmacyDrugs")]
        public HttpResponseMessage GetDeptPharmacyDrugs(DeptPharmacyDrugsRequset par)
        {
            Action<DeptPharmacyDrugsRequset, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = " GetDeptPharmacyDrugs 123123123";
                resp.code = ResponseResultCode.SUCCESS;
            };
            return CreateResponse(CommonExecute(ac, par));
        }
    }
}