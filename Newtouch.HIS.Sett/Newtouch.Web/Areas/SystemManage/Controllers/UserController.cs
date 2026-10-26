using System.Web.Mvc;
using Newtouch.Common;
using System.Collections.Generic;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Core.Common;
using System;
using FrameworkBase.MultiOrg.Domain.Entity;
using System.Linq;
using Newtouch.HIS.Domain.ValueObjects;
using StackExchange.Redis.Extensions.Core.Extensions;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// Người dùng hệ thống 扩展
    /// </summary>
    [AutoResolveIgnore]
    public class UserController : FrameworkBase.MultiOrg.Web.Areas.SystemManage.Controllers.SysUserController
    {
        private readonly Domain.IDomainServices.IOutPatientChargeQueryDmnService _outPatientChargeQueryDmnService;
        private readonly ISysDepartmentRepo _sysDepartmentRepo;
        private readonly ISysUserDmnService _sysUserDmnService;
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="sysOrganizeDmnService"></param>
        /// <param name="sysDepartmentRepo"></param>
        /// <param name="sysUserDmnService"></param>
        /// <param name="userRoleAuthDmnService"></param>
        public UserController(ISysOrganizeDmnService sysOrganizeDmnService
            , ISysDepartmentRepo sysDepartmentRepo
            , ISysUserDmnService sysUserDmnService
            , IUserRoleAuthDmnService userRoleAuthDmnService
            , Domain.IDomainServices.IOutPatientChargeQueryDmnService outPatientChargeQueryDmnService)
            : base(sysOrganizeDmnService, sysDepartmentRepo, sysUserDmnService, userRoleAuthDmnService)
        {
            this._outPatientChargeQueryDmnService = outPatientChargeQueryDmnService;
            this._sysDepartmentRepo = sysDepartmentRepo;
            this._sysUserDmnService = sysUserDmnService;
        }

        /// <summary>
        /// 用户选择器
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public override ActionResult Selector()
        {
            return View();
        }

        /// <summary>
        /// 获取系统用户树 Cấp ba（Cơ quan+Khoa+Người dùng）
        /// 要求 用户已关联人员
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="keyValue"></param>
        /// <param name="from"></param>
        /// <param name="isExpand"></param>
        /// <param name="isContansChildOrg"></param>
        /// <param name="isShowEmpty">是否显示空白节点（Khoa）</param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public override ActionResult GetSysUserSelectorTree(string organizeId = null, string keyValue = null, string from = null, bool isExpand = true, bool isContansChildOrg = true
            , bool isShowEmpty = false)
        {
            Func<List<TreeViewModel>, List<TreeViewModel>> funcTreeItemFilter = null;
            if (from == "mzshoufeicaozuoyuan")  //门诊收费操作员
            {
                funcTreeItemFilter = (treeList) =>
                {
                    var list = _outPatientChargeQueryDmnService.GetSettOperByOrg(organizeId);   //具体的医院
                    treeList.RemoveAll(p => p.showcheck && !list.Contains(p.Code));
                    return treeList;
                };
            }
            return base.ComGetSysUserSelectorTree(organizeId, keyValue, from, isExpand, isContansChildOrg, isShowEmpty
                , funcTreeItemFilter: funcTreeItemFilter);
        }
        /// <summary>
        /// 获取系统用户树 Cấp hai（Khoa+Người dùng（Nhân viên））-Tra cứu có thể
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="from"></param>
        /// <param name="isShowEmpty">是否显示空白节点（Nút khoa）</param>
        /// <param name="isExpand"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public virtual ActionResult GetSysUserSelectorTreeQ(string keyword, string keyValue = null, string from = null, bool isShowEmpty = false, bool isExpand = true,string orgId = null)
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
        protected internal ActionResult f_GetSysUserSelectorTreeQ(string keyword = null, string keyValue = null, string from = null, bool isShowEmpty = false, bool isExpand = true, string orgId = null
            , Func<List<TreeViewModel>, List<TreeViewModel>> funcTreeItemFilter = null
            , Func<List<RoleUnionUser>> funcCheckedList = null)
        {
            var treeList = new List<TreeViewModel>();

            //科室集合
            var detpData = _sysDepartmentRepo.GetList(OrganizeId??orgId, "1");
            //人员集合（有对应登录账户）
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
                    staffTree.checkstate = 0;   //外面赋值，方便缓存

                    treeList.Add(staffTree);
                }
                TreeViewModel deptTree = new TreeViewModel();
                deptTree.id = deptItem.Id;
                deptTree.text = deptItem.Name;
                deptTree.value = deptItem.Code;
                deptTree.parentId = deptItem.ParentId;
                deptTree.isexpand = isExpand;  //Mặc định Không mở rộng，人太多
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

            #region 补充父节点 start
            // by haijiang.mo 修复搜索二级以下的子部门用户，因无法构建出树结构，Tìm kiếm mà thất bại
            var parentDeptList = new HashSet<string>();
            // 控制总计算深度，Dữ liệu vì ngăn lỗi，找不到父节点死循环
            var totalReFindParent = 10;
            do
            {
                totalReFindParent--;
                parentDeptList = new HashSet<string>();
                // 找出treeList Không có trongparentId của 
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

                parentDeptList.ForEach(id => {
                    var deptItem = detpData.Where(p => p.Id == id).First();
                    if (deptItem != null)
                    {
                        TreeViewModel deptTree = new TreeViewModel();
                        deptTree.id = deptItem.Id;
                        deptTree.text = deptItem.Name;
                        deptTree.value = deptItem.Code;
                        deptTree.parentId = deptItem.ParentId;
                        deptTree.isexpand = isExpand;  //Mặc định Không mở rộng，人太多
                        deptTree.complete = true;
                        deptTree.showcheck = false;
                        deptTree.hasChildren = true;
                        treeList.Add(deptTree);
                    }
                });

            } while (parentDeptList.Count > 0 && totalReFindParent > 0);
            #endregion 补充父节点 end

            //Bộ nhớ đệmend
            IList<RoleUnionUser> checkedUserList = null; //Đãcheckedngười dùng
            if (from == "rolerelatioinuser" && !string.IsNullOrWhiteSpace(keyValue))   //角色已关联用户
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
                        //同时让上级展开
                        var thisParentNode = treeList.Where(p => p.id == treeItem.parentId).FirstOrDefault();
                        if (thisParentNode != null)
                        {
                            thisParentNode.isexpand = true;  //đã chọn có cấp dưới，这里一定展开之
                        }
                    }
                }
            }
            return Content(treeList.TreeViewJson(null));
        }
    }
}
