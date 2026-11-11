using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.Common;
using Newtouch.HIS.Application;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;


namespace Newtouch.HIS.Web.Controllers
{
    public class ModuleController : ControllerBase
    {
        private readonly ISysModuleDmnService _sysModuleDmnService;
        private readonly IModuleApp _moduleApp;

        // NX2-001: file goc dung _moduleApp (GetTreeSelectJson/GetFormJson/
        // DeleteForm) nhung khong khai bao field va khong co ctor; field
        // _sysModuleDmnService cung khong bao gio duoc gan (runtime NRE).
        // Ca 2 service da dang ky Singleton trong Program.cs (IModuleApp :68,
        // ISysModuleDmnService :140).
        public ModuleController(IModuleApp moduleApp, ISysModuleDmnService sysModuleDmnService)
        {
            _moduleApp = moduleApp;
            _sysModuleDmnService = sysModuleDmnService;
        }


        //Thả xuống(Chọn thực đơn Chỉnh sửa Cấp trên)
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetTreeSelectJson()
        {
            var data = _moduleApp.GetValidList();
            var treeList = new List<TreeSelectModel>();
            foreach (SysModuleEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.Id;
                treeModel.text = item.Name;
                treeModel.parentId = item.ParentId;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson(null));
        }

        //grid json
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetTreeGridJson(string keyword)
        {
            var data = _sysModuleDmnService.GetMenuListByTopOrg(Constants.TopOrganizeId);
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.ToList().TreeWhere(t => t.Name.Contains(keyword), parentId: "ParentId");
            }
            var treeList = new List<TreeGridModel>();
            foreach (SysModuleEntity item in data)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = data.Count(t => t.ParentId == item.Id) == 0 ? false : true;
                treeModel.id = item.Id;
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.ParentId;
                treeModel.expanded = hasChildren;
                treeModel.entityJson = item.ToJson();
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeGridJson(null));
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetFormJson(string keyValue)
        {
            var data = _moduleApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitForm(SysModuleEntity moduleEntity, string keyValue)
        {
            // G37 F37-5: map zt chap nhan ca "true"/"1" (gia tri DB) va
            // "false"/"0"; khong post zt -> null (giu zt goc khi sua).
            string mappedZt = null;
            if (moduleEntity.zt == "true" || moduleEntity.zt == "1")
            {
                mappedZt = "1";
            }
            else if (moduleEntity.zt == "false" || moduleEntity.zt == "0")
            {
                mappedZt = "0";
            }
            if (!string.IsNullOrEmpty(keyValue))
            {
                // G37 F37-5: sua — load ban ghi goc, merge-skip-null (chi
                // copy field post non-null), giu cac cot khong post. Neu
                // khong co ban ghi -> loi TV thay vi Success 0 dong.
                var existing = _moduleApp.GetForm(keyValue);
                if (existing == null)
                {
                    throw new FailedException("Không tìm thấy thực đơn cần sửa");
                }
                if (moduleEntity.Name != null)
                {
                    existing.Name = moduleEntity.Name;
                }
                if (moduleEntity.Code != null)
                {
                    existing.Code = moduleEntity.Code;
                }
                if (moduleEntity.UrlAddress != null)
                {
                    existing.UrlAddress = moduleEntity.UrlAddress;
                }
                if (moduleEntity.Target != null)
                {
                    existing.Target = moduleEntity.Target;
                }
                if (moduleEntity.Icon != null)
                {
                    existing.Icon = moduleEntity.Icon;
                }
                if (moduleEntity.Description != null)
                {
                    existing.Description = moduleEntity.Description;
                }
                if (moduleEntity.EnName != null)
                {
                    existing.EnName = moduleEntity.EnName;
                }
                if (moduleEntity.ParentId != null)
                {
                    existing.ParentId = moduleEntity.ParentId;
                }
                // px la int? — null nghia la khong post, copy ca khi post 0.
                if (moduleEntity.px != null)
                {
                    existing.px = moduleEntity.px;
                }
                if (mappedZt != null)
                {
                    existing.zt = mappedZt;
                }
                _sysModuleDmnService.SubmitForm(existing, keyValue, Constants.TopOrganizeId);
            }
            else
            {
                moduleEntity.zt = mappedZt ?? "1";
                _sysModuleDmnService.SubmitForm(moduleEntity, keyValue, Constants.TopOrganizeId);
            }
            return Success("Thao tác thành công。");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteForm(string keyValue)
        {
            _moduleApp.DeleteForm(keyValue);
            return Success("Xóa thành công。");
        }

        #region Cơ sở ủy quyền

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Organizes()
        {
            return View();
        }

        /// <summary>
        /// Ủy quyền cho tổ chức chỉ định
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="orgList"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateAuthOrganizeList(string keyValue, string orgList)
        {
            // G37 F37-6: keyValue rong -> loi TV thay vi Success khong lam gi.
            if (string.IsNullOrWhiteSpace(keyValue))
            {
                throw new FailedException("Thiếu mã thực đơn cần ủy quyền");
            }
            _sysModuleDmnService.UpdateAuthOrganizeList(keyValue, orgList, 1);
            return Success("Thao tác thành công。");
        }

        /// <summary>
        /// Ủy quyền cho tất cả tổ chức
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public IActionResult AuthAllOrganize(string keyValue)
        {
            // G37 F37-6: nhu tren.
            if (string.IsNullOrWhiteSpace(keyValue))
            {
                throw new FailedException("Thiếu mã thực đơn cần ủy quyền");
            }
            _sysModuleDmnService.AuthAllOrganize(keyValue, 1);
            return Success("Thao tác thành công。");
        }

        /// <summary>
        /// Thu hồi tất cả ủy quyền(Tổ chức)
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public IActionResult AuthCancelAllOrganize(string keyValue)
        {
            // G37 F37-6: nhu tren.
            if (string.IsNullOrWhiteSpace(keyValue))
            {
                throw new FailedException("Thiếu mã thực đơn cần ủy quyền");
            }
            _sysModuleDmnService.AuthCancelAllOrganize(keyValue, 1);
            return Success("Thao tác thành công。");
        }

        #endregion
    }
}
