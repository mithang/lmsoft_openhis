using System.Collections.Generic;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using Microsoft.AspNetCore.Mvc;
using Newtouch.HIS.Domain.Entity;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using System.Linq;
using Newtouch.Infrastructure;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Domain.IDomainServices;

namespace Newtouch.HIS.Base.HOSP.Web.Controllers
{
    public class DepartmentController : ControllerBase
    {
        private readonly ISysDepartmentRepository _sysDepartmentRepository;
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;
        private readonly ISysWardRepo _sysWardRepo;
        private readonly ISysDepartmentWardRelationRepo _sysDepartmentWardRelationRepo;

        public DepartmentController(ISysDepartmentRepository sysDepartmentRepository
            , ISysOrganizeDmnService sysOrganizeDmnService, ISysWardRepo sysWardRepo
            , ISysDepartmentWardRelationRepo sysDepartmentWardRelationRepo)
        {
            this._sysDepartmentRepository = sysDepartmentRepository;
            this._sysOrganizeDmnService = sysOrganizeDmnService;
            this._sysWardRepo = sysWardRepo;
            this._sysDepartmentWardRelationRepo = sysDepartmentWardRelationRepo;
        }

        //组织机构（医院） 科室 下拉 数据源
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetTreeSelectJson(string organizeId, string treeidFieldName = "Code")
        {
            var data = _sysDepartmentRepository.GetValidListByOrg(organizeId);
            var treeList = new List<TreeSelectModel>();
            foreach (SysDepartmentEntity item in data)
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

        [HttpGet]
        [HandlerAjaxOnly]
        /// <summary>
        /// 获取科室列表
        /// </summary>
        /// <returns></returns>
        public IActionResult GetTreeGridJson(string organizeId, string keyword)
        {
            var data = _sysOrganizeDmnService.GetListByOrg(organizeId).ToList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.Name.Contains(keyword) || t.Code.Contains(keyword), parentId: "ParentId");
            }
            var treeList = new List<TreeGridModel>();
            foreach (SysDepartmentEntity item in data)
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

        /// <summary>
        /// 修改信息时，把信息带到新页面
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetFormJson(string keyValue)
        {
            var entity = _sysDepartmentRepository.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public IActionResult SubmitForm(SysDepartmentEntity entity, string keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            entity.zxks = entity.zxks == "true" ? "1" : "0";
            entity.zlks = entity.zlks == "true" ? "1" : "0";
            if (string.IsNullOrWhiteSpace(entity.OrganizeId))
            {
                throw new FailedException("Vui lòng chọn tổ chức");
            }
            if (!string.IsNullOrWhiteSpace(keyValue) && keyValue == entity.ParentId)
            {
                throw new FailedException("Chọn khoa cấp trên sai");
            }
            entity.TopOrganizeId = Constants.TopOrganizeId;
            _sysDepartmentRepository.SubmitForm(entity, keyValue);
            return Success("Thao tác thành công.");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        public IActionResult UpdateYbUpload(int uploadYB, string id)
        {
            _sysDepartmentRepository.UpdateYbUpload(uploadYB, id);
            return Success("Thao tác thành công.");
        }

        #region 关联病区

        public virtual IActionResult WardInfo()
        {
            return View();
        }

        public IActionResult GetWardInfo(string departmentId)
        {
            var treeList = new List<TreeViewModel>();
            var deptInfo = _sysDepartmentRepository.FindEntity(departmentId);
            if (deptInfo == null || deptInfo.OrganizeId == "")
            {
                return Content(treeList.TreeViewJson(null));
            }

            string OrganizeId = deptInfo.OrganizeId;
            //系统有效病区列表
            var wardList = _sysWardRepo.SelectWardList(OrganizeId);
            //病区绑定信息
            var wards = new List<SysDepartmentWardRelationEntity>();

            if (!string.IsNullOrWhiteSpace(departmentId))
            {
                wards = _sysDepartmentWardRelationRepo.GetDeptWardList(departmentId).ToList();
            }
            foreach (var item in wardList)
            {
                TreeViewModel tree = new TreeViewModel();
                int i = wards.Count(t => t.bqCode == item.bqCode);

                tree.id = item.bqCode;
                tree.text = item.bqmc;
                tree.value = item.bqCode;
                tree.parentId = null;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = i;
                tree.hasChildren = false;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson(null));
        }

        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public IActionResult UpdateDeptWard(string keyValue, string wardList)
        {
            var deptInfo = _sysDepartmentRepository.FindEntity(keyValue);
            if (deptInfo != null || deptInfo.OrganizeId != "")
            {
                string OrganizeId = deptInfo.OrganizeId;
                _sysOrganizeDmnService.UpdateDepartmentWard(keyValue, (wardList ?? "").Split(','), OrganizeId);
                return Success("Thao tác thành công.");
            }
            else
            {
                return Error("Thao tác thất bại,Thông tin khoa không đúng");
            }

        }

        #endregion

    }
}