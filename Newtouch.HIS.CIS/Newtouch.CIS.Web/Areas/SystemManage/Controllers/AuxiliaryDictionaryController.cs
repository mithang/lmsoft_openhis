using System.Collections.Generic;
using Newtouch.CIS.Web.Attributes;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Application.Interface;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.CIS.Repository;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Framework.Web.Controllers;

using Newtouch.CIS.Compat;
using Newtouch.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.CIS.Compat;

namespace Newtouch.CIS.Web.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    public class AuxiliaryDictionaryController : OrgControllerBase
    {
        private readonly IAuxiliaryDictionaryRepo _auxiliaryDictionaryRepo;

        public AuxiliaryDictionaryController(IAuxiliaryDictionaryRepo auxiliaryDictionaryRepo)
        {
            this._auxiliaryDictionaryRepo = auxiliaryDictionaryRepo;
        }
        // GET: AuxiliaryDictionary

        //Tổ chức（yviện） từ điển dướikéo Nguồn dữ liệu
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetTreeSelectJson(string treeidFieldName = "Code")
        {
            var data = _auxiliaryDictionaryRepo.GetValidListByOrg(this.OrganizeId);
            var treeList = new List<TreeSelectModel>();
            foreach (SysAuxiliaryDictionaryEntity item in data)
            {
                if (treeidFieldName == "Code")
                {
                    TreeSelectModel treeModel = new TreeSelectModel();
                    treeModel.id = item.Code;
                    treeModel.text = item.Name;
                    treeModel.parentId = item.ParentId == null ? null :
                        data.Where(p => p.Id == item.ParentId).Select(p => p.Code).FirstOrDefault();
                    treeList.Add(treeModel);
                }
                else
                {
                    TreeSelectModel treeModel = new TreeSelectModel();
                    treeModel.id = item.Id;
                    treeModel.text = item.Name;
                    treeModel.parentId = item.ParentId;
                    treeList.Add(treeModel);
                }
            }
            return Content(treeList.TreeSelectJson(null));
        }

        /// <summary>
        /// Lấytừ điểndanh sách
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetTreeGridJson(string keyword)
        {
            var data = _auxiliaryDictionaryRepo.GetListByOrg(this.OrganizeId).ToList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.Name.Contains(keyword) || t.Code.Contains(keyword), parentId: "ParentId");
            }
            var treeList = new List<TreeGridModel>();
            foreach (SysAuxiliaryDictionaryEntity item in data)
            {
                var hasChildren = data.Count(t => t.ParentId == item.Id) != 0;
                treeList.Add(new TreeGridModel
                {
                    id = item.Id,
                    isLeaf = hasChildren,
                    parentId = item.ParentId,
                    expanded = hasChildren,
                    entityJson = item.ToJson()
                });
            }
            return Content(treeList.TreeGridJson(null));
        }

        /// <summary>
        /// Khi sửa thông tin，thông tinmangđếnmớitrangmặt
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetFormJson(string keyValue)
        {
            var entity = _auxiliaryDictionaryRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Lưu
        /// </summary>
        [HttpPost]
        [HandlerAjaxOnly]
        public IActionResult SubmitForm(SysAuxiliaryDictionaryEntity entity, string keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            if (!string.IsNullOrWhiteSpace(keyValue) && keyValue == entity.ParentId)
            {
                throw new FailedException("Cấp trênTừ điểnChọnLỗi");
            }
            entity.OrganizeId = this.OrganizeId;
            _auxiliaryDictionaryRepo.SubmitForm(entity, keyValue);
            return Success("Thao tác thành công.");
        }

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult DeleteForm(string keyValue)
        {
            _auxiliaryDictionaryRepo.DeleteForm(keyValue);
            return Success("Xóa thành công.");
        }
    }
}