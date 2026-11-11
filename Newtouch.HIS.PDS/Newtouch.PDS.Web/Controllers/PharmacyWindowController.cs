
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Infrastructure.EF;
using Newtouch.HIS.DomainServices.PharmacyDrugStorage;
using Newtouch.HIS.DomainServices;
using Newtouch.Core.Common;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage;
using Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Web.Controllers
{
    public class PharmacyWindowController : ControllerBase
    {
        //private readonly IPharmacyDmnService _PharmacyDmnService;
        private readonly ISysPharmacyWindowDmnService _SysPharmacyWindowDmnService;
        private readonly ISysOrganizeDmnService _SysOrganizeDmnService;
        private readonly ISysPharmacyDepartmentBaseDmnService _SysPharmacyDepartmentBaseDmnService;

        public PharmacyWindowController(
            //IPharmacyDmnService PharmacyDmnService,
          ISysPharmacyWindowDmnService SysPharmacyWindowDmnService,
          ISysOrganizeDmnService SysOrganizeDmnService,
          ISysPharmacyDepartmentBaseDmnService _SysPharmacyDepartmentBaseDmnService
          )
        {
            //this._PharmacyDmnService = PharmacyDmnService;
            this._SysPharmacyWindowDmnService = SysPharmacyWindowDmnService;
            this._SysOrganizeDmnService = SysOrganizeDmnService;
            this._SysPharmacyDepartmentBaseDmnService = _SysPharmacyDepartmentBaseDmnService;
        }

        /// <summary>
        /// Truy vấn thông tin cửa sổ nhà thuốc
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
                rows = _SysPharmacyWindowDmnService.GetPagintionList(pagintion, organizeId, keyword),
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
        public IActionResult GetFormJson(int? keyValue)
        {
            var data = _SysPharmacyWindowDmnService.GetFormJson(this.OrganizeId, keyValue);
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
        public IActionResult submitForm(SysPharmacyWindowVO entity, int? keyValue)
        {
            if (keyValue.HasValue && keyValue.Value > 0)
            {
                var existing = _SysPharmacyWindowDmnService.GetFormJson(this.OrganizeId, keyValue);
                if (existing == null)
                {
                    throw new FailedException("Không tìm thấy cửa sổ cần sửa");
                }
                // Chỉ validate field đã post (non-null); merge-skip-null, giữ OrganizeId + zt gốc
                if (entity.yfckCode != null && (string.IsNullOrWhiteSpace(entity.yfckCode) || entity.yfckCode.Length > 20))
                {
                    throw new FailedException("Mã cửa sổ không được để trống và tối đa 20 ký tự");
                }
                if (entity.yfckmc != null && (string.IsNullOrWhiteSpace(entity.yfckmc) || entity.yfckmc.Length > 50))
                {
                    throw new FailedException("Tên cửa sổ không được để trống và tối đa 50 ký tự");
                }
                if (entity.yfckCode != null) existing.yfckCode = entity.yfckCode;
                if (entity.yfckmc != null) existing.yfckmc = entity.yfckmc;
                if (entity.px.HasValue) existing.px = entity.px;
                var zt = MapFlag(entity.zt);
                if (zt != null) existing.zt = zt;
                _SysPharmacyWindowDmnService.submitForm(existing, keyValue);
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
                if (string.IsNullOrWhiteSpace(entity.yfckCode) || entity.yfckCode.Length > 20)
                {
                    throw new FailedException("Mã cửa sổ không được để trống và tối đa 20 ký tự");
                }
                if (string.IsNullOrWhiteSpace(entity.yfckmc) || entity.yfckmc.Length > 50)
                {
                    throw new FailedException("Tên cửa sổ không được để trống và tối đa 50 ký tự");
                }
                entity.zt = MapFlag(entity.zt) ?? "1";
                _SysPharmacyWindowDmnService.submitForm(entity, keyValue);
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

        /// <summary>
        /// TheoorganizeId Lấy bộ phận nhà thuốc
        /// </summary>
        /// <returns></returns>
        public IActionResult PharmacyDepartmentList(string organizeId, byte? yjbmjb)
        {
            var list = _SysPharmacyDepartmentBaseDmnService.GetList(organizeId, yjbmjb);
            return Content(list.ToJson());
        }
    }
}