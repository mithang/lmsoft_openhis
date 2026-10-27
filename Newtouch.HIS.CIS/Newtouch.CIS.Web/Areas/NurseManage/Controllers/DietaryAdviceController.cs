using NewtouchHIS.Framework.Web.Controllers;


using Newtouch.CIS.Web.Models;
using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Application.Interface;
using Newtouch.CIS.Repository;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.Infrastructure;
using Newtouch.Repository;
using Newtouch.CIS.Compat;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.CIS.Web.Areas.NurseManage.Controllers
{

    [Area("NurseManage")]

    public class DietaryAdviceController : OrgControllerBase
    {
        private readonly IInpatientDietBaseRepo _inpatientDietBaseRepo;
        private readonly IInpatientDietSfxmdyRepo _inpatientDietSfxmdyRepo;
        private readonly IDietaryAdviceDmnService _iDietaryAdviceDmnService;

        public DietaryAdviceController(IInpatientDietBaseRepo inpatientDietBaseRepo, IInpatientDietSfxmdyRepo inpatientDietSfxmdyRepo, IDietaryAdviceDmnService iDietaryAdviceDmnService)
        {
            this._inpatientDietBaseRepo = inpatientDietBaseRepo;
            this._inpatientDietSfxmdyRepo = inpatientDietSfxmdyRepo;
            this._iDietaryAdviceDmnService = iDietaryAdviceDmnService;
        }
        public IActionResult DietBase() {
            return View();
        }

        public IActionResult GetGridList(Pagination pagination, string lb, string keyword) {
            var data = new
            {
                rows = _inpatientDietBaseRepo.GetGridList(pagination, lb, keyword, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        public IActionResult GetFormJson(string keyvalue) {
            var data = _inpatientDietBaseRepo.FindEntity(p => p.Id == keyvalue && p.zt == "1");
            return Content(data.ToJson());
        }

        public IActionResult submitForm(DAFormVO vo, string keyvalue)
        {
            InpatientDietBaseEntity entity = new InpatientDietBaseEntity();
            entity = vo.MapperTo(entity);
            entity.OrganizeId = this.OrganizeId;
            entity.zt = "1";
            entity.bdsfxm = vo.bdsfxm.ToBool();
            if (!string.IsNullOrWhiteSpace(vo.ParentId))
            {
                entity.ParentId = vo.ParentId.Substring(0, vo.ParentId.Length - 1);
            }
            _inpatientDietBaseRepo.SubmitForm(entity, keyvalue);
            return Success();
        }

        public IActionResult GetSearchType() {
            var treeList = new List<TreeViewModel>();
            var dflList = _inpatientDietBaseRepo.IQueryable().Where(p => p.DietType == "00" && p.zt == "1");
            foreach (var item in dflList)
            {
                TreeViewModel tree = new TreeViewModel();
                tree.id = item.Id;
                tree.text = item.Name;
                tree.value = item.Id;
                tree.parentId = null;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = false;
                tree.hasChildren = false;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson(null));
        }

        public IActionResult GetSSXH(string from=null) {
            var data = _inpatientDietBaseRepo.IQueryable().Where(p => p.DietType == ((int)EnumSSLB.ssxh).ToString() && p.zt == "1");
            if (from=="kyz")
            {
                data= _inpatientDietBaseRepo.IQueryable().Where(p => (p.DietType == ((int)EnumSSLB.ssxh).ToString()||(p.DietType== ((int)EnumSSLB.lx).ToString()&&p.ParentId==null)) && p.zt == "1");
            }
            return Content(data.ToJson());
        }

        public IActionResult GetGridMXList(string Id)
        {
            var data = _iDietaryAdviceDmnService.GetmxList(Id,OrganizeId);
            return Content(data.ToJson());
        }

        public IActionResult DeleteForm(string keyvalue ) {
            _inpatientDietBaseRepo.DeleteForm(keyvalue);
            return Success();
        }

        public IActionResult SaveData(List<DAMXFormVO>  gridMxData, List<string> deldata) {
            _iDietaryAdviceDmnService.SubmitService(OrganizeId, gridMxData, deldata);
            return Success();
        }

        public IActionResult GetYSLB(string Id) {
            var data = _inpatientDietBaseRepo.IQueryable().Where(p => p.ParentId.Contains(Id) && p.zt == "1" && p.OrganizeId == OrganizeId);
            return Content(data.ToJson());
        }

        public IActionResult GetyszsList(string keyword)
        {
            var data=  _inpatientDietBaseRepo.IQueryable().Where(p=>p.zt=="1"&&p.OrganizeId==this.OrganizeId&&p.DietType== ((int)EnumSSLB.tsyq).ToString());
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                data = data.Where(p => p.DietBigType == keyword);
            }
            return Content(data.ToJson());
        }
    }
}