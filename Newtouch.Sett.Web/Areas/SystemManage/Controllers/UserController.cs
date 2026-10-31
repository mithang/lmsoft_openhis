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
    /// hệthốngdùnghộ Mở rộng
    /// </summary>
    [Area("SystemManage")]

    public class UserController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly IOutPatientChargeQueryDmnService _outPatientChargeQueryDmnService;
        private readonly ISysDepartmentRepo _sysDepartmentRepo;
        private readonly ISysUserDmnService _sysUserDmnService;
        /// <summary>
        /// Hàm khởi tạo mặc định
        /// </summary>
        /// <param name="sysOrganizeDmnService"></param>
        /// <param name="sysDepartmentRepo"></param>
        /// <param name="sysUserDmnService"></param>
        /// <param name="userRoleAuthDmnService"></param>
        public UserController(ISysOrganizeDmnService sysOrganizeDmnService
            , ISysDepartmentRepo sysDepartmentRepo
            , ISysUserDmnService sysUserDmnService
            , IUserRoleAuthDmnService userRoleAuthDmnService
            , IOutPatientChargeQueryDmnService outPatientChargeQueryDmnService)
        {
            this._outPatientChargeQueryDmnService = outPatientChargeQueryDmnService;
            this._sysDepartmentRepo = sysDepartmentRepo;
            this._sysUserDmnService = sysUserDmnService;
        }

        /// <summary>
        /// dùnghộchọnchọndụng cụ
        /// </summary>
        /// <returns></returns>
        /// <summary>
        /// dùnghộchọnchọncây(Cấp ba:Cơ sở+Khoa+Người dùng).gốcthựchiệnđiềudùnghaivàochếquỹloạiphươngphương pháp
        /// ComGetSysUserSelectorTree(khôngnguồnmã);nàynơitrảquayrỗngcây JSON,chờ SSO/quỹloạinặngcơ quan.
        /// </summary>
        public IActionResult GetSysUserSelectorTree(string organizeId = null, string keyValue = null, string from = null, bool isExpand = true, bool isContansChildOrg = true
            , bool isShowEmpty = false)
        {
            return Content("[]");
        }

        /// <summary>
        /// đượclấyhệthốngdùnghộcây haicấp(Khoa+Người dùng(Nhân viên))-có thểtratra cứu
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="from"></param>
        /// <param name="isShowEmpty">làkhônghiển thịhiển thịrỗngtrắngtiếtđiểm(khoaphòngtiếtđiểm)</param>
        /// <param name="isExpand"></param>
        /// <returns></returns>

        public virtual IActionResult GetSysUserSelectorTreeQ(string keyword, string keyValue = null, string from = null, bool isShowEmpty = false, bool isExpand = true,string orgId = null)
        {
            return f_GetSysUserSelectorTreeQ(keyword, keyValue, from, isShowEmpty, isExpand, orgId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="from"></param>
        /// <param name="isShowEmpty"></param>
        /// <param name="isExpand"></param>
        /// <param name="funcTreeItemFilter"></param>
        /// <param name="funcCheckedList"></param>
        /// <returns></returns>
        protected internal IActionResult f_GetSysUserSelectorTreeQ(string keyword = null, string keyValue = null, string from = null, bool isShowEmpty = false, bool isExpand = true, string orgId = null
            , Func<List<TreeViewModel>, List<TreeViewModel>> funcTreeItemFilter = null
            , Func<List<RoleUnionUser>> funcCheckedList = null)
        {
            var treeList = new List<TreeViewModel>();

            //khoaphòngtậphợp
            var detpData = _sysDepartmentRepo.GetList(OrganizeId??orgId, "1");
            //ngườiviêntậphợp(cóđốiphảiđăngghitài khoảnhộ)
            var userStaffData = _sysUserDmnService.GetSatffVOListByOrg(OrganizeId??orgId, keyword);

            foreach (SysDepartmentVEntity deptItem in detpData)
            {
                var deptUserStaffData = userStaffData.Where(p => p.DepartmentCode == deptItem.Code).ToList();
                foreach (var userStaffVO in deptUserStaffData)
                {
                    TreeViewModel staffTree = new TreeViewModel();
                    staffTree.id = userStaffVO.StaffId;
                    staffTree.text = userStaffVO.Name;
                    staffTree.value = userStaffVO.UserId;
                    staffTree.parentId = deptItem.Id;
                    staffTree.Code = userStaffVO.UserCode;
                    staffTree.hasChildren = false;
                    staffTree.showcheck = true;
                    staffTree.checkstate = 0;   //Gán giá trị ở ngoài,Thuận tiện cho bộ nhớ đệm

                    treeList.Add(staffTree);
                }
                TreeViewModel deptTree = new TreeViewModel();
                deptTree.id = deptItem.Id;
                deptTree.text = deptItem.Name;
                deptTree.value = deptItem.Code;
                deptTree.parentId = deptItem.ParentId;
                deptTree.isexpand = isExpand;  //Mặc định không mở rộng,Quá nhiều người
                deptTree.complete = true;
                deptTree.showcheck = false;
                //deptTree.hasChildren = deptUserStaffData.Count > 0 || detpData.Any(p => p.ParentId == deptItem.Id);
                deptTree.hasChildren = true;
                if (deptUserStaffData != null && deptUserStaffData.Count > 0 && !string.IsNullOrWhiteSpace(keyword))
                {
                    treeList.Add(deptTree);
                }
                else if (string.IsNullOrWhiteSpace(keyword))
                {
                    treeList.Add(deptTree);
                }

            }
            if (funcTreeItemFilter != null)
            {
                treeList = funcTreeItemFilter(treeList);
            }

            #region Bổ sungNút cha start
            // by haijiang.mo sửalạitìmtìmhaicấpbằngdướicủaconbộngoại trúdùnghộ,vìkhôngphương phápcơ quanlậpracâykếtcơ quan,màtìmtìmmấtthất bại
            var parentDeptList = new HashSet<string>();
            // điều khiểnchếtổngtínhtínhsâuđộ,phòng ngừadừngvìlàsốdữ liệusaisai,tìmkhôngđếnNút chachếttuân theovòng
            var totalReFindParent = 10;
            do
            {
                totalReFindParent--;
                parentDeptList = new HashSet<string>();
                // tìmratreeList trongkhôngcóparentId của 
                foreach (TreeViewModel item in treeList)
                {
                    if (item.parentId == null)
                    {
                        continue;
                    }
                    else
                    {
                        bool exists = treeList.Where(p => p.id == item.parentId).ToList().Count > 0;
                        if (exists)
                        {
                            continue;
                        }
                        else
                        {
                            parentDeptList.Add(item.parentId);
                        }
                    }

                }

                parentDeptList.ToList().ForEach(id => {
                    var deptItem = detpData.Where(p => p.Id == id).First();
                    if (deptItem != null)
                    {
                        TreeViewModel deptTree = new TreeViewModel();
                        deptTree.id = deptItem.Id;
                        deptTree.text = deptItem.Name;
                        deptTree.value = deptItem.Code;
                        deptTree.parentId = deptItem.ParentId;
                        deptTree.isexpand = isExpand;  //Mặc định không mở rộng,Quá nhiều người
                        deptTree.complete = true;
                        deptTree.showcheck = false;
                        deptTree.hasChildren = true;
                        treeList.Add(deptTree);
                    }
                });

            } while (parentDeptList.Count > 0 && totalReFindParent > 0);
            #endregion Bổ sungNút cha end

            //bộ nhớ đệmend
            IList<RoleUnionUser> checkedUserList = null; //Đãcheckedcủadùnghộ
            if (from == "rolerelatioinuser" && !string.IsNullOrWhiteSpace(keyValue))   //Vai tròđãliên quanliêndùnghộ
            {
                checkedUserList = _outPatientChargeQueryDmnService.GetCurUserIdListRoleId(keyValue);
            }

            if (funcCheckedList != null)
            {
                var funccheckedListResult = funcCheckedList();
                if (funccheckedListResult != null && funccheckedListResult.Count > 0)
                {
                    checkedUserList = (checkedUserList ?? new List<RoleUnionUser>())
                        .Union(funccheckedListResult).ToList();
                }
            }

            if (checkedUserList != null && checkedUserList.Count > 0)
            {
                foreach (var treeItem in treeList)
                {
                    //if (treeItem.showcheck && checkedUserList.Any(p => p.First == treeItem.value && p.Second == treeItem.Ex3))
                    if (treeItem.showcheck && checkedUserList.Any(p => p.First == treeItem.value))
                    {
                        treeItem.checkstate = 1;
                        //Đồng thời để cấp trên mở rộng
                        var thisParentNode = treeList.Where(p => p.id == treeItem.parentId).FirstOrDefault();
                        if (thisParentNode != null)
                        {
                            thisParentNode.isexpand = true;  //Cấp dưới có dòng được chọn,Ở đây nhất định mở rộng
                        }
                    }
                }
            }
            return Content(treeList.TreeViewJson(null));
        }
        public override IActionResult Index()
        {
            return View("~/Views/Shared/SysManageNotice.cshtml");
        }

        public override IActionResult Form()
        {
            return View("~/Views/Shared/SysManageNotice.cshtml");
        }

        public IActionResult Details()
        {
            return View("~/Views/Shared/SysManageNotice.cshtml");
        }

        public IActionResult PersonalInfo()
        {
            return View("~/Views/Shared/SysManageNotice.cshtml");
        }

        public IActionResult RevisePassword()
        {
            return View("~/Views/Shared/SysManageNotice.cshtml");
        }

        public IActionResult Roles()
        {
            return View("~/Views/Shared/SysManageNotice.cshtml");
        }

        public IActionResult Selector()
        {
            return View("~/Views/Shared/SysManageNotice.cshtml");
        }
    }
}
