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
    /// <summary>
    /// 
    /// </summary>
    public class StaffController : ControllerBase
    {
        private readonly ISysStaffRepo _sysStaffRepo;
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;
        private readonly ISysDepartmentRepo _sysDepartmentRepo;
        private readonly ISysUserExDmnService _sysUserDmnService;
        private readonly ICache _cache;


        /// <summary>
        /// liên kết người dùng
        /// </summary>
        /// <returns></returns>
        public ActionResult Selector()
        {
            return View();
        }

        /// <summary>
        /// lấy cơ quan nhân viên cây cấp 3 (cơ quan+khoa+nhân viên)
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetStaffSelecotrTree(string organizeId = null, string keyValue = null, string from = null, bool isShowEmpty = false, bool isExpand = true, int cacheTime = 30, string initIdSelected = null)
        {
            if (string.IsNullOrWhiteSpace(organizeId))
            {
                organizeId = base.GetAuthOrganizeId();  //mặc định quyền OrganizeId
            }
            //bộ nhớ đệm start
            var treeList = _cache.Get<List<TreeViewModel>>(string.Format(CacheKey.OrganizeStaffTreeSetKey, organizeId), () =>
            {
                var organizedata = _sysOrganizeDmnService.GetValidListByParentOrg(organizeId);
                var iitreeList = new List<TreeViewModel>();
                foreach (var orgItem in organizedata)
                {
                    //cơ quan khoa tập hợp
                    var orgDetpData = _sysDepartmentRepo.GetList(orgItem.Id);
                    //cơ quan nhân viên tập hợp
                    var orgStaffData = _sysUserDmnService.GetStaffListByOrg(orgItem.Id);
                    #region 机构下所有科室
                    foreach (var deptItem in orgDetpData)
                    {
                        var deptStaffData = orgStaffData.Where(p => p.DepartmentCode == deptItem.Code).ToList();
                        #region 机构下所有用户
                        foreach (var staffItem in deptStaffData)
                        {
                            TreeViewModel staffTree = new TreeViewModel();
                            staffTree.id = staffItem.Id;
                            staffTree.text = staffItem.Name;
                            staffTree.value = staffItem.gh;
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
                        deptTree.isexpand = true;  //mặc định không mở rộng, quá nhiều người
                        deptTree.complete = true;
                        deptTree.showcheck = false;
                        deptTree.hasChildren = deptStaffData.Count > 0 || orgDetpData.Any(p => p.ParentId == deptItem.Id);
                        iitreeList.Add(deptTree);
                    }
                    #endregion
                    TreeViewModel tree = new TreeViewModel();
                    tree.id = orgItem.Id;
                    tree.text = orgItem.Name;
                    tree.value = orgItem.Code;
                    tree.parentId = orgItem.Id == organizeId ? null : orgItem.ParentId; //đặc biệt
                    tree.isexpand = isExpand;
                    tree.complete = true;
                    tree.showcheck = false;
                    tree.hasChildren = orgDetpData.Count > 0 || orgStaffData.Count > 0 || organizedata.Any(p => p.ParentId == orgItem.Id);
                    iitreeList.Add(tree);
                }
                return iitreeList;
            }, cacheTime);
            //bộ nhớ đệm end
            IList<string> checkedStaffIdList = new List<string>();
            //if (from == "userrelatioinstaff" &&!string.IsNullOrWhiteSpace(keyValue))   //người dùng đã liên kết nhân viên
            //{
            //    checkedStaffIdList = _sysStaffRepo.GetCurStaffIdListByUserId(keyValue);
            //}

            var initIdSelectedArr = (initIdSelected ?? "").Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            checkedStaffIdList = checkedStaffIdList.Union(initIdSelectedArr).Distinct().ToList();

            if (checkedStaffIdList != null && checkedStaffIdList.Count > 0)
            {
                foreach (var treeItem in treeList)
                {
                    if (treeItem.showcheck && checkedStaffIdList.Contains(treeItem.id))
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

    }
}