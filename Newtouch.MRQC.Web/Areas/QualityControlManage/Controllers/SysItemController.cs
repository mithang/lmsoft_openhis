using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.MRQC.Domain;
using Newtouch.MRQC.Domain.ValueObjects;
using Newtouch.MRQC.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.MRQC.Web.Areas.QualityControlManage.Controllers
{
    [Area("QualityControlManage")]
    public class SysItemController : OrgControllerBase
    {
        private readonly IQcItemDataRepo _qcitemdataRepo;
        private readonly IQcBlzkDmnService _qcblzkdmnservice;

        public SysItemController(IQcItemDataRepo qcitemdataRepo, IQcBlzkDmnService qcblzkdmnservice)
        {
            _qcitemdataRepo = qcitemdataRepo;
            _qcblzkdmnservice = qcblzkdmnservice;
        }

        public override IActionResult Index()
        {
            return base.Index();
        }

        public override IActionResult Form()
        {
            return base.Form();
        }

        /// <summary>
        /// Hạng mục kiểm soát chất lượng: cây loại mẫu bệnh án (via center API)
        /// </summary>
        public IActionResult GetQcBllxMbTree(string keyword)
        {
            var reqObj = new
            {
                Data = "",
                AppId = AuthenManageHelper.appId,
                OrganizeId = OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            var outstr = AuthenManageHelper.HttpPost<BllxmbRecordsVo>(reqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/MedicalBl/MedicalbllxTreeRecord", this.UserIdentity);
            var treeList = new List<TreeGridModel>();
            foreach (var item in outstr.data.bllxmbRecord)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = outstr.data.bllxmbRecord.Count(t => t.parentId == item.Id) == 0 ? false : true;
                treeModel.id = item.Id.ToString();
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.parentId == null ? null : item.parentId.ToString();
                treeModel.expanded = hasChildren;
                treeModel.entityJson = item.ToJson();
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeGridJson(null));
        }

        /// <summary>
        /// Bảo trì hạng mục kiểm soát chất lượng
        /// </summary>
        public IActionResult GetZkxmList(string keyword)
        {
            var data = _qcitemdataRepo.IQueryable().Where(p => p.BlmbId == keyword && p.OrganizeId == this.OrganizeId && p.zt == "1").OrderBy(p => p.Px).ToList();

            return Content(data.ToJson());
        }

        public IActionResult GetItemMXData(int Id)
        {
            var data = _qcitemdataRepo.IQueryable(p => p.Id == Id && p.OrganizeId == this.OrganizeId && p.zt == "1").ToList();

            return Content(data.ToJson());
        }

        public IActionResult SubmitForm(ZkxmEntityVo ZkxmMXListVO)
        {
            _qcblzkdmnservice.SubmitForm(ZkxmMXListVO);
            return Success();
        }

        public IActionResult ZkDetailDel(int Id)
        {
            _qcitemdataRepo.DeleteForm(Id, this.OrganizeId);
            return Success();
        }
    }
}
