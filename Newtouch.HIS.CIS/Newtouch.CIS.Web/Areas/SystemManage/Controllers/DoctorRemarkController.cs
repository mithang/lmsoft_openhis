using NewtouchHIS.Framework.Web.Controllers;
using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Application.Interface;
using Newtouch.CIS.Repository;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.CIS.Compat;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.CIS.Web.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    public class DoctorRemarkController : OrgControllerBase
    {
        // GET: SystemManage/DoctorRemark
        private readonly ISysDoctorRemarkRepo _sysDoctorRemarkRepo;
        private readonly IBaseDataDmnService _baseDataDmnService;

        public DoctorRemarkController(ISysDoctorRemarkRepo sysDoctorRemarkRepo, IBaseDataDmnService baseDataDmnService)
        {
            this._sysDoctorRemarkRepo = sysDoctorRemarkRepo;
            this._baseDataDmnService = baseDataDmnService;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetTreeGridJson(string keyword)
        {
            var data = _sysDoctorRemarkRepo.GetListByOrg(this.OrganizeId).ToList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.Where(t => t.ztmc.Contains(keyword) || t.ztCode.Contains(keyword)).ToList();
            }
            return Content(data.ToJson());
        }

        /// <summary>
        /// 修改信息时，把信息带到新页面
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetFormJson(string keyValue)
        {
            var entity = _sysDoctorRemarkRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost]
        [HandlerAjaxOnly]
        public IActionResult SubmitForm(SysDoctorRemarkEntity entity, string keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            entity.OrganizeId = this.OrganizeId;
            _sysDoctorRemarkRepo.SubmitForm(entity, keyValue);
            return Success("Thao tác thành công.");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult DeleteForm(string keyValue)
        {
            _sysDoctorRemarkRepo.DeleteForm(keyValue);
            return Success("Xóa thành công.");
        }

        /// <summary>
        /// 系统嘱托 浮层
        /// </summary>
        /// <returns></returns>

        public IActionResult GetSysDoctorRemark(string keyword)
        {
            var data = _baseDataDmnService.GetSysDoctorRemark(this.OrganizeId,this.UserIdentity.DepartmentCode);
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                data = data.Where(a => a.ztCode.Contains(keyword) || a.ztmc.Contains(keyword)).ToList();
            }
            return Content(data.ToJson());
        }



        #region Bác sĩ站--》Văn bảnNhập--》Chỉ định文本框Nút
        //public IActionResult surgery_record()
        //{
            
        //    var data=_sysDoctorRemarkRepo.surgery_record(this.OrganizeId);
            
        //    return Content(data.ToJson());
        //}

        #endregion




    }
}