
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Infrastructure.EF;
using Newtouch.HIS.Repository;
using Newtouch.HIS.DomainServices.PharmacyDrugStorage;
using Newtouch.HIS.DomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Core.Common;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Web.Controllers
{
    public class PharmacyDepartmentController : ControllerBase
    {
        private readonly ISysPharmacyDepartmentBaseDmnService _SysPharmacyDepartmentBaseDmnService;
        private readonly ISysOrganizeDmnService _SysOrganizeDmnService;
        //private readonly ISysPharmacyDepartmentOpenMedicineRepo _SysPharmacyDepartmentOpenMedicineRepo;
        //private readonly IPharmacyDmnService _PharmacyDmnService;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="SysPharmacyDepartmentRepo"></param>
        public PharmacyDepartmentController(ISysPharmacyDepartmentBaseDmnService _SysPharmacyDepartmentBaseDmnService
            //, ISysPharmacyDepartmentOpenMedicineRepo SysPharmacyDepartmentOpenMedicineRepo
            , ISysOrganizeDmnService SysOrganizeDmnService
            //, IPharmacyDmnService PharmacyDmnService
            )
        {
            //this._SysPharmacyDepartmentOpenMedicineRepo = SysPharmacyDepartmentOpenMedicineRepo;
            this._SysPharmacyDepartmentBaseDmnService = _SysPharmacyDepartmentBaseDmnService;
            this._SysOrganizeDmnService = SysOrganizeDmnService;
            //this._PharmacyDmnService = PharmacyDmnService;
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
                rows = _SysPharmacyDepartmentBaseDmnService.GetPagintionList(pagination, organizeId, keyword),
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
            var data = _SysPharmacyDepartmentBaseDmnService.GetFormJson(this.OrganizeId,keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult submitForm(SysPharmacyDepartmentVO entity, int? keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            entity.fybz = entity.fybz == "true" ? "1" : "0";
            entity.ynwbz = entity.ynwbz == "true" ? "1" : "0";
            if (string.IsNullOrWhiteSpace(entity.OrganizeId))
            {
                throw new FailedException("请选择组织机构");
            }
            else if (!_SysOrganizeDmnService.IsMedicalOrganize(entity.OrganizeId))
            {
                throw new FailedException("请选择医疗机构（医院或诊所）");
            }
            _SysPharmacyDepartmentBaseDmnService.submitForm(entity, keyValue);
            return Success("Thao tác thành công");
        }

        //#region xt_yfbm_yp维护

        ///// <summary>
        ///// 主页面 
        ///// </summary>
        ///// <returns></returns>
        //public IActionResult OpenMedicineIndex()
        //{
        //    return View();

        //}

        ///// <summary>
        ///// 更新添加页面
        ///// </summary>
        ///// <returns></returns>
        //public IActionResult OpenMadicineForm()
        //{
        //    return View();
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="keyword"></param>
        ///// <param name="organizeId"></param>
        ///// <returns></returns>
        //public IActionResult GetOpenMedicineGridJson(string keyword, string organizeId)
        //{
        //    var data = _PharmacyDmnService.OpenMedicineIndex(organizeId, keyword);
        //    return Content(data.ToJson());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="keyword"></param>
        ///// <returns></returns>
        //public IActionResult GetOpenMedicineFormJson(string keyValue)
        //{
        //    var data = _SysPharmacyDepartmentOpenMedicineRepo.FindEntity(keyValue);
        //    return Content(data.ToJson());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //public IActionResult OpenMedicineSubmitForm(SysPharmacyDepartmentOpenMedicineEntity entity, string keyValue)
        //{
        //    entity.zt = entity.zt == "true" ? "1" : "0";
        //    if (string.IsNullOrWhiteSpace(entity.OrganizeId))
        //    {
        //        throw new FailedException("请选择组织机构");
        //    }
        //    else if (!_SysOrganizeDmnService.IsMedicalOrganize(entity.OrganizeId))
        //    {
        //        throw new FailedException("请选择医疗机构（医院或诊所）");
        //    }
        //    _SysPharmacyDepartmentOpenMedicineRepo.submitForm(entity, keyValue);
        //    return Success("操作成功");
        //}

        //#endregion

    }
}