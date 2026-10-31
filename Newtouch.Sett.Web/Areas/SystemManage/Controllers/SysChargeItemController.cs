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
    /// 
    /// </summary>
    [Obsolete("replaced by/SystemManage/BaseData/SelectSfxmYp")]
    [Area("SystemManage")]

    public class SysChargeItemController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly ISysChargeItemRepo _sysChargeItemRepo;
        private readonly ISysChargeItemDmnService _sysChargeItemDmnService;
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;
        private readonly ICache _cache;
        public SysChargeItemController(ISysChargeItemRepo sysChargeItemRepo, ISysChargeItemDmnService sysChargeItemDmnService, ISysOrganizeDmnService sysOrganizeDmnService, ICache cache)
        {
            _sysChargeItemRepo = sysChargeItemRepo;
            _sysChargeItemDmnService = sysChargeItemDmnService;
            _sysOrganizeDmnService = sysOrganizeDmnService;
            _cache = cache;
        }


        public IActionResult Selector()
        {
            return View();
        }

        /// <summary>
        /// Khoản thu phí Chọn Search
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetChargeItemSelectData(string keyword, string organizeId, bool? zlfzlbz = true, string mzzybz = null, string dlCode = null)
        {
            if (string.IsNullOrWhiteSpace(organizeId))
            {
                organizeId = this.OrganizeId;
            }
            var list = _sysChargeItemDmnService.SelectSearch(keyword, organizeId, zlfzlbz, mzzybz);
            return Json(list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="organizeId"></param>
        /// <param name="zlfzlbz"></param>
        /// <param name="mzzybz"></param>
        /// <returns></returns>
        public IActionResult GetChargeItemAndMedicineSelectData(string keyword, string organizeId, bool? zlfzlbz = true, string mzzybz = null)
        {
            if (string.IsNullOrWhiteSpace(organizeId))
            {
                organizeId = this.OrganizeId;
            }
            var list = _sysChargeItemDmnService.SelectItemAndMedicineSearch(keyword, organizeId, zlfzlbz, mzzybz);
            return Json(list);
        }

        /// <summary>
        /// đượclấymáycơ quanngườiviêncây Cấp ba(Cơ sở+Khoa+Nhân viên)
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult GetChargeItemSelecotrTree(string organizeId = null, bool isExpand = true, int cacheTime = 30, string initIdSelected = null)
        {
            if (string.IsNullOrWhiteSpace(organizeId))
            {
                organizeId = base.GetAuthOrganizeId();  //mặc định QuyềnOrganizeId
            }

            IList<string> checkedStaffIdList = new List<string>();
            var initIdSelectedArr = (initIdSelected ?? "").Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            checkedStaffIdList = checkedStaffIdList.Union(initIdSelectedArr).Distinct().ToList();
            //bộ nhớ đệmstart
            var treeList = _cache.Get(string.Format(CacheKey.OrganizeStaffTreeSetKey, organizeId), () =>
            {
                var organizedata = _sysOrganizeDmnService.GetValidListByParentOrg(organizeId);
                var iitreeList = new List<TreeViewModel>();
                foreach (var orgItem in organizedata)
                {
                    //nơicóthuphíhạngmục
                    var orgsfxmData = _sysChargeItemDmnService.GetSFDLList(organizeId);
                    foreach (var Item in orgsfxmData)
                    {
                        TreeViewModel deptTree = new TreeViewModel();
                        deptTree.id = Item.dlId.ToString();
                        deptTree.text = Item.dlmc;
                        deptTree.value = Item.dlCode;
                        deptTree.parentId = null;
                        deptTree.isexpand = isExpand;  //Mặc định không mở rộng,Quá nhiều người
                        deptTree.complete = true;
                        deptTree.showcheck = true;
                        deptTree.hasChildren = false;
                        if (checkedStaffIdList.Contains(Item.dlCode.ToString()))
                        {
                            deptTree.checkstate = 1;
                        }
                        iitreeList.Add(deptTree);
                    }
                }
                return iitreeList;
            }, cacheTime);
            return Content(treeList.TreeViewJson(null));
        }

    }
}
