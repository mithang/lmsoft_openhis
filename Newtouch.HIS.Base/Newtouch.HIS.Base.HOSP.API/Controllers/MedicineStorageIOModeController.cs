using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtouch.HIS.Base.HOSP.API.Envelope;
using Newtouch.HIS.Base.HOSP.Request;
using Newtouch.HIS.Base.HOSP.API.Services;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Base.HOSP.API.Controllers
{
    /// <summary>药品出入库方式</summary>
    [Route("api/MedicineStorageIOMode")]
    [ApiController]
    public class MedicineStorageIOModeController : ApiControllerBase
    {
        private readonly ISysMedicineStorageIOModeRepo _sysMedicineStorageIOModeRepo;

        public MedicineStorageIOModeController(TokenStore tokenStore, ISysMedicineStorageIOModeRepo sysMedicineStorageIOModeRepo)
            : base(tokenStore)
        {
            _sysMedicineStorageIOModeRepo = sysMedicineStorageIOModeRepo;
        }

        [HttpPost("Query")]
        public ContentResult Query([FromBody] MedicineStorageIOModeQueryRequest request)
        {
            System.Action<MedicineStorageIOModeQueryRequest, DefaultResponse> ac = (req, resp) =>
            {
                var list = _sysMedicineStorageIOModeRepo.IQueryable().Where(p => p.zt == "1" && p.crkbz == request.crkbz).ToList();

                resp.data = FilterObjectProperty(list, req.ResponseColumns,
                    excludeProps: new string[] { "crkbz", "CreatorCode", "CreateTime", "LastModifyTime", "LastModifierCode", "zt", "px" });

                resp.code = ResponseResultCode.SUCCESS;
            };

            var response = CommonExecute(ac, request);
            return CreateResponse(response);
        }
    }
}
