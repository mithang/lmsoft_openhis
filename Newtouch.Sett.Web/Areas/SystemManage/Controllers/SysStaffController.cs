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
    // NX6-002: port that — controller goc vendor (ban goc: PDS StaffController +
    // HOSP StaffController, cung ho Newtouch; action + view Selector cua Sett
    // duoc khoi phuc tu lich su git 16d37a4f^). Truoc day khong co controller nen
    // /SystemManage/SysStaff/Selector 404, 13 caller JS (chon BS tri lieu /
    // RehabDoctor...) hong live. Du lieu cay dung dich vu that cua Sett
    // (NX6-002: GetValidListByParentOrg + GetList khoa + GetSatffVOListByOrg).
    // Khong cache (theo ban HOSP; PDS cache 30 phut — bo qua de tranh cay cu).
    // dutyCode: view truyen len nhung ban goc PDS/HOSP KHONG loc theo duty
    // (action khong nhan tham so nay) — giu dung hanh vi vendor, ghi nhan de
    // quyet dinh san pham rieng (khong tu y loc).
    [Area("SystemManage")]

    public class SysStaffController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;
        private readonly ISysDepartmentRepo _sysDepartmentRepo;
        private readonly ISysUserDmnService _sysUserDmnService;

        public SysStaffController(ISysOrganizeDmnService sysOrganizeDmnService, ISysDepartmentRepo sysDepartmentRepo, ISysUserDmnService sysUserDmnService)
        {
            this._sysOrganizeDmnService = sysOrganizeDmnService;
            this._sysDepartmentRepo = sysDepartmentRepo;
            this._sysUserDmnService = sysUserDmnService;
        }

        /// <summary>
        /// Chon nhan vien (dialog cay 3 cap: Co so + Khoa + Nhan vien)
        /// </summary>
        /// <returns></returns>
        public IActionResult Selector()
        {
            return View();
        }

        /// <summary>
        /// Lay cay nhan su co so Cap 3 (Co so + Khoa + Nhan su)
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult GetStaffSelecotrTree(string organizeId = null, string keyValue = null, string from = null, bool isShowEmpty = false, bool isExpand = true, string initIdSelected = null)
        {
            if (string.IsNullOrWhiteSpace(organizeId))
            {
                organizeId = base.GetAuthOrganizeId();  //Mac dinh QuyenOrganizeId
            }
            var organizedata = _sysOrganizeDmnService.GetValidListByParentOrg(organizeId);
            var treeList = new List<TreeViewModel>();
            foreach (var orgItem in organizedata)
            {
                //Tap hop khoa cua co so
                var orgDetpData = _sysDepartmentRepo.GetList(orgItem.Id);
                //Tap hop nhan su cua co so
                var orgStaffData = _sysUserDmnService.GetSatffVOListByOrg(orgItem.Id, null);
                #region Tat ca khoa thuoc co so
                foreach (var deptItem in orgDetpData)
                {
                    var deptStaffData = orgStaffData.Where(p => p.DepartmentCode == deptItem.Code).ToList();
                    #region Tat ca nguoi dung thuoc co so
                    foreach (var staffItem in deptStaffData)
                    {
                        TreeViewModel staffTree = new TreeViewModel();
                        staffTree.id = staffItem.StaffId;
                        staffTree.text = staffItem.Name;
                        staffTree.value = staffItem.gh;
                        staffTree.parentId = deptItem.Id;
                        staffTree.hasChildren = false;
                        staffTree.showcheck = true;
                        staffTree.checkstate = 0;   //Gan tu ben ngoai, Thuan tien luu dem
                        treeList.Add(staffTree);
                    }
                    #endregion
                    TreeViewModel deptTree = new TreeViewModel();
                    deptTree.id = deptItem.Id;
                    deptTree.text = deptItem.Name;
                    deptTree.value = deptItem.Code;
                    deptTree.parentId = deptItem.ParentId == null ? orgItem.Id : deptItem.ParentId;
                    deptTree.isexpand = isExpand;  //Mac dinh khong mo rong, Qua nhieu nguoi
                    deptTree.complete = true;
                    deptTree.showcheck = false;
                    deptTree.hasChildren = deptStaffData.Count > 0 || orgDetpData.Any(p => p.ParentId == deptItem.Id);
                    treeList.Add(deptTree);
                }
                #endregion
                TreeViewModel tree = new TreeViewModel();
                tree.id = orgItem.Id;
                tree.text = orgItem.Name;
                tree.value = orgItem.Code;
                tree.parentId = orgItem.Id == organizeId ? null : orgItem.ParentId; //Dac biet
                tree.isexpand = isExpand;
                tree.complete = true;
                tree.showcheck = false;
                tree.hasChildren = orgDetpData.Count > 0 || orgStaffData.Count > 0 || organizedata.Any(p => p.ParentId == orgItem.Id);
                treeList.Add(tree);
            }
            IList<string> checkedStaffIdList = new List<string>();
            var initIdSelectedArr = (initIdSelected ?? "").Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            checkedStaffIdList = checkedStaffIdList.Union(initIdSelectedArr).Distinct().ToList();

            if (checkedStaffIdList != null && checkedStaffIdList.Count > 0)
            {
                foreach (var treeItem in treeList)
                {
                    if (treeItem.showcheck && checkedStaffIdList.Contains(treeItem.id))
                    {
                        treeItem.checkstate = 1;
                        //Dong thoi mo rong cap tren
                        var thisParentNode = treeList.Where(p => p.id == treeItem.parentId).FirstOrDefault();
                        if (thisParentNode != null)
                        {
                            thisParentNode.isexpand = true;  //Cap duoi co duoc chon, O day nhat dinh mo rong no
                        }
                    }
                }
            }
            if (!isShowEmpty)   //Khong hien thi thua
            {
                var count = 1;  //So dong da loai bo
                while (count > 0)
                {
                    count = treeList.RemoveAll(p => !p.showcheck && !p.hasChildren);
                    //Dieu chinh lai cay Loai bo Khong co cap duoi (Cap duoi vua bi chuyen) lai Khong hien thishowcheckcua
                    treeList.RemoveAll(p => !p.showcheck && !treeList.Any(sub => sub.parentId == p.id));
                }
            }
            return Content(treeList.TreeViewJson(null));
        }

    }
}
