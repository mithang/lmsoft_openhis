using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtouch.HIS.Base.HOSP.API.Envelope;
using Newtouch.HIS.Base.HOSP.Request;
using Newtouch.HIS.Base.HOSP.API.Services;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Base.HOSP.API.Controllers
{
    /// <summary>Nhà cung cấp thuốc</summary>
    [Route("api/MedicineSupplier")]
    [ApiController]
    public class MedicineSupplierController : ApiControllerBase
    {
        private readonly ISysMedicineSupplierRepo _sysMedicineSupplierRepo;

        public MedicineSupplierController(TokenStore tokenStore, ISysMedicineSupplierRepo sysMedicineSupplierRepo)
            : base(tokenStore)
        {
            _sysMedicineSupplierRepo = sysMedicineSupplierRepo;
        }

        [HttpPost("Query")]
        [DefaultAuthorize]
        public ContentResult Query([FromBody] MedicineSupplierQueryRequest request)
        {
            Action<MedicineSupplierQueryRequest, DefaultResponse> ac = (req, resp) =>
            {
                var codeUp = (req.keyword ?? "").ToUpper().Trim();
                var list = _sysMedicineSupplierRepo.IQueryable().Where(p => p.zt == "1"
                    && (codeUp == "" || p.gysmc.Contains(req.keyword) || p.gysCode.Contains(codeUp) || p.py.Contains(codeUp))
                    && p.OrganizeId == req.OrganizeId).ToList();

                resp.data = FilterObjectProperty(list, req.ResponseColumns,
                    includeProps: new string[] { "gysId", "gysCode", "gysmc", "py" });

                resp.code = ResponseResultCode.SUCCESS;
            };

            var response = CommonExecute(ac, request);
            return CreateResponse(response);
        }
    }
}
