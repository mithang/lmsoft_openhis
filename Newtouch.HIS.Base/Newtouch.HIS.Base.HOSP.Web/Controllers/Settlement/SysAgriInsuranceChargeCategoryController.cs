using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using Newtouch.HIS.Domain.IDomainServices;
using NewtouchHIS.Lib.Base.Exceptions;

namespace Newtouch.HIS.Base.HOSP.Web.Controllers
{
    /// <summary>
    /// Danh mục thu phí bảo hiểm nông nghiệp hệ thống lớn
    /// </summary>
    public class SysAgriInsuranceChargeCategoryController : ControllerBase
    {
        private readonly ISysAgriInsuranceChargeCategoryRepo _sysAgriInsuranceChargeCategoryRepo;
        private readonly ISysOrganizeDmnService _SysOrganizeDmnService;

        public SysAgriInsuranceChargeCategoryController(ISysAgriInsuranceChargeCategoryRepo sysAgriInsuranceChargeCategoryRepo
            , ISysOrganizeDmnService sysOrganizeDmnService)
        {
            this._sysAgriInsuranceChargeCategoryRepo = sysAgriInsuranceChargeCategoryRepo;
            this._SysOrganizeDmnService = sysOrganizeDmnService;
        }
        
        /// <summary>
        /// TheoorganizeIdLấy thông tin danh mục lớn hiệu lực
        /// </summary>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IActionResult GetListSelectJson(string organizeId)
        {
            var list = _sysAgriInsuranceChargeCategoryRepo.GetValidList(organizeId);
            return Content(list.ToJson());
        }

        /// <summary>
        /// Lấy danh sách
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetGridJson(string organizeId, string keyword)
        {
            var data = _sysAgriInsuranceChargeCategoryRepo.GetList(organizeId, keyword);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Thêm mới hoặc sửaForm
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult GetFormJson(int keyValue)
        {
            var entity = _sysAgriInsuranceChargeCategoryRepo.GetForm(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult SubmitForm(SysAgriInsuranceChargeCategoryEntity entity, int? keyValue)
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
            _sysAgriInsuranceChargeCategoryRepo.SubmitForm(entity, keyValue);
            return Success("Thao tác thành công.");
        }
    }
}