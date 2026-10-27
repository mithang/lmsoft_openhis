using NewtouchHIS.Framework.Web.Controllers;
using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Application.Interface;
using Newtouch.CIS.Repository;
using Newtouch.Domain.Entity;
using Newtouch.Domain.Entity.Outpatient;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.CIS.Compat;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.CIS.Web.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    public class BodyPartsController : OrgControllerBase
    {
        // GET: SystemManage/BodyParts
        private readonly ISysBodyPartsRepo _sysBodyPartsRepo;
        private readonly IPacs_ExamBodyPartsRepo _pacs_ExamBodyPartsRepo;
        private readonly IBaseDataDmnService _baseDataDmnService;

        public BodyPartsController(ISysBodyPartsRepo sysBodyPartsRepo, IPacs_ExamBodyPartsRepo pacs_ExamBodyPartsRepo, IBaseDataDmnService baseDataDmnService)
        {
            this._sysBodyPartsRepo = sysBodyPartsRepo;
            this._pacs_ExamBodyPartsRepo = pacs_ExamBodyPartsRepo;
            this._baseDataDmnService = baseDataDmnService;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetTreeGridJson(string keyword,string bwlx)
        {
            if (bwlx=="jc")
            {
                var jcdata= _baseDataDmnService.GetJcListByOrg(this.OrganizeId).ToList();
                jcdata = jcdata.Where(t =>t.bwflCode.Contains(keyword)|| t.bwflmc.Contains(keyword) || t.bwmc.Contains(keyword) || t.bwCode.Contains(keyword)).ToList();
                return Content(jcdata.ToJson());
            }
            var data = _sysBodyPartsRepo.GetListByOrg(this.OrganizeId).ToList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.Where(t => t.bwflCode.Contains(keyword) || t.bwflmc.Contains(keyword) || t.bwmc.Contains(keyword) || t.bwCode.Contains(keyword)).ToList();
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
            var entity = _sysBodyPartsRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }
        /// <summary>
        /// 修改信息时，把信息带到新页面
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetJcFormJson(string keyValue)
        {
            var entity = _pacs_ExamBodyPartsRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost]
        [HandlerAjaxOnly]
        public IActionResult SubmitForm(SysBodyPartsEntity entity, string keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            entity.OrganizeId = this.OrganizeId;
            _sysBodyPartsRepo.SubmitForm(entity, keyValue);
            return Success("Thao tác thành công.");
        }
        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost]
        [HandlerAjaxOnly]
        public IActionResult jcbwSubmitForm(Pacs_ExamBodyPartsEntity entity, string keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            entity.OrganizeId = this.OrganizeId;
            _pacs_ExamBodyPartsRepo.SubmitForm(entity, keyValue);
            return Success("Thao tác thành công.");
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult DeleteForm(string keyValue)
        {
            _sysBodyPartsRepo.DeleteForm(keyValue);
            return Success("Xóa thành công.");
        }
        /// <summary>
        /// 删除检查部位
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult DeleteJcForm(string keyValue)
        {
            _pacs_ExamBodyPartsRepo.DeleteForm(keyValue);
            return Success("Xóa thành công.");
        }
        /// <summary>
        /// 部位类别
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetSysBodyBwFl(string keyword)
        {
            var data = _baseDataDmnService.GetSysBodyBwFl(this.OrganizeId);
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                data = data.Where(a => a.bwflCode.Contains(keyword) || a.bwflmc.Contains(keyword)).ToList();
            }
            return Content(data.ToJson());
        }
        /// <summary>
        /// 检查部位类别
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetSysBodyJcBwFl(string keyword)
        {
            var data = _baseDataDmnService.GetSysBodyJcBwFl(this.OrganizeId);
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                data = data.Where(a => a.bwflCode.Contains(keyword) || a.bwflmc.Contains(keyword)).ToList();
            }
            return Content(data.ToJson());
        }

        /// <summary>
        /// 系统部位 浮层
        /// </summary>
        /// <returns></returns>

        public IActionResult GetSysBodyParts(string keyword,string bwfl=null,string bwmc=null)
        {
            var data = _baseDataDmnService.GetSysBodyParts(this.OrganizeId);
            bwfl = bwfl ?? "";
            bwmc = bwmc ?? "";
            if (!string.IsNullOrWhiteSpace(bwfl))
                data = data.Where(a => a.bwflCode.Contains(bwfl) || a.bwflmc.Contains(bwfl)).ToList();

            if (!string.IsNullOrWhiteSpace(bwmc))
                data = data.Where(a => a.bwCode.Contains(bwmc) || a.bwmc.Contains(bwmc)).ToList();

            if (!string.IsNullOrWhiteSpace(keyword)&& string.IsNullOrWhiteSpace(bwmc)&& string.IsNullOrWhiteSpace(bwfl))
            {
                data = data.Where(a => a.bwCode.Contains(keyword) || a.bwmc.Contains(keyword) || a.bwflCode.Contains(keyword) || a.bwflmc.Contains(keyword)).ToList();
            }
            return Content(data.Take(15).ToJson());
        }
        

        /// <summary>
        /// 影像部位 浮层
        /// </summary>
        /// <returns></returns>

        public IActionResult GetYxBodyParts(string keyword, string bwfl = null, string bwmc = null)
        {
            var data = _baseDataDmnService.GetYxBodyParts(this.OrganizeId);
            bwfl = bwfl ?? "";
            bwmc = bwmc ?? "";
            if (!string.IsNullOrWhiteSpace(bwfl))
                data = data.Where(a => a.bwflmc.Contains(bwfl)).ToList();

            if (!string.IsNullOrWhiteSpace(bwmc))
                data = data.Where(a => a.jcbw.Contains(bwmc)).ToList();

            if (!string.IsNullOrWhiteSpace(keyword) && string.IsNullOrWhiteSpace(bwmc) && string.IsNullOrWhiteSpace(bwfl))
            {
                data = data.Where(a => a.jcbw.Contains(keyword)|| a.bwflCode.Contains(keyword) || a.bwflmc.Contains(keyword)).ToList();
            }
            return Content(data.Take(15).ToJson());
        }

        public IActionResult GetYxBodyMethod(string keyword) {
            var data = _baseDataDmnService.GetYxBodyMethod(this.OrganizeId,keyword);
            return Content(data.ToJson());
        }
        public IActionResult JcForm()
        {
            return View();
        }
    }
}