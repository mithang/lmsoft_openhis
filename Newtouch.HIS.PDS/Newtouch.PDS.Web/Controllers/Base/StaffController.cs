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
        /// Người dùng liên kết
        /// </summary>
        /// <returns></returns>
        public IActionResult Selector()
        {
            return View();
        }

        /// <summary>
        /// Lấy cây nhân sự cơ sở Cấp 3（Cơ sở+Khoa+Nhân sự）
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult GetStaffSelecotrTree(string organizeId = null, string keyValue = null, string from = null, bool isShowEmpty = false, bool isExpand = true, int cacheTime = 30, string initIdSelected = null)
        {
            if (string.IsNullOrWhiteSpace(organizeId))
            {
                organizeId = base.GetAuthOrganizeId();  //Mặc định QuyềnOrganizeId
            }
            //Đệmstart
            var treeList = _cache.Get<List<TreeViewModel>>(string.Format(CacheKey.OrganizeStaffTreeSetKey, organizeId), () =>
            {
                var organizedata = _sysOrganizeDmnService.GetValidListByParentOrg(organizeId);
                var iitreeList = new List<TreeViewModel>();
                foreach (var orgItem in organizedata)
                {
                    //Tập hợp khoa của cơ sở
                    var orgDetpData = _sysDepartmentRepo.GetList(orgItem.Id);
                    //Tập hợp nhân sự của cơ sở
                    var orgStaffData = _sysUserDmnService.GetStaffListByOrg(orgItem.Id);
                    #region Tất cả khoa thuộc cơ sở
                    foreach (var deptItem in orgDetpData)
                    {
                        var deptStaffData = orgStaffData.Where(p => p.DepartmentCode == deptItem.Code).ToList();
                        #region Tất cả người dùng thuộc cơ sở
                        foreach (var staffItem in deptStaffData)
                        {
                            TreeViewModel staffTree = new TreeViewModel();
                            staffTree.id = staffItem.Id;
                            staffTree.text = staffItem.Name;
                            staffTree.value = staffItem.gh;
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
                        deptTree.isexpand = true;  //Mặc định không mở rộng，Quá nhiều người
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
                    tree.parentId = orgItem.Id == organizeId ? null : orgItem.ParentId; //Đặc biệt
                    tree.isexpand = isExpand;
                    tree.complete = true;
                    tree.showcheck = false;
                    tree.hasChildren = orgDetpData.Count > 0 || orgStaffData.Count > 0 || organizedata.Any(p => p.ParentId == orgItem.Id);
                    iitreeList.Add(tree);
                }
                return iitreeList;
            }, cacheTime);
            //Đệmend
            IList<string> checkedStaffIdList = new List<string>();
            //if (from == "userrelatioinstaff" && !string.IsNullOrWhiteSpace(keyValue))   //Nhân sự người dùng đã liên kết
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

    }
}