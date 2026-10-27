using Newtouch.HIS.Domain.Entity;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Application.Interface;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.HIS.Base.Web.Controllers
{
    public class OrganizeController : ControllerBase
    {
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;
        private readonly ISysOrganizeRepository _sysOrganizeRepository;
        private readonly ISysUserApp _sysUserApp;
        private readonly ISysApplicationDmnService _sysApplicationDmnService;

        public OrganizeController(ISysOrganizeDmnService sysOrganizeDmnService
            , ISysOrganizeRepository sysOrganizeRepository, ISysUserApp sysUserApp
            , ISysApplicationDmnService sysApplicationDmnService)
        {
            this._sysOrganizeDmnService = sysOrganizeDmnService;
            this._sysOrganizeRepository = sysOrganizeRepository;
            this._sysUserApp = sysUserApp;
            this._sysApplicationDmnService = sysApplicationDmnService;
        }

        //grid json
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = _sysOrganizeRepository.GetPagintionListByTopOrg(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
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
            if (string.IsNullOrWhiteSpace(entity.Code))
            {
                throw new FailedException("Mã không được để trống");
            }
            entity.zt = entity.zt == "true" ? "1" : "0";

            if (string.IsNullOrWhiteSpace(keyValue))
            {
                //Thêm mới
                entity.ParentId = null;
                entity.TopOrganizeId = null;
            }

            if (string.IsNullOrWhiteSpace(entity.ParentId) || entity.ParentId == "&nbsp;")
            {
                entity.ParentId = null;
            }

            _sysOrganizeRepository.SubmitForm(entity, keyValue);

            if (string.IsNullOrWhiteSpace(keyValue) && string.IsNullOrWhiteSpace(entity.ParentId))
            {
                //Đã tạo thành công một tổ chức cấp cao nhất Thử khởi tạo một quản trị viên hệ thống cho tổ chức đóadmin
                try
                {
                    _sysUserApp.CreateDefaultAdminToOrg(entity.Id, Constants.LogonDefaultPassword);
                    //return Success("Thao tác thành công。và đã khởi tạo thành công quản trị viên hệ thốngadmin");
                    return Success("Thao tác thành công<br/>và đã khởi tạo thành công quản trị viên hệ thốngadmin");
                }
                catch (Exception ex)
                {
                }
            }

            return Success("Thao tác thành công.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appid"></param>
        /// <returns></returns>
        public IActionResult GetAuthOrganizeList(string appid)
        {
            var orgList = _sysOrganizeRepository.IQueryable().Where(p => p.zt == "1").ToList();
            var treeList = new List<TreeViewModel>();
            IList<SysOrganizeEntity> appCurrentOrgEntityList = new List<SysOrganizeEntity>();
            if (!string.IsNullOrWhiteSpace(appid))
            {
                appCurrentOrgEntityList = _sysApplicationDmnService.GetAuthedOrgListByAppId(appid);
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
                tree.checkstate = appCurrentOrgEntityList.Count(t => t.Id == item.Id);
                tree.hasChildren = false;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson(null));
        }

    }
}