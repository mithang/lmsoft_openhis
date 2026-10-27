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
    /// Cửa sổ nhà thuốc
    /// </summary>
    public class PharmacyWindowController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IPharmacyDmnService _PharmacyDmnService;
        private readonly ISysPharmacyWindowRepo _SysPharmacyWindowRepo;
        private readonly ISysOrganizeDmnService _SysOrganizeDmnService;
        private readonly ISysPharmacyDepartmentRepo _sysPharmacyDepartmentRepo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PharmacyDmnService"></param>
        public PharmacyWindowController(IPharmacyDmnService PharmacyDmnService,
            ISysPharmacyWindowRepo SysPharmacyWindowRepo,
            ISysOrganizeDmnService SysOrganizeDmnService
            , ISysPharmacyDepartmentRepo sysPharmacyDepartmentRepo
            , ISysChargeCategoryRepo SysChargeCategoryRepo)
        {
            this._PharmacyDmnService = PharmacyDmnService;
            this._SysPharmacyWindowRepo = SysPharmacyWindowRepo;
            this._SysOrganizeDmnService = SysOrganizeDmnService;
            this._sysPharmacyDepartmentRepo = sysPharmacyDepartmentRepo;
        }

        /// <summary>
        /// Tra cứu thông tin cửa sổ nhà thuốc
        /// </summary>
        /// <param name="pagintion"></param>
        /// <param name="yfckCode"></param>
        /// <param name="yfckmc"></param>
        /// <returns></returns>
        public IActionResult GetWindowsInformation(Pagination pagintion, string keyword, string organizeId)
        {
            pagintion.sidx = "CreateTime desc";
            pagintion.sord = "asc";
            var data = new
            {
                rows = _PharmacyDmnService.GetPagintionList(pagintion, organizeId, keyword),
                total = pagintion.total,
                page = pagintion.page,
                records = pagintion.records,
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// Sửa thông tin cửa sổ
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult GetFormJson(int ? keyValue)
        {
            var data = _SysPharmacyWindowRepo.FindEntity(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult submitForm(SysPharmacyWindowEntity entity, int? keyValue)
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
            _SysPharmacyWindowRepo.submitForm(entity, keyValue);
            return Success("Thao tác thành công");
        }

        /// <summary>
        /// TheoorganizeIdLấy khoa dược
        /// </summary>
        /// <returns></returns>
        public IActionResult PharmacyDepartmentList(string organizeId ,byte? yjbmjb)
        {
            var list = _sysPharmacyDepartmentRepo.GetList(organizeId,yjbmjb);        
            return Content(list.ToJson()); 
        }       
    }
}