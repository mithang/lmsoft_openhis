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
    /// 
    /// </summary>
    public class PharmacyDepartmentController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ISysPharmacyDepartmentRepo _SysPharmacyDepartmentRepo;
        private readonly ISysOrganizeDmnService _SysOrganizeDmnService;
        private readonly ISysPharmacyDepartmentOpenMedicineRepo _SysPharmacyDepartmentOpenMedicineRepo;
        private readonly IPharmacyDmnService _PharmacyDmnService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SysPharmacyDepartmentRepo"></param>
        public PharmacyDepartmentController(ISysPharmacyDepartmentRepo SysPharmacyDepartmentRepo
            , ISysPharmacyDepartmentOpenMedicineRepo SysPharmacyDepartmentOpenMedicineRepo
            , ISysOrganizeDmnService SysOrganizeDmnService
            , IPharmacyDmnService PharmacyDmnService
            )
        {
            this._SysPharmacyDepartmentOpenMedicineRepo = SysPharmacyDepartmentOpenMedicineRepo;
            this._SysPharmacyDepartmentRepo = SysPharmacyDepartmentRepo;
            this._SysOrganizeDmnService = SysOrganizeDmnService;
            this._PharmacyDmnService = PharmacyDmnService;
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
                rows = _SysPharmacyDepartmentRepo.GetPagintionList(pagination, organizeId, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult GetFormJson(int? keyValue)
        {
            var data = _SysPharmacyDepartmentRepo.FindEntity(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult submitForm(SysPharmacyDepartmentEntity entity, int? keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            entity.fybz = entity.fybz == "true" ? "1" : "0";
            entity.ynwbz = entity.ynwbz == "true" ? "1" : "0";
            if (string.IsNullOrWhiteSpace(entity.OrganizeId))
            {
                throw new FailedException("Vui lòng chọn tổ chức");
            }
            else if (!_SysOrganizeDmnService.IsMedicalOrganize(entity.OrganizeId))
            {
                throw new FailedException("Vui lòng chọn cơ sở y tế(Bệnh viện hoặc phòng khám)");
            }
            _SysPharmacyDepartmentRepo.submitForm(entity, keyValue);
            return Success("Thao tác thành công");
        }

        #region xt_yfbm_ypBảo trì

        /// <summary>
        /// 主页面 
        /// </summary>
        /// <returns></returns>
        public IActionResult OpenMedicineIndex()
        {
            return View();

        }

        /// <summary>
        /// 更新添加页面
        /// </summary>
        /// <returns></returns>
        public IActionResult OpenMadicineForm()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IActionResult GetOpenMedicineGridJson(string keyword, string organizeId)
        {
            var data = _PharmacyDmnService.OpenMedicineIndex(organizeId, keyword);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetOpenMedicineFormJson(string keyValue)
        {
            var data = _SysPharmacyDepartmentOpenMedicineRepo.FindEntity(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult OpenMedicineSubmitForm(SysPharmacyDepartmentOpenMedicineEntity entity ,string  keyValue)
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
            _SysPharmacyDepartmentOpenMedicineRepo.submitForm(entity, keyValue);
            return Success("Thao tác thành công");
        }

        #endregion

    }
}