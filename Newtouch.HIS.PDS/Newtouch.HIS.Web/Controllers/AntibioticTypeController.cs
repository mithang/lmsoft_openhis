using Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage;
using Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Controllers
{
    public class AntibioticTypeController : ControllerBase
    {
        /// <summary>
        /// Tạo view tổng quan danh mục kháng sinh
        /// </summary>
        ISysMedicineAntibioticTypeDmnService _ISysMedicineAntibioticTypeDmnService;
        public ActionResult AntibioticTypeQuery()
        {
            return View();
        }
        /// <summary>
        /// tạo kháng sinh loại cài đặt trang khung nhìn
        /// </summary>
        /// <returns></returns>
        public ActionResult AntibioticTypeSettingForm()
        {
            return View();
        }


        public AntibioticTypeController(ISysMedicineAntibioticTypeDmnService sysMedicineAntibioticTypeDmnService)
        {
            this._ISysMedicineAntibioticTypeDmnService = sysMedicineAntibioticTypeDmnService;
        }

        /// <summary>
        /// Lấy thông tin danh mục kháng sinh theo cấp danh mục chỉ định
        /// </summary>
        /// <param name="lbdj"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public ActionResult SelectGridList(string typelevel, string parentId = null)
        {
            var data = _ISysMedicineAntibioticTypeDmnService.GetValidListByOrg(OrganizeId, typelevel, parentId);
            return Content(data.ToJson());
        }
        /// <summary>
        /// kháng sinh loại thông tin tải lên
        /// </summary>
        /// <param name="xm"></param>
        /// <param name="keyvalue"></param>
        /// <returns></returns>
        public ActionResult submitForm_AntibioticType(SysMedicineAntibioticTypeVO AntibioticType)
        {
            SysMedicineAntibioticTypeVO entity = new SysMedicineAntibioticTypeVO();
            entity = AntibioticType.MapperTo(entity);
            entity.OrganizeId = OrganizeId;
            entity.zt = "1";
            _ISysMedicineAntibioticTypeDmnService.SubmitForm(entity);
            return Success();
        }
        /// <summary>
        /// xóa kháng sinh loại
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult DeleteData(string Id)
        {
            SysMedicineAntibioticTypeVO entity = _ISysMedicineAntibioticTypeDmnService.GetById(OrganizeId, Id);
            entity.zt = "0";
            _ISysMedicineAntibioticTypeDmnService.SubmitForm(entity);
            return Success();
        }
        /// <summary>
        /// đạt Id lấy thực ví dụ
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult GetById(string Id)
        {
            SysMedicineAntibioticTypeVO entity = _ISysMedicineAntibioticTypeDmnService.GetById( OrganizeId,Id);
            return Content(entity.ToJson());
        }

        public ActionResult GetSelectJson(string parentId)
        {
            var data = _ISysMedicineAntibioticTypeDmnService.GetListByParentId(OrganizeId, parentId);
            List<object> list = new List<object>();
            foreach (SysMedicineAntibioticTypeVO item in data)
            {
                list.Add(new { id = item.Id + "|" + item.qxjb, text = item.typeName });//Đưa thông tin cấp quyền vàoId trong
            }
            return Content(list.ToJson());
        }
    }
}