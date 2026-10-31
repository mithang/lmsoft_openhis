using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Base.HOSP.Web.Controllers
{
    public class OrganizeController : ControllerBase
    {
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;
        private readonly ISysOrganizeRepository _sysOrganizeRepository;
        private readonly ISysModuleDmnService _sysModuleDmnService;

        public OrganizeController(ISysOrganizeDmnService sysOrganizeDmnService
            , ISysOrganizeRepository sysOrganizeRepository
            , ISysModuleDmnService sysModuleDmnService)
        {
            this._sysOrganizeDmnService = sysOrganizeDmnService;
            this._sysOrganizeRepository = sysOrganizeRepository;
            this._sysModuleDmnService = sysModuleDmnService;
        }

        //Thả xuống Nguồn dữ liệu
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetTreeSelectJson()
        {
            var data = _sysOrganizeRepository.GetValidListByTopOrg(Constants.TopOrganizeId);
            var treeList = new List<TreeSelectModel>();
            foreach (SysOrganizeEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.Id;
                treeModel.text = item.Name;
                treeModel.parentId = item.ParentId;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson(null));
        }

        //Thả xuống Nguồn dữ liệu
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetChildTreeSelectJson(string orgId)
        {
            if (string.IsNullOrWhiteSpace(orgId))
            {
                orgId = base.GetAuthOrganizeId();
            }
            var data = _sysOrganizeRepository.GetValidListByParentOrg(orgId);
            var treeList = new List<TreeSelectModel>();
            foreach (SysOrganizeEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.Id;
                treeModel.text = item.Name;
                treeModel.parentId = item.Id == orgId ? null : item.ParentId;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson(null));
        }

        //grid json
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetTreeGridJson(string keyword)
        {
            string topOrgId = base.GetAuthOrganizeId();
            //Người dùng tập đoàn có thể sửa tất cả tổ chức,Nhân viên thao tác của mỗi tổ chức chỉ được bảo trì tổ chức con thuộc tổ chức mình.
            var data = _sysOrganizeRepository.GetListByParentOrg(topOrgId);
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.Name.Contains(keyword) || t.Code.Contains(keyword), parentId: "ParentId");
            }
            var treeList = new List<TreeGridModel>();
            foreach (var item in data)
            {
                item.AllowEdit = item.Id != topOrgId; //Cấp cao nhất không thể sửa,Khác có thể sửa

                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = data.Any(t => t.ParentId == item.Id);
                treeModel.id = item.Id;
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.Id == topOrgId ? null : item.ParentId;
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
            var data = _sysOrganizeRepository.FindEntity(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public IActionResult SubmitForm(SysOrganizeEntity entity, string keyValue)
        {
            if (!string.IsNullOrWhiteSpace(keyValue) && entity.ParentId == keyValue)
            {
                throw new FailedException("Chọn cơ sở cấp trên sai");
            }
            if (string.IsNullOrWhiteSpace(entity.ParentId))
            {
                throw new FailedException("Vui lòng chọn cơ sở cấp trên");
            }
            if (string.IsNullOrWhiteSpace(entity.Code))
            {
                throw new FailedException("Mã không được để trống");
            }
            entity.zt = entity.zt == "true" ? "1" : "0";
            entity.TopOrganizeId = Constants.TopOrganizeId;
            _sysOrganizeRepository.SubmitForm(entity, keyValue);
            return Success("Thao tác thành công.");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        //[HandlerAuthorize]
        //[ValidateAntiForgeryToken]
        public IActionResult DeleteForm(string keyValue)
        {
            _sysOrganizeRepository.Delete(p => p.Id == keyValue);
            return Success("Xóa thành công.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult GetAuthOrganizeList(string keyValue, string authType = "module")
        {
            var orgList = _sysOrganizeRepository.GetValidTopOrgList();
            var treeList = new List<TreeViewModel>();
            IList<SysOrganizeEntity> currentOrgEntityList = new List<SysOrganizeEntity>();
            if (authType == "module")
            {
                if (!string.IsNullOrWhiteSpace(keyValue))
                {
                    currentOrgEntityList = _sysModuleDmnService.GetAuthedOrgListByModuleId(keyValue, 1);
                }
            }
            else if (authType == "modulebutton")
            {
                if (!string.IsNullOrWhiteSpace(keyValue))
                {
                    currentOrgEntityList = _sysModuleDmnService.GetAuthedOrgListByModuleId(keyValue, 2);
                }
            }

            foreach (SysOrganizeEntity item in orgList)
            {
                TreeViewModel tree = new TreeViewModel();
                tree.id = item.Id;
                tree.text = item.Name;
                tree.value = item.Code;
                tree.parentId = null;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = currentOrgEntityList.Count(t => t.Id == item.Id);
                tree.hasChildren = false;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson(null));
        }

    }
}