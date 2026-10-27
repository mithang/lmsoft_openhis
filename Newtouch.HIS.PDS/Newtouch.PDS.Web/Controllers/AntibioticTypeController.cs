using Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage;
using NewtouchHIS.Lib.Base.Extension;
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
        public IActionResult submitForm_AntibioticType(SysMedicineAntibioticTypeVO AntibioticType)
        {
            SysMedicineAntibioticTypeVO entity = new SysMedicineAntibioticTypeVO();
            entity = AntibioticType.MapperTo(entity);
            entity.OrganizeId = OrganizeId;
            entity.zt = "1";
            _ISysMedicineAntibioticTypeDmnService.SubmitForm(entity);
            return Success();
        }
        /// <summary>
        /// Xóa loại kháng sinh
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IActionResult DeleteData(string Id)
        {
            SysMedicineAntibioticTypeVO entity = _ISysMedicineAntibioticTypeDmnService.GetById(OrganizeId, Id);
            entity.zt = "0";
            _ISysMedicineAntibioticTypeDmnService.SubmitForm(entity);
            return Success();
        }
        /// <summary>
        /// ĐạtIdLấy thể hiện
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
                list.Add(new { id = item.Id + "|" + item.qxjb, text = item.typeName });//Mang thông tin cấp quyền vàoIdtrong
            }
            return Content(list.ToJson());
        }
    }
}