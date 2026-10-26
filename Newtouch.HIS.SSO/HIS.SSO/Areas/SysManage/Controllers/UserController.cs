using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Framework.Web.Controllers.SysManage;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Framework.Filter;

namespace HIS.SSO.Areas.SysManage.Controllers
{
    [Area("SysManage")]
    public class UserController :  OrgControllerBase
    {
        private readonly ISysRoleDmnService _sysRoleDmnService;
        private readonly ISysDepartmentDmnService _sysDepartmentDmnService;
        private readonly ISysUserDmnService _sysUserDmnService;
        private readonly IUserRoleAuthDmnService _userRoleAuthDmnService;
        private readonly ISysOrgDmnService _sysOrgDmnService;

        public UserController(ISysRoleDmnService sysRoleDmnService, ISysDepartmentDmnService sysDepartmentDmnService, ISysUserDmnService sysUserDmnService, IUserRoleAuthDmnService userRoleAuthDmnService, ISysOrgDmnService sysOrgDmnService)
        {
            _sysRoleDmnService = sysRoleDmnService;
            _sysDepartmentDmnService = sysDepartmentDmnService;
            _sysUserDmnService = sysUserDmnService;
            _userRoleAuthDmnService = userRoleAuthDmnService;
            _sysOrgDmnService = sysOrgDmnService;
        }
        /// <summary>
        /// trang danh sách
        /// </summary>
        /// <returns></returns>
        public IActionResult Selector()
        {
            ViewBag.OrganizeId = this.UserIdentity?.OrganizeId;
            ViewBag.TopOrganizeId = this.UserIdentity?.TopOrganizeId;
            return View();
        }
        public IActionResult UserRoles()
        {
            ViewBag.OrganizeId = this.UserIdentity?.OrganizeId;
            ViewBag.TopOrganizeId = this.UserIdentity?.TopOrganizeId;
            return View();
        }
        public async Task<ActionResult> GetPagintionGridJson(string organizeId, string keyValue = null)
        {
            var data = await _sysUserDmnService.GetSatffVOListByOrgorKeyvalue(OrganizeId, keyValue);
            return Content(data.ToJson());

        }
            /// <summary>
            /// lấy cây người dùng hệ thống cấp ba（tổ chức+khoa+người dùng（nhân viên））
            /// </summary>
            /// <returns></returns>
            public async Task<ActionResult> GetSysUserSelectorTree(string organizeId = null, string keyValue = null, string from = null, bool isShowEmpty = false, bool isExpand = true, bool isContansChildOrg = true)
        {
            if (string.IsNullOrWhiteSpace(organizeId))
            {
                organizeId = base.GetAuthOrganizeId();  //mặc định quyềnOrganizeId
            }
            //bộ nhớ đệmstart
            var organizedata = await _sysOrgDmnService.GetOrganizeTree(organizeId);
            if (!isContansChildOrg)
            {
                //chỉ tổ chức hiện tại
                organizedata = organizedata.Where(p => p.OrganizeId == organizeId).ToList();
            }
            var treeList = new List<TreeViewModel>();
            foreach (SysOrgVo orgItem in organizedata)
            {
                //tập hợp khoa của tổ chức
                var orgDetpData = await _sysDepartmentDmnService.GetListByOrg(orgItem.OrganizeId);
                //tập hợp nhân viên tổ chức（có tài khoản đăng nhập tương ứng）
                var orgUserStaffData = await _sysUserDmnService.GetSatffVOListByOrg(orgItem.OrganizeId, this.UserIdentity.TopOrganizeId);
                #region tất cả các khoa thuộc tổ chức
                foreach (SysDepartmentEntity deptItem in orgDetpData)
                {
                    var deptUserStaffData = orgUserStaffData.Where(p => p.DepartmentCode == deptItem.Code).ToList();
                    #region tất cả người dùng thuộc tổ chức
                    foreach (SysUserStaffVO userStaffVO in deptUserStaffData)
                    {
                        TreeViewModel staffTree = new TreeViewModel();
                        staffTree.id = userStaffVO.StaffId;
                        staffTree.text = userStaffVO.Name;
                        staffTree.value = userStaffVO.UserId;
                        staffTree.parentId = deptItem.Id;
                        staffTree.Code = userStaffVO.UserCode;
                        staffTree.hasChildren = false;
                        staffTree.showcheck = true;
                        staffTree.checkstate = 0;   //gán giá trị từ bên ngoài，để tiện lưu bộ nhớ đệm

                        staffTree.Ex3 = orgItem.OrganizeId; //closest OrganizeId

                        treeList.Add(staffTree);
                    }
                    #endregion
                    TreeViewModel deptTree = new TreeViewModel();
                    deptTree.id = deptItem.Id;
                    deptTree.text = deptItem.Name;
                    deptTree.value = deptItem.Code;
                    deptTree.parentId = deptItem.ParentId == null ? orgItem.OrganizeId : deptItem.ParentId;
                    deptTree.isexpand = isExpand;  //mặc định không mở rộng，quá nhiều người
                    deptTree.complete = true;
                    deptTree.showcheck = false;
                    //deptTree.hasChildren = deptUserStaffData.Count > 0 || orgDetpData.Any(p => p.ParentId == deptItem.Id);
                    deptTree.hasChildren = true;
                    treeList.Add(deptTree);
                }
                #endregion
                TreeViewModel tree = new TreeViewModel();
                tree.id = orgItem.OrganizeId;
                tree.text = orgItem.Name;
                tree.value = orgItem.Code;
                tree.parentId = orgItem.OrganizeId == organizeId ? null : orgItem.ParentId; //đặc biệt
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = false;
                tree.hasChildren = orgDetpData.Count > 0 || orgUserStaffData.Count > 0 || organizedata.Any(p => p.ParentId == orgItem.OrganizeId);
                treeList.Add(tree);
            }
            //bộ nhớ đệmend
            IList<FirstSecond> checkedUserList = null; //đãcheckedngười dùng của
            if (from == "rolerelatioinuser" && !string.IsNullOrWhiteSpace(keyValue))   //vai trò đã liên kết người dùng
            {
                checkedUserList = await _userRoleAuthDmnService.GetCurUserIdListByRoleId(keyValue);
            }

            if (checkedUserList != null && checkedUserList.Count > 0)
            {
                foreach (var treeItem in treeList)
                {
                    if (treeItem.showcheck && checkedUserList.Any(p => p.First == treeItem.value && p.Second == treeItem.Ex3))
                    {
                        treeItem.checkstate = 1;
                        //đồng thời để cấp trên mở rộng
                        var thisParentNode = treeList.Where(p => p.id == treeItem.parentId).FirstOrDefault();
                        if (thisParentNode != null)
                        {
                            thisParentNode.isexpand = true;  //cấp dưới có mục đã chọn，ở đây nhất định phải mở rộng
                        }
                    }
                }
            }
            if (!isShowEmpty)   //không hiển thị những cái thừa
            {
                var count = 1;  //số dòng bị xóa
                while (count > 0)
                {
                    count = treeList.RemoveAll(p => !p.showcheck && !p.hasChildren);
                    //điều chỉnh lại cây xóa không có cấp dưới（cấp dưới vừa bị dời đi） lại không hiển thịshowcheckcủa
                    treeList.RemoveAll(p => !p.showcheck && !treeList.Any(sub => sub.parentId == p.id));
                }
            }
            return Content(treeList.TreeViewJson(null));
        }
    }
}
