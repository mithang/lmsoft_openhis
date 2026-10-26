using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FrameworkBase.MultiOrg.Web;
using Newtouch.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;

namespace Newtouch.CIS.Web.Areas.SystemManage.Controllers
{
    public class AuxiliaryDictionaryController : OrgControllerBase
    {
        private readonly IAuxiliaryDictionaryRepo _auxiliaryDictionaryRepo;
        // GET: AuxiliaryDictionary

        //Tổ chức (bệnh viện) Từ điển Thả xuống Nguồn dữ liệu
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeSelectJson(string treeidFieldName = "Code")
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
        /// Lấy danh sách từ điển
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeGridJson(string keyword)
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
        /// Khi sửa thông tin, mang thông tin sang trang mới
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = _auxiliaryDictionaryRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Lưu
        /// </summary>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SubmitForm(SysAuxiliaryDictionaryEntity entity, string keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            if (!string.IsNullOrWhiteSpace(keyValue) && keyValue == entity.ParentId)
            {
                throw new FailedException("上级词典选择错误");
            }
            entity.OrganizeId = this.OrganizeId;
            _auxiliaryDictionaryRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteForm(string keyValue)
        {
            _auxiliaryDictionaryRepo.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }
}