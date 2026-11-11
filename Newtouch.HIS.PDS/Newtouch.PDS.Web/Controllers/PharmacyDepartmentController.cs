
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult submitForm(SysPharmacyDepartmentVO entity, int? keyValue)
        {
            if (keyValue.HasValue && keyValue.Value > 0)
            {
                var existing = _SysPharmacyDepartmentBaseDmnService.GetFormJson(this.OrganizeId, keyValue);
                if (existing == null)
                {
                    throw new FailedException("Không tìm thấy bộ phận cần sửa");
                }
                // Chỉ validate field đã post (non-null); merge-skip-null, giữ OrganizeId + zt gốc
                if (entity.yfbmCode != null && (string.IsNullOrWhiteSpace(entity.yfbmCode) || entity.yfbmCode.Length > 20))
                {
                    throw new FailedException("Mã bộ phận không được để trống và tối đa 20 ký tự");
                }
                if (entity.yfbmmc != null && (string.IsNullOrWhiteSpace(entity.yfbmmc) || entity.yfbmmc.Length > 50))
                {
                    throw new FailedException("Tên bộ phận không được để trống và tối đa 50 ký tự");
                }
                if (entity.ksCode != null && (string.IsNullOrWhiteSpace(entity.ksCode) || entity.ksCode.Length > 20))
                {
                    throw new FailedException("Mã khoa không được để trống và tối đa 20 ký tự");
                }
                if (entity.py != null && (string.IsNullOrWhiteSpace(entity.py) || entity.py.Length > 50))
                {
                    throw new FailedException("Chữ đầu pinyin không được để trống và tối đa 50 ký tự");
                }
                if (entity.mzzybz != null && entity.mzzybz != "0" && entity.mzzybz != "1" && entity.mzzybz != "2" && entity.mzzybz != "3")
                {
                    throw new FailedException("Cờ ngoại trú/nội trú không hợp lệ");
                }
                if (entity.yjbmjb != 0 && entity.yjbmjb != 1 && entity.yjbmjb != 2)
                {
                    throw new FailedException("Cấp khoa dược không hợp lệ");
                }
                if (entity.yfbmCode != null) existing.yfbmCode = entity.yfbmCode;
                if (entity.yfbmmc != null) existing.yfbmmc = entity.yfbmmc;
                if (entity.ksCode != null) existing.ksCode = entity.ksCode;
                if (entity.py != null) existing.py = entity.py;
                if (entity.mzzybz != null) existing.mzzybz = entity.mzzybz;
                if (entity.yjbmjb != 0) existing.yjbmjb = entity.yjbmjb;
                if (entity.px.HasValue) existing.px = entity.px;
                var zt = MapFlag(entity.zt);
                if (zt != null) existing.zt = zt;
                var fybz = MapFlag(entity.fybz);
                if (fybz != null) existing.fybz = fybz;
                var ynwbz = MapFlag(entity.ynwbz);
                if (ynwbz != null) existing.ynwbz = ynwbz;
                _SysPharmacyDepartmentBaseDmnService.submitForm(existing, keyValue);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(entity.OrganizeId))
                {
                    throw new FailedException("Vui lòng chọn tổ chức");
                }
                else if (!_SysOrganizeDmnService.IsMedicalOrganize(entity.OrganizeId))
                {
                    throw new FailedException("Vui lòng chọn cơ sở y tế（Bệnh viện hoặc phòng khám）");
                }
                if (string.IsNullOrWhiteSpace(entity.yfbmCode) || entity.yfbmCode.Length > 20)
                {
                    throw new FailedException("Mã bộ phận không được để trống và tối đa 20 ký tự");
                }
                if (string.IsNullOrWhiteSpace(entity.yfbmmc) || entity.yfbmmc.Length > 50)
                {
                    throw new FailedException("Tên bộ phận không được để trống và tối đa 50 ký tự");
                }
                if (string.IsNullOrWhiteSpace(entity.ksCode) || entity.ksCode.Length > 20)
                {
                    throw new FailedException("Mã khoa không được để trống và tối đa 20 ký tự");
                }
                if (string.IsNullOrWhiteSpace(entity.py) || entity.py.Length > 50)
                {
                    throw new FailedException("Chữ đầu pinyin không được để trống và tối đa 50 ký tự");
                }
                if (string.IsNullOrWhiteSpace(entity.mzzybz) || (entity.mzzybz != "0" && entity.mzzybz != "1" && entity.mzzybz != "2" && entity.mzzybz != "3"))
                {
                    throw new FailedException("Cờ ngoại trú/nội trú không hợp lệ");
                }
                if (entity.yjbmjb != 1 && entity.yjbmjb != 2)
                {
                    throw new FailedException("Cấp khoa dược không hợp lệ");
                }
                entity.zt = MapFlag(entity.zt) ?? "1";
                entity.fybz = MapFlag(entity.fybz) ?? "1";
                entity.ynwbz = MapFlag(entity.ynwbz) ?? "1";
                _SysPharmacyDepartmentBaseDmnService.submitForm(entity, keyValue);
            }
            return Success("Thao tác thành công");
        }

        /// <summary>
        /// Map cờ "true"/"1"→"1", "false"/"0"→"0"; không post (null) → null (giữ giá trị gốc khi sửa)
        /// </summary>
        private static string MapFlag(string value)
        {
            if (value == "true" || value == "1") return "1";
            if (value == "false" || value == "0") return "0";
            return null;
        }

        //#region xt_yfbm_yp Bảo trì

        ///// <summary>
        ///// Trang chính 
        ///// </summary>
        ///// <returns></returns>
        //public IActionResult OpenMedicineIndex()
        //{
        //    return View();

        //}

        ///// <summary>
        ///// Cập nhật trang thêm
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
        //        throw new FailedException("Vui lòng chọn tổ chức");
        //    }
        //    else if (!_SysOrganizeDmnService.IsMedicalOrganize(entity.OrganizeId))
        //    {
        //        throw new FailedException("Vui lòng chọn cơ sở y tế(Bệnh viện hoặc phòng khám)");
        //    }
        //    _SysPharmacyDepartmentOpenMedicineRepo.submitForm(entity, keyValue);
        //    return Success("Thao tác thành công");
        //}

        //#endregion

    }
}