using NewtouchHIS.Framework.Web.Controllers;

using Newtouch.EMR.Domain.IDomainServices;
using Newtouch.EMR.Domain.ValueObjects;

using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Attributes;

namespace Newtouch.EMR.Web.Areas.MedicalRecordManage.Controllers
{
    [Area("MedicalRecordManage")]
    public class OutpatientController : OrgControllerBase
    {
        private readonly IOutpatientDmnService _outpatientDmnService;

        public OutpatientController(IOutpatientDmnService outpatientDmnService)
        {
            this._outpatientDmnService = outpatientDmnService;
        }
        // GET: MedicalRecordManage/Outpatient
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult PatList(Pagination pagination,string mjzbz,string keyword)
        {
            var chargeQueryList = new
            {
                rows = _outpatientDmnService.GetTreatingOrTreatedList(pagination, OrganizeId,Convert.ToInt32( mjzbz), this.UserIdentity.rygh, null, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(chargeQueryList.ToJson());
        }

        #region
        public IActionResult MedRecordTreeEditJson(string keyValue, string bllx, string mzh)
        {
            var pat = _outpatientDmnService.GetPatMzbymzh(OrganizeId,mzh,this.UserIdentity.rygh);
            MedRecordTreeEditVO data = new MedRecordTreeEditVO();
            if (pat != null)
            {
                data.bllxId = keyValue;
                data.mzh = pat.mzh;
                data.xm = pat.xm;
                data.birth = pat.csny;
                data.brxzmc = pat.brxzmc;
                data.sex = pat.xb;
                data.bllx = bllx;
                data.mbqx = (int)Enummbqx.pub;

                data.brjs = pat.xm + " / " + pat.xb;
                data.brjs += " / " + "Tuổi" + " / " + pat.brxzmc + " / " + pat.mzh;

            }
            return Content(data.ToJson());
        }

        /// <summary>
        /// 获取病历树
        /// </summary>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public IActionResult GetMedRecordTree(string mzh)
        {
            var data = _outpatientDmnService.GetOutPatMedRecordTree(this.OrganizeId, mzh, this.UserIdentity.rygh);
            var treeList = new List<TreeGridModel>();
            foreach (var item in data)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = data.Count(t => t.parentId == item.Id) == 0 ? false : true;
                treeModel.id = item.Id.ToString();
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.parentId == null ? null : item.parentId.ToString();
                treeModel.expanded = hasChildren;
                treeModel.entityJson = item.ToJson();

                treeList.Add(treeModel);

            }
            return Content(treeList.TreeGridJson(null));
        }
        #endregion
    }
}