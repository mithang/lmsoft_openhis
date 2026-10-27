using Newtouch.MR.ManageSystem.Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Init;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.MR.ManageSystem.Infrastructure;
using Newtouch.MRMS.Web.Models;
using Newtouch.MR.ManageSystem.Domain.IDomainServices;
using Newtouch.MR.ManageSystem.Domain.IRepository;
using Newtouch.MR.ManageSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.MRMS.Web.Areas.RecordManage.Controllers
{
    [Area("RecordManage")]
    public class MrFeeItemController : OrgControllerBase
	{
        public MrFeeItemController(IMrFeeRepo MrFeeRepo, IMrFeeRelDmnService MrFeeRelDmnService, IMrFeeRelRepo MrFeeRelRepo)
        {
            this._MrFeeRepo = MrFeeRepo;
            this._MrFeeRelDmnService = MrFeeRelDmnService;
            this._MrFeeRelRepo = MrFeeRelRepo;
        }

		private readonly IMrFeeRepo _MrFeeRepo;
		private readonly IMrFeeRelDmnService _MrFeeRelDmnService;
		private readonly IMrFeeRelRepo _MrFeeRelRepo;

		// GET: RecordManage/MrFeeItem
		public override IActionResult Index()
        {
            return View();
        }

		public IActionResult getFeeTree() {
			//var treeList = new List<TreeViewModel>();
			//var idList =new List<string>();
			//idList.Add("101");
			//idList.Add("102");
			//idList.Add("103");

			//TreeViewModel tree1 = new TreeViewModel();
			//tree1.id = "1";
			//tree1.text = "1text";
			//tree1.value = "1";
			//tree1.parentId = null;
			//tree1.isexpand = true;
			//tree1.complete = true;
			//tree1.showcheck = true;
			//tree1.checkstate = 0;
			//tree1.hasChildren = true;
			//tree1.Ex1 = "p";
			//treeList.Add(tree1);

			//for (var i = 0; i < idList.Count; i++) {
			//	TreeViewModel tree = new TreeViewModel();
			//	tree.id = idList[i];
			//	tree.text = idList[i]+"text";
			//	tree.value = idList[i];
			//	tree.parentId = "1";
			//	tree.isexpand = true;
			//	tree.complete = true;
			//	tree.showcheck = true;
			//	tree.checkstate = 0;
			//	tree.hasChildren = true;
			//	tree.Ex1 = "p";
			//	treeList.Add(tree);
			//}

			//TreeViewModel tree3 = new TreeViewModel();
			//tree3.id = "10101";
			//tree3.text = "10101text";
			//tree3.value = "10101";
			//tree3.parentId = "101";
			//tree3.isexpand = true;
			//tree3.complete = true;
			//tree3.showcheck = true;
			//tree3.checkstate = 0;
			//tree3.hasChildren = false;
			//tree3.Ex1 = "p";
			//treeList.Add(tree3);

			//return Content(treeList.TreeViewJson(null));

			var treeList = new List<TreeViewModel>();
			var feeList= _MrFeeRepo.GetAllFeeList(OrganizeId);
			foreach(var list in feeList) {
				TreeViewModel tree = new TreeViewModel();
				tree.id = list.Code;
				tree.text = list.Name;
				tree.value = list.Id;
				tree.parentId =list.ParentCode;
				tree.isexpand = true;
				tree.complete = true;
				tree.showcheck = false;
				tree.checkstate = 0;
				tree.hasChildren = hasChild(list.Code,feeList);
				tree.Ex1 = "c";
				treeList.Add(tree);
			}
			return Content(treeList.TreeViewJson(null));
		}

		private bool hasChild(string code,IList<bafeeEntity> feeList) {
			var flag = false;
			foreach (var obj in feeList) {
				if (obj.ParentCode == code) {
					flag=true;
					break;
				}
			}
			return flag;
		}

		//Chọn cây để hiện danh sách mục
		public IActionResult GetPagintionListById(Pagination pagination, string id)
		{
			var pat = _MrFeeRelDmnService.GetPagintionListById(pagination, OrganizeId, id);

			var data = new
			{
				rows = pat,
				total = pagination.total,
				page = pagination.page,
				records = pagination.records
			};
			return Content(data.ToJson());
		}

		//Hiện các mục chưa phân loại
		public IActionResult GetPagintionItem(Pagination pagination, string code, string keyword,string hissfdl) {

			var pat = _MrFeeRelDmnService.GetPagintionItem(pagination, OrganizeId, keyword,hissfdl,code);

			var data = new
			{
				rows = pat,
				total = pagination.total,
				page = pagination.page,
				records = pagination.records
			};
			return Content(data.ToJson());
		}

		public int SubmitForm(bafeeRelEntity entity)
		{
			if (string.IsNullOrWhiteSpace(entity.OrganizeId))
			{
				entity.OrganizeId = OrganizeId;
			}
			entity.zt = "1";
			entity.CreateTime = Convert.ToDateTime(DateTime.Now);
			entity.CreatorCode = UserIdentity.UserName;
			_MrFeeRelRepo.SaveForm(entity);
			return 1;
		}

		public IActionResult Save(IList<itemEntity> list,string code,string hissfdl)
		{
			foreach (var obj in list)
			{
				bafeeRelEntity rel = new bafeeRelEntity();
				rel.sfxm = obj.sfxmCode;
				rel.sfxmmc = obj.sfxmmc;
				rel.feetypecode = code;
				SubmitForm(rel);
			}
			return Success("Thao tác thành công。");
		}

        public IActionResult SavebyHISsfdl(string code,string hissfdl)
        {
            _MrFeeRelDmnService.SaveRelbyHissfdl(code, hissfdl, this.UserIdentity.rygh,OrganizeId);
            return Success("Thao tác thành công。");
        }
        public IActionResult SavebySfxm(string code,string ids)
        {
            _MrFeeRelDmnService.SaveRelbyhissfxm(code, ids, this.UserIdentity.rygh,OrganizeId);
            return Success("Thao tác thành công。");
        }
		public IActionResult DeleteForm(string keyValue)
		{
			_MrFeeRelRepo.DeleteForm(keyValue);
			return Success("Thao tác thành công。");
		}

	}
}