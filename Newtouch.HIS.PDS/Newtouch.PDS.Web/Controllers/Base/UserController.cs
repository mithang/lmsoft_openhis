using NewtouchHIS.Lib.Framework.Filter;
using Newtouch.Core.Common.Utils;
using Newtouch.Core.Common.Interface;
using Newtouch.Common;

using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;


namespace Newtouch.HIS.Web.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;
        private readonly ISysDepartmentRepo _sysDepartmentRepo;
        private readonly ISysUserExDmnService _sysUserDmnService;
        private readonly IUserRoleAuthDmnService _userRoleAuthDmnService;
        private readonly ICache _cache;


        /// <summary>
        /// Bộ chọn người dùng
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Selector()
        {
            return View();
        }

        /// <summary>
        /// Lấy cây người dùng hệ thống Cấp 3（Cơ sở+Khoa+Người dùng（Nhân sự））
        /// </summary>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public IActionResult GetSysUserSelectorTree(string organizeId = null, string keyValue = null, string from = null, bool isShowEmpty = false, bool isExpand = true, int cacheTime = 30)
        {
            if (string.IsNullOrWhiteSpace(organizeId))
            {
                organizeId = base.GetAuthOrganizeId();  //Mặc định QuyềnOrganizeId
            }
            //Cây người dùng hệ thống có đệm
            var treeList = _cache.Get<List<TreeViewModel>>(string.Format(CacheKey.OrganizeUserTreeSetKey, organizeId), () => {
                var organizedata = _sysOrganizeDmnService.GetValidListByParentOrg(organizeId);
                var iitreeList = new List<TreeViewModel>();
                foreach (var orgItem in organizedata)
                {
                    //Tập hợp khoa của cơ sở
                    var orgDetpData = _sysDepartmentRepo.GetList(orgItem.Id);
                    //Tập hợp nhân sự của cơ sở（Có tài khoản đăng nhập tương ứng）
                    var orgUserStaffData = _sysUserDmnService.GetSatffVOListByOrg(orgItem.Id);
                    #region Tất cả khoa thuộc cơ sở
                    foreach (var deptItem in orgDetpData)
                    {
                        var deptUserStaffData = orgUserStaffData.Where(p => p.DepartmentCode == deptItem.Code).ToList();
                        #region Tất cả người dùng thuộc cơ sở
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
                            staffTree.checkstate = 0;   //Gán từ bên ngoài，Thuận tiện lưu đệm
                            iitreeList.Add(staffTree);
                        }
                        #endregion
                        TreeViewModel deptTree = new TreeViewModel();
                        deptTree.id = deptItem.Id;
                        deptTree.text = deptItem.Name;
                        deptTree.value = deptItem.Code;
                        deptTree.parentId = deptItem.ParentId == null ? orgItem.Id : deptItem.ParentId;
                        deptTree.isexpand = isExpand;  //Mặc định không mở rộng，Quá nhiều người
                        deptTree.complete = true;
                        deptTree.showcheck = false;
                        deptTree.hasChildren = deptUserStaffData.Count > 0 || orgDetpData.Any(p => p.ParentId == deptItem.Id);
                        iitreeList.Add(deptTree);
                    }
                    #endregion
                    TreeViewModel tree = new TreeViewModel();
                    tree.id = orgItem.Id;
                    tree.text = orgItem.Name;
                    tree.value = orgItem.Code;
                    tree.parentId = orgItem.Id == organizeId ? null : orgItem.ParentId; //Đặc biệt
                    tree.isexpand = true;
                    tree.complete = true;
                    tree.showcheck = false;
                    tree.hasChildren = orgDetpData.Count > 0 || orgUserStaffData.Count > 0 || organizedata.Any(p => p.ParentId == orgItem.Id);
                    iitreeList.Add(tree);
                }
                return iitreeList;
            }, cacheTime);

            IList<string> checkedUserIdList = null; //đãcheckedngười dùng của
            if (from == "rolerelatioinuser" && !string.IsNullOrWhiteSpace(keyValue))   //Người dùng vai trò đã liên kết
            {
                checkedUserIdList = _userRoleAuthDmnService.GetCurUserIdListByRoleId(keyValue);
            }

            if (checkedUserIdList != null && checkedUserIdList.Count > 0)
            {
                foreach (var treeItem in treeList)
                {
                    if (treeItem.showcheck && checkedUserIdList.Contains(treeItem.value))
                    {
                        treeItem.checkstate = 1;
                        //Đồng thời mở rộng cấp trên
                        var thisParentNode = treeList.Where(p => p.id == treeItem.parentId).FirstOrDefault();
                        if (thisParentNode != null)
                        {
                            thisParentNode.isexpand = true;  //Cấp dưới có được chọn，Ở đây nhất định mở rộng nó
                        }
                    }
                }
            }
            if (!isShowEmpty)   //Không hiển thị thừa
            {
                var count = 1;  //Số dòng đã loại bỏ
                while (count > 0)
                {
                    count = treeList.RemoveAll(p => !p.showcheck && !p.hasChildren);
                    //Điều chỉnh lại cây Loại bỏ Không có cấp dưới（Cấp dưới vừa bị chuyển） lại Không hiển thịshowcheckcủa
                    treeList.RemoveAll(p => !p.showcheck && !treeList.Any(sub => sub.parentId == p.id));
                }
            }
            return Content(treeList.TreeViewJson(null));
        }

        /// <summary>
        /// Thông tin cá nhân
        /// </summary>
        /// <returns></returns>
        public IActionResult PersonalInfo()
        {
            return View();
        }

        /// <summary>
        /// Gửi Đặt lại mật khẩu cá nhân
        /// </summary>
        /// <param name="newpwd"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public IActionResult SubmitPersonalPassword(string newpwd)
        {
            _sysUserDmnService.RevisePassword(newpwd, this.UserIdentity.UserId);
            return Success("Đặt lại mật khẩu thành công。");
        }

    }
}
