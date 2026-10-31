using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Framework.Attributes;
using Newtouch.CIS.Web.Attributes;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Application.Interface;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.CIS.Repository;
using Newtouch.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.CIS.Compat;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.CIS.Web.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    public class InspectionCategoryController : OrgControllerBase
    {
        // GET: SystemManage/InspectionCategory
        private readonly IInspectionCategoryRepo _inspectionCategoryRepo;

        public InspectionCategoryController(IInspectionCategoryRepo inspectionCategoryRepo)
        {
            this._inspectionCategoryRepo = inspectionCategoryRepo;
        }

        [HttpGet]
        [HandlerAjaxOnly]
        /// <summary>
        /// Lấytừ điểndanh sách
        /// </summary>
        /// <returns></returns>
        public IActionResult GetTreeGridJson(string keyword)
        {
            var data = _inspectionCategoryRepo.GetListByOrg(this.OrganizeId).ToList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.Where(t => t.dlmc.Contains(keyword) || t.dlCode.Contains(keyword)).ToList();
            }
            return Content(data.ToJson());
        }

        /// <summary>
        /// Khi sửa thông tin,thông tinmangđếnmớitrangmặt
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetFormJson(string keyValue)
        {
            var entity = _inspectionCategoryRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Lưu
        /// </summary>
        [HttpPost]
        [HandlerAjaxOnly]
        public IActionResult SubmitForm(InspectionCategoryEntity entity, string keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            entity.OrganizeId = this.OrganizeId;
            _inspectionCategoryRepo.SubmitForm(entity, keyValue);
            return Success("Thao tác thành công.");
        }

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult DeleteForm(string keyValue)
        {
            _inspectionCategoryRepo.DeleteForm(keyValue);
            return Success("Xóa thành công.");
        }

        public IActionResult Chat()
        {
            return View();
        }
    }
}