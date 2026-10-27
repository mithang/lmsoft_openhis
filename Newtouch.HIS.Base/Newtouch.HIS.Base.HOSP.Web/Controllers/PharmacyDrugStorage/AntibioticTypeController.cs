using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Base.HOSP.Web.Controllers
{
    public class AntibioticTypeController : ControllerBase
    {
        /// <summary>
        /// Tạo khung nhìn trang tổng quan loại kháng sinh
        /// </summary>
        ISysMedicineAntibioticTypeRepo _ISysMedicineAntibioticTypeRepo;
        public IActionResult AntibioticTypeQuerry()
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


        public AntibioticTypeController(ISysMedicineAntibioticTypeRepo sysMedicineAntibioticTypeRepo)
        {
            this._ISysMedicineAntibioticTypeRepo = sysMedicineAntibioticTypeRepo;
        }

        /// <summary>
        /// Lấy thông tin loại kháng sinh theo cấp loại được chỉ định
        /// </summary>
        /// <param name="lbdj"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public IActionResult SelectGridList(string typelevel, string parentId = null)
        {
            var data = _ISysMedicineAntibioticTypeRepo.GetValidListByOrg(OrganizeId, typelevel, parentId);
            return Content(data.ToJson());
        }
        /// <summary>
        /// Tải lên thông tin loại kháng sinh
        /// </summary>
        /// <param name="xm"></param>
        /// <param name="keyvalue"></param>
        /// <returns></returns>
        public IActionResult submitForm_AntibioticType(SysMedicineAntibioticTypeEntity AntibioticType)
        {
            SysMedicineAntibioticTypeEntity entity = new SysMedicineAntibioticTypeEntity();
            entity = AntibioticType.MapperTo(entity);
            entity.OrganizeId = OrganizeId;
            entity.zt = "1";
            _ISysMedicineAntibioticTypeRepo.SubmitForm(entity);
            return Success();
        }
        /// <summary>
        /// Xóa loại kháng sinh
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IActionResult DeleteData(string Id)
        {
            SysMedicineAntibioticTypeEntity entity = _ISysMedicineAntibioticTypeRepo.FindEntity(p => p.Id == Id && p.OrganizeId == OrganizeId);
            entity.zt = "0";
            _ISysMedicineAntibioticTypeRepo.SubmitForm(entity);
            return Success();
        }
        /// <summary>
        /// Thông quaIdLấy thực thể
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IActionResult GetById(string Id)
        {
            SysMedicineAntibioticTypeEntity entity = _ISysMedicineAntibioticTypeRepo.FindEntity(p => p.Id == Id && p.OrganizeId == OrganizeId);
            return Content(entity.ToJson());
        }

        public IActionResult GetSelectJson(string parentId)
        {
            var data = _ISysMedicineAntibioticTypeRepo.GetListByParentId(OrganizeId, parentId);
            List<object> list = new List<object>();
            if (data!=null)
            {
                foreach (SysMedicineAntibioticTypeEntity item in data)
                {
                    list.Add(new { id = item.Id + "|" + item.qxjb, text = item.typeName });//Mang thông tin cấp quyền vàoIdtrong
                }
            }
            
            return Content(list.ToJson());
        }
    }
}