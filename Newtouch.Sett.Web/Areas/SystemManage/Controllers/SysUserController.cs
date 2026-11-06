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
    // NX6-002: port that — controller goc vendor (ban goc: PDS UserController,
    // cung ho Newtouch; action + view Selector cua Sett duoc khoi phuc tu lich
    // su git 16d37a4f^). Truoc day khong co controller nen
    // /SystemManage/SysUser/Selector 404, 3 caller JS (chon nguoi dung cho
    // hoa don / vai tro) hong live. LUU Y: view goc cua Sett goi du lieu qua
    // /SystemManage/User/GetSysUserSelectorTreeQ (endpoint song, dung chung
    // dich vu that NX6-002), khong qua GetSysUserSelectorTree; action nay duoc
    // port day du theo ban PDS de route vendor song (phuc vu caller go truc
    // tiep + view goc EMR/CIS). Khong [HandlerAjaxOnly] (theo endpoint Q cua
    // Sett; de probe/kieu goi thuong deu chay). Khong cache (theo ban HOSP).
    // Nhan rolerelatioinuser: dung IOutPatientChargeQueryDmnService
    // .GetCurUserIdListRoleId nhu f_GetSysUserSelectorTreeQ cua UserController
    // (dich vu nay hien van la stub tra [] — gioi han co san, ghi nhan rieng).
    [Area("SystemManage")]

    public class SysUserController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;
        private readonly ISysDepartmentRepo _sysDepartmentRepo;
        private readonly ISysUserDmnService _sysUserDmnService;
        private readonly IOutPatientChargeQueryDmnService _outPatientChargeQueryDmnService;

        public SysUserController(ISysOrganizeDmnService sysOrganizeDmnService, ISysDepartmentRepo sysDepartmentRepo, ISysUserDmnService sysUserDmnService
            , IOutPatientChargeQueryDmnService outPatientChargeQueryDmnService)
        {
            this._sysOrganizeDmnService = sysOrganizeDmnService;
            this._sysDepartmentRepo = sysDepartmentRepo;
            this._sysUserDmnService = sysUserDmnService;
            this._outPatientChargeQueryDmnService = outPatientChargeQueryDmnService;
        }

        /// <summary>
        /// Bo chon nguoi dung
        /// </summary>
        /// <returns></returns>
        public IActionResult Selector()
        {
            return View();
        }

        /// <summary>
        /// Lay cay nguoi dung he thong Cap 3 (Co so + Khoa + Nguoi dung (Nhan su))
        /// </summary>
        /// <returns></returns>
        public IActionResult GetSysUserSelectorTree(string organizeId = null, string keyValue = null, string from = null, bool isShowEmpty = false, bool isExpand = true, bool isContansChildOrg = true)
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
                //Tap hop nhan su cua co so (Co tai khoan dang nhap tuong ung)
                var orgUserStaffData = _sysUserDmnService.GetSatffVOListByOrg(orgItem.Id, null);
                #region Tat ca khoa thuoc co so
                foreach (var deptItem in orgDetpData)
                {
                    var deptUserStaffData = orgUserStaffData.Where(p => p.DepartmentCode == deptItem.Code).ToList();
                    #region Tat ca nguoi dung thuoc co so
                    foreach (var userStaffVO in deptUserStaffData)
                    {
                        TreeViewModel staffTree = new TreeViewModel();
                        staffTree.id = userStaffVO.StaffId;
                        staffTree.text = userStaffVO.Name;
                        staffTree.value = userStaffVO.UserId;
                        staffTree.Code = userStaffVO.UserCode;
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
                    deptTree.hasChildren = deptUserStaffData.Count > 0 || orgDetpData.Any(p => p.ParentId == deptItem.Id);
                    treeList.Add(deptTree);
                }
                #endregion
                TreeViewModel tree = new TreeViewModel();
                tree.id = orgItem.Id;
                tree.text = orgItem.Name;
                tree.value = orgItem.Code;
                tree.parentId = orgItem.Id == organizeId ? null : orgItem.ParentId; //Dac biet
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = false;
                tree.hasChildren = orgDetpData.Count > 0 || orgUserStaffData.Count > 0 || organizedata.Any(p => p.ParentId == orgItem.Id);
                treeList.Add(tree);
            }

            IList<RoleUnionUser> checkedUserList = null; //da checked cua dung ho
            if (from == "rolerelatioinuser" && !string.IsNullOrWhiteSpace(keyValue))   //Nguoi dung vai tro da lien ket
            {
                checkedUserList = _outPatientChargeQueryDmnService.GetCurUserIdListRoleId(keyValue);
            }

            if (checkedUserList != null && checkedUserList.Count > 0)
            {
                foreach (var treeItem in treeList)
                {
                    if (treeItem.showcheck && checkedUserList.Any(p => p.First == treeItem.value))
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
