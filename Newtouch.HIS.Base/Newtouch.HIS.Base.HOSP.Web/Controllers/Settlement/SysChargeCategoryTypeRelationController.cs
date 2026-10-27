using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Base.HOSP.Web.Controllers
{
    /// <summary>
    /// Nhóm thu phí lớn Loại danh mục lớn
    /// </summary>
    public class SysChargeCategoryTypeRelationController : ControllerBase
    {
        private readonly ISysChargeCategoryTypeRelationRepo _sysChargeCategoryTypeRelationRepo;
        private readonly ISysOrganizeDmnService _SysOrganizeDmnService;

        public SysChargeCategoryTypeRelationController(ISysChargeCategoryTypeRelationRepo sysChargeCategoryTypeRelationRepo, ISysOrganizeDmnService SysOrganizeDmnService)
        {
            this._sysChargeCategoryTypeRelationRepo = sysChargeCategoryTypeRelationRepo;
            this._SysOrganizeDmnService = SysOrganizeDmnService;
        }

        /// <summary>
        /// Lấy danh sách
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetGridJson(string organizeId, string keyword)
        {
            var data = _sysChargeCategoryTypeRelationRepo.GetList(organizeId, keyword);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Thêm mới hoặc sửaForm
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult GetFormJson(string keyValue)
        {
            var data = _sysChargeCategoryTypeRelationRepo.FindEntity(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult SubmitForm(SysChargeCategoryTypeRelationEntity entity, string keyValue)
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
           _sysChargeCategoryTypeRelationRepo.SubmitForm(entity, keyValue);
            return Success("Thao tác thành công.");
        }

    }
}