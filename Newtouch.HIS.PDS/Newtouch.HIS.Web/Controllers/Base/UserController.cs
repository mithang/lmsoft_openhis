using Newtouch.Common;
using Newtouch.Core.Common.Interface;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Domain.IRepository;

namespace Newtouch.HIS.Web.Controllers
{
    public class UserController : FrameworkBase.MultiOrg.Web.Areas.SystemManage.Controllers.SysUserController
    {
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;
        private readonly ISysDepartmentRepo _sysDepartmentRepo;
        private readonly ISysUserExDmnService _sysUserDmnService;
        private readonly IUserRoleAuthDmnService _userRoleAuthDmnService;
        private readonly ICache _cache;


        /// <summary>
        /// 用户选择器
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Selector()
        {
            return View();
        }

        /// <summary>
        /// lấy hệ thống người dùng cây cấp 3 (cơ quan+khoa+người dùng (nhân viên))
        /// </summary>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult GetSysUserSelectorTree(string organizeId = null, string keyValue = null, string from = null, bool isShowEmpty = false, bool isExpand = true, int cacheTime = 30)
        {
            if (string.IsNullOrWhiteSpace(organizeId))
            {
                organizeId = base.GetAuthOrganizeId();  //mặc định quyền OrganizeId
            }
            //带缓存的系统用户树
            var treeList = _cache.Get<List<TreeViewModel>>(string.Format(CacheKey.OrganizeUserTreeSetKey, organizeId), () => {
                var organizedata = _sysOrganizeDmnService.GetValidListByParentOrg(organizeId);
                var iitreeList = new List<TreeViewModel>();
                foreach (var orgItem in organizedata)
                {
                    //cơ quan khoa tập hợp
                    var orgDetpData = _sysDepartmentRepo.GetList(orgItem.Id);
                    //cơ quan nhân viên tập hợp (có tương ứng đăng nhập tài khoản)
                    var orgUserStaffData = _sysUserDmnService.GetSatffVOListByOrg(orgItem.Id);
                    #region 机构下所有科室
                    foreach (var deptItem in orgDetpData)
                    {
                        var deptUserStaffData = orgUserStaffData.Where(p => p.DepartmentCode == deptItem.Code).ToList();
                        #region 机构下所有用户
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
                            staffTree.checkstate = 0;   //gán giá trị từ bên ngoài, thuận tiện cho bộ nhớ đệm
                            iitreeList.Add(staffTree);
                        }
                        #endregion
                        TreeViewModel deptTree = new TreeViewModel();
                        deptTree.id = deptItem.Id;
                        deptTree.text = deptItem.Name;
                        deptTree.value = deptItem.Code;
                        deptTree.parentId = deptItem.ParentId == null ? orgItem.Id : deptItem.ParentId;
                        deptTree.isexpand = isExpand;  //mặc định không mở rộng, quá nhiều người
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
                    tree.parentId = orgItem.Id == organizeId ? null : orgItem.ParentId; //đặc biệt
                    tree.isexpand = true;
                    tree.complete = true;
                    tree.showcheck = false;
                    tree.hasChildren = orgDetpData.Count > 0 || orgUserStaffData.Count > 0 || organizedata.Any(p => p.ParentId == orgItem.Id);
                    iitreeList.Add(tree);
                }
                return iitreeList;
            }, cacheTime);

            IList<string> checkedUserIdList = null; //đã checked của người dùng
            if (from == "rolerelatioinuser" && !string.IsNullOrWhiteSpace(keyValue))   //vai trò đã liên kết người dùng
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
                        //đồng thời mở rộng cấp trên
                        var thisParentNode = treeList.Where(p => p.id == treeItem.parentId).FirstOrDefault();
                        if (thisParentNode != null)
                        {
                            thisParentNode.isexpand = true;  //cấp dưới có mục được chọn, ở đây nhất định mở rộng
                        }
                    }
                }
            }
            if (!isShowEmpty)   //không hiển thị phần thừa
            {
                var count = 1;  //loại bỏ của dòng số
                while (count > 0)
                {
                    count = treeList.RemoveAll(p => !p.showcheck && !p.hasChildren);
                    //lại điều chỉnh dưới cây loại bỏ không có cấp dưới (cấp dưới vừa bị di chuyển) lại không hiển thị showcheck của
                    treeList.RemoveAll(p => !p.showcheck && !treeList.Any(sub => sub.parentId == p.id));
                }
            }
            return Content(treeList.TreeViewJson(null));
        }

        /// <summary>
        /// cái người thông tin
        /// </summary>
        /// <returns></returns>
        public ActionResult PersonalInfo()
        {
            return View();
        }

        /// <summary>
        /// gửi đặt lại cái người mật khẩu
        /// </summary>
        /// <param name="newpwd"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SubmitPersonalPassword(string newpwd)
        {
            _sysUserDmnService.RevisePassword(newpwd, this.UserIdentity.UserId);
            return Success("重置密码成功。");
        }

    }
}
