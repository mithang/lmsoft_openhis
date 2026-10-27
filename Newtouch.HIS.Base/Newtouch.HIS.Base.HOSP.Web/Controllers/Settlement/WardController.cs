using Newtouch.HIS.Domain.IRepository;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using Newtouch.HIS.Domain.Entity;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Domain.IDomainServices;

namespace Newtouch.HIS.Base.HOSP.Web.Controllers
{
    public class WardController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ISysWardRepo _SysWardRepo;
        private readonly ISysOrganizeDmnService _SysOrganizeDmnService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SysWardRepo"></param>
        /// <param name="SysOrganizeDmnService"></param>
        public WardController(ISysWardRepo SysWardRepo , ISysOrganizeDmnService SysOrganizeDmnService)
        {
            this._SysWardRepo = SysWardRepo;
            this._SysOrganizeDmnService = SysOrganizeDmnService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IActionResult GetGridJson(Pagination pagination, string keyword, string organizeId)
        {
            pagination.sidx = "CreateTime desc";
            pagination.sord = "asc";
            var data = new
            {
                rows = _SysWardRepo.GetPagintionList(pagination, organizeId, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        public IActionResult Selectzccx(string organizeId, int xz)
        {
            var data = _SysWardRepo.Selectzccx(this.OrganizeId, xz);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult GetFormJson(int? keyValue)
        {
            var data = _SysWardRepo.FindEntity(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult submitForm(SysWardEntity entity, int? keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            if (string.IsNullOrWhiteSpace(entity.OrganizeId))
            {
                throw new FailedException("Vui lòng chọn tổ chức");
            }
            else if (!_SysOrganizeDmnService.IsMedicalOrganize(entity.OrganizeId))
            {
                throw new FailedException("Vui lòng chọn cơ sở y tế(Bệnh viện hoặc phòng khám)");
            }
            _SysWardRepo.submitForm(entity, keyValue);
            return Success("Thao tác thành công");
        }
    }
}