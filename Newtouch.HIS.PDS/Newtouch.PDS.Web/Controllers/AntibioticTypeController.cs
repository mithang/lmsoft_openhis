using Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Web.Controllers
{
    public class AntibioticTypeController : ControllerBase
    {
        /// <summary>
        /// Tạo khung nhìn trang tổng quan loại kháng sinh
        /// </summary>
        ISysMedicineAntibioticTypeDmnService _ISysMedicineAntibioticTypeDmnService;
        public IActionResult AntibioticTypeQuery()
        {
            return View();
        }
        /// <summary>
        /// Tạo khung nhìn trang cài đặt loại kháng sinh
        /// </summary>
        /// <returns></returns>
        public IActionResult AntibioticTypeSettingForm()
        {
            return View();
        }


        public AntibioticTypeController(ISysMedicineAntibioticTypeDmnService sysMedicineAntibioticTypeDmnService)
        {
            this._ISysMedicineAntibioticTypeDmnService = sysMedicineAntibioticTypeDmnService;
        }

        /// <summary>
        /// Lấy thông tin loại kháng sinh cấp loại chỉ định
        /// </summary>
        /// <param name="lbdj"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public IActionResult SelectGridList(string typelevel, string parentId = null)
        {
            var data = _ISysMedicineAntibioticTypeDmnService.GetValidListByOrg(OrganizeId, typelevel, parentId);
            return Content(data.ToJson());
        }
        /// <summary>
        /// Tải lên thông tin loại kháng sinh
        /// </summary>
        /// <param name="xm"></param>
        /// <param name="keyvalue"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult submitForm_AntibioticType(SysMedicineAntibioticTypeVO AntibioticType)
        {
            // G37 F37-3: validate truoc khi ghi — khong de loi NOT NULL /
            // truncation SQL tho lot ra UI. Do dai theo xt_kssType
            // (typeName varchar(50), typelevel varchar(2), qxjb varchar(2),
            // qxjbmc varchar(50), parentId varchar(50)).
            var typeName = AntibioticType.typeName == null ? null : AntibioticType.typeName.Trim();
            if (string.IsNullOrEmpty(typeName))
            {
                throw new FailedException("Tên loại kháng sinh không được để trống");
            }
            if (typeName.Length > 50)
            {
                throw new FailedException("Tên loại kháng sinh tối đa 50 ký tự");
            }
            var typelevel = AntibioticType.typelevel == null ? null : AntibioticType.typelevel.Trim();
            if (string.IsNullOrEmpty(typelevel))
            {
                throw new FailedException("Cấp loại không được để trống");
            }
            if (typelevel.Length > 2)
            {
                throw new FailedException("Cấp loại tối đa 2 ký tự");
            }
            if (AntibioticType.qxjb != null && AntibioticType.qxjb.Length > 2)
            {
                throw new FailedException("Cấp quyền tối đa 2 ký tự");
            }
            if (AntibioticType.qxjbmc != null && AntibioticType.qxjbmc.Length > 50)
            {
                throw new FailedException("Tên cấp quyền tối đa 50 ký tự");
            }
            if (AntibioticType.parentId != null && AntibioticType.parentId.Length > 50)
            {
                throw new FailedException("Mã cấp trên tối đa 50 ký tự");
            }

            if (!string.IsNullOrEmpty(AntibioticType.Id))
            {
                // G37 F37-3: sua — load ban ghi goc (GetById da ve Base STG,
                // F37-1), merge-skip-null, giu OrganizeId + zt goc. Khong cho
                // sua dong cua to chuc khac (bao ve kieu G35 F4).
                var existing = _ISysMedicineAntibioticTypeDmnService.GetById(OrganizeId, AntibioticType.Id);
                if (existing == null)
                {
                    throw new FailedException("Không tìm thấy loại kháng sinh cần sửa");
                }
                if (existing.OrganizeId != OrganizeId)
                {
                    throw new FailedException("Không có quyền sửa loại kháng sinh của tổ chức khác");
                }
                existing.typeName = typeName;
                existing.typelevel = typelevel;
                if (AntibioticType.qxjb != null)
                {
                    existing.qxjb = AntibioticType.qxjb;
                }
                if (AntibioticType.qxjbmc != null)
                {
                    existing.qxjbmc = AntibioticType.qxjbmc;
                }
                if (AntibioticType.parentId != null)
                {
                    existing.parentId = AntibioticType.parentId;
                }
                _ISysMedicineAntibioticTypeDmnService.SubmitForm(existing);
            }
            else
            {
                SysMedicineAntibioticTypeVO entity = new SysMedicineAntibioticTypeVO();
                // NX2-001: MapperTo khong co overload 1 tham so (chi co 2 overload
                // generic 0-tham-so trong Shims/MapperExtensions.cs). Dung overload
                // JSON round-trip MapperTo<SysMedicineAntibioticTypeVO>() — cung kieu
                // copy (serialize AntibioticType roi deserialize ve cung VO), entity
                // vua new xong roi gan lai ngay nen ngu nghia giu nguyen.
                entity = AntibioticType.MapperTo<SysMedicineAntibioticTypeVO>();
                entity.typeName = typeName;
                entity.typelevel = typelevel;
                entity.OrganizeId = OrganizeId;
                entity.zt = "1";
                _ISysMedicineAntibioticTypeDmnService.SubmitForm(entity);
            }
            return Success();
        }
        /// <summary>
        /// Xóa loại kháng sinh
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteData(string Id)
        {
            // G37 F37-1: GetById da ve Base STG (DbNames) — dong tao o STG
            // gio tim thay; dong khong ton tai / cua to chuc khac -> loi TV
            // thay vi NRE.
            SysMedicineAntibioticTypeVO entity = _ISysMedicineAntibioticTypeDmnService.GetById(OrganizeId, Id);
            if (entity == null)
            {
                throw new FailedException("Không tìm thấy loại kháng sinh cần xóa");
            }
            entity.zt = "0";
            _ISysMedicineAntibioticTypeDmnService.SubmitForm(entity);
            return Success();
        }
        /// <summary>
        /// Đạt Id Lấy thể hiện
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IActionResult GetById(string Id)
        {
            SysMedicineAntibioticTypeVO entity = _ISysMedicineAntibioticTypeDmnService.GetById( OrganizeId,Id);
            return Content(entity.ToJson());
        }

        public IActionResult GetSelectJson(string parentId)
        {
            var data = _ISysMedicineAntibioticTypeDmnService.GetListByParentId(OrganizeId, parentId);
            List<object> list = new List<object>();
            foreach (SysMedicineAntibioticTypeVO item in data)
            {
                list.Add(new { id = item.Id + "|" + item.qxjb, text = item.typeName });//Mang thông tin cấp quyền vào Idtrong
            }
            return Content(list.ToJson());
        }
    }
}