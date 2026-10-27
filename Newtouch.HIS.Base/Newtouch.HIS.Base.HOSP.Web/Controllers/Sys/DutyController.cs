using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Base.HOSP.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class DutyController : ControllerBase
    {
        private readonly ISysDutyRepo _sysDutyRepo;
        private readonly ISysStaffDutyRepo _sysStaffDutyRepo;

        public DutyController(ISysDutyRepo sysDutyRepo, ISysStaffDutyRepo sysStaffDutyRepo)
        {
            this._sysDutyRepo = sysDutyRepo;
            this._sysStaffDutyRepo = sysStaffDutyRepo;
        }

        public IActionResult GetDutyList(string staffId)
        {
            var dutyList = _sysDutyRepo.GetValidList();  //Lấy danh sách vị trí hiệu lực
            var treeList = new List<TreeViewModel>();
            var staffCurrentDutyEntityList = new List<SysStaffDutyEntity>();
            if (!string.IsNullOrWhiteSpace(staffId))
            {
                staffCurrentDutyEntityList = _sysStaffDutyRepo.GetListByStaffId(staffId).ToList();
            }
            foreach (var item in dutyList)
            {
                TreeViewModel tree = new TreeViewModel();
                tree.id = item.Id;
                tree.text = item.Name;
                tree.value = item.Code;
                tree.parentId = null;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = staffCurrentDutyEntityList.Count(t => t.DutyId == item.Id);
                tree.hasChildren = false;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson(null));
        }

        /// <summary>
        /// Lấy danh sách
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetGridJson(string keyword)
        {
            var data = _sysDutyRepo.GetList(keyword);
            return Content(data.ToJson());
        }

    }
}