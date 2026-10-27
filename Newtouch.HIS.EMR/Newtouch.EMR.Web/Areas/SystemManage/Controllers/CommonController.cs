
using NewtouchHIS.Framework.Web.Controllers;

using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IDomainServices;
using Newtouch.EMR.Domain.IRepository;
using Newtouch.EMR.Infrastructure.EnumMR;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Attributes;

namespace Newtouch.EMR.Web.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    public class CommonController : OrgControllerBase
    {
        private readonly ICommonDmnService _CommonDmnService;
        private readonly ISysDiagnosisRepo _sysDiagnosisRepo;
        private readonly ISysDepartmentRepo _SysDepartmentRepo;
        private readonly Ibl_bllxRepo _BllxRepo;
        private readonly IBlmblbDmnService _BlmblbDmnService;
        private readonly IBlysRepo _blysRepo;

        public CommonController(ICommonDmnService commonDmnService, ISysDiagnosisRepo sysDiagnosisRepo, ISysDepartmentRepo sysDepartmentRepo, Ibl_bllxRepo bllxRepo, IBlmblbDmnService blmblbDmnService, IBlysRepo blysRepo)
        {
            this._CommonDmnService = commonDmnService;
            this._sysDiagnosisRepo = sysDiagnosisRepo;
            this._SysDepartmentRepo = sysDepartmentRepo;
            this._BllxRepo = bllxRepo;
            this._BlmblbDmnService = blmblbDmnService;
            this._blysRepo = blysRepo;
        }
        private static string LinkToOR= EmrConfig.GetAppConfigValue("EnableLinkToOR");
        private static string LinkToMRMS = EmrConfig.GetAppConfigValue("EnableLinkToMRMS");
        // GET: SystemManage/Common
        /// <summary>
        /// 病案大类维护页面
        /// </summary>
        /// <returns></returns>
        public IActionResult RecordType()
        {
            return View();
        }
        public IActionResult RecordTypeForm()
        {
            return View();
        }
        public IActionResult RecordTypeChildForm()
        {
            return View();
        }
        public IActionResult GetBllxListGrid(string bllx,string keyword)
        {
            var data = _CommonDmnService.GetBllxListDetail(OrganizeId,bllx,keyword);
            return Content(data.ToJson());
        }

        public IActionResult GetBllxListTreeGrid(Pagination pagination,string keyword)
        {
            var data = _CommonDmnService.GetBllxListDetail(OrganizeId);
            var treeList = new List<TreeGridModel>();
            foreach (var item in data)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = data.Count(t => t.ParentId == item.Id) == 0 ? false : true;
                treeModel.id = item.Id.ToString();
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.ParentId == null ? null : item.ParentId.ToString();
                treeModel.expanded = hasChildren;
                treeModel.entityJson = item.ToJson();

                treeList.Add(treeModel);

            }
            return Content(treeList.TreeGridJson(null));
        }

        public IActionResult GetBllxbyId(string keyValue)
        {
            var data = _BllxRepo.FindEntity(p => p.OrganizeId == OrganizeId && (p.Id == keyValue || p.bllx==keyValue) && p.zt=="1");
            return Content(data.ToJson());
        }

        public IActionResult SubmitForm(bl_bllxEntity ety,string keyValue)
        {
            ety.OrganizeId = OrganizeId;
            _BllxRepo.SubmitForm(ety, keyValue);
            return Success("Lưu thành công");
        }
        public IActionResult DeleteForm(string keyValue)
        {
            _BllxRepo.DeleteForm( keyValue,OrganizeId,this.UserIdentity.rygh);
            return Success("Xóa thành công");
        }

        public IActionResult GetBllxTreeList(string Id,string selector,string mzbz=null)
        {
            var treeList = new List<TreeViewModel>();
            if (!string.IsNullOrWhiteSpace(Id))
            {
                return Content(treeList.TreeViewJson(null));
            }
            var root = _CommonDmnService.GetBllxList(OrganizeId, "",mzbz);
            if (root != null && root.Count > 0)
            {
                foreach (var r in root)
                {
                    TreeViewModel rootTree = new TreeViewModel();
                    rootTree.id = r.Code;
                    rootTree.text = r.Name;
                    rootTree.value = r.Id;
                    rootTree.parentId = null;
                    rootTree.Code = r.Code;
                    rootTree.hasChildren = true;
                    rootTree.isexpand = false; //keyValue==r.Id ? true:false;
                    if (!string.IsNullOrWhiteSpace(selector) && r.Code == selector)
                    {
                        rootTree.Ex1 = "1";
                    }

                    var leaf = _CommonDmnService.GetBllxListDetail(OrganizeId, r.Id, null, "2");
                    if (leaf != null && leaf.Count > 0)
                    {
                        rootTree.isexpand = true;
                        foreach (var l in leaf)
                        {
                            TreeViewModel leafTree = new TreeViewModel();
                            leafTree.id = l.bllx;
                            leafTree.text = l.bllxmc;
                            leafTree.value = l.Id;
                            leafTree.parentId = r.Code;
                            leafTree.Code = l.bllxcode;
                            leafTree.hasChildren = false;
                            leafTree.isexpand = false;
                            if (!string.IsNullOrWhiteSpace(selector) && l.bllx==selector)
                            {
                                rootTree.Ex1 = "1";
                            }
                            treeList.Add(leafTree);
                        }
                    }

                    treeList.Add(rootTree);
                }

            }

            return Content(treeList.TreeViewJson(null)); 
        }
        public IActionResult GetBlmbTreeList(string keyValue,string mbqx)
        {
            var treeList = new List<TreeGridModel>();
            var mobanlist = _BlmblbDmnService.MedRecordTmpListTree(OrganizeId, keyValue, mbqx);
            if (mobanlist != null && mobanlist.Count > 0)
            {
                foreach (var m in mobanlist)
                {
                    TreeGridModel mobanTree = new TreeGridModel();
                    bool hasChildren = mobanlist.Count(t => t.parentId == m.Id) == 0 ? false : true;
                    mobanTree.id = m.Id;
                    mobanTree.isLeaf = hasChildren; 
                    mobanTree.parentId = m.parentId == null ? null : m.parentId.ToString();
                    mobanTree.expanded = hasChildren;
                    mobanTree.entityJson = m.ToJson();
                    treeList.Add(mobanTree);
                }
            }
            return Content(treeList.TreeGridJson(null));
        }
        public IActionResult GetYsTreeList(string keyword)
        {
            var treeList = new List<TreeGridModel>();
            var dataList = _blysRepo.GetYsTreeV2(OrganizeId, keyword); ;
            if (dataList != null && dataList.Count > 0)
            {
                foreach (var m in dataList)
                {
                    TreeGridModel mobanTree = new TreeGridModel();
                    //mobanTree.parentId = m.parentId;
                    if (m.yssjid == "-1")
                    {
                        mobanTree.parentId = null;
                        mobanTree.isLeaf = true;
                        if (keyword != null && keyword != "")
                        {
                            mobanTree.expanded = true;
                        }
                        else
                        {
                            mobanTree.expanded = false;
                        }

                    }
                    else
                    {
                        bool hasChildren = dataList.Count(t => t.yssjid == m.Id) == 0 ? false : true;
                        mobanTree.parentId = m.yssjid;
                        mobanTree.isLeaf = hasChildren;
                        if (keyword != null && keyword != "")
                        {
                            mobanTree.expanded = true;
                        }
                        else
                        {
                            mobanTree.expanded = false;
                        }
                    }
                    mobanTree.id = m.Id;
                    mobanTree.entityJson = m.ToJson();
                    treeList.Add(mobanTree);
                }
            }
            return Content(treeList.TreeGridJson(null));
        }
        public IActionResult GetYsMXList(string YsId)
        {
            var dataList = _blysRepo.GetYsMX(OrganizeId, YsId); 
            return Content(dataList.ToJson());
        }
        #region tools
        public IActionResult GetStaffSign()
        {
            var ety = _CommonDmnService.StaffInfo(this.UserIdentity.rygh, OrganizeId);
            if (ety != null && ety.Count > 0)
            {
                return Content(ety.FirstOrDefault().ToJson());
            }
            return Content(ety.ToJson());
        }

        public IActionResult GetBllxList(string code,string mzbz=null)
        {
            var ety = _CommonDmnService.GetBllxList(OrganizeId,code,mzbz) ;
            return Content(ety.ToJson());
        }

        /// <summary>
        /// 获取系统诊断列表
        /// </summary>
        /// <returns></returns>
        public IActionResult GetDiagList()
        {
            var ety = _CommonDmnService.ZdList(OrganizeId, "");
            return Content(ety.ToJson());
        }

        /// <summary>
        /// 诊断 检索
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public JsonResult GetDiagnosisList(string keyword, string zdlx, string zdtype,string ybnhlx)
        {
            if (string.IsNullOrEmpty(ybnhlx))
            {
                ybnhlx = null;
            }
            if (zdtype == ((int)EnumZyZdlxbs.cy).ToString()&&zdlx=="TCM")
            {
                //var zzlist = _sysTcmsyndromeRepo.GetList(this.OrganizeId, keyword);
                var zzlist = _CommonDmnService.GetZyzhList(this.OrganizeId,keyword);
                return Json(zzlist);
            }
            else {
                var list = _sysDiagnosisRepo.GetList(this.OrganizeId, keyword, zdlx, ybnhlx);
                return Json(list);
            }
        }

        public IActionResult GetDiagnosisGrid(Pagination pagination, string keyword, int? zdlx)
        {
            var data = new
            {
                rows = _CommonDmnService.GetList(pagination, this.OrganizeId, keyword, zdlx),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 手术 检索
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public JsonResult GetOperationList(string keyword,bool type)
        {
            var list = _CommonDmnService.OpList(this.OrganizeId, keyword,type);
            return Json(list);
        }

        public JsonResult GetOperationListPage(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = _CommonDmnService.OpListPage(pagination, this.OrganizeId, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            //var list = _CommonDmnService.OpListPage(pagination,this.OrganizeId, keyword);
            return Json(data);
        }
        /// <summary>
        /// 麻醉方式
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAnesList(string keyword)
        {
            if (LinkToOR == "true")
            {
                var ety = _CommonDmnService.AnesList(OrganizeId, keyword);
                return Content(ety.ToJson());
            }
            else {
                return Content("");
            }            
        }

        public IActionResult GetNotchGradeList(string keyword)
        {
            if (LinkToOR == "true")
            {
                var ety = _CommonDmnService.NotchGradeList(OrganizeId, keyword);
                return Content(ety.ToJson());
            }
            else
            {
                return Content("");
            }            
        }

        public IActionResult GetCommonList(string keyword, string type)
        {
            var ety = _CommonDmnService.DicCommonList(OrganizeId, keyword, type);
            return Content(ety.ToJson());
        }

        public IActionResult GetDepartment(string keyword)
        {
            var ety = _SysDepartmentRepo.GetList(this.OrganizeId, "1", keyword);
            return Content(ety.ToJson());
        }

        //查询病区
        public IActionResult GetInpatientArea(string keyword)
        {
            var ety = _CommonDmnService.GetInpatientArea(OrganizeId, keyword);

            return Content(ety.ToJson());
        }

        //public IActionResult GetXtbrxz(string keyword)
        //{
        //    var ety = _CommonDmnService.BrxzList(this.OrganizeId, keyword);
        //    return Content(ety.ToJson());
        //}
        /// <summary>
        /// 国籍
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetNationality(string keyword)
        {
            var ety = _CommonDmnService.DicNationalityList(this.OrganizeId, keyword);
            return Content(ety.ToJson());
        }
        /// <summary>
        /// 民族
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetNationas(string keyword)
        {
            var ety = _CommonDmnService.DicNationsList(this.OrganizeId, keyword);
            return Content(ety.ToJson());
        }

        /// <summary>
        /// 岗位人员列表
        /// </summary>
        /// <param name="dutyCode"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetStaffListByDutyCode(string dutyCode, string keyword)
        {
            var list = _CommonDmnService.GetStaffByDutyCode(this.OrganizeId, dutyCode, keyword);
            return Content(list.ToJson());
        }

        /// <summary>
        /// 岗位列表
        /// </summary>
        /// <param name="dutyCode"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetDutyList(string dutyCode,string bllxId)
        {
            var list = _CommonDmnService.GetDutyListRelbllx(this.OrganizeId, bllxId, dutyCode);
            return Content(list.ToJson());
        }
        #endregion

    }
}