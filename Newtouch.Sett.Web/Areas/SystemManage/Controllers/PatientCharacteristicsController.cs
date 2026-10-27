using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.Domain;
using Newtouch.Sett.Infrastructure;
using Newtouch.Sett.Web.Controllers;
using Newtouch.Sett.Web.Helpers;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Newtouch.Sett.Web.Areas.SystemManage.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [Area("SystemManage")]

    public class PatientCharacteristicsController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly ISysPatientNatureRepo _sysPatiNatureRepo;
        public PatientCharacteristicsController(ISysPatientNatureRepo sysPatiNatureRepo)
        {
            _sysPatiNatureRepo = sysPatiNatureRepo;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetTreeGridJson(string keyword)
        {
            var data = _sysPatiNatureRepo.GetbxzcBySearch(null, this.OrganizeId);
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.brxzmc.Contains(keyword) || t.brxz.Contains(keyword) || t.py.Contains(keyword)
                , parentId: "ParentId");
            }
            var treeList = new List<TreeGridModel>();
            foreach (SysPatientNatureEntity item in data)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = data.Any(t => t.ParentId == item.brxzbh);
                treeModel.id = item.brxzbh.ToString();
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.ParentId == null ? null : item.ParentId.ToString();
                treeModel.expanded = hasChildren;
                treeModel.entityJson = item.ToJson();
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeGridJson(null));
        }

        /// <summary>
        /// 病人性质下拉树
        /// </summary>
        /// <returns></returns>
        public IActionResult GetTreeSelectJson()
        {
            var data = _sysPatiNatureRepo.GetbxzcBySearch(null, this.OrganizeId);
            var treeList = new List<TreeSelectModel>();
            foreach (SysPatientNatureEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.brxzbh.ToString();  //选的是Id
                treeModel.text = item.brxzmc;
                treeModel.parentId = item.ParentId == null ? null : item.ParentId.ToString();
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson(null));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult DeleteForm(int keyValue)
        {
            _sysPatiNatureRepo.DeleteForm(keyValue, this.OrganizeId);
            return Success("Xóa thành công");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brxzbh"></param>
        /// <returns></returns>
        public IActionResult Form(int brxzbh)
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <returns></returns>
        public IActionResult GetFormJson(string KeyValue)
        {
            var data = _sysPatiNatureRepo.GetForm(KeyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult SubmitForm(SysPatientNatureEntity entity, string keyValue)
        {
            if (!string.IsNullOrWhiteSpace(keyValue) && (entity.ParentId ?? -1).ToString() == keyValue)
            {
                return Error("Chọn cấp cha bị lỗi");
            }
            entity.zt = entity.zt == "true" ? "1" : "0";
            entity.pzbz = entity.pzbz == "true" ? "1" : "0";
            entity.OrganizeId = this.OrganizeId;
            _sysPatiNatureRepo.SubmitForm(entity, keyValue);
            return Success("Thao tác thành công。");
        }

    }
}
